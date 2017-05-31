using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace Tasual
{
	public partial class Tasual_Main : Form
	{
		// ==============
		//  Declarations
		// ==============

		public List<Task> TaskArray = new List<Task>();
		public Setting Settings = new Setting();

		OlvListViewHitTestInfo CalendarPopout = null;
		OlvListViewHitTestInfo Tasual_ListView_FirstClickInfo = null;
		bool Tasual_ListView_DoubleClickEdit = false;
		bool Tasual_ListView_PreviouslySelected = false;
		string Tasual_LastSelectedGroup;

		OLVColumn DescriptionColumn;
		OLVColumn IconColumn;
		OLVColumn CategoryColumn;
		OLVColumn DueColumn;
		OLVColumn TimeColumn;


		// ================
		//  Initialization
		// ================

		public Tasual_Main()
		{
			// Initialize interface objects
			InitializeComponent();

			// Load Settings
			Setting.Load(ref Settings);
			Tasual_Settings_Relocate();
			Tasual_Settings_Apply();

			// Load TaskArray
			ArrayHandler.Load(ref TaskArray, Settings);
			Tasual_ListView.SetObjects(TaskArray);

			// Setup ObjectListView
			Tasual_ListView_Setup();
			Tasual_ListView_AddColumns();
			Tasual_ListView.RebuildColumns();

			// Update StatusLabel to reflect item counts
			Tasual_StatusLabel_UpdateCounts();

			// Other initialization
			Tasual_Timer_ListViewClick.Interval = SystemInformation.DoubleClickTime;
		}


		// =============================
		//  Common/Supporting Functions
		// =============================

		// Allow other dialogs to trigger a save
		// This was necessary as simply calling ArrayHandler.Save() from other classes would give a CS1690 warning for
		// using a field of a marshal-by-reference class. Additionally, these methods must continue using references 
		// otherwise other functions in the program cease to be able to retrieve data from TaskArray.
		// TODO: Is this really the right way to handle this? Should we keep the array handling code purely in the 
		//       Tasual_Main class? Or is there another way to handle it.
		public void Tasual_Array_Save()
		{
			ArrayHandler.Save(ref TaskArray, Settings);
		}

		public void Tasual_Array_ClearAll()
		{
			TaskArray.Clear();
			ArrayHandler.Save(ref TaskArray, Settings);
			ArrayHandler.Load(ref TaskArray, Settings);
			Tasual_UpdateGroupKeys();
			Tasual_ListView.SetObjects(TaskArray);
			Tasual_ListView.BuildList();
		}

		public void Tasual_Settings_Apply()
		{
			TopMost = Settings.AlwaysOnTop;
			StartupManager.SetStartupStatus(Settings.LaunchOnStartup);
		}

		public void Tasual_Settings_Relocate()
		{
			if (Settings.SaveWindowPos)
			{
				StartPosition = FormStartPosition.Manual;
				Location = Settings.Location;
				Size = Settings.Size;
				WindowState = Settings.WindowState;
			}
		}

		public void Tasual_Settings_Save()
		{
			Setting.Save(ref Settings);
		}

		public void Tasual_StatusLabel_UpdateCounts()
		{
			int Complete = Tasual_ListView.CheckedItems.Count;
			int Total = Tasual_ListView.GetItemCount();

			if (Complete == Total)
			{
				Tasual_StatusLabel.Text = "All tasks complete";
			}
			else
			{
				Tasual_StatusLabel.Text = string.Format("{0} of {1} tasks complete", Complete, Total);
			}
		}

		public void Tasual_UpdateGroupKeys()
		{
			foreach (Task Task in TaskArray)
			{
				Task.CategoryGroupKey = TimeInfo.GetGroupStringFromTask(Task, Settings);
				Task.DueGroupKey = TimeInfo.GetDueStringFromTask(Task);
			}
		}

		public void Tasual_UpdateGroupKeys(Task Task)
		{
			Task.CategoryGroupKey = TimeInfo.GetGroupStringFromTask(Task, Settings);
			Task.DueGroupKey = TimeInfo.GetDueStringFromTask(Task);
		}

		public void Tasual_CheckNeedsUpdate()
		{
			/* 
			 * When a singular task expires
			 *  - TODO: Check to see if we should push a notification
			 *  - Set expired flag and delete if dismissal is immediate
			 *  - Update listview
			 *  
			 * When a simple or recurring task expires
			 *  - Check to see if task has another iteration on the way
			 *    - Compare current count to total iterations allowed
			 *    - Check end date to make sure the next iteration wouldn't be after the end date
			 *    - If both conditions pass, create a new iteration
			 *  - TODO: Check to see if we should push a notification
			 *  - Set expired flag and delete if dismissal is immediate
			 *  - Update listview
			 */

			bool UpdateList = false;
			List<Task> RemovalList = new List<Task>();
			List<Task> AddedList = new List<Task>();

			foreach (Task Task in TaskArray)
			{
				if (Task.Checked)
				{
					if (Settings.RemoveCompleted != Setting.RemoveType.Never)
					{
						if (DateTime.Now > (Task.Time.CheckedTime + Setting.GetRemoveTimeSpan(Settings.RemoveCompleted)))
						{
							// Delete task
							RemovalList.Add(Task);
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
								RemovalList.Add(Task);
								UpdateList = true;
							}
						}
						// else do nothing
					}
					else if (DateTime.Now > Task.Time.Next)
					{
						// Task is now expired, check to see if we should delete it
						Task.Time.Expired = true;
						if (Task.Time.Dismiss == TimeInfo.DismissType.Immediate) // immediate deletion
						{
							// Delete task
							RemovalList.Add(Task);
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

			foreach (Task RemoveTask in RemovalList)
			{
				TaskArray.Remove(RemoveTask);
			}

			foreach (Task AddTask in AddedList)
			{
				TaskArray.Add(AddTask);
			}

			if (UpdateList)
			{
				ArrayHandler.Save(ref TaskArray, Settings);
				Tasual_UpdateGroupKeys();
				Tasual_ListView.BuildList();
				Tasual_StatusLabel_UpdateCounts();
			}
		}

		private void Tasual_QuickCreate()
		{
			string GroupName = "Tasks";
			// defaults to the first group it finds OR the last selected group
			if (!string.IsNullOrEmpty(Tasual_LastSelectedGroup))
			{
				GroupName = Tasual_LastSelectedGroup;
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

			Task Task = new Task(
				false,
				0,
				GroupName,
				"New task",
				"",
				"",
				"",
				new TimeInfo()
			);

			TaskArray.Add(Task);
			ArrayHandler.Save(ref TaskArray, Settings);
			Tasual_UpdateGroupKeys(Task);
			Tasual_ListView.BuildList();
			Tasual_StatusLabel_UpdateCounts();
			Tasual_ListView.PossibleFinishCellEditing();
			Tasual_ListView.EnsureModelVisible(Task);
			Tasual_ListView.EditModel(Task);
		}


		// ====================
		//  ListView Functions
		// ====================

		private void Tasual_ListView_FormatRow(object sender, FormatRowEventArgs e)
		{
			if (e.Item.Checked)
			{
				e.Item.ForeColor = Color.FromArgb(255, 189, 208, 230);
			}
			else
			{
				e.Item.ForeColor = Color.FromArgb(255, 36, 90, 150);
			}
		}

		private void Tasual_ListView_ModelCanDrop_Category(object sender, ModelDropEventArgs Args)
		{
			Task Target = (Task)Args.TargetModel;
			Args.Effect = DragDropEffects.None;

			if (Target != null)
			{
				var SourceTasks = Args.SourceModels.Cast<Task>();
				//bool AnySourceUndraggable = SourceTasks.Any(Source => (!TimeInfo.CanDragDropCategory(Source, Settings)));
				//if (AnySourceUndraggable)
				//{
				//	Args.InfoMessage = "Cannot drag from undraggable category";
				//}
				//else if (!TimeInfo.CanDragDropCategory(Target, Settings))
				//{
				//	Args.InfoMessage = "Cannot drag to undraggable category";
				//}

				// both source and target are draggable, now lets check whether they're the same
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
		}

		private void Tasual_ListView_ModelCanDrop_DueTime(object sender, ModelDropEventArgs Args)
		{
			Task Target = (Task)Args.TargetModel;
			Args.Effect = DragDropEffects.None;

			if (Target != null)
			{
				var SourceTasks = Args.SourceModels.Cast<Task>();
				if (Settings.GroupStyle == Setting.GroupStyles.DueTime)
				{
					if (SourceTasks.Any(Source => (TimeInfo.CompareDueStringFromTasks(Source, Target))))
					{
						Args.InfoMessage = "Cannot drag to the same time";
					}
					else
					{
						// looks good!
						Args.Effect = DragDropEffects.Move;
					}
				}
			}
		}

		private void Tasual_ListView_ModelCanDrop_ChooseHandler(object sender, ModelDropEventArgs Args)
		{
			if (Settings.GroupTasks)
			{
				if (Settings.GroupStyle == Setting.GroupStyles.Category)
				{
					Tasual_ListView_ModelCanDrop_Category(sender, Args);
				}
				else if (Settings.GroupStyle == Setting.GroupStyles.DueTime)
				{
					Tasual_ListView_ModelCanDrop_DueTime(sender, Args);
				}
			}
		}

		private void Tasual_ListView_ModelDropped_Category(object sender, ModelDropEventArgs Args)
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
										DateTime NewDate = DateTime.Now;
										NewDate = NewDate.AddSeconds(-1);
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
										NewDate = NewDate.AddSeconds(-1);
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
			Tasual_UpdateGroupKeys();
			Tasual_ListView.BuildList();
		}

		private void Tasual_ListView_ModelDropped_DueTime(object sender, ModelDropEventArgs Args)
		{
			Task Target = (Task)Args.TargetModel;
			Args.Effect = DragDropEffects.None;

			if (Target != null)
			{
				var SourceTasks = Args.SourceModels.Cast<Task>();
				if (Settings.GroupStyle == Setting.GroupStyles.DueTime)
				{
					if (SourceTasks.Any(Source => (TimeInfo.CompareDueStringFromTasks(Source, Target))))
					{
						Args.InfoMessage = "Cannot drag to the same time";
					}
					else
					{
						// looks good!
						Args.Effect = DragDropEffects.Move;
					}
				}
			}
		}

		private void Tasual_ListView_ModelDropped_ChooseHandler(object sender, ModelDropEventArgs Args)
		{
			if (Settings.GroupTasks)
			{
				if (Settings.GroupStyle == Setting.GroupStyles.Category)
				{
					Tasual_ListView_ModelDropped_Category(sender, Args);
				}
				else if (Settings.GroupStyle == Setting.GroupStyles.DueTime)
				{
					Tasual_ListView_ModelDropped_DueTime(sender, Args);
				}
			}
		}

		private void Tasual_ListView_Setup()
		{
			Tasual_ListView.ShowItemCountOnGroups = false;
			Tasual_ListView.UseCustomSelectionColors = true;
			Tasual_ListView.SelectedBackColor = Color.FromArgb(255, 222, 232, 246);
			Tasual_ListView.SelectedForeColor = Color.FromArgb(255, 36, 90, 150);
			Tasual_ListView.UnfocusedSelectedBackColor = Color.FromArgb(255, 222, 232, 246);
			Tasual_ListView.UnfocusedSelectedForeColor = Color.FromArgb(255, 36, 90, 150);
			Tasual_ListView.FormatRow += new EventHandler<FormatRowEventArgs>(Tasual_ListView_FormatRow);

			HeaderFormatStyle FormatStyle = new HeaderFormatStyle();
			FormatStyle.Normal.ForeColor = Color.FromArgb(255, 36, 90, 150);
			FormatStyle.Normal.BackColor = Color.FromArgb(255, 222, 232, 246);
			FormatStyle.Hot.ForeColor = Color.FromArgb(255, 36, 90, 150);
			FormatStyle.Hot.BackColor = Color.FromArgb(255, 240, 240, 255);
			FormatStyle.Pressed.ForeColor = Color.FromArgb(255, 36, 90, 150);
			FormatStyle.Pressed.BackColor = Color.FromArgb(255, 240, 240, 255);
			Tasual_ListView.HeaderUsesThemes = false;
			Tasual_ListView.HeaderFormatStyle = FormatStyle;

			TextOverlay Overlay = new TextOverlay();
			Overlay.Font = this.Font;
			Overlay.Text = "All tasks completed. Create some tasks!";
			Overlay.TextColor = Color.FromArgb(255, 36, 90, 150);
			Overlay.BackColor = Color.FromArgb(255, 222, 232, 246);
			Overlay.BorderColor = Color.FromArgb(255, 36, 90, 150);
			Overlay.BorderWidth = 3;
			Overlay.CornerRounding = 0;
			Tasual_ListView.EmptyListMsgOverlay = Overlay;

			Tasual_ListView.PersistentCheckBoxes = true;
			Tasual_ListView.CheckBoxes = true;
			Tasual_ListView.CheckedAspectName = "Checked";
			Tasual_ListView.CellEditUseWholeCell = true;

			Tasual_ListView.IsSimpleDragSource = true;
			Tasual_ListView.IsSimpleDropSink = true;
			SimpleDropSink Sink = (SimpleDropSink)Tasual_ListView.DropSink;
			Sink.CanDropOnItem = false;
			Sink.CanDropBetween = true;
			Sink.FeedbackColor = Color.FromArgb(255, 222, 232, 246);
			//Tasual_ListView.
			Tasual_ListView.ModelCanDrop += new EventHandler<ModelDropEventArgs>(Tasual_ListView_ModelCanDrop_ChooseHandler);
			Tasual_ListView.ModelDropped += new EventHandler<ModelDropEventArgs>(Tasual_ListView_ModelDropped_ChooseHandler);

			Tasual_ListView.BooleanCheckStateGetter = delegate (object RowObject)
			{
				return ((Task)RowObject).Checked;
			};

			Tasual_ListView.BooleanCheckStatePutter = delegate (object RowObject, bool NewValue)
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
				Tasual_UpdateGroupKeys(Task);
				Tasual_ListView.BuildList();
				Tasual_StatusLabel_UpdateCounts();
				return NewValue;
			};
		}

		public void Tasual_ListView_UpdateColumnSettings()
		{
			Tasual_ListView.ShowGroups = Settings.GroupTasks;
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

			Tasual_ListView.AlwaysGroupByColumn = SelectedColumn; //CategoryColumn DueColumn
			Tasual_ListView.PrimarySortColumn = SelectedColumn;// CategoryColumn DueColumn
		}

		private void Tasual_ListView_AddColumns()
		{
			Tasual_UpdateGroupKeys();
			DescriptionColumn = new OLVColumn("Description", "Description");
			DescriptionColumn.MinimumWidth = 100;
			DescriptionColumn.FillsFreeSpace = true;
			DescriptionColumn.IsVisible = true;
			DescriptionColumn.IsEditable = true;
			DescriptionColumn.Sortable = false;
			DescriptionColumn.DisplayIndex = 1;
			DescriptionColumn.LastDisplayIndex = 1;
			DescriptionColumn.HeaderTextAlign = HorizontalAlignment.Center;
			Tasual_ListView.AllColumns.Add(DescriptionColumn);
			Tasual_ListView.Columns.AddRange(new ColumnHeader[] { DescriptionColumn });

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
			Tasual_ListView.AllColumns.Add(IconColumn);
			Tasual_ListView.Columns.AddRange(new ColumnHeader[] { IconColumn });

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
			Tasual_ListView.AllColumns.Add(CategoryColumn);
			Tasual_ListView.Columns.AddRange(new ColumnHeader[] { CategoryColumn });

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
			Tasual_ListView.AllColumns.Add(DueColumn);
			Tasual_ListView.Columns.AddRange(new ColumnHeader[] { DueColumn });

			TimeColumn = new OLVColumn("Time", "Time");
			TimeColumn.MinimumWidth = 130;
			TimeColumn.IsVisible = true;
			TimeColumn.IsEditable = false;
			TimeColumn.Sortable = false; // TODO: Allow sorting by this
			TimeColumn.DisplayIndex = 5;
			TimeColumn.LastDisplayIndex = 5;
			TimeColumn.TextAlign = Settings.SubItemTextAlign;
			TimeColumn.HeaderTextAlign = Settings.SubItemHeaderAlign;
			TimeColumn.AspectToStringConverter = delegate(object Input)
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
			Tasual_ListView.AllColumns.Add(TimeColumn);
			Tasual_ListView.Columns.AddRange(new ColumnHeader[] { TimeColumn });

			Tasual_ListView_UpdateColumnSettings();
			Tasual_ListView.SortGroupItemsByPrimaryColumn = true;
			Tasual_ListView.PrimarySortOrder = SortOrder.Ascending;
			Tasual_ListView.SecondarySortColumn = DescriptionColumn;
		}


		// ====================
		//  MenuStrip Handlers
		// ====================

		// Tasual_Main: "Create"
		private void Tasual_MenuStrip_Create_Advanced_Click(object sender, EventArgs e)
		{
			Tasual_Create CreateForm = new Tasual_Create(this);
			CreateForm.ShowDialog(this);
		}

		private void Tasual_MenuStrip_Create_Quick_Click(object sender, EventArgs e)
		{
			Tasual_QuickCreate();
		}

		// Tasual_Main: "Edit"
		private void Tasual_MenuStrip_Edit_Advanced_Click(object sender, EventArgs e)
		{
			if (Tasual_ListView.SelectedItem != null)
			{
				Task Task = (Task)Tasual_ListView.SelectedItem.RowObject;
				Tasual_Create CreateForm = new Tasual_Create(this, TaskArray.IndexOf(Task));
				CreateForm.ShowDialog(this);
			}
		}

		private void Tasual_MenuStrip_Edit_Quick_Click(object sender, EventArgs e)
		{
			if (Tasual_ListView.SelectedItem != null)
			{
				Task Task = (Task)Tasual_ListView.SelectedItem.RowObject;
				Tasual_ListView.PossibleFinishCellEditing();
				Tasual_ListView.EnsureModelVisible(Task);
				Tasual_ListView.EditModel(Task);
			}
		}

		// Tasual_Main: "Settings"
		private void Tasual_MenuStrip_Settings_Click(object sender, EventArgs e)
		{
			Tasual_Settings SettingsForm = new Tasual_Settings(this);
			SettingsForm.ShowDialog(this);
		}

		// Tasual_Main: "Sources"
		private void Tasual_MenuStrip_Sources_Click(object sender, EventArgs e)
		{
			// Currently unused
		}

		// Tasual_ListView: "Group"
		private void Tasual_MenuStrip_Group_Delete_Click(object sender, EventArgs e)
		{
			OLVGroup Group = (OLVGroup)Tasual_MenuStrip_Group.Tag;
			List<Task> RemovalList = new List<Task>();

			foreach (Task Task in TaskArray)
			{
				if (TimeInfo.GetGroupStringFromTask(Task, Settings).Remove(0, 1) == Group.Name)
				{
					RemovalList.Add(Task);
				}
			}

			foreach (Task RemoveTask in RemovalList)
			{
				TaskArray.Remove(RemoveTask);
			}

			ArrayHandler.Save(ref TaskArray, Settings);
			Tasual_ListView.BuildList();
			Tasual_StatusLabel_UpdateCounts();
		}

		private void Tasual_MenuStrip_Group_Hide_Click(object sender, EventArgs e)
		{
			OLVGroup Group = (OLVGroup)Tasual_MenuStrip_Group.Tag;
			Group.Collapsed = true;
		}

		private void Tasual_MenuStrip_Group_MoveTasks_ClickHandler(object sender, EventArgs e)
		{
			ToolStripDropDownItem Item = (ToolStripDropDownItem)sender;
			OLVGroup Group = (OLVGroup)Tasual_MenuStrip_Group.Tag;
			ArrayHandler.ReAssignGroup(TaskArray, Group.Name, Item.Text);
			Tasual_UpdateGroupKeys();
			Tasual_ListView.BuildList();
		}

		private void Tasual_MenuStrip_Group_MoveTasks_DropDownOpening(object sender, EventArgs e)
		{
			OLVGroup Group = (OLVGroup)Tasual_MenuStrip_Group.Tag;
			List<string> AlreadySelectedGroups = new List<string>();

			Tasual_MenuStrip_Group_MoveTasks.DropDownItems.Clear();
			Tasual_MenuStrip_Group_MoveTasks.DropDownItems.Add("(No other groups available)");
			Tasual_MenuStrip_Group_MoveTasks.DropDownItems[0].Enabled = false;

			foreach (Task Task in TaskArray)
			{
				if (!AlreadySelectedGroups.Contains(Task.Group) && (Group.Name != Task.Group))
				{
					Tasual_MenuStrip_Group_MoveTasks.DropDownItems.Add(Task.Group, null, Tasual_MenuStrip_Group_MoveTasks_ClickHandler);
					AlreadySelectedGroups.Add(Task.Group);
					Tasual_MenuStrip_Group_MoveTasks.DropDownItems[0].Visible = false;
				}
			}
		}

		private void Tasual_MenuStrip_Group_Opening(object sender, CancelEventArgs e)
		{
			OLVGroup Group = (OLVGroup)Tasual_MenuStrip_Group.Tag;

			if (Group.Collapsed)
			{
				Tasual_MenuStrip_Group_Show.Visible = true;
				Tasual_MenuStrip_Group_Hide.Visible = false;
			}
			else
			{
				Tasual_MenuStrip_Group_Show.Visible = false;
				Tasual_MenuStrip_Group_Hide.Visible = true;
			}
		}

		private void Tasual_MenuStrip_Group_Show_Click(object sender, EventArgs e)
		{
			OLVGroup Group = (OLVGroup)Tasual_MenuStrip_Group.Tag;
			Group.Collapsed = false;
		}

		// Tasual_ListView: "Icon"
		private void Tasual_MenuStrip_Icon_AddLink_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			Tasual_Link LinkForm = new Tasual_Link(this, TaskArray.IndexOf(Task));
			LinkForm.ShowDialog(this);
		}

		private void Tasual_MenuStrip_Icon_AddLocation_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			Tasual_Location LocationForm = new Tasual_Location(this, TaskArray.IndexOf(Task));
			LocationForm.ShowDialog(this);
		}

		private void Tasual_MenuStrip_Icon_AddNotes_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			Tasual_Notes NotesForm = new Tasual_Notes(this, TaskArray.IndexOf(Task));
			NotesForm.ShowDialog(this);
		}

		private void Tasual_MenuStrip_Icon_Link_Clipboard_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			Clipboard.SetText(Task.Link);
		}

		private void Tasual_MenuStrip_Icon_Link_Edit_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			Tasual_Link LinkForm = new Tasual_Link(this, TaskArray.IndexOf(Task));
			LinkForm.ShowDialog(this);
		}

		private void Tasual_MenuStrip_Icon_Link_Follow_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			URLExtensions.Follow(Task.Link);
		}

		private void Tasual_MenuStrip_Icon_Link_Remove_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			Task.Link = "";
			ArrayHandler.Save(ref TaskArray, Settings);
			//Tasual_UpdateGroupKeys(Task);
			Tasual_ListView.BuildList();
		}

		private void Tasual_MenuStrip_Icon_Location_Clipboard_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			Clipboard.SetText(Task.Location);
		}

		private void Tasual_MenuStrip_Icon_Location_Edit_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			Tasual_Location LocationForm = new Tasual_Location(this, TaskArray.IndexOf(Task));
			LocationForm.ShowDialog(this);
		}

		private void Tasual_MenuStrip_Icon_Location_Maps_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			URLExtensions.Follow(string.Format("http://maps.google.com/?q={0}", Uri.EscapeDataString(Task.Location)));
		}

		private void Tasual_MenuStrip_Icon_Location_Remove_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			Task.Location = "";
			ArrayHandler.Save(ref TaskArray, Settings);
			//Tasual_UpdateGroupKeys();
			Tasual_ListView.BuildList();
		}

		private void Tasual_MenuStrip_Icon_Notes_Clipboard_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			Clipboard.SetText(Task.Notes);
		}

		private void Tasual_MenuStrip_Icon_Notes_Edit_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			Tasual_Notes NotesForm = new Tasual_Notes(this, TaskArray.IndexOf(Task));
			NotesForm.ShowDialog(this);
		}

		private void Tasual_MenuStrip_Icon_Notes_Remove_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			Task.Notes = "";
			ArrayHandler.Save(ref TaskArray, Settings);
			// Tasual_UpdateGroupKeys();
			Tasual_ListView.BuildList();
		}

		private void Tasual_MenuStrip_Icon_Opening(object sender, CancelEventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;

			if (!string.IsNullOrEmpty(Task.Location))
			{
				Tasual_MenuStrip_Icon_AddLocation.Visible = false;
				Tasual_MenuStrip_Icon_Location.Visible = true;
			}
			else
			{
				Tasual_MenuStrip_Icon_AddLocation.Visible = true;
				Tasual_MenuStrip_Icon_Location.Visible = false;
			}
			if (!string.IsNullOrEmpty(Task.Link))
			{
				Tasual_MenuStrip_Icon_AddLink.Visible = false;
				Tasual_MenuStrip_Icon_Link.Visible = true;
			}
			else
			{
				Tasual_MenuStrip_Icon_AddLink.Visible = true;
				Tasual_MenuStrip_Icon_Link.Visible = false;
			}
			if (!string.IsNullOrEmpty(Task.Notes))
			{
				Tasual_MenuStrip_Icon_AddNotes.Visible = false;
				Tasual_MenuStrip_Icon_Notes.Visible = true;
			}
			else
			{
				Tasual_MenuStrip_Icon_AddNotes.Visible = true;
				Tasual_MenuStrip_Icon_Notes.Visible = false;
			}

			if (URLExtensions.Valid(Task.Link))
			{
				Tasual_MenuStrip_Icon_Link_Follow.Enabled = true;
			}
			else
			{
				Tasual_MenuStrip_Icon_Link_Follow.Enabled = false;
			}
		}

		// Tasual_ListView: "Item"
		private void Tasual_MenuStrip_Item_Create_Quick_Click(object sender, EventArgs e)
		{
			Tasual_QuickCreate();
		}

		private void Tasual_MenuStrip_Item_Create_Advanced_Click(object sender, EventArgs e)
		{
			Tasual_Create CreateForm = new Tasual_Create(this);
			CreateForm.ShowDialog(this);
		}

		private void Tasual_MenuStrip_Item_Delete_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Item.Tag;
			TaskArray.Remove(Task);
			ArrayHandler.Save(ref TaskArray, Settings);
			Tasual_ListView.BuildList();
			Tasual_StatusLabel_UpdateCounts();
		}

		private void Tasual_MenuStrip_Item_Duplicate_Click(object sender, EventArgs e)
		{
			// PRETTY MUCH do a clean copy here, but with some minor caveats

			Task Task = (Task)Tasual_MenuStrip_Item.Tag;
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
			Tasual_UpdateGroupKeys(NewTask);
			Tasual_ListView.BuildList();
			Tasual_StatusLabel_UpdateCounts();
		}

		private void Tasual_MenuStrip_Item_Edit_Advanced_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Item.Tag;
			Tasual_Create CreateForm = new Tasual_Create(this, TaskArray.IndexOf(Task));
			CreateForm.ShowDialog(this);
		}

		private void Tasual_MenuStrip_Item_Edit_Quick_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Item.Tag;
			Tasual_ListView.PossibleFinishCellEditing();
			Tasual_ListView.EditModel(Task);
		}

		private void Tasual_MenuStrip_Item_Move_ClickHandler(object sender, EventArgs e)
		{
			ToolStripDropDownItem Item = (ToolStripDropDownItem)sender;
			Task Task = (Task)Tasual_MenuStrip_Item.Tag;
			Task.Group = Item.Text;
			Tasual_UpdateGroupKeys(Task);
			Tasual_ListView.BuildList();
		}

		private void Tasual_MenuStrip_Item_Move_DropDownOpening(object sender, EventArgs e)
		{
			Task TaggedTask = (Task)Tasual_MenuStrip_Item.Tag;
			List<string> AlreadySelectedGroups = new List<string>();

			Tasual_MenuStrip_Item_Move.DropDownItems.Clear();
			Tasual_MenuStrip_Item_Move.DropDownItems.Add("(No other groups available)");
			Tasual_MenuStrip_Item_Move.DropDownItems[0].Enabled = false;

			foreach (Task Task in TaskArray)
			{
				if (!AlreadySelectedGroups.Contains(Task.Group) && (TaggedTask.Group != Task.Group))
				{
					Tasual_MenuStrip_Item_Move.DropDownItems.Add(Task.Group, null, Tasual_MenuStrip_Item_Move_ClickHandler);
					AlreadySelectedGroups.Add(Task.Group);
					Tasual_MenuStrip_Item_Move.DropDownItems[0].Visible = false;
				}
			}
		}

		// Tasual_Notify: "Notify"
		private void Tasual_MenuStrip_Notify_Show_Click(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Minimized)
			{
				WindowState = FormWindowState.Normal;
			}
			Activate();
		}

		private void Tasual_MenuStrip_Notify_Settings_Click(object sender, EventArgs e)
		{
			Tasual_Settings SettingsForm = new Tasual_Settings(this);
			SettingsForm.ShowDialog(this);
		}

		private void Tasual_MenuStrip_Notify_Quit_Click(object sender, EventArgs e)
		{
			Close();
		}

		// Tasual_StatusLabel: "Status"
		private void Tasual_MenuStrip_Status_Clear_Click(object sender, EventArgs e)
		{
			Tasual_Confirm_Clear ConfirmForm = new Tasual_Confirm_Clear(this);
			ConfirmForm.ShowDialog(this);
		}


		// ================
		//  Other Handlers
		// ================

		// Main Form
		private void Tasual_Main_FormClosing(object sender, FormClosingEventArgs e)
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

			Tasual_Notify.Dispose();
		}

		private void Tasual_Main_Resize(object sender, EventArgs e)
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

		// Notification Icon
		private void Tasual_Notify_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (WindowState == FormWindowState.Minimized)
				{
					WindowState = FormWindowState.Normal;
				}
				Activate();
			}
			else if (e.Button == MouseButtons.Right)
			{
				Tasual_MenuStrip_Notify.Show(Cursor.Position);
			}
		}

		// Labels
		private void Tasual_StatusLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Tasual_MenuStrip_Status.Show(Tasual_StatusLabel, new Point(0, Tasual_StatusLabel.Height));
		}

		private void Tasual_AboutLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Tasual_About AboutForm = new Tasual_About();
			AboutForm.ShowDialog(this);
		}

		// Tasual_ListView: Misc handlers
		private void Tasual_ListView_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
		{
			foreach (OLVGroup Group in e.Groups)
			{
				Group.Name = Group.Header;
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

		private void Tasual_ListView_CellEditFinished(object sender, CellEditEventArgs e)
		{
			ArrayHandler.Save(ref TaskArray, Settings);
			Tasual_MenuStrip_Edit.Enabled = false;
		}

		private void Tasual_ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			/*Console.WriteLine("hmm");

			//e.Item.
			\

			CheckedTime = DateTime.MinValue;
			ArrayHandler.Save(ref TaskArray, Settings);
			Tasual_ListView.BuildList();
			Tasual_StatusLabel_UpdateCounts();*/
		}

		private void Tasual_ListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Tasual_ListView.SelectedItem != null)
			{
				Tasual_MenuStrip_Edit.Enabled = true;
				Tasual_LastSelectedGroup = ((Task)Tasual_ListView.SelectedItem.RowObject).Group;
			}
			else
			{
				Tasual_MenuStrip_Edit.Enabled = false;
			}
		}

		// Tasual_ListView: Click handlers
		private void Tasual_ListView_SingleClick(MouseEventArgs e)
		{
			if (Tasual_ListView_PreviouslySelected == true)
			{
				if (Tasual_ListView.SelectedItem != null)
				{
					Tasual_ListView.EditModel(Tasual_ListView.SelectedItem.RowObject);
				}
			}
			else
			{
				// do nothing
			}
		}

		private void Tasual_ListView_DoubleClick(MouseEventArgs e)
		{
			OlvListViewHitTestInfo SecondClickInfo = Tasual_ListView.OlvHitTest(e.X, e.Y);

			if ((Tasual_ListView_FirstClickInfo.Item != null) && (SecondClickInfo.Item != null))
			{
				if (Tasual_ListView_FirstClickInfo.Item == SecondClickInfo.Item)
				{
					if (Tasual_ListView.SelectedItem != null)
					{
						Task Task = (Task)Tasual_ListView.SelectedItem.RowObject;
						Tasual_ListView.PossibleFinishCellEditing();
						//Tasual_ListView.Edit
						Tasual_ListView.EditModel(Task);
					}
				}
			}
		}

		private void Tasual_Timer_ListViewClick_Tick(object sender, EventArgs e)
		{
			if ((MouseButtons & MouseButtons.Left) == 0)
			{
				Tasual_ListView_SingleClick((MouseEventArgs)Tasual_Timer_ListViewClick.Tag);
			}
			Tasual_ListView_FirstClickInfo = null;
			Tasual_ListView_PreviouslySelected = false;
			Tasual_Timer_ListViewClick.Stop();
		}

		private void Tasual_ListView_MouseDown(object sender, MouseEventArgs e)
		{
			OlvListViewHitTestInfo Info = Tasual_ListView.OlvHitTest(e.X, e.Y);

			if (Info.Group != null)
			{
				//Console.WriteLine("Item: {0}", (Info.Column != null) ? Info.Column.Name : "foobar");
				// TODO: Fix the issue where clicking blank space inside of the group still triggers the group
				if (e.Button == MouseButtons.Right)
				{
					Tasual_MenuStrip_Group.Tag = Info.Group;
					Tasual_MenuStrip_Group.Show(Cursor.Position);
				}
			}
			else if (Info.Item != null)
			{
				if (Tasual_Timer_ListViewClick.Enabled && (e.Button == MouseButtons.Left)) // second click
				{
					if (Tasual_ListView_FirstClickInfo.Item != null)
					{
						if (Tasual_ListView_FirstClickInfo.Item == Info.Item)
						{
							Tasual_ListView_DoubleClickEdit = true;
						}
					}

					Tasual_ListView_PreviouslySelected = false;
					Tasual_ListView_FirstClickInfo = null;
					Tasual_Timer_ListViewClick.Stop();
				}
				else // first click
				{
					switch (Info.Column.AspectName)
					{
						case "Description":
							{
								if (e.Button == MouseButtons.Left)
								{
									Tasual_Timer_ListViewClick.Start();

									if (Tasual_ListView.SelectedItem == Info.Item)
									{
										Tasual_ListView_PreviouslySelected = true;
									}

									Tasual_ListView_FirstClickInfo = Info;
									Tasual_Timer_ListViewClick.Tag = e;
								}
								else
								{
									Tasual_MenuStrip_Item.Tag = Info.Item.RowObject;
									Tasual_MenuStrip_Item.Show(Cursor.Position.X, Cursor.Position.Y);
								}
								break;
							}
						case "Icons":
							{
								Tasual_MenuStrip_Icon.Tag = Info.Item.RowObject;
								Tasual_MenuStrip_Icon.Show(Cursor.Position.X, Cursor.Position.Y);
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

		private void Tasual_ListView_MouseUp(object sender, MouseEventArgs e)
		{
			if (CalendarPopout != null)
			{
				Tasual_TimePop Calendar = new Tasual_TimePop(this, TaskArray.IndexOf((Task)Tasual_ListView.SelectedItem.RowObject));

				Rectangle Bounds = CalendarPopout.SubItem.Bounds;
				Calendar.Location = PointToScreen(new Point(Bounds.Left, Bounds.Bottom + Bounds.Height + 5));
				Calendar.Show(this);

				CalendarPopout = null;
			}

			else if (Tasual_ListView_DoubleClickEdit)
			{
				if (Tasual_ListView.SelectedItem != null)
				{
					Task Task = (Task)Tasual_ListView.SelectedItem.RowObject;
					Tasual_ListView.PossibleFinishCellEditing();
					Tasual_ListView.EditModel(Task);
				}
				Tasual_ListView_DoubleClickEdit = false;
			}
		}

		private void Tasual_ListView_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Delete:
					{
						if (Tasual_ListView.SelectedItem != null)
						{
							Task Task = (Task)Tasual_ListView.SelectedItem.RowObject;
							TaskArray.Remove(Task);
							ArrayHandler.Save(ref TaskArray, Settings);
							Tasual_ListView.BuildList();
							Tasual_StatusLabel_UpdateCounts();
						}
						break;
					}

				case Keys.Enter:
					{
						Tasual_QuickCreate();
						break;
					}
			}
		}




		private void Tasual_Timer_CheckUpdate_Tick(object sender, EventArgs e)
		{
			Tasual_CheckNeedsUpdate();
		}
	}
}
