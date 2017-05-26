using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
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


		// ================
		//  Initialization
		// ================

		public Tasual_Main()
		{
			InitializeComponent();

			Tasual_Timer_ListViewClick.Interval = SystemInformation.DoubleClickTime;
		}


		// =============================
		//  Common/Supporting Functions
		// =============================

		public void Tasual_ClearAll()
		{
			TaskArray.Clear();
			Tasual_Array_Save();
			Tasual_Array_Load();
			Tasual_ListView.BuildList();
		}

		public static Tasual_Main ReturnFormInstance()
		{
			return Application.OpenForms[0] as Tasual_Main;
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


		// =================
		//  Array Functions
		// =================

		private void Tasual_Array_ReAssignGroup(string OldTaskGroup, string NewTaskGroup)
		{
			foreach (Task Task in TaskArray)
			{
				if (Task == null) { break; }
				if (Task.Group == OldTaskGroup)
				{
					Task.Group = NewTaskGroup;
				}
			}
		}

		public void Tasual_Array_Save()
		{
			switch (Settings.Protocol)
			{
				case Setting.Protocols.Text: Tasual_Array_Save_Text(); break;
				case Setting.Protocols.JSON: Tasual_Array_Save_JSON(); break;
			}
		}

		public void Tasual_Array_Save_JSON()
		{
			try
			{
				string Output = JsonConvert.SerializeObject(TaskArray, Formatting.Indented);
				//Console.WriteLine("{0}", Output);

				using (FileStream OutputFile = File.Open("local.json", FileMode.Create))
				using (StreamWriter OutputStream = new StreamWriter(OutputFile))
				using (JsonWriter OutputJson = new JsonTextWriter(OutputStream))
				{
					OutputJson.Formatting = Formatting.Indented;

					JsonSerializer Serializer = new JsonSerializer();
					Serializer.Serialize(OutputJson, TaskArray);
				}

			}
			catch (Exception e)
			{
				Console.WriteLine("Tasual_Array_Save_JSON(): {0}\nTrace: {1}", e.Message, e.StackTrace);
			}
		}

		public void Tasual_Array_Save_Text()
		{
			Console.WriteLine("Tasual_Array_Save_Text();");
			try
			{
				using (StreamWriter OutputFile = new StreamWriter(Settings.TextFile))
				{
					foreach (Task Task in TaskArray)
					{
						string Line;
						Line = Task.Checked.ToString();
						Line = Line + (char)29 + Task.Priority.ToString();
						Line = Line + (char)29 + Task.Group;
						Line = Line + (char)29 + Task.Description;

						DateTimeOffset TimeOffset;
						TimeOffset = new DateTimeOffset(Task.Time.Start.ToLocalTime());
						Line = Line + (char)29 + TimeOffset.ToUnixTimeSeconds();

						TimeOffset = new DateTimeOffset(Task.Time.End.ToLocalTime());
						Line = Line + (char)29 + TimeOffset.ToUnixTimeSeconds();

						TimeOffset = new DateTimeOffset(Task.Time.Next.ToLocalTime());
						Line = Line + (char)29 + TimeOffset.ToUnixTimeSeconds();
						OutputFile.WriteLine(Line);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Tasual_Array_Save_Text(): {0}\nTrace: {1}", e.Message, e.StackTrace);
			}
		}

		public void Tasual_Array_Load()
		{
			switch (Settings.Protocol)
			{
				case Setting.Protocols.Text: Tasual_Array_Load_Text(); break;
				case Setting.Protocols.JSON: Tasual_Array_Load_JSON(); break;
			}
		}

		public void Tasual_Array_Load_JSON()
		{
			try
			{
				TaskArray.Clear();

				using (StreamReader InputFile = File.OpenText("local.json"))
				using (JsonReader InputJson = new JsonTextReader(InputFile))
				{
					JsonSerializer Serializer = new JsonSerializer();
					TaskArray = (List<Task>)Serializer.Deserialize(InputJson, typeof(List<Task>));
					Tasual_ListView.SetObjects(TaskArray);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Tasual_Array_Load_JSON(): {0}\nTrace: {1}", e.Message, e.StackTrace);
			}
		}

		public void Tasual_Array_Load_Text()
		{
			Console.WriteLine("Tasual_Array_Load_Text();");
			try
			{
				TaskArray.Clear();

				using (StreamReader InputFile = new StreamReader(Settings.TextFile))
				{
					int Counter = 0;
					string Line;

					while ((Line = InputFile.ReadLine()) != null)
					{
						Task NewItem = new Task();
						int ArgType = 0;
						string[] Segments = Line.Split((char)29);
						double UnixTime;

						foreach (string Token in Segments)
						{
							// lets do something with this data now
							int Temp = 0;
							bool TempBool = false;

							switch (ArgType)
							{
								case (int)Task.Arguments.Checked: { Boolean.TryParse(Token, out TempBool); NewItem.Checked = TempBool; break; }
								case (int)Task.Arguments.Priority: { Int32.TryParse(Token, out Temp); NewItem.Priority = Temp; break; }
								case (int)Task.Arguments.Group: { NewItem.Group = Token; break; }
								case (int)Task.Arguments.Description: { NewItem.Description = Token; break; }
								case (int)Task.Arguments.Created:
									{
										Double.TryParse(Token, out UnixTime);
										NewItem.Time.Start = 
											DateTimeOffset.FromUnixTimeSeconds((long)UnixTime).DateTime.ToLocalTime();
										break;
									}
								case (int)Task.Arguments.Ending:
									{
										Double.TryParse(Token, out UnixTime);
										NewItem.Time.End = 
											DateTimeOffset.FromUnixTimeSeconds((long)UnixTime).DateTime.ToLocalTime();
										break;
									}
								case (int)Task.Arguments.Next:
									{
										Double.TryParse(Token, out UnixTime);
										NewItem.Time.Next = 
											DateTimeOffset.FromUnixTimeSeconds((long)UnixTime).DateTime.ToLocalTime();
										break;
									}
								default:
									{
										Console.WriteLine("Too many arguments in file!");
										break;
									}
							}

							++ArgType;
						}

						if (ArgType == (int)Task.Arguments.Count)
						{
							TaskArray.Add(NewItem);
						}

						Counter++;
					}
				}

			}
			catch (Exception e)
			{
				Console.WriteLine("Tasual_Array_Load_Text(): {0}\nTrace: {1}", e.Message, e.StackTrace);
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
			Tasual_ListView.ShowGroups = true;
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

		private void Tasual_ListView_AddColumns()
		{
			OLVColumn DescriptionColumn = new OLVColumn("Description", "Description");
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

			OLVColumn IconColumn = new OLVColumn("Icons", "Icons");
			IconColumn.Renderer = new ImageRenderer();
			IconColumn.AspectGetter = delegate (object Input)
			{
				Task Task = (Task)Input;
				int[] Images = new int[3] { -1, -1, -1 };
				int CheckedOffset = 0;
				if (Task.Checked) { CheckedOffset = 3; }

				if (Task.Location != null) { Images[0] = 1 + CheckedOffset; }
				if (Task.Link != null) { Images[1] = 2 + CheckedOffset; }
				if (Task.Notes != null) { Images[2] = 3 + CheckedOffset; }

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

			OLVColumn CategoryColumn = new OLVColumn("Category", "Group");
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

			OLVColumn DueColumn = new OLVColumn("Due", "Time");
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

			OLVColumn TimeColumn = new OLVColumn("Time", "Time");
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
				return TimeInfo.FormatTime(Time.Start.ToLocalTime(), TimeInfo.TimeFormat.Short);
			};
			Tasual_ListView.AllColumns.Add(TimeColumn);
			Tasual_ListView.Columns.AddRange(new ColumnHeader[] { TimeColumn });

			Tasual_ListView.AlwaysGroupByColumn = CategoryColumn; //CategoryColumn DueColumn
			Tasual_ListView.PrimarySortColumn = CategoryColumn;// CategoryColumn DueColumn
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

		private void Tasual_MenuStrip_Create_Quick_Click(object sender, EventArgs e)
		{
			// TODO: Try to find the "first" group first, if nothing is found then just use the default group of "Tasks"
			//if (Tasual_ListView.OLVGroups.FirstOrDefault() != null)
			//{
			//    GroupName = Tasual_ListView.OLVGroups.FirstOrDefault().Name;
			//}

			Task Task = new Task(
				false,
				0,
				0,
				"Tasks",
				"New task",
				new TimeInfo(),
				new Timer()
			);

			TaskArray.Add(Task);
			Tasual_Array_Save();
			Tasual_ListView.BuildList();
			Tasual_StatusLabel_UpdateCounts();
			Tasual_ListView.EditModel(Task);
		}

		// Tasual_Main: "Edit"
		private void Tasual_MenuStrip_Edit_Click(object sender, EventArgs e)
		{
			if (Tasual_ListView.SelectedItem != null)
			{
				Tasual_ListView.EditModel(Tasual_ListView.SelectedItem.RowObject);
			}
		}

		// Tasual_Main: "Settings"
		private void Tasual_MenuStrip_Settings_Click(object sender, EventArgs e)
		{
			Tasual_Settings SettingsForm = new Tasual_Settings();
			SettingsForm.ShowDialog(this);
		}

		// Tasual_Main: "Sources"
		private void Tasual_MenuStrip_Sources_Click(object sender, EventArgs e)
		{
			Tasual_Array_Save_JSON();
			Tasual_Array_Load_JSON();
			Tasual_ListView.SetObjects(TaskArray);
			Tasual_ListView.BuildList();
		}

		// Tasual_ListView: "Group"
		private void Tasual_MenuStrip_Group_Delete_Click(object sender, EventArgs e)
		{
			OLVGroup Group = (OLVGroup)Tasual_MenuStrip_Group.Tag;
			List<Task> RemovalList = new List<Task>();

			foreach (Task Task in TaskArray)
			{
				if (TimeInfo.GetGroupStringFromTask(Task, Settings) == Group.Name)
				{
					RemovalList.Add(Task);
				}
			}

			foreach (Task RemoveTask in RemovalList)
			{
				TaskArray.Remove(RemoveTask);
			}

			Tasual_Array_Save();
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
			Tasual_Array_ReAssignGroup(Group.Name, Item.Text);
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
		private void Tasual_MenuStrip_Icon_Notes_Edit_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			Tasual_Notes NotesForm = new Tasual_Notes(this, TaskArray.IndexOf(Task));
			NotesForm.ShowDialog(this);
		}

		private void Tasual_MenuStrip_Icon_Opening(object sender, CancelEventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;

			if (Task.Location != null)
			{
				Tasual_MenuStrip_Icon_AddLocation.Visible = false;
				Tasual_MenuStrip_Icon_Location.Visible = true;
			}
			else
			{
				Tasual_MenuStrip_Icon_AddLocation.Visible = true;
				Tasual_MenuStrip_Icon_Location.Visible = false;
			}
			if (Task.Link != null)
			{
				Tasual_MenuStrip_Icon_AddLink.Visible = false;
				Tasual_MenuStrip_Icon_Link.Visible = true;
			}
			else
			{
				Tasual_MenuStrip_Icon_AddLink.Visible = true;
				Tasual_MenuStrip_Icon_Link.Visible = false;
			}
			if (Task.Notes != null)
			{
				Tasual_MenuStrip_Icon_AddNotes.Visible = false;
				Tasual_MenuStrip_Icon_Notes.Visible = true;
			}
			else
			{
				Tasual_MenuStrip_Icon_AddNotes.Visible = true;
				Tasual_MenuStrip_Icon_Notes.Visible = false;
			}
		}

		private void Tasual_MenuStrip_Icon_AddNotes_Click(object sender, EventArgs e)
		{
			Task Task = (Task)Tasual_MenuStrip_Icon.Tag;
			Tasual_Notes NotesForm = new Tasual_Notes(this, TaskArray.IndexOf(Task));
			NotesForm.ShowDialog(this);
		}

		// Tasual_ListView: "Item"
		private void Tasual_MenuStrip_Item_Delete_Click(object sender, EventArgs e)
		{
			TaskArray.Remove((Task)Tasual_MenuStrip_Item.Tag);
			Tasual_Array_Save();
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
			BringToFront();
		}

		private void Tasual_MenuStrip_Notify_Settings_Click(object sender, EventArgs e)
		{
			Tasual_Settings SettingsForm = new Tasual_Settings();
			SettingsForm.ShowDialog(ReturnFormInstance());
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
		private void Tasual_Main_Load(object sender, EventArgs e)
		{
			// TODO: Load settings from json file
			Settings.AlwaysOnTop = false; // currently unused
			Settings.PromptClear = true; // currently unused
			Settings.PromptDelete = true; // currently unused
			Settings.Protocol = Setting.Protocols.JSON;
			Settings.TextFile = "localdb.txt";
			Settings.AlwaysShowCompletedGroup = true;
			Settings.AlwaysShowOverdueGroup = true;
			Settings.AlwaysShowTodayGroup = true;
			Settings.ShowItemCounts = true;

			// load task array
			Tasual_Array_Load();

			// set up objectlistview
			Tasual_ListView_Setup();
			Tasual_ListView_AddColumns();
			Tasual_ListView.SetObjects(TaskArray);
			Tasual_ListView.RebuildColumns();
			//Tasual_ListView.AutoResizeColumns();

			// update status label to reflect item counts
			Tasual_StatusLabel_UpdateCounts();
		}

		private void Tasual_Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			Tasual_Notify.Dispose();
		}

		// Notification Icon
		private void Tasual_Notify_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReturnFormInstance().Activate();
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
			Tasual_Array_Save();
		}

		private void Tasual_ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			Tasual_Array_Save();
			Tasual_ListView.BuildList();
			Tasual_StatusLabel_UpdateCounts();
		}

		private void Tasual_ListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Tasual_ListView.SelectedItem != null)
			{
				Tasual_MenuStrip_Edit.Enabled = true;
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
				Tasual_Popout_Calendar Calendar = new Tasual_Popout_Calendar();

				Rectangle Bounds = CalendarPopout.SubItem.Bounds;
				Calendar.Location = PointToScreen(new Point(Bounds.Left, Bounds.Bottom + Bounds.Height + 5));
				Calendar.Show(this);

				CalendarPopout = null;
			}
		}
	}
}
