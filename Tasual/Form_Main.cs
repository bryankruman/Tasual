// ===========================================
//  Copyright (c) 2017 Bryan Kruman
//
//  See LICENSE.rtf file in the project root
//  for full license information.
// ===========================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace Tasual
{
	/// <summary>
	/// Main form for the Tasual application.
	/// </summary>
	public partial class Form_Main : Form
	{
		// ==============
		//  Declarations
		// ==============

		/// <summary>Primary array containing list of tasks.</summary>
		public List<Task> TaskArray = new List<Task>();

		/// <summary>Settings object containing current application settings.</summary>
		public Setting Settings = new Setting();

		/// <summary>Persistent OlvListViewHitTestInfo for the calendar popout window.</summary>
		private OlvListViewHitTestInfo CalendarPopout = null;

		/// <summary>Persistent OlvListViewHitTestInfo for the first registered click in the ListView.</summary>
		private OlvListViewHitTestInfo ListView_FirstClickInfo = null;

		/// <summary>Whether or not this double click should result in an edit.</summary>
		private bool ListView_DoubleClickEdit = false;

		/// <summary>Whether or not we have previously selected the same item.</summary>
		private bool ListView_PreviouslySelected = false;

		/// <summary>The group that was most recently selected.</summary>
		private string LastSelectedGroup;

		/// <summary>OLVColumn for the description column.</summary>
		private OLVColumn DescriptionColumn;

		/// <summary>OLVColumn for the icon column.</summary>
		private OLVColumn IconColumn;

		/// <summary>OLVColumn for the category column.</summary>
		private OLVColumn CategoryColumn;

		/// <summary>OLVColumn for the due column.</summary>
		private OLVColumn DueColumn;

		/// <summary>OLVColumn for the time column.</summary>
		private OLVColumn TimeColumn;


		// ================
		//  Initialization
		// ================

		/// <summary>
		/// Initialization for the Tasual main form.
		/// </summary>
		public Form_Main()
		{
			// Initialize interface objects
			InitializeComponent();

			// Load Settings
			Setting.Load(ref Settings);
			Relocate();
			Settings_Apply();

			// Load TaskArray
			ArrayHandler.Load(ref TaskArray, Settings);
			ListView.SetObjects(TaskArray);

			// Setup ObjectListView
			ListView_Setup();
			ListView_AddColumns();
			ListView.RebuildColumns();

			// Update StatusLabel to reflect item counts
			UpdateStatusLabel();

			// Other initialization
			Timer_ListViewClick.Interval = SystemInformation.DoubleClickTime;
		}


		// =============================
		//  Common/Supporting Functions
		// =============================

		/// <summary>
		/// Outside reference wrapper to save the array.
		/// </summary>
		/// <remarks>
		/// This was necessary as simply calling ArrayHandler.Save() from other classes would give a CS1690 warning for
		/// using a field of a marshal-by-reference class. Additionally, these methods must continue using references 
		/// otherwise other functions in the program cease to be able to retrieve data from TaskArray.
		/// 
		/// TODO: Is this really the right way to handle this? Should we keep the array handling code purely in the 
		/// main class? or is there another way to handle it...
		/// </remarks>
		public void Array_Save()
		{
			ArrayHandler.Save(ref TaskArray, Settings);
		}

		/// <summary>
		/// Shorthand function to clear the entire task array, delete the local database, and reset the ListView.
		/// </summary>
		public void Array_ClearAll()
		{
			foreach(Task Task in TaskArray)
			{
				Task.Removed = true;
			}
			ArrayHandler.Save(ref TaskArray, Settings);
			ArrayHandler.Load(ref TaskArray, Settings);
			UpdateGroupKeys();
			ListView.SetObjects(TaskArray);
			ListView.BuildList();
			UpdateStatusLabel();
		}

		/// <summary>
		/// Check whether we should prompt to confirm an action to take, if so then show a messagebox to get that information.
		/// </summary>
		/// <param name="Prompt">Should we skip prompting and just do the action anyway.</param>
		/// <param name="Action">Description of action to perform.</param>
		/// <returns>Whether or not to proceed with an action.</returns>
		private bool ConfirmAction(bool Prompt, string Action)
		{
			if (!Prompt) { return true; }

			DialogResult Result = MessageBox.Show(
				string.Format("Are you sure you want to {0}?", Action),
				"Tasual",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning);

			if (Result == DialogResult.Yes)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Apply application properties that are changeable in settings.
		/// </summary>
		public void Settings_Apply()
		{
			TopMost = Settings.AlwaysOnTop;
			StartupManager.SetStartupStatus(Settings.LaunchOnStartup);
		}

		/// <summary>
		/// Re-locate the Tasual main form based upon the properties saved in settings.
		/// </summary>
		private void Relocate()
		{
			if (Settings.SaveWindowPos)
			{
				StartPosition = FormStartPosition.Manual;
				Location = Settings.Location;
				Size = Settings.Size;
				WindowState = Settings.WindowState;
			}
		}

		/// <summary>
		/// Outside reference wrapper to save settings.
		/// </summary>
		public void Settings_Save()
		{
			Setting.Save(ref Settings);
		}

		/// <summary>
		/// Update the status label at the bottom of the main form.
		/// </summary>
		public void UpdateStatusLabel()
		{
			int Complete = ListView.CheckedItems.Count;
			int Total = ListView.GetItemCount();

			if (Complete == Total)
			{
				StatusLabel.Text = "All tasks complete";
			}
			else
			{
				StatusLabel.Text = string.Format("{0} of {1} tasks complete", Complete, Total);
			}
		}

		/// <summary>
		/// Generate new GroupKeys for all tasks in the array.
		/// </summary>
		public void UpdateGroupKeys()
		{
			foreach (Task Task in TaskArray)
			{
				Task.CategoryGroupKey = TimeInfo.GetGroupStringFromTask(Task, Settings);
				Task.DueGroupKey = TimeInfo.GetDueStringFromTask(Task);
			}
		}

		/// <summary>
		/// Generate new GroupKey for selected task.
		/// </summary>
		/// <param name="Task">Task for which to generate a new group key.</param>
		public void UpdateGroupKeys(Task Task)
		{
			Task.CategoryGroupKey = TimeInfo.GetGroupStringFromTask(Task, Settings);
			Task.DueGroupKey = TimeInfo.GetDueStringFromTask(Task);
		}

		/// <summary>
		/// Check to see if the selected task is in a collapsed group, and expand that group if so.
		/// </summary>
		/// <param name="Task">Task to check to see if it is in a collapsed group.</param>
		public void CheckCollapsedGroup(Task Task)
		{
			foreach (OLVGroup Group in ListView.OLVGroups)
			{
				if ((Group.Key.ToString() == Task.CategoryGroupKey) || (Group.Key.ToString() == Task.DueGroupKey))
				{
					Group.Collapsed = false;
					ArrayHandler.Info.Collapsed.Remove(Group.Key.ToString());
				}
			}
		}

		/// <summary>
		/// Process through task array and check the status of all items.
		/// </summary>
		/// <remarks>
		/// This function may be a bit complicated, however it is relatively straightforward
		/// when you break it down and take it step by step. All we're doing is checking 
		/// each task and seeing if there is any work that needs to be done.
		/// 
		/// When a singular task expires
		/// - TODO: Check to see if we should push a notification
		/// - Set expired flag and delete if dismissal is immediate
		/// - Update listview
		/// 
		/// When a simple or recurring task expires
		/// - Check to see if task has another iteration on the way
		///   - Compare current count to total iterations allowed
		///   - Check end date to make sure the next iteration wouldn't be after the end date
		///   - If both conditions pass, create a new iteration
		/// - TODO: Check to see if we should push a notification
		/// - Set expired flag and delete if dismissal is immediate
		/// - Update listview
		/// </remarks>
		private void CheckTaskStatus()
		{
			string PathToFile = Path.Combine(Settings.StorageFolder, "tasks.db");
			bool UpdateList = false;
			List<Task> AddedList = new List<Task>();

			if (ArrayHandler.StorageNewerThanProgram(PathToFile))
			{
				// File in storage is newer than our internal array
				// Lets compare the changed array and the internal array before saving just to be safe
				List<Task> ChangedArray = new List<Task>();

				ArrayHandler.Load(ref ChangedArray, Settings);
				ArrayHandler.Compare(ref TaskArray, ref ChangedArray);

				UpdateList = true;
			}

			foreach (Task Task in TaskArray)
			{
				if (Task.Removed) { continue; }

				if (Task.Checked)
				{
					if (Settings.RemoveCompleted != Setting.RemoveType.Never)
					{
						if (DateTime.Now > (Task.Time.CheckedTime + Setting.GetRemoveTimeSpan(Settings.RemoveCompleted)))
						{
							// Delete task
							Task.Removed = true;
							UpdateList = true;
						}
					}
				}
				else if (TimeInfo.Scheduled(Task.Time))
				{
					if (Task.Time.Expired)
					{
						if (Task.Time.Dismiss != TimeInfo.DismissType.Never)
						{
							if (DateTime.Now > (Task.Time.Next + TimeInfo.GetDismissTimeSpan(Task.Time.Dismiss)))
							{
								// Delete task
								Task.Removed = true;
								UpdateList = true;
							}
						}
					}
					else if (DateTime.Now > Task.Time.Next)
					{
						// Task is now expired, check to see if we should delete it
						Task.Time.Expired = true;
						if (Task.Time.Dismiss == TimeInfo.DismissType.Immediate) // immediate deletion
						{
							// Delete task
							Task.Removed = true;
							UpdateList = true;
						}

						// Check to see if we should do another iteration
						int Count = TimeInfo.FindIterationCount(Task.Time);

						// Count iterations first
						if (Task.Time.Iterations != 0)
						{
							if (Count >= Task.Time.Iterations) // TODO: Check if off by one error is here
							{
								// No more iterations of this task
								continue;
							}
						}

						// Next check all other conditions and set the new time
						DateTime Next = TimeInfo.FindNextIteration(Task.Time);
						if (Next != DateTime.MinValue)
						{
							TimeInfo NewTime = new TimeInfo(
								Task.Time.Summary,
								DateTime.MinValue,
								Task.Time.Created,
								DateTime.Now,
								Task.Time.Start,
								Next,
								Task.Time.End,
								Task.Time.Dismiss,
								false,
								Task.Time.Iterations,
								Count,
								Task.Time.Yearly,
								Task.Time.Monthly,
								Task.Time.Weekly,
								Task.Time.Daily,
								Task.Time.TimeOfDay,
								Task.Time.MonthFilter,
								Task.Time.WeekFilter,
								Task.Time.DayFilter,
								Task.Time.SpecificDay
							);

							Task NewTask = new Task(
								Task.ID,
								false,
								false,
								Task.Priority,
								Task.Group,
								Task.Description,
								Task.Notes,
								Task.Link,
								Task.Location,
								NewTime
							);

							AddedList.Add(NewTask);
							UpdateList = true;
						}
						// else // It didn't pass/we don't have any more iterations
					}
				}
			}

			foreach (Task AddTask in AddedList)
			{
				TaskArray.Add(AddTask);
			}

			if (UpdateList)
			{
				ArrayHandler.Save(ref TaskArray, Settings);
				UpdateGroupKeys();
				ListView.BuildList();
				UpdateStatusLabel();
			}
		}

		/// <summary>
		/// Create a new semi-setup empty task and add it to the ListView.
		/// </summary>
		private void QuickCreate(bool FromClipboard = false)
		{
			string GroupName = "Tasks";
			string Description = "";
			string Notes = "";
			string Link = "";

			// defaults to the first group it finds OR the last selected group
			if (!string.IsNullOrEmpty(LastSelectedGroup))
			{
				GroupName = LastSelectedGroup;
			}
			else
			{
				foreach (Task Search in TaskArray)
				{
					if (!string.IsNullOrEmpty(Search.Group))
					{
						GroupName = Search.Group;
						break;
					}
				}
			}

			TimeInfo NewTime = new TimeInfo();
			NewTime.Created = DateTime.Now;
			NewTime.Modified = DateTime.Now;

			if (FromClipboard && Clipboard.ContainsText())
			{
				string ClipboardText = Clipboard.GetText();

				if (ClipboardText.Length > 30)
				{
					Description = String.Format("Paste: {0}...", ClipboardText.Substring(0, 30));
					Notes = ClipboardText;
				}
				else
				{
					Description = ClipboardText;
				}

				Link = URLExtensions.FindURL(ClipboardText);
			}

			Task Task = new Task(
				Task.GenerateID(),
				false,
				false,
				0,
				GroupName,
				Description,
				Notes,
				Link,
				"",
				NewTime
			);

			TaskArray.Add(Task);
			//ArrayHandler.Save(ref TaskArray, Settings); // Actually, don't save until after we're sure we want to keep this
			UpdateGroupKeys(Task);
			CheckCollapsedGroup(Task);
			ListView.BuildList();
			UpdateStatusLabel();
			ListView.PossibleFinishCellEditing();
			ListView.EnsureModelVisible(Task);
			ListView.EditModel(Task);
		}


		// ====================
		//  ListView Functions
		// ====================

		/// <summary>
		/// Set formatting for the description row based upon whether the item is checked or not.
		/// </summary>
		/// <param name="Sender">Sender of the FormatRow event.</param>
		/// <param name="Args">FormatRow event arguments.</param>
		private void ListView_FormatRow(object Sender, FormatRowEventArgs Args)
		{
			if (Args.Item.Checked)
			{
				Args.Item.ForeColor = Color.FromArgb(255, 189, 208, 230);
			}
			else
			{
				Args.Item.ForeColor = Color.FromArgb(255, 36, 90, 150);
			}
		}

		private void ListView_GroupCollapsing(object Sender, GroupExpandingCollapsingEventArgs Args)
		{
			if (Args.IsExpanding)
			{
				ArrayHandler.Info.Collapsed.Remove(Args.Group.Key.ToString());
			}
			else
			{
				ArrayHandler.Info.Collapsed.Add(Args.Group.Key.ToString());
			}
			ArrayHandler.Save(ref TaskArray, Settings);
		}

		/// <summary>
		/// Handler for ModelCanDrop event when sorted by category.
		/// </summary>
		/// <param name="Sender">Sender of the ModelCanDrop event.</param>
		/// <param name="Args">Model drop event arguments.</param>
		private void ListView_ModelCanDrop_Category(object Sender, ModelDropEventArgs Args)
		{
			Task Target = (Task)Args.TargetModel;
			Args.Effect = DragDropEffects.None;

			if (Target == null) { return; }

			var SourceTasks = Args.SourceModels.Cast<Task>();
			if (SourceTasks.Any(Source => (TimeInfo.CompareGroupFromTasks(Source, Target, Settings))))
			{
				Args.InfoMessage = "Cannot drop to same category";
			}
			else
			{
				// looks good!
				Args.Effect = DragDropEffects.Move;
			}
		}

		/// <summary>
		/// Handler for ModelCanDrop event when sorted by due time.
		/// </summary>
		/// <param name="Sender">Sender of the ModelCanDrop event.</param>
		/// <param name="Args">Model drop event arguments.</param>
		private void ListView_ModelCanDrop_DueTime(object Sender, ModelDropEventArgs Args)
		{
			Task Target = (Task)Args.TargetModel;
			Args.Effect = DragDropEffects.None;

			if (Target == null) { return; }

			var SourceTasks = Args.SourceModels.Cast<Task>();
			if (SourceTasks.Any(Source => (TimeInfo.CompareDueStringFromTasks(Source, Target))))
			{
				Args.InfoMessage = "Cannot drag to the same time or status";
			}
			else
			{
				// looks good!
				Args.Effect = DragDropEffects.Move;
			}
		}

		/// <summary>
		/// Select the handler for ModelCanDrop event depending on whether we group by category or due time.
		/// </summary>
		/// <param name="Sender">Sender of the ModelCanDrop event.</param>
		/// <param name="Args">Model drop event arguments.</param>
		private void ListView_ModelCanDrop_ChooseHandler(object Sender, ModelDropEventArgs Args)
		{
			if (Settings.GroupTasks)
			{
				if (Settings.GroupStyle == Setting.GroupStyles.Category)
				{
					ListView_ModelCanDrop_Category(Sender, Args);
				}
				else if (Settings.GroupStyle == Setting.GroupStyles.DueTime)
				{
					ListView_ModelCanDrop_DueTime(Sender, Args);
				}
			}
		}

		/// <summary>
		/// Handler for ModelDropped event when sorted by category.
		/// </summary>
		/// <param name="Sender">Sender of the ModelDropped event.</param>
		/// <param name="Args">Model drop event arguments.</param>
		private void ListView_ModelDropped_Category(object Sender, ModelDropEventArgs Args)
		{
			Task Target = (Task)Args.TargetModel;
			if (Target == null) { return; }

			switch (TimeInfo.GetGroupTypeFromTask(Target, Settings))
			{
				// OVERDUE TARGET TASK
				case TimeInfo.GroupTypes.Overdue:
					{
						foreach (Task Task in Args.SourceModels)
						{
							switch (TimeInfo.GetGroupTypeFromTask(Task, Settings))
							{
								case TimeInfo.GroupTypes.Standard:
								case TimeInfo.GroupTypes.Today:
									{
										// Reschedule, DON'T change the group
										TimeInfo NewTime = new TimeInfo();
										NewTime.Created = Task.Time.Created;
										NewTime.Modified = DateTime.Now;
										DateTime NewDate = DateTime.Now.AddSeconds(-1);
										NewTime.Start = NewDate;
										NewTime.Next = NewDate;
										Task.Time = NewTime;
										break;
									}

								case TimeInfo.GroupTypes.Completed:
									{
										// Reschedule, uncheck, DON'T change the group
										Task.Checked = false;
										TimeInfo NewTime = new TimeInfo();
										NewTime.Created = Task.Time.Created;
										NewTime.Modified = DateTime.Now;
										DateTime NewDate = DateTime.Now.AddSeconds(-1);
										NewTime.Start = NewDate;
										NewTime.Next = NewDate;
										NewTime.CheckedTime = DateTime.MinValue;
										Task.Time = NewTime;
										break;
									}
							}
						}
						break;
					}

				// TODAY TARGET TASK
				case TimeInfo.GroupTypes.Today:
					{
						foreach (Task Task in Args.SourceModels)
						{
							switch (TimeInfo.GetGroupTypeFromTask(Task, Settings))
							{
								case TimeInfo.GroupTypes.Standard:
								case TimeInfo.GroupTypes.Overdue:
									{
										// Reschedule, DON'T change the group
										TimeInfo NewTime = new TimeInfo();
										NewTime.Created = Task.Time.Created;
										NewTime.Modified = DateTime.Now;
										DateTime NewDate = DateTime.Now;
										NewDate = NewDate - NewDate.TimeOfDay;
										NewDate = NewDate + TimeSpan.FromSeconds(86399);
										NewTime.Start = NewDate;
										NewTime.Next = NewDate;
										Task.Time = NewTime;
										break;
									}

								case TimeInfo.GroupTypes.Completed:
									{
										// Reschedule, uncheck, DON'T change the group
										Task.Checked = false;
										TimeInfo NewTime = new TimeInfo();
										NewTime.Created = Task.Time.Created;
										NewTime.Modified = DateTime.Now;
										DateTime NewDate = DateTime.Now;
										NewDate = NewDate - NewDate.TimeOfDay;
										NewDate = NewDate + TimeSpan.FromSeconds(86399);
										NewTime.Start = NewDate;
										NewTime.Next = NewDate;
										NewTime.CheckedTime = DateTime.MinValue;
										Task.Time = NewTime;
										break;
									}
							}
						}
						break;
					}

				// STANDARD TARGET TASK
				case TimeInfo.GroupTypes.Standard:
					{
						foreach (Task Task in Args.SourceModels)
						{
							switch (TimeInfo.GetGroupTypeFromTask(Task, Settings))
							{
								case TimeInfo.GroupTypes.Overdue:
								case TimeInfo.GroupTypes.Today:
									{
										// Unschedule, and change group
										Task.Group = Target.Group;
										TimeInfo NewTime = new TimeInfo();
										NewTime.Created = Task.Time.Created;
										NewTime.Modified = DateTime.Now;
										Task.Time = NewTime;
										break;
									}

								case TimeInfo.GroupTypes.Standard:
									{
										// Leave scheduling, change group
										Task.Group = Target.Group;
										break;
									}

								case TimeInfo.GroupTypes.Completed:
									{
										// Unschedule if expired, uncheck, and change group
										Task.Group = Target.Group;
										Task.Checked = false;
										Task.Time.CheckedTime = DateTime.MinValue;
										TimeInfo NewTime = new TimeInfo();
										NewTime.Created = Task.Time.Created;
										NewTime.Modified = DateTime.Now;
										Task.Time = NewTime;
										break;
									}
							}
						}
						break;
					}

				// COMPLETED TARGET TASK
				case TimeInfo.GroupTypes.Completed:
					{
						foreach (Task Task in Args.SourceModels)
						{
							switch (TimeInfo.GetGroupTypeFromTask(Task, Settings))
							{
								case TimeInfo.GroupTypes.Overdue:
								case TimeInfo.GroupTypes.Today:
								case TimeInfo.GroupTypes.Standard:
									{
										// Don't disable the time, just set it to be completed
										Task.Checked = true;
										Task.Time.CheckedTime = DateTime.Now;
										break;
									}

									// We don't need to do anything if source was already checked
							}
						}
						break;
					}
			}

			ArrayHandler.Save(ref TaskArray, Settings);
			UpdateGroupKeys(); // TODO: Only cycle through the objects that got updated
			ListView.BuildList();
		}

		/// <summary>
		/// Handler for ModelDropped event when sorted by due time.
		/// </summary>
		/// <param name="Sender">Sender of the ModelDropped event.</param>
		/// <param name="Args">Model drop event arguments.</param>
		private void ListView_ModelDropped_DueTime(object Sender, ModelDropEventArgs Args)
		{
			Task Target = (Task)Args.TargetModel;
			if (Target == null) { return; }

			var SourceTasks = Args.SourceModels.Cast<Task>();
			switch (TimeInfo.GetDueTypeFromTask(Target))
			{
				case TimeInfo.DueTypes.Overdue:
					{
						foreach (Task Task in Args.SourceModels)
						{
							Task.Checked = false;
							TimeInfo NewTime = new TimeInfo();
							NewTime.Created = Task.Time.Created;
							NewTime.Modified = DateTime.Now;
							DateTime NewDate = DateTime.Now.AddSeconds(-1);
							NewTime.Start = NewDate;
							NewTime.Next = NewDate;
							NewTime.CheckedTime = DateTime.MinValue;
							Task.Time = NewTime;
						}
						break;
					}
				case TimeInfo.DueTypes.Today:
					{
						foreach (Task Task in Args.SourceModels)
						{
							Task.Checked = false;
							TimeInfo NewTime = new TimeInfo();
							NewTime.Created = Task.Time.Created;
							NewTime.Modified = DateTime.Now;
							DateTime NewDate = DateTime.Now;
							NewDate = NewDate - NewDate.TimeOfDay;
							NewDate = NewDate + TimeSpan.FromSeconds(86399);
							NewTime.Start = NewDate;
							NewTime.Next = NewDate;
							NewTime.CheckedTime = DateTime.MinValue;
							Task.Time = NewTime;
						}
						break;
					}
				case TimeInfo.DueTypes.Tomorrow:
					{
						foreach (Task Task in Args.SourceModels)
						{
							Task.Checked = false;
							TimeInfo NewTime = new TimeInfo();
							NewTime.Created = Task.Time.Created;
							NewTime.Modified = DateTime.Now;
							DateTime NewDate = DateTime.Now.AddDays(1);
							NewDate = NewDate - NewDate.TimeOfDay;
							NewDate = NewDate + TimeSpan.FromSeconds(86399);
							NewTime.Start = NewDate;
							NewTime.Next = NewDate;
							NewTime.CheckedTime = DateTime.MinValue;
							Task.Time = NewTime;
						}
						break;
					}
				case TimeInfo.DueTypes.Weekday:
					{
						foreach (Task Task in Args.SourceModels)
						{
							Task.Checked = false;
							TimeInfo NewTime = new TimeInfo();
							NewTime.Created = Task.Time.Created;
							NewTime.Modified = DateTime.Now;
							DateTime NewDate = Target.Time.Next;
							NewDate = NewDate - NewDate.TimeOfDay;
							NewDate = NewDate + TimeSpan.FromSeconds(86399);
							NewTime.Start = NewDate;
							NewTime.Next = NewDate;
							NewTime.CheckedTime = DateTime.MinValue;
							Task.Time = NewTime;
						}
						break;
					}
				case TimeInfo.DueTypes.OneWeek:
					{
						foreach (Task Task in Args.SourceModels)
						{
							Task.Checked = false;
							TimeInfo NewTime = new TimeInfo();
							NewTime.Created = Task.Time.Created;
							NewTime.Modified = DateTime.Now;
							DateTime NewDate = DateTime.Now.AddDays(8);
							NewDate = NewDate - NewDate.TimeOfDay;
							NewDate = NewDate + TimeSpan.FromSeconds(86399);
							NewTime.Start = NewDate;
							NewTime.Next = NewDate;
							NewTime.CheckedTime = DateTime.MinValue;
							Task.Time = NewTime;
						}
						break;
					}
				case TimeInfo.DueTypes.TwoWeeks:
					{
						foreach (Task Task in Args.SourceModels)
						{
							Task.Checked = false;
							TimeInfo NewTime = new TimeInfo();
							NewTime.Created = Task.Time.Created;
							NewTime.Modified = DateTime.Now;
							DateTime NewDate = DateTime.Now.AddDays(15);
							NewDate = NewDate - NewDate.TimeOfDay;
							NewDate = NewDate + TimeSpan.FromSeconds(86399);
							NewTime.Start = NewDate;
							NewTime.Next = NewDate;
							NewTime.CheckedTime = DateTime.MinValue;
							Task.Time = NewTime;
						}
						break;
					}
				case TimeInfo.DueTypes.ThreeWeeks:
					{
						foreach (Task Task in Args.SourceModels)
						{
							Task.Checked = false;
							TimeInfo NewTime = new TimeInfo();
							NewTime.Created = Task.Time.Created;
							NewTime.Modified = DateTime.Now;
							DateTime NewDate = DateTime.Now.AddDays(22);
							NewDate = NewDate - NewDate.TimeOfDay;
							NewDate = NewDate + TimeSpan.FromSeconds(86399);
							NewTime.Start = NewDate;
							NewTime.Next = NewDate;
							NewTime.CheckedTime = DateTime.MinValue;
							Task.Time = NewTime;
						}
						break;
					}
				case TimeInfo.DueTypes.OneMonth:
					{
						foreach (Task Task in Args.SourceModels)
						{
							Task.Checked = false;
							TimeInfo NewTime = new TimeInfo();
							NewTime.Created = Task.Time.Created;
							NewTime.Modified = DateTime.Now;
							DateTime NewDate = DateTime.Now.AddMonths(1);
							NewDate = NewDate.AddDays(1 - NewDate.Day);
							NewDate = NewDate - NewDate.TimeOfDay;
							NewDate = NewDate + TimeSpan.FromSeconds(86399);
							NewTime.Start = NewDate;
							NewTime.Next = NewDate;
							NewTime.CheckedTime = DateTime.MinValue;
							Task.Time = NewTime;
						}
						break;
					}
				case TimeInfo.DueTypes.Future:
					{
						foreach (Task Task in Args.SourceModels)
						{
							Task.Checked = false;
							TimeInfo NewTime = new TimeInfo();
							NewTime.Created = Task.Time.Created;
							NewTime.Modified = DateTime.Now;
							NewTime.CheckedTime = DateTime.MinValue;
							Task.Time = NewTime;
						}
						break;
					}
				case TimeInfo.DueTypes.Completed:
					{
						foreach (Task Task in Args.SourceModels)
						{
							Task.Checked = true;
							Task.Time.CheckedTime = DateTime.Now;
						}
						break;
					}
			}

			ArrayHandler.Save(ref TaskArray, Settings);
			UpdateGroupKeys(); // TODO: Only cycle through the objects that got updated
			ListView.BuildList();
		}

		/// <summary>
		/// Select the handler for ModelDropped event depending on whether we group by category or due time.
		/// </summary>
		/// <param name="Sender">Sender of the ModelDropped event.</param>
		/// <param name="Args">Model drop event arguments.</param>
		private void ListView_ModelDropped_ChooseHandler(object Sender, ModelDropEventArgs Args)
		{
			if (Settings.GroupTasks)
			{
				if (Settings.GroupStyle == Setting.GroupStyles.Category)
				{
					ListView_ModelDropped_Category(Sender, Args);
				}
				else if (Settings.GroupStyle == Setting.GroupStyles.DueTime)
				{
					ListView_ModelDropped_DueTime(Sender, Args);
				}
			}
		}

		/// <summary>
		/// Initial setup of the main ListView control.
		/// </summary>
		private void ListView_Setup()
		{
			ListView.ShowItemCountOnGroups = false;
			ListView.UseCustomSelectionColors = true;
			ListView.SelectedBackColor = Color.FromArgb(255, 222, 232, 246);
			ListView.SelectedForeColor = Color.FromArgb(255, 36, 90, 150);
			ListView.UnfocusedSelectedBackColor = Color.FromArgb(255, 222, 232, 246);
			ListView.UnfocusedSelectedForeColor = Color.FromArgb(255, 36, 90, 150);
			ListView.FormatRow += new EventHandler<FormatRowEventArgs>(ListView_FormatRow);

			HeaderFormatStyle FormatStyle = new HeaderFormatStyle();
			FormatStyle.Normal.ForeColor = Color.FromArgb(255, 36, 90, 150);
			FormatStyle.Normal.BackColor = Color.FromArgb(255, 222, 232, 246);
			FormatStyle.Hot.ForeColor = Color.FromArgb(255, 36, 90, 150);
			FormatStyle.Hot.BackColor = Color.FromArgb(255, 240, 240, 255);
			FormatStyle.Pressed.ForeColor = Color.FromArgb(255, 36, 90, 150);
			FormatStyle.Pressed.BackColor = Color.FromArgb(255, 240, 240, 255);
			ListView.HeaderUsesThemes = false;
			ListView.HeaderFormatStyle = FormatStyle;

			TextOverlay Overlay = new TextOverlay();
			Overlay.Font = this.Font;
			Overlay.Text = "All tasks completed. Create some tasks!";
			Overlay.TextColor = Color.FromArgb(255, 36, 90, 150);
			Overlay.BackColor = Color.FromArgb(255, 222, 232, 246);
			Overlay.BorderColor = Color.FromArgb(255, 36, 90, 150);
			Overlay.BorderWidth = 3;
			Overlay.CornerRounding = 0;
			ListView.EmptyListMsgOverlay = Overlay;

			ListView.PersistentCheckBoxes = true;
			ListView.CheckBoxes = true;
			ListView.CheckedAspectName = "Checked";
			ListView.CellEditUseWholeCell = true;

			ListView.IsSimpleDragSource = true;
			ListView.IsSimpleDropSink = true;
			SimpleDropSink Sink = (SimpleDropSink)ListView.DropSink;
			Sink.CanDropOnItem = false;
			Sink.CanDropBetween = true;
			Sink.FeedbackColor = Color.FromArgb(255, 222, 232, 246);
			ListView.ModelCanDrop += new EventHandler<ModelDropEventArgs>(ListView_ModelCanDrop_ChooseHandler);
			ListView.ModelDropped += new EventHandler<ModelDropEventArgs>(ListView_ModelDropped_ChooseHandler);
			ListView.GroupExpandingCollapsing += new EventHandler<GroupExpandingCollapsingEventArgs>(ListView_GroupCollapsing);

			ListView.UseFiltering = true;
			ListView.ModelFilter = new ModelFilter(delegate (object x)
			{
				return !((Task)x).Removed;
			});

			ListView.BooleanCheckStateGetter = delegate (object RowObject)
			{
				return ((Task)RowObject).Checked;
			};

			ListView.BooleanCheckStatePutter = delegate (object RowObject, bool NewValue)
			{
				Task Task = (Task)RowObject;

				if (NewValue)
				{
					Task.Time.CheckedTime = DateTime.Now;
					Task.Checked = true;
				}
				else
				{
					Task.Time.CheckedTime = DateTime.MinValue;
					Task.Checked = false;
				}

				ArrayHandler.Save(ref TaskArray, Settings);
				UpdateGroupKeys(Task);
				ListView.BuildList();
				UpdateStatusLabel();
				return NewValue;
			};
		}

		/// <summary>
		/// Update columns properties that are changeable in settings.
		/// </summary>
		public void ListView_UpdateColumnSettings()
		{
			ListView.ShowGroups = Settings.GroupTasks;
			IconColumn.IsVisible = ((Settings.EnabledColumns & Setting.Columns.Notes) != 0);
			CategoryColumn.IsVisible = ((Settings.EnabledColumns & Setting.Columns.Category) != 0);
			DueColumn.IsVisible = ((Settings.EnabledColumns & Setting.Columns.Due) != 0);
			TimeColumn.IsVisible = ((Settings.EnabledColumns & Setting.Columns.Time) != 0);

			OLVColumn SelectedColumn = null;
			if (Settings.GroupStyle == Setting.GroupStyles.Category)
			{
				SelectedColumn = CategoryColumn;
			}
			else
			{
				SelectedColumn = DueColumn;
			}

			ListView.AlwaysGroupByColumn = SelectedColumn; //CategoryColumn DueColumn
			ListView.PrimarySortColumn = SelectedColumn;// CategoryColumn DueColumn
		}

		/// <summary>
		/// Populate columns into the ObjectListView.
		/// </summary>
		private void ListView_AddColumns()
		{
			UpdateGroupKeys();
			DescriptionColumn = new OLVColumn("Description", "Description");
			DescriptionColumn.MinimumWidth = 100;
			DescriptionColumn.FillsFreeSpace = true;
			DescriptionColumn.IsVisible = true;
			DescriptionColumn.IsEditable = true;
			DescriptionColumn.Sortable = false;
			DescriptionColumn.DisplayIndex = 1;
			DescriptionColumn.LastDisplayIndex = 1;
			DescriptionColumn.HeaderTextAlign = HorizontalAlignment.Center;
			ListView.AllColumns.Add(DescriptionColumn);
			ListView.Columns.AddRange(new ColumnHeader[] { DescriptionColumn });

			IconColumn = new OLVColumn("Icons", "Icons");
			IconColumn.Renderer = new ImageRenderer();
			IconColumn.AspectGetter = delegate (object Input)
			{
				Task Task = (Task)Input;
				int[] Images = new int[3] { -1, -1, -1 };
				int CheckedOffset = 0;
				if (Task.Checked) { CheckedOffset = 3; }

				if (!string.IsNullOrEmpty(Task.Location)) { Images[0] = 1 + CheckedOffset; }
				if (!string.IsNullOrEmpty(Task.Link)) { Images[1] = 2 + CheckedOffset; }
				if (!string.IsNullOrEmpty(Task.Notes)) { Images[2] = 3 + CheckedOffset; }

				if ((Images[0] + Images[1] + Images[2]) == -3)
				{
					Images[0] = 0;
				}

				return Images;
			};
			IconColumn.MinimumWidth = 54;
			IconColumn.MaximumWidth = 54;
			IconColumn.IsVisible = true;
			IconColumn.IsEditable = false;
			IconColumn.Sortable = false;
			IconColumn.DisplayIndex = 2;
			IconColumn.LastDisplayIndex = 2;
			IconColumn.TextAlign = HorizontalAlignment.Right;
			IconColumn.ShowTextInHeader = false;
			ListView.AllColumns.Add(IconColumn);
			ListView.Columns.AddRange(new ColumnHeader[] { IconColumn });

			CategoryColumn = new OLVColumn("Category", "CategoryGroupKey");
			CategoryColumn.MinimumWidth = 100;
			CategoryColumn.IsVisible = false;
			CategoryColumn.IsEditable = true;
			CategoryColumn.Sortable = true;
			CategoryColumn.DisplayIndex = 3;
			CategoryColumn.LastDisplayIndex = 3;
			CategoryColumn.GroupKeyToTitleConverter = delegate (object Input)
			{
				return (Input as string).Remove(0, Math.Min(1, (Input as string).Length));
			};
			CategoryColumn.TextAlign = Settings.SubItemTextAlign;
			CategoryColumn.HeaderTextAlign = Settings.SubItemHeaderAlign;
			ListView.AllColumns.Add(CategoryColumn);
			ListView.Columns.AddRange(new ColumnHeader[] { CategoryColumn });

			DueColumn = new OLVColumn("Due", "DueGroupKey");
			DueColumn.MinimumWidth = 80;
			DueColumn.IsVisible = false;
			DueColumn.IsEditable = false;
			DueColumn.Sortable = true;
			DueColumn.DisplayIndex = 4;
			DueColumn.LastDisplayIndex = 4;
			DueColumn.TextAlign = Settings.SubItemTextAlign;
			DueColumn.HeaderTextAlign = Settings.SubItemHeaderAlign;
			DueColumn.AspectToStringConverter = delegate (object Input)
			{
				return (Input as string).Remove(0, Math.Min(2, (Input as string).Length));
			};
			DueColumn.GroupKeyToTitleConverter = delegate (object Input)
			{
				return (Input as string).Remove(0, Math.Min(2, (Input as string).Length));
			};
			ListView.AllColumns.Add(DueColumn);
			ListView.Columns.AddRange(new ColumnHeader[] { DueColumn });

			TimeColumn = new OLVColumn("Time", "Time");
			TimeColumn.MinimumWidth = 130;
			TimeColumn.IsVisible = true;
			TimeColumn.IsEditable = false;
			TimeColumn.Sortable = false; // TODO: Allow sorting by this
			TimeColumn.DisplayIndex = 5;
			TimeColumn.LastDisplayIndex = 5;
			TimeColumn.TextAlign = Settings.SubItemTextAlign;
			TimeColumn.HeaderTextAlign = Settings.SubItemHeaderAlign;
			TimeColumn.AspectToStringConverter = delegate (object Input)
			{
				TimeInfo Time = (TimeInfo)Input;
				if (TimeInfo.Scheduled(Time))
				{
					return TimeInfo.FormatTime(Time.Next, TimeInfo.TimeFormat.Short);
				}
				else
				{
					return "-";
				}
			};
			ListView.AllColumns.Add(TimeColumn);
			ListView.Columns.AddRange(new ColumnHeader[] { TimeColumn });

			ListView_UpdateColumnSettings();
			ListView.SortGroupItemsByPrimaryColumn = true;
			ListView.PrimarySortOrder = SortOrder.Ascending;
			ListView.SecondarySortColumn = DescriptionColumn;
		}

		/// <summary>
		/// Event handler for when ObjectListView is creating groups.
		/// </summary>
		/// <param name="Sender">Sender of the AboutToCreateGroups event.</param>
		/// <param name="Args">Create groups event arguments.</param>
		private void ListView_AboutToCreateGroups(object Sender, CreateGroupsEventArgs Args)
		{
			foreach (OLVGroup Group in Args.Groups)
			{
				Group.Name = Group.Header;

				if (ArrayHandler.Info.Collapsed.Contains(Group.Key))
				{
					Group.Collapsed = true;
				}

				if (Settings.ShowItemCounts)
				{
					Group.Header = String.Format(
						"{0} ({1} {2})",
						Group.Name,
						Group.Items.Count(),
						((Group.Items.Count() > 1) ? "items" : "item")
					);
				}
			}
		}

		/// <summary>
		/// Event handler for when we finish editing a cell in the ListView control.
		/// </summary>
		/// <param name="Sender">Sender of the CellEditFinished event.</param>
		/// <param name="Args">Cell edit event arguments.</param>
		private void ListView_CellEditFinished(object Sender, CellEditEventArgs Args)
		{
			Task Task = Args.RowObject as Task;
			if (Task == null) { return; }

			if (string.IsNullOrEmpty(Task.Description))
			{
				// This one will ACTUALLY remove the task, not just hide it from the list
				TaskArray.Remove(Task);
			}

			ArrayHandler.Save(ref TaskArray, Settings);
			//ListView.UpdateObject(e.RowObject);
			// TODO: We should be able to do this for just one object, not the whole list...
			ListView.BuildList();
			MenuStrip_Edit.Enabled = false;
		}

		/// <summary>
		/// Event handler for when you select an item.
		/// </summary>
		/// <param name="Sender">Sender of the SelectedIndexChanged event.</param>
		/// <param name="Args">Selection change event arguments.</param>
		private void ListView_SelectedIndexChanged(object Sender, EventArgs Args)
		{
			if (ListView.SelectedItem != null)
			{
				MenuStrip_Edit.Enabled = true;
				LastSelectedGroup = ((Task)ListView.SelectedItem.RowObject).Group;
			}
			else
			{
				MenuStrip_Edit.Enabled = false;
			}
		}

		/// <summary>
		/// Event sub-handler for when you single click a ListView item.
		/// </summary>
		/// <param name="Args">Mouse event arguments.</param>
		private void ListView_SingleClick(MouseEventArgs Args)
		{
			if (ListView_PreviouslySelected == true)
			{
				if (ListView.SelectedItem != null)
				{
					ListView.EditModel(ListView.SelectedItem.RowObject);
				}
			}
			else
			{
				// do nothing
			}
		}

		/// <summary>
		/// Event sub-handler for when you double click a ListView item.
		/// </summary>
		/// <param name="Args">Mouse event arguments.</param>
		private void ListView_DoubleClick(MouseEventArgs Args)
		{
			OlvListViewHitTestInfo SecondClickInfo = ListView.OlvHitTest(Args.X, Args.Y);

			if ((ListView_FirstClickInfo.Item != null) && (SecondClickInfo.Item != null))
			{
				if (ListView_FirstClickInfo.Item == SecondClickInfo.Item)
				{
					if (ListView.SelectedItem != null)
					{
						Task Task = (Task)ListView.SelectedItem.RowObject;
						ListView.PossibleFinishCellEditing();
						ListView.EditModel(Task);
					}
				}
			}
		}

		/// <summary>
		/// Timer to activate SingleClick sub-handler when the timer expires.
		/// </summary>
		/// <param name="Sender">Sender of the Tick event.</param>
		/// <param name="Args">Tick event arguments.</param>
		private void Timer_ListViewClick_Tick(object Sender, EventArgs Args)
		{
			if ((MouseButtons & MouseButtons.Left) == 0)
			{
				ListView_SingleClick((MouseEventArgs)Timer_ListViewClick.Tag);
			}
			ListView_FirstClickInfo = null;
			ListView_PreviouslySelected = false;
			Timer_ListViewClick.Stop();
		}

		/// <summary>
		/// MouseDown event handler for the ListView control.
		/// </summary>
		/// <param name="Sender">Sender of the MouseDown event.</param>
		/// <param name="Args">Mouse event arguments.</param>
		private void ListView_MouseDown(object Sender, MouseEventArgs Args)
		{
			OlvListViewHitTestInfo Info = ListView.OlvHitTest(Args.X, Args.Y);

			if (Info.Group != null)
			{
				//Console.WriteLine("Item: {0}", (Info.Column != null) ? Info.Column.Name : "foobar");
				// TODO: Fix the issue where clicking blank space inside of the group still triggers the group
				if (Args.Button == MouseButtons.Right)
				{
					MenuStrip_Group.Tag = Info.Group;
					MenuStrip_Group.Show(Cursor.Position);
				}
			}
			else if (Info.Item != null)
			{
				if (Timer_ListViewClick.Enabled && (Args.Button == MouseButtons.Left)) // second click
				{
					if (ListView_FirstClickInfo.Item != null)
					{
						if (ListView_FirstClickInfo.Item == Info.Item)
						{
							ListView_DoubleClickEdit = true;
						}
					}

					ListView_PreviouslySelected = false;
					ListView_FirstClickInfo = null;
					Timer_ListViewClick.Stop();
				}
				else // first click
				{
					switch (Info.Column.AspectName)
					{
						case "Description":
							{
								if (Args.Button == MouseButtons.Left)
								{
									Timer_ListViewClick.Start();

									if (ListView.SelectedItem == Info.Item)
									{
										ListView_PreviouslySelected = true;
									}

									ListView_FirstClickInfo = Info;
									Timer_ListViewClick.Tag = Args;
								}
								else
								{
									MenuStrip_Item.Tag = Info.Item.RowObject;
									MenuStrip_Item.Show(Cursor.Position.X, Cursor.Position.Y);
								}
								break;
							}
						case "Icons":
							{
								MenuStrip_Icon.Tag = Info.Item.RowObject;
								MenuStrip_Icon.Show(Cursor.Position.X, Cursor.Position.Y);
								break;
							}
						case "Group":
							{
								Console.WriteLine("Clicked on a category!");
								break;
							}
						case "Time":
							{
								if (Info.SubItem != null)
								{
									if (CalendarPopout == null)
									{
										CalendarPopout = Info;
									}
									else { CalendarPopout = null; }
								}
								break;
							}
					}
				}
			}
			/* else if (Info != null)
			{
				// TODO: Should we handle clicking blank space in any particular way?
			}*/
		}

		/// <summary>
		/// MouseUp event handler for the ListView control.
		/// </summary>
		/// <param name="Sender">Sender of the MouseUp event.</param>
		/// <param name="Args">Mouse event arguments.</param>
		private void ListView_MouseUp(object Sender, MouseEventArgs Args)
		{
			if (CalendarPopout != null)
			{
				Form_TimePop Calendar = new Form_TimePop(
					this,
					TaskArray.IndexOf((Task)ListView.SelectedItem.RowObject)
				);

				Rectangle Bounds = CalendarPopout.SubItem.Bounds;
				Calendar.Location = PointToScreen(new Point(Bounds.Left, Bounds.Bottom + Bounds.Height + 5));
				Calendar.Show(this);

				CalendarPopout = null;
			}

			else if (ListView_DoubleClickEdit)
			{
				if (ListView.SelectedItem != null)
				{
					Task Task = (Task)ListView.SelectedItem.RowObject;
					ListView.PossibleFinishCellEditing();
					ListView.EditModel(Task);
				}
				ListView_DoubleClickEdit = false;
			}
		}

		/// <summary>
		/// KeyDown event handler for the ListView control.
		/// </summary>
		/// <param name="Sender">Sender of the KeyDown event.</param>
		/// <param name="Args">Key event arguments.</param>
		private void ListView_KeyDown(object Sender, KeyEventArgs Args)
		{
			switch (Args.KeyCode)
			{
				case Keys.V:
					{
						if (Args.Modifiers == Keys.Control)
						{
							QuickCreate(true);
						}
						break;
					}

				case Keys.Delete:
					{
						if (ListView.SelectedItem != null)
						{
							if (ConfirmAction(Settings.PromptDelete, "delete this task"))
							{
								Task Task = (Task)ListView.SelectedItem.RowObject;
								Task.Removed = true;
								ArrayHandler.Save(ref TaskArray, Settings);
								ListView.BuildList();
								UpdateStatusLabel();
							}
						}
						break;
					}

				case Keys.Enter:
					{
						QuickCreate();
						break;
					}
			}
		}


		// ===============
		//  Misc Handlers
		// ===============

		/// <summary>
		/// Main window FormClosing event handler.
		/// </summary>
		/// <param name="Sender">Sender of the FormClosing event.</param>
		/// <param name="Args">Form closing event arguments.</param>
		private void Main_FormClosing(object Sender, FormClosingEventArgs Args)
		{
			if (Settings.SaveWindowPos)
			{
				if (WindowState != FormWindowState.Normal)
				{
					if (WindowState == FormWindowState.Minimized)
					{
						// Always load back up with a normal window state when previously minimized
						Settings.WindowState = FormWindowState.Normal;
					}
					else
					{
						Settings.WindowState = FormWindowState.Maximized;
					}
					Settings.Location = RestoreBounds.Location;
					Settings.Size = RestoreBounds.Size;
				}
				else
				{
					Settings.WindowState = FormWindowState.Normal;
					Settings.Location = Location;
					Settings.Size = Size;
				}

				Setting.Save(ref Settings);
			}

			Notify.Dispose();
		}

		/// <summary>
		/// Event handler for the main form Resize event.
		/// </summary>
		/// <param name="Sender">Sender of the Resize event.</param>
		/// <param name="Args">Resize event arguments.</param>
		private void Main_Resize(object Sender, EventArgs Args)
		{
			if (Settings.MinimizeToTray)
			{
				if (WindowState == FormWindowState.Minimized)
				{
					ShowInTaskbar = false;
				}
				else
				{
					ShowInTaskbar = true;
				}
			}
			else
			{
				ShowInTaskbar = true;
			}
		}

		/// <summary>
		/// CheckUpdate timer tick event handler. Triggers a call to CheckTaskStatus().
		/// </summary>
		/// <param name="Sender">Sender of the Tick event.</param>
		/// <param name="Args">Tick event arguments.</param>
		private void Timer_CheckUpdate_Tick(object Sender, EventArgs Args)
		{
			CheckTaskStatus();
		}

		/// <summary>
		/// Event handler for the MouseClick event on the notification icon.
		/// </summary>
		/// <param name="Sender">Sender of the MouseClick event.</param>
		/// <param name="Args">Mouse event arguments.</param>
		private void Notify_MouseClick(object Sender, MouseEventArgs Args)
		{
			if (Args.Button == MouseButtons.Left)
			{
				if (WindowState == FormWindowState.Minimized)
				{
					WindowState = FormWindowState.Normal;
				}
				Activate();
			}
			else if (Args.Button == MouseButtons.Right)
			{
				MenuStrip_Notify.Show(Cursor.Position);
			}
		}

		/// <summary>
		/// LinkClicked event handler for the status label link.
		/// </summary>
		/// <param name="Sender">Sender of the LinkClicked event.</param>
		/// <param name="Args">LinkLabelLinkClicked event arguments.</param>
		private void StatusLabel_LinkClicked(object Sender, LinkLabelLinkClickedEventArgs Args)
		{
			MenuStrip_Status.Show(StatusLabel, new Point(0, StatusLabel.Height));
		}

		/// <summary>
		/// LinkClicked event handler for the about label link.
		/// </summary>
		/// <param name="Sender">Sender of the LinkClicked event.</param>
		/// <param name="Args">LinkLabelLinkClicked event arguments.</param>
		private void AboutLabel_LinkClicked(object Sender, LinkLabelLinkClickedEventArgs Args)
		{
			Form_About AboutForm = new Form_About();
			AboutForm.ShowDialog(this);
		}


		// ====================
		//  MenuStrip Handlers
		// ====================

		// Main: "Create"
		/// <summary>
		/// MenuStrip event handler - Create button: Advanced: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Create_Advanced_Click(object Sender, EventArgs Args)
		{
			Form_Create CreateForm = new Form_Create(this);
			CreateForm.ShowDialog(this);
		}

		/// <summary>
		/// MenuStrip event handler - Create button: Quick: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Create_Quick_Click(object Sender, EventArgs Args)
		{
			QuickCreate();
		}

		// Main: "Edit"
		/// <summary>
		/// MenuStrip event handler - Edit button: Advanced: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Edit_Advanced_Click(object Sender, EventArgs Args)
		{
			if (ListView.SelectedItem != null)
			{
				Task Task = (Task)ListView.SelectedItem.RowObject;
				Form_Create CreateForm = new Form_Create(this, TaskArray.IndexOf(Task));
				CreateForm.ShowDialog(this);
			}
		}

		/// <summary>
		/// MenuStrip event handler - Edit button: Quick: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Edit_Quick_Click(object Sender, EventArgs Args)
		{
			if (ListView.SelectedItem != null)
			{
				Task Task = (Task)ListView.SelectedItem.RowObject;
				ListView.PossibleFinishCellEditing();
				ListView.EnsureModelVisible(Task);
				ListView.EditModel(Task);
			}
		}

		// Main: "Settings"
		/// <summary>
		/// MenuStrip event handler - Settings button: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Settings_Click(object Sender, EventArgs Args)
		{
			Form_Settings SettingsForm = new Form_Settings(this);
			SettingsForm.ShowDialog(this);
		}

		// Main: "Sources"
		/// <summary>
		/// MenuStrip event handler - Sources button: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Sources_Click(object Sender, EventArgs Args)
		{
			// Currently unused
		}

		// ListView: "Group"
		/// <summary>
		/// ContextMenuStrip event handler - Group header: Create: Advanced: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Group_Create_Advanced_Click(object Sender, EventArgs Args)
		{
			Form_Create CreateForm = new Form_Create(this);
			CreateForm.ShowDialog(this);
		}

		/// <summary>
		/// ContextMenuStrip event handler - Group header: Create: Quick: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Group_Create_Quick_Click(object Sender, EventArgs Args)
		{
			OLVGroup Group = (OLVGroup)MenuStrip_Group.Tag;
			if (TaskArray.Any(Task => (Task.Group == Group.Name)))
			{
				LastSelectedGroup = Group.Name;
			}
			QuickCreate();
		}

		/// <summary>
		/// ContextMenuStrip event handler - Group header: Delete: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Group_Delete_Click(object Sender, EventArgs Args)
		{
			if (!ConfirmAction(Settings.PromptDelete, "delete this group")) { return; }

			OLVGroup Group = (OLVGroup)MenuStrip_Group.Tag;

			foreach (Task Task in TaskArray)
			{
				if (TimeInfo.GetGroupStringFromTask(Task, Settings).Remove(0, 1) == Group.Name)
				{
					//RemovalList.Add(Task);
					Task.Removed = true;
				}
			}

			ArrayHandler.Save(ref TaskArray, Settings);
			ListView.BuildList();
			UpdateStatusLabel();
		}

		/// <summary>
		/// ContextMenuStrip event handler - Group header: Hide: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Group_Hide_Click(object Sender, EventArgs Args)
		{
			OLVGroup Group = (OLVGroup)MenuStrip_Group.Tag;
			Group.Collapsed = true;
		}

		/// <summary>
		/// ContextMenuStrip event handler - Group header: Move Tasks: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Group_MoveTasks_ClickHandler(object Sender, EventArgs Args)
		{
			ToolStripDropDownItem Item = (ToolStripDropDownItem)Sender;
			OLVGroup Group = (OLVGroup)MenuStrip_Group.Tag;
			ArrayHandler.ReAssignGroup(ref TaskArray, Group.Name, Item.Text);
			UpdateGroupKeys();
			ListView.BuildList();
		}

		/// <summary>
		/// ContextMenuStrip event handler - Group header: Move Tasks: DropDownOpening
		/// </summary>
		/// <param name="Sender">Sender of the DropDownOpening event.</param>
		/// <param name="Args">DropDownOpening event arguments.</param>
		private void MenuStrip_Group_MoveTasks_DropDownOpening(object Sender, EventArgs Args)
		{
			OLVGroup Group = (OLVGroup)MenuStrip_Group.Tag;
			List<string> AlreadySelectedGroups = new List<string>();

			MenuStrip_Group_MoveTasks.DropDownItems.Clear();
			MenuStrip_Group_MoveTasks.DropDownItems.Add("(No other groups available)");
			MenuStrip_Group_MoveTasks.DropDownItems[0].Enabled = false;

			foreach (Task Task in TaskArray)
			{
				if (!AlreadySelectedGroups.Contains(Task.Group) && (Group.Name != Task.Group))
				{
					MenuStrip_Group_MoveTasks.DropDownItems.Add(
						Task.Group,
						null,
						MenuStrip_Group_MoveTasks_ClickHandler
					);
					AlreadySelectedGroups.Add(Task.Group);
					MenuStrip_Group_MoveTasks.DropDownItems[0].Visible = false;
				}
			}
		}

		/// <summary>
		/// ContextMenuStrip event handler - Group header: Opening
		/// </summary>
		/// <param name="Sender">Sender of the Opening event.</param>
		/// <param name="Args">Cancel "Opening" event arguments.</param>
		private void MenuStrip_Group_Opening(object Sender, CancelEventArgs Args)
		{
			OLVGroup Group = (OLVGroup)MenuStrip_Group.Tag;

			if (Group.Collapsed)
			{
				MenuStrip_Group_Show.Visible = true;
				MenuStrip_Group_Hide.Visible = false;
			}
			else
			{
				MenuStrip_Group_Show.Visible = false;
				MenuStrip_Group_Hide.Visible = true;
			}
		}

		/// <summary>
		/// ContextMenuStrip event handler - Group header: Show: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Group_Show_Click(object Sender, EventArgs Args)
		{
			OLVGroup Group = (OLVGroup)MenuStrip_Group.Tag;
			Group.Collapsed = false;
		}

		// ListView: "Icon"
		/// <summary>
		/// ContextMenuStrip event handler - Icon column: AddLink: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Icon_AddLink_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Icon.Tag;
			Form_Link LinkForm = new Form_Link(this, TaskArray.IndexOf(Task));
			LinkForm.ShowDialog(this);
		}

		/// <summary>
		/// ContextMenuStrip event handler - Icon column: AddLocation: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Icon_AddLocation_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Icon.Tag;
			Form_Location LocationForm = new Form_Location(this, TaskArray.IndexOf(Task));
			LocationForm.ShowDialog(this);
		}

		/// <summary>
		/// ContextMenuStrip event handler - Icon column: AddNotes: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Icon_AddNotes_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Icon.Tag;
			Form_Notes NotesForm = new Form_Notes(this, TaskArray.IndexOf(Task));
			NotesForm.ShowDialog(this);
		}

		/// <summary>
		/// ContextMenuStrip event handler - Icon column: Link: Clipboard: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Icon_Link_Clipboard_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Icon.Tag;
			Clipboard.SetText(Task.Link);
		}

		/// <summary>
		/// ContextMenuStrip event handler - Icon column: Link: Edit: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Icon_Link_Edit_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Icon.Tag;
			Form_Link LinkForm = new Form_Link(this, TaskArray.IndexOf(Task));
			LinkForm.ShowDialog(this);
		}

		/// <summary>
		/// ContextMenuStrip event handler - Icon column: Link: Follow: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Icon_Link_Follow_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Icon.Tag;
			URLExtensions.Follow(Task.Link);
		}

		/// <summary>
		/// ContextMenuStrip event handler - Icon column: Link: Remove: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Icon_Link_Remove_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Icon.Tag;
			Task.Link = "";
			ArrayHandler.Save(ref TaskArray, Settings);
			//UpdateGroupKeys(Task);
			ListView.BuildList();
		}

		/// <summary>
		/// ContextMenuStrip event handler - Icon column: Location: Clipboard: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Icon_Location_Clipboard_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Icon.Tag;
			Clipboard.SetText(Task.Location);
		}

		/// <summary>
		/// ContextMenuStrip event handler - Icon column: Location: Edit: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Icon_Location_Edit_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Icon.Tag;
			Form_Location LocationForm = new Form_Location(this, TaskArray.IndexOf(Task));
			LocationForm.ShowDialog(this);
		}

		/// <summary>
		/// ContextMenuStrip event handler - Icon column: Location: Maps: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Icon_Location_Maps_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Icon.Tag;
			URLExtensions.Follow(string.Format(
				"http://maps.google.com/?q={0}",
				Uri.EscapeDataString(Task.Location)
			));
		}

		/// <summary>
		/// ContextMenuStrip event handler - Icon column: Location: Remove: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Icon_Location_Remove_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Icon.Tag;
			Task.Location = "";
			ArrayHandler.Save(ref TaskArray, Settings);
			//UpdateGroupKeys();
			ListView.BuildList();
		}

		/// <summary>
		/// ContextMenuStrip event handler - Icon column: Notes: Clipboard: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Icon_Notes_Clipboard_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Icon.Tag;
			Clipboard.SetText(Task.Notes);
		}

		/// <summary>
		/// ContextMenuStrip event handler - Icon column: Notes: Edit: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Icon_Notes_Edit_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Icon.Tag;
			Form_Notes NotesForm = new Form_Notes(this, TaskArray.IndexOf(Task));
			NotesForm.ShowDialog(this);
		}

		/// <summary>
		/// ContextMenuStrip event handler - Icon column: Notes: Remove: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Icon_Notes_Remove_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Icon.Tag;
			Task.Notes = "";
			ArrayHandler.Save(ref TaskArray, Settings);
			// UpdateGroupKeys();
			ListView.BuildList();
		}

		/// <summary>
		/// ContextMenuStrip event handler - Icon column: Opening
		/// </summary>
		/// <param name="Sender">Sender of the Opening event.</param>
		/// <param name="Args">Cancel "Opening" event arguments.</param>
		private void MenuStrip_Icon_Opening(object Sender, CancelEventArgs Args)
		{
			Task Task = (Task)MenuStrip_Icon.Tag;

			if (!string.IsNullOrEmpty(Task.Location))
			{
				MenuStrip_Icon_AddLocation.Visible = false;
				MenuStrip_Icon_Location.Visible = true;
			}
			else
			{
				MenuStrip_Icon_AddLocation.Visible = true;
				MenuStrip_Icon_Location.Visible = false;
			}
			if (!string.IsNullOrEmpty(Task.Link))
			{
				MenuStrip_Icon_AddLink.Visible = false;
				MenuStrip_Icon_Link.Visible = true;
			}
			else
			{
				MenuStrip_Icon_AddLink.Visible = true;
				MenuStrip_Icon_Link.Visible = false;
			}
			if (!string.IsNullOrEmpty(Task.Notes))
			{
				MenuStrip_Icon_AddNotes.Visible = false;
				MenuStrip_Icon_Notes.Visible = true;
			}
			else
			{
				MenuStrip_Icon_AddNotes.Visible = true;
				MenuStrip_Icon_Notes.Visible = false;
			}

			if (URLExtensions.Valid(Task.Link))
			{
				MenuStrip_Icon_Link_Follow.Enabled = true;
			}
			else
			{
				MenuStrip_Icon_Link_Follow.Enabled = false;
			}
		}

		// ListView: "Item"
		/// <summary>
		/// ContextMenuStrip event handler - ListView Item: Create: Quick: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Item_Create_Quick_Click(object Sender, EventArgs Args)
		{
			QuickCreate();
		}

		/// <summary>
		/// ContextMenuStrip event handler - ListView Item: Create: Advanced: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Item_Create_Advanced_Click(object Sender, EventArgs Args)
		{
			Form_Create CreateForm = new Form_Create(this);
			CreateForm.ShowDialog(this);
		}

		/// <summary>
		/// ContextMenuStrip event handler - ListView Item: Delete: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Item_Delete_Click(object Sender, EventArgs Args)
		{
			if (ConfirmAction(Settings.PromptDelete, "delete this task"))
			{
				Task Task = (Task)MenuStrip_Item.Tag;
				//TaskArray.Remove(Task);
				Task.Removed = true;
				ArrayHandler.Save(ref TaskArray, Settings);
				ListView.BuildList();
				UpdateStatusLabel();
			}
		}

		/// <summary>
		/// ContextMenuStrip event handler - ListView Item: Duplicate: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Item_Duplicate_Click(object Sender, EventArgs Args)
		{
			// PRETTY MUCH do a clean copy here, but with some minor adjustments

			Task Task = (Task)MenuStrip_Item.Tag;
			TimeInfo NewTime = new TimeInfo(
				Task.Time.Summary,
				Task.Time.CheckedTime,
				Task.Time.Created,
				DateTime.Now,
				Task.Time.Start,
				Task.Time.Next,
				Task.Time.End,
				Task.Time.Dismiss,
				false,
				Task.Time.Iterations,
				Task.Time.Count,
				Task.Time.Yearly,
				Task.Time.Monthly,
				Task.Time.Weekly,
				Task.Time.Daily,
				Task.Time.TimeOfDay,
				Task.Time.MonthFilter,
				Task.Time.WeekFilter,
				Task.Time.DayFilter,
				Task.Time.SpecificDay
			);

			Task NewTask = new Task(
				Task.GenerateID(),
				false,
				Task.Checked,
				Task.Priority,
				Task.Group,
				Task.Description,
				Task.Notes,
				Task.Link,
				Task.Location,
				NewTime
			);

			TaskArray.Add(NewTask);
			UpdateGroupKeys(NewTask);
			ListView.BuildList();
			UpdateStatusLabel();
		}

		/// <summary>
		/// ContextMenuStrip event handler - ListView Item: Edit: Advanced: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Item_Edit_Advanced_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Item.Tag;
			Form_Create CreateForm = new Form_Create(this, TaskArray.IndexOf(Task));
			CreateForm.ShowDialog(this);
		}

		/// <summary>
		/// ContextMenuStrip event handler - ListView Item: Edit: Quick: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Item_Edit_Quick_Click(object Sender, EventArgs Args)
		{
			Task Task = (Task)MenuStrip_Item.Tag;
			ListView.PossibleFinishCellEditing();
			ListView.EditModel(Task);
		}

		/// <summary>
		/// ContextMenuStrip event handler - ListView Item: Move: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Item_Move_ClickHandler(object Sender, EventArgs Args)
		{
			ToolStripDropDownItem Item = (ToolStripDropDownItem)Sender;
			Task Task = (Task)MenuStrip_Item.Tag;
			Task.Group = Item.Text;
			UpdateGroupKeys(Task);
			ListView.BuildList();
		}

		/// <summary>
		/// ContextMenuStrip event handler - ListView Item: Move: DropDownOpening
		/// </summary>
		/// <param name="Sender">Sender of the DropDownOpening event.</param>
		/// <param name="Args">DropDownOpening event arguments.</param>
		private void MenuStrip_Item_Move_DropDownOpening(object Sender, EventArgs Args)
		{
			Task TaggedTask = (Task)MenuStrip_Item.Tag;
			List<string> AlreadySelectedGroups = new List<string>();

			MenuStrip_Item_Move.DropDownItems.Clear();
			MenuStrip_Item_Move.DropDownItems.Add("(No other groups available)");
			MenuStrip_Item_Move.DropDownItems[0].Enabled = false;

			foreach (Task Task in TaskArray)
			{
				if (!AlreadySelectedGroups.Contains(Task.Group) && (TaggedTask.Group != Task.Group))
				{
					MenuStrip_Item_Move.DropDownItems.Add(
						Task.Group,
						null,
						MenuStrip_Item_Move_ClickHandler
					);
					AlreadySelectedGroups.Add(Task.Group);
					MenuStrip_Item_Move.DropDownItems[0].Visible = false;
				}
			}
		}

		// Notify: "Notify"
		/// <summary>
		/// ContextMenuStrip event handler - Notification icon: Show: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Notify_Show_Click(object Sender, EventArgs Args)
		{
			if (WindowState == FormWindowState.Minimized)
			{
				WindowState = FormWindowState.Normal;
			}
			Activate();
		}

		/// <summary>
		/// ContextMenuStrip event handler - Notification icon: Settings: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Notify_Settings_Click(object Sender, EventArgs Args)
		{
			Form_Settings SettingsForm = new Form_Settings(this);
			SettingsForm.ShowDialog(this);
		}

		/// <summary>
		/// ContextMenuStrip event handler - Notification icon: Quit: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Notify_Quit_Click(object Sender, EventArgs Args)
		{
			Close();
		}

		// StatusLabel: "Status"
		/// <summary>
		/// ContextMenuStrip event handler - StatusLabel: Clear: Click
		/// </summary>
		/// <param name="Sender">Sender of the click event.</param>
		/// <param name="Args">Click event arguments.</param>
		private void MenuStrip_Status_Clear_Click(object Sender, EventArgs Args)
		{
			if (ConfirmAction(Settings.PromptClear, "clear all tasks"))
			{
				Array_ClearAll();
			}
		}
	}
}
