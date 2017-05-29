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
			Tasual_ListView.SetObjects(TaskArray);
			Tasual_ListView.BuildList();
		}

		public void Tasual_Settings_Apply()
		{
			this.TopMost = Settings.AlwaysOnTop;
			StartupManager.SetStartupStatus(Settings.LaunchOnStartup);
		}

		public void Tasual_Settings_Relocate()
		{
			if (Settings.SaveWindowPos)
			{
				this.StartPosition = FormStartPosition.Manual;
				this.Location = Settings.Location;
				this.Size = Settings.Size;
				this.WindowState = Settings.WindowState;
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

			Tasual_ListView.PersistentCheckBoxes = true;
			Tasual_ListView.CheckBoxes = true;
			Tasual_ListView.CheckedAspectName = "Checked";
			Tasual_ListView.CellEditUseWholeCell = true;

			Tasual_ListView.IsSimpleDragSource = true;
			Tasual_ListView.IsSimpleDropSink = true;
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
			IconColumn.HeaderTextAlign = HorizontalAlignment.Center;
			IconColumn.ShowTextInHeader = false;
			Tasual_ListView.AllColumns.Add(IconColumn);
			Tasual_ListView.Columns.AddRange(new ColumnHeader[] { IconColumn });

			CategoryColumn = new OLVColumn("Category", "Group");
			CategoryColumn.MinimumWidth = 100;
			CategoryColumn.IsVisible = false;
			CategoryColumn.IsEditable = true;
			CategoryColumn.DisplayIndex = 3;
			CategoryColumn.LastDisplayIndex = 3;
			CategoryColumn.GroupKeyGetter = delegate (object Input)
			{
				Task Task = (Task)Input;
				return TimeInfo.GetGroupStringFromTask(Task, Settings);
			};
			CategoryColumn.GroupKeyToTitleConverter = delegate (object Input)
			{
				return ((string)Input).Remove(0, 1);
			};
			CategoryColumn.TextAlign = HorizontalAlignment.Center;
			CategoryColumn.HeaderTextAlign = HorizontalAlignment.Center;
			Tasual_ListView.AllColumns.Add(CategoryColumn);
			Tasual_ListView.Columns.AddRange(new ColumnHeader[] { CategoryColumn });

			DueColumn = new OLVColumn("Due", "Time");
			DueColumn.MinimumWidth = 80;
			DueColumn.IsVisible = false;
			DueColumn.IsEditable = false;
			DueColumn.DisplayIndex = 4;
			DueColumn.LastDisplayIndex = 4;
			DueColumn.TextAlign = HorizontalAlignment.Center;
			DueColumn.HeaderTextAlign = HorizontalAlignment.Center;
			DueColumn.AspectToStringConverter = delegate (object Input)
			{
				TimeInfo TimeInfo = (TimeInfo)Input;
				return TimeInfo.GetDueStringFromTimeInfo(TimeInfo);
			};
			DueColumn.GroupKeyGetter = delegate (object Input)
			{
				Task Task = (Task)Input;
				return TimeInfo.GetDueIntFromTask(Task);
			};
			DueColumn.GroupKeyToTitleConverter = delegate (object Input)
			{
				int Key = (int)Input;
				return TimeInfo.GetDueStringFromInt(Key);
			};
			Tasual_ListView.AllColumns.Add(DueColumn);
			Tasual_ListView.Columns.AddRange(new ColumnHeader[] { DueColumn });

			TimeColumn = new OLVColumn("Time", "Time");
			TimeColumn.MinimumWidth = 130;
			TimeColumn.IsVisible = true;
			TimeColumn.IsEditable = false;
			TimeColumn.DisplayIndex = 5;
			TimeColumn.LastDisplayIndex = 5;
			TimeColumn.TextAlign = HorizontalAlignment.Center;
			TimeColumn.HeaderTextAlign = HorizontalAlignment.Center;
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
			Tasual_Create TaskForm = new Tasual_Create(this);
			TaskForm.ShowDialog(this);
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
				0,
				GroupName,
				"New task",
				new TimeInfo(),
				new Timer()
			);

			TaskArray.Add(Task);
			ArrayHandler.Save(ref TaskArray, Settings);
			Tasual_ListView.BuildList();
			Tasual_StatusLabel_UpdateCounts();
			Tasual_ListView.PossibleFinishCellEditing();
			Tasual_ListView.EnsureModelVisible(Task);
			Tasual_ListView.EditModel(Task);
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
			//Tasual_Array_Save_JSON();
			//Tasual_Array_Load_JSON();
			//Tasual_ListView.SetObjects(TaskArray);
			//Tasual_ListView.BuildList();
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
		private void Tasual_MenuStrip_Item_Delete_Click(object sender, EventArgs e)
		{
			TaskArray.Remove((Task)Tasual_MenuStrip_Item.Tag);
			ArrayHandler.Save(ref TaskArray, Settings);
			Tasual_ListView.BuildList();
			Tasual_StatusLabel_UpdateCounts();
		}

		private void Tasual_MenuStrip_Item_Edit_Click(object sender, EventArgs e)
		{
			Tasual_ListView.EditModel((Task)Tasual_MenuStrip_Item.Tag);
		}

		// Tasual_Notify: "Notify"
		private void Tasual_MenuStrip_Notify_Show_Click(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
			{
				this.WindowState = FormWindowState.Normal;
			}
			this.Activate();
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
					this.ShowInTaskbar = false;
				}
				else
				{
					this.ShowInTaskbar = true;
				}
			}
			else
			{
				this.ShowInTaskbar = true;
			}
		}

		// Notification Icon
		private void Tasual_Notify_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (this.WindowState == FormWindowState.Minimized)
				{
					this.WindowState = FormWindowState.Normal;
				}
				this.Activate();
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
			ArrayHandler.Save(ref TaskArray, Settings);
			Tasual_ListView.BuildList();
			Tasual_StatusLabel_UpdateCounts();
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
						this.BeginInvoke((MethodInvoker)delegate
						{
							Tasual_ListView.EditModel(Tasual_ListView.SelectedItem.RowObject);
						});
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
					Tasual_ListView_DoubleClick(e);
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

		private void Tasual_MenuStrip_Item_Edit_Quick_Click(object sender, EventArgs e)
		{

		}

		private void Tasual_MenuStrip_Item_Edit_Advanced_Click(object sender, EventArgs e)
		{

		}
	}
}
