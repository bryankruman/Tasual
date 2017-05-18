using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Reflection;

namespace Tasual
{
    public partial class Tasual_Main : Form
	{
        // ==============
        //  Declarations
        // ==============

		public List<TaskItem> TaskArray = new List<TaskItem>();
        public List<string> HiddenGroups = new List<string>();
        public List<string> HiddenGroupHasStub = new List<string>();

        StyleEnum Tasual_Setting_Style = StyleEnum.Custom;
        string Tasual_Setting_TextFile = "localdb.txt";

        ListViewHitTestInfo CalendarPopout = null;
        ListViewHitTestInfo Tasual_ListView_FirstClickInfo = null;
        bool Tasual_ListView_PreviouslySelected = false;

        public enum StyleEnum
        {
            Custom,
            Simple,
            Detailed
        }

        public enum ArgEnum
		{
			Type,
			Priority,
			Status,
			Group,
			Description,
			Created,
			Ending,
			Next,
			Count
		}

		public enum TypeEnum
		{
			TYPE_USER_SINGLE,
			TYPE_USER_RECURRING,
			TYPE_USER_DEBT_OWED,
			TYPE_USER_DEBT_LENT,
			TYPE_SYNDICATION_SINGLE,
			TYPE_SYNDICATION_RECURRING
		}

		public enum PrioEnum
		{
			PRIO_LOW,
			PRIO_MED,
			PRIO_HIGH
		}

		public enum StatusEnum
		{
			New,
			Complete,
            Toggle
		}

		public enum ProtocolEnum
		{
			PRO_TEXT//,
					//PRO_XML,
					//PRO_RTM,
					//PRO_TASUAL
		}


        // ===========================
        //  Misc/Supporting Functions
        // ===========================

        public void Tasual_PrintTaskToConsole(TaskItem TaskItem)
		{
			Console.WriteLine(
				"TaskItem: '{0}', '{1}', '{2}', '{3}', '{4}', ('{5}', '{6}', '{7}')",
				TaskItem.Type,
				TaskItem.Priority,
				TaskItem.Status,
				TaskItem.Group,
				TaskItem.Description,
				TaskItem.Time.Created,
				TaskItem.Time.Ending,
				TaskItem.Time.Next
			);
		}

        private void Tasual_StatusLabel_UpdateCounts()
        {
            int Complete = 0;
            int Total = 0;

            foreach (TaskItem Task in TaskArray)
            {
                if (Task == null) { break; }

                ++Total;
                if (Task.Status == (int)StatusEnum.Complete) { ++Complete; }
            }

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
            foreach (TaskItem Task in TaskArray)
            {
                if (Task == null) { break; }
                if (Task.Group == OldTaskGroup)
                {
                    Task.Group = NewTaskGroup;
                }
            }
        }

        public void Tasual_Array_CreateTask(
			ref List<TaskItem> Array,
			int Type,
			int Priority,
			int Status,
			string Group,
			string Description,
			TaskItem.TaskTime Time,
            bool Edit)
		{
			TaskItem NewTask = new TaskItem();

			NewTask.Type = Type;
			NewTask.Priority = Priority;
			NewTask.Status = Status;
			NewTask.Group = Group;
			NewTask.Description = Description;
			NewTask.Time = Time;

			Array.Add(NewTask);
			Tasual_Array_Save_Text(ref Array);

            // force hidden group to be unhidden
            if(HiddenGroups.Contains(NewTask.Group))
            {
                HiddenGroups.Remove(NewTask.Group);
                Tasual_ListView_PopulateFromArray(ref TaskArray);
            }

            ListViewItem Item = Tasual_ListView_CreateListViewItem(ref NewTask);

            Tasual_StatusLabel_UpdateCounts();
            Tasual_ListView_SizeColumns();

            if (Edit)
            {
                Tasual_ListView.LabelEdit = true;
                Item.BeginEdit();
            }
        }

		public void Tasual_Array_Save_Text(ref List<TaskItem> Array)
		{
			try
			{
				using (StreamWriter OutputFile = new StreamWriter(Tasual_Setting_TextFile))
				{
					foreach (TaskItem Task in Array)
					{
						string Line;
						Line = Task.Type.ToString();
						Line = Line + (char)29 + Task.Priority.ToString();
						Line = Line + (char)29 + Task.Status.ToString();
						Line = Line + (char)29 + Task.Group;
						Line = Line + (char)29 + Task.Description;
						Line = Line + (char)29 + Task.Time.Created.ToString();
						Line = Line + (char)29 + Task.Time.Ending.ToString();
						Line = Line + (char)29 + Task.Time.Next.ToString();
						OutputFile.WriteLine(Line);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void Tasual_Array_Load_Text(ref List<TaskItem> Array)
		{
			try
			{
				TaskArray.Clear();

				using (StreamReader InputFile = new StreamReader(Tasual_Setting_TextFile))
				{
					int counter = 0;
					string Line;

					while ((Line = InputFile.ReadLine()) != null)
					{
						TaskItem NewItem = new TaskItem();
						int argtype = 0;
						string[] segments = Line.Split((char)29);

						foreach (string token in segments)
						{
							// lets do something with this data now
							switch (argtype)
							{
								case (int)ArgEnum.Type:         { Int32.TryParse(token, out NewItem.Type); break; }
								case (int)ArgEnum.Priority:     { Int32.TryParse(token, out NewItem.Priority); break; }
								case (int)ArgEnum.Status:       { Int32.TryParse(token, out NewItem.Status); break; }
								case (int)ArgEnum.Group:        { NewItem.Group = token; break; }
								case (int)ArgEnum.Description:  { NewItem.Description = token; break; }
								case (int)ArgEnum.Created:        { Double.TryParse(token, out NewItem.Time.Created); break; }
								case (int)ArgEnum.Ending:          { Double.TryParse(token, out NewItem.Time.Ending); break; }
								case (int)ArgEnum.Next:         { Double.TryParse(token, out NewItem.Time.Next); break; }
								default:
									{
										Console.WriteLine("TOO MANY ARGUMENTS IN FILE !!!!!!");
										break;
									}
							}

							++argtype;
							//Console.WriteLine(token);
						}

						if (argtype == (int)ArgEnum.Count)
							TaskArray.Add(NewItem);

						//Console.WriteLine(line);
						counter++;
					}
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}


        // ====================
        //  ListView Functions
        // ====================

        private Color Tasual_ListView_ForeColor(int Status, bool Selected)
        {
            switch (Status)
            {
                case (int)StatusEnum.Complete:
                    {
                        if (Selected)
                        {
                            return Color.FromArgb(255, 189, 208, 230);
                        }
                        else
                        {
                            return Color.FromArgb(255, 189, 208, 230);
                        }
                    }
                case (int)StatusEnum.New:
                    {
                        if (Selected)
                        {
                            return Color.FromArgb(255, 36, 90, 150);
                        }
                        else
                        {
                            return Color.FromArgb(255, 36, 90, 150);
                        }
                    }
                default: return Color.FromArgb(255, 0, 0, 0);
            }
        }

        private Color Tasual_ListView_BackColor(int Status, bool Selected)
        {
            switch (Status)
            {
                case (int)StatusEnum.Complete:
                    {
                        if (Selected)
                        {
                            return Color.White;//FromArgb(255, 189, 208, 230);
                        }
                        else
                        {
                            return Color.White;
                        }
                    }
                case (int)StatusEnum.New:
                    {
                        if (Selected)
                        {
                            return Color.White;//FromArgb(255, 36, 90, 150);
                        }
                        else
                        {
                            return Color.White;
                        }
                    }
                default: return Color.FromArgb(255, 0, 0, 0);
            }
        }

        private void Tasual_ListView_ChangeStatus(ref ListViewItem Item, int Status)
        {
            TaskItem Task = (TaskItem)Item.Tag;

            switch (Status)
            {
                case (int)StatusEnum.Toggle:
                    {
                        if (Task.Status == (int)StatusEnum.Complete)
                        {
                            Status = (int)StatusEnum.New;
                            goto case (int)StatusEnum.New;
                        }
                        else if (Task.Status == (int)StatusEnum.New)
                        {
                            Status = (int)StatusEnum.Complete;
                            goto case (int)StatusEnum.Complete;
                        }
                        break;
                    }

                case (int)StatusEnum.Complete:
                    {
                        Item.ForeColor = Tasual_ListView_ForeColor((int)StatusEnum.Complete, Item.Selected);//Color.FromArgb(255, 189, 208, 230);
                        Item.ImageIndex = 0;
                        break;
                    }

                case (int)StatusEnum.New:
                    {
                        Item.ForeColor = Tasual_ListView_ForeColor((int)StatusEnum.New, Item.Selected);//Color.FromArgb(255, 36, 90, 150);
                        Item.ImageIndex = 1;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Tasual_ListView_ChangeStatus(): Invalid Status!");
                        return;
                    }
            }

            Task.Status = Status;
        }

        private void Tasual_ListView_SizeColumns()
        {
            Tasual_ListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.None);
            Tasual_ListView.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            Tasual_ListView.Columns[1].Width = Math.Max(100, Tasual_ListView.Columns[1].Width);
            Tasual_ListView.Columns[0].Width = Math.Max(100, (Tasual_ListView.ClientSize.Width - Tasual_ListView.Columns[1].Width));
        }

        private void Tasual_ListView_ClearAll()
        {
            HiddenGroupHasStub.Clear();
            Tasual_ListView.Columns.Clear();
            Tasual_ListView.Groups.Clear();
            Tasual_ListView.Items.Clear();
            Tasual_ListView.Update();
            Tasual_ListView.Refresh();
        }

        private ListViewGroup Tasual_ListView_FindGroup(string GroupName)
        {
            ListViewGroup Found = null;
            foreach (ListViewGroup Group in Tasual_ListView.Groups)
            {
                if (Group.Name == GroupName)
                {
                    Found = Group;
                    break;
                }
            }
            return Found;
        }

        private void Tasual_ListView_AssignGroup(string TaskGroup, ref ListViewItem Item)
        {
            ListViewGroup Found = null;
            foreach (ListViewGroup Group in Tasual_ListView.Groups)
            {
                if (Group.Name == TaskGroup)
                {
                    Found = Group;
                    break;
                }
            }
            if (Found == null)
            {
                ListViewGroup NewGroup = new ListViewGroup();
                NewGroup.Name = TaskGroup;
                NewGroup.Header = NewGroup.Name;
                Tasual_ListView.Groups.Add(NewGroup);
                Console.WriteLine("Group: {0} - {1}", Tasual_ListView.Groups.IndexOf(NewGroup), NewGroup.Name);
                Item.Group = NewGroup;
            }
            else
            {
                Item.Group = Found;
            }
        }

        public ListViewItem Tasual_ListView_CreateListViewItem(ref TaskItem Task)
        {
            string[] Item_S;

            // determine which columns we're going to have
            switch (Tasual_Setting_Style)
            {
                // "CUSTOM" STYLE:
                // - groups: overdue at top, normal group listings below, completed at bottom
                // - columns: Description, Time
                case StyleEnum.Custom:
                    {
                        Item_S = new string[2];
                        Item_S[0] = Task.Description;
                        DateTime CreationTime = DateTimeOffset.FromUnixTimeSeconds((long)Task.Time.Created).DateTime.ToLocalTime();
                        Item_S[1] = DateTimeExtensions.ElapsedTime(CreationTime);
                        break;
                    }

                // "SIMPLE" STYLE:
                // - groups: overdue at top, today, tomorrow, this week, next week, future, completed at bottom
                // - columns: Description, Time
                case StyleEnum.Simple:
                    {
                        Item_S = new string[2];
                        Item_S[0] = Task.Description;
                        Item_S[1] = Task.Group.ToString();
                        break;
                    }

                // "DETAILED" STYLE:
                // - groups: overdue at top, today, tomorrow, this week, next week, future, completed at bottom
                // - columns: Description, Category, Time
                case StyleEnum.Detailed:
                    {
                        Item_S = new string[3];
                        Item_S[0] = Task.Description;
                        Item_S[1] = Task.Group.ToString();
                        DateTime CreationTime = DateTimeOffset.FromUnixTimeSeconds((long)Task.Time.Created).DateTime.ToLocalTime();
                        Item_S[2] = DateTimeExtensions.ElapsedTime(CreationTime);
                        break;
                    }

                default:
                    {
                        throw new Exception("Tasual_ListView_CreateListViewItem(): Invalid style setting!");
                    }
            }

            ListViewItem Item = new ListViewItem(Item_S);
            Item.Tag = Task;

            Tasual_ListView_AssignGroup(Task.Group.ToString(), ref Item);
            Tasual_ListView_ChangeStatus(ref Item, Task.Status);
            Tasual_ListView.Items.Add(Item);

            return Item;
        }

        public void Tasual_ListView_PopulateFromArray(ref List<TaskItem> TaskArray)
        {
            Tasual_ListView_ClearAll();

            switch (Tasual_Setting_Style)
            {
                // See Tasual_ListView_CreateListViewItem() for details
                case StyleEnum.Custom:
                    {
                        Tasual_ListView.Columns.Add("Description");
                        Tasual_ListView.Columns.Add("Time");
                        break;
                    }

                case StyleEnum.Simple:
                    {
                        Tasual_ListView.Columns.Add("Description");
                        Tasual_ListView.Columns.Add("Time");
                        break;
                    }

                case StyleEnum.Detailed:
                    {
                        Tasual_ListView.Columns.Add("Description");
                        Tasual_ListView.Columns.Add("Category");
                        Tasual_ListView.Columns.Add("Time");
                        break;
                    }

                default:
                    {
                        throw new Exception("Tasual_ListView_PopulateFromArray(): Invalid style setting!");
                    }
            }

            foreach (TaskItem Task in TaskArray)
            {
                if (Task == null) { break; }

                if (HiddenGroups.Contains(Task.Group))
                {
                    Console.WriteLine("Found a hidden group! {0}", Task.Group);
                    if (!HiddenGroupHasStub.Contains(Task.Group))
                    {
                        HiddenGroupHasStub.Add(Task.Group);

                        // create "stub" which sits underneath a hidden group name and states how many items are in it/that they're hidden
                        // we want to make a fake task as we don't want it to actually go into the array
                        // only needs: Description, Time, Group and Status
                        TaskItem Stub = new TaskItem();
                        Stub.Status = (int)StatusEnum.New;
                        Stub.Group = "Testing";
                        Stub.Description = "This item has been hidden";
                        TaskItem.TaskTime Time = new TaskItem.TaskTime();
                        var CurrentTimeOffset = new DateTimeOffset(DateTime.Now.ToLocalTime());
                        Time.Created = CurrentTimeOffset.ToUnixTimeSeconds();
                        Time.Ending = 2;
                        Time.Next = 3;
                        Stub.Time = Time;
                        Tasual_ListView_CreateListViewItem(ref Stub);
                    }
                }
                else
                {
                    TaskItem Referral = Task;
                    Tasual_ListView_CreateListViewItem(ref Referral);
                }
            }

            Tasual_StatusLabel_UpdateCounts();
            Tasual_ListView_SizeColumns();
        }


        // ================
        //  Event Handlers
        // ================

        // MenuStrips
        private void Tasual_MenuStrip_Create_Advanced_Click(object sender, EventArgs e)
        {
            Tasual_Create_Task TaskForm = new Tasual_Create_Task();
            TaskForm.Show(this);
        }

        private void Tasual_MenuStrip_Create_Quick_Click(object sender, EventArgs e)
        {
            TaskItem.TaskTime Time = new TaskItem.TaskTime();
            var CurrentTimeOffset = new DateTimeOffset(DateTime.Now.ToLocalTime());
            Time.Created = CurrentTimeOffset.ToUnixTimeSeconds();
            Time.Ending = 2;
            Time.Next = 3;
            Tasual_Array_CreateTask(ref TaskArray, 0, 0, 0, "Testing", "New task", Time, true);
        }

        private void Tasual_MenuStrip_Edit_Click(object sender, EventArgs e)
        {
            Tasual_ListView.LabelEdit = true;
            Tasual_ListView.FocusedItem.BeginEdit();
        }

        private void Tasual_MenuStrip_Settings_AlwaysOnTop_Click(object sender, EventArgs e)
        {
            Tasual_Array_ReAssignGroup("Testing", "");
            Tasual_Array_Save_Text(ref TaskArray);
            Tasual_Array_Load_Text(ref TaskArray);
            Tasual_ListView_PopulateFromArray(ref TaskArray);
        }

        private void Tasual_MenuStrip_Sources_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_StatusLabel_MenuStrip_Clear_Click(object sender, EventArgs e)
        {
            Tasual_Confirm_Clear ConfirmForm = new Tasual_Confirm_Clear(this);
            ConfirmForm.ShowDialog(this);
        }

        // Notification Icon
        private void Tasual_Notify_Click(object sender, EventArgs e)
        {
            ReturnFormInstance().Activate();
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

        // Main Form
        private void Tasual_Main_Resize(object sender, EventArgs e)
        {
            Invalidate();
            Tasual_ListView_SizeColumns();
        }

        // ListView
        private void Tasual_ListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            //Tasual_SizeColumns();
            e.NewWidth = this.Tasual_ListView.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void Tasual_ListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            Tasual_ListView.LabelEdit = false;

            ListViewItem Item = Tasual_ListView.Items[e.Item];

            if (Item != null)
            {
                Item.Selected = false;

                this.BeginInvoke((MethodInvoker)delegate
                {
                    TaskItem Task = (TaskItem)Item.Tag;
                    if ((Task != null) && (Item != null))
                    {
                        Task.Description = Item.Text.ToString();
                        Tasual_Array_Save_Text(ref TaskArray);
                    }
                });
            }
        }

        private void Tasual_ListView_SingleClick(MouseEventArgs e)
        {
            if (Tasual_ListView_PreviouslySelected == true)
            {
                Tasual_ListView.LabelEdit = true;
                Tasual_ListView.FocusedItem.BeginEdit();
            }
            else
            {
                // do nothing
            }
        }

        private void Tasual_ListView_DoubleClick(MouseEventArgs e)
        {
            ListViewHitTestInfo SecondClickInfo = Tasual_ListView.HitTest(e.X, e.Y);

            if ((Tasual_ListView_FirstClickInfo.Item != null) && (SecondClickInfo.Item != null))
            {
                if (Tasual_ListView_FirstClickInfo.Item == SecondClickInfo.Item)
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        Tasual_ListView.LabelEdit = true;
                        Tasual_ListView.FocusedItem.BeginEdit();
                    });
                }
            }
        }

        private void Tasual_Timer_ListViewClick_Tick(object sender, EventArgs e)
        {
            if ((Control.MouseButtons & MouseButtons.Left) == 0)
            {
                Tasual_ListView_SingleClick((MouseEventArgs)Tasual_Timer_ListViewClick.Tag);
            }
            Tasual_ListView_FirstClickInfo = null;
            Tasual_ListView_PreviouslySelected = false;
            Tasual_Timer_ListViewClick.Stop();
        }

        private void Tasual_ListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo Info = Tasual_ListView.HitTest(e.X, e.Y);

            // todo: check location to see if they clicked on the icon/checkbox
            // if so, immediately toggle state of task

            if (Info.Item != null)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        {
                            if (Tasual_Timer_ListViewClick.Enabled) // second click
                            {
                                Tasual_ListView_DoubleClick(e);
                                Tasual_ListView_PreviouslySelected = false;
                                Tasual_ListView_FirstClickInfo = null;
                                Tasual_Timer_ListViewClick.Stop();
                            }
                            else // first click
                            {
                                if (e.X <= 20) // clicked checkbox area
                                {
                                    ListViewItem SelectedItem = Info.Item;
                                    Tasual_ListView_ChangeStatus(ref SelectedItem, (int)StatusEnum.Toggle);
                                    Tasual_StatusLabel_UpdateCounts();
                                    Tasual_Array_Save_Text(ref TaskArray);
                                }
                                else // clicked item area
                                {
                                    int ColumnIndex = Info.Item.SubItems.IndexOf(Info.SubItem);
                                    //Console.WriteLine("Column Index: {0}", ColumnIndex);

                                    if (ColumnIndex == 0) // clicked item description
                                    {
                                        Tasual_Timer_ListViewClick.Start();

                                        if (Info.Item.Selected)
                                        {
                                            Tasual_ListView_PreviouslySelected = true;
                                        }

                                        Tasual_ListView_FirstClickInfo = Info;
                                        Tasual_Timer_ListViewClick.Tag = e;
                                    }
                                    else // clicked subitem
                                    {

                                        if (Info.SubItem != null)
                                        {
                                            if (CalendarPopout == null)
                                            {
                                                CalendarPopout = Info;
                                            }
                                            else { CalendarPopout = null; }

                                            /*
                                            MonthCalendar TimeSelector = new MonthCalendar();

                                            Rectangle Bounds = Info.SubItem.Bounds;
                                            TimeSelector.Location = new Point(Cursor.Position.X, Cursor.Position.Y); //new Point(0, Bounds.Bottom);
                                            TimeSelector.Size = new Size(300, 300);

                                            this.Controls.Add(TimeSelector);
                                            TimeSelector.BringToFront();
                                            */
                                        }
                                    }
                                }
                            }
                            break;
                        }

                    case MouseButtons.Right:
                        {
                            Tasual_MenuStrip_Item.Show(Cursor.Position.X, Cursor.Position.Y);
                            break;
                        }

                    case MouseButtons.Middle:
                        {
                            // todo: perhaps duplicate item if middle clicked?
                            break;
                        }

                    default:
                        {
                            // can this even happen?
                            Console.WriteLine("unknown button");
                            break;
                        }
                }
            }
            else if (Info != null)
            {
                
                Console.WriteLine("What got clicked?");
            }
        }

        private void Tasual_ListView_MouseUp(object sender, MouseEventArgs e)
        {
            if(CalendarPopout != null)
            {
                Tasual_CalendarPopout Calendar = new Tasual_CalendarPopout();

                Rectangle Bounds = CalendarPopout.SubItem.Bounds;
                Calendar.Location = PointToScreen(new Point(Bounds.Left, Bounds.Bottom + Bounds.Height + 5));
                Calendar.Show(this);

                CalendarPopout = null;
            }
        }

        private void Tasual_ListView_GroupHeaderClick(object sender, MouseEventArgs e)
        {
            /* for now, lets just not worry about group header clicks... for now.
            switch(e.Button)
            {
                case MouseButtons.Left:
                    {
                        //Tasual_ListView.FindNearestItem(SearchDirectionHint.Down, Cursor.Position.X, Cursor.Position.Y).Group.Name,
                        ListViewHitTestInfo Info = Tasual_ListView.HitTest(e.X, e.Y);

                        foreach (ListViewGroup Group in Tasual_ListView.Groups)
                        {
                            //Console.WriteLine("scanning through groups: {0}", Group.);
                        }

                        //Console.WriteLine("Header: {0} - {1} - {2}", Info.Item.Name, e.Button, this.Name.ToString());
                        break;
                    }

                default:
                case MouseButtons.Right:
                    {
                        Tasual_MenuStrip_Group.Show(Cursor.Position.X, Cursor.Position.Y);
                        break;
                    }
            }
            */
        }

#if false
        ListViewItem UnderlinedItem;
        private void Tasual_ListView_MouseMove(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo Info = Tasual_ListView.HitTest(e.X, e.Y);

            if (Info.Item != null)
            {
                Tasual_ListView.Cursor = Cursors.Hand;
                if (Info.Item != UnderlinedItem)
                {
                    if (UnderlinedItem != null)
                        { UnderlinedItem.Font = new Font(UnderlinedItem.Font.Name, 9); }

                    Info.Item.Font = new Font(Info.Item.Font.Name, 9, FontStyle.Underline);
                    UnderlinedItem = Info.Item;
                }
            }
            else
            {
                Tasual_ListView.Cursor = Cursors.Default;
            }
        }
#endif

        private void Tasual_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tasual_ListView.Items.Cast<ListViewItem>()
                .ToList().ForEach(Item =>
                {
                    TaskItem Task = (TaskItem)Item.Tag;
                    Item.BackColor = Tasual_ListView_BackColor(Task.Status, false);
                    Item.ForeColor = Tasual_ListView_ForeColor(Task.Status, false);
                });
            Tasual_ListView.SelectedItems.Cast<ListViewItem>()
                .ToList().ForEach(Item =>
                {
                    TaskItem Task = (TaskItem)Item.Tag;
                    Item.BackColor = Tasual_ListView_BackColor(Task.Status, true);
                    Item.ForeColor = Tasual_ListView_ForeColor(Task.Status, true);
                });
        }

        private void Tasual_ListView_DragDrop(object sender, DragEventArgs e)
        {
            Point Loc = Tasual_ListView.PointToClient(new Point(e.X, e.Y));
            ListViewItem DisplacedItem = Tasual_ListView.GetItemAt(Loc.X, Loc.Y);

            if ((DisplacedItem == null) || (e.Data.GetDataPresent(typeof(ListViewItem)) == false))
                return;

            ListViewItem DraggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

            Console.WriteLine(
                "Dragged from {0} to {1}: {2} > {3}",
                DraggedItem.Index,
                DisplacedItem.Index,
                DraggedItem.Text.ToString(),
                DisplacedItem.Text.ToString()
            );

            //int OldIndex

            if (DraggedItem != DisplacedItem)
            {
                Rectangle Bounds = DisplacedItem.GetBounds(ItemBoundsPortion.Entire);

            }
        }

        private void Tasual_ListView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Tasual_ListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Tasual_ListView.DoDragDrop(e.Item, DragDropEffects.Copy);
        }


        // ================
        //  Core Functions
        // ================

        public Tasual_Main()
		{
			InitializeComponent();

            ControlExtensions.DoubleBuffered(Tasual_ListView, true);
            Tasual_Timer_ListViewClick.Interval = SystemInformation.DoubleClickTime;

            SubNativeWindow ListViewHandleClass = new SubNativeWindow();
            ListViewHandleClass.AssignHandle(Tasual_ListView.Handle);

            TasualListView.GroupHeaderClick += new MouseEventHandler(Tasual_ListView_GroupHeaderClick);
        }

		private void Tasual_Main_Load(object sender, EventArgs e)
		{
			// load task array
			Tasual_Array_Load_Text(ref TaskArray);

            //HiddenGroups.Add("Testing");

            // load tasks into Tasual_ListView
            Tasual_ListView_PopulateFromArray(ref TaskArray);
		}

        public static Tasual_Main ReturnFormInstance()
        {
            return Application.OpenForms[0] as Tasual_Main;
        }
    }

    public class TaskItem
	{
		public struct TaskTime
		{
			public double Created;
			public double Ending;
			public double Next;
		}

		public int Type;
		public int Priority;
		public int Status;
		public string Group;
		public string Description;
		public TaskTime Time;

        //public ListViewGroup GroupTag;


		// constructors
		//taskitem_c();
		//taskitem_c(int, int, int, int, string, xyztime);

		// deconstructor
		//~taskitem_c();
	}

    public static class DateTimeExtensions
    {
        public static string ElapsedTime(DateTime dtEvent)
        {
            TimeSpan TS = DateTime.Now - dtEvent;
            int intYears = DateTime.Now.Year - dtEvent.Year;
            int intMonths = DateTime.Now.Month - dtEvent.Month;
            int intDays = DateTime.Now.Day - dtEvent.Day;
            int intHours = DateTime.Now.Hour - dtEvent.Hour;
            int intMinutes = DateTime.Now.Minute - dtEvent.Minute;
            int intSeconds = DateTime.Now.Second - dtEvent.Second;
            if (intYears > 0) return String.Format("{0} {1} ago", intYears, (intYears == 1) ? "year" : "years");
            else if (intMonths > 0) return String.Format("{0} {1} ago", intMonths, (intMonths == 1) ? "month" : "months");
            else if (intDays > 0) return String.Format("{0} {1} ago", intDays, (intDays == 1) ? "day" : "days");
            else if (intHours > 0) return String.Format("{0} {1} ago", intHours, (intHours == 1) ? "hour" : "hours");
            else if (intMinutes > 0) return String.Format("{0} {1} ago", intMinutes, (intMinutes == 1) ? "minute" : "minutes");
            else if (intSeconds > 1) return String.Format("{0} {1} ago", intSeconds, (intSeconds == 1) ? "second" : "seconds");
            else if (intSeconds >= 0) return "Just now";
            else
            {
                return String.Format("{0} {1}", dtEvent.ToShortDateString(), dtEvent.ToShortTimeString());
            }
        }
    }

    public static class ControlExtensions
    {
        public static void DoubleBuffered(this Control Item, bool Enable)
        {
            var PropertyInfo = Item.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            PropertyInfo.SetValue(Item, Enable, null);
        }
    }

    public class SubNativeWindow : NativeWindow
    {
        private const int LVM_HITTEST = 0x1000 + 18;
        private const int LVHT_EX_GROUP_HEADER = 0x10000000;

        [StructLayout(LayoutKind.Sequential)]
        private struct LVHITTESTINFO
        {
            public int pt_x;
            public int pt_y;
            public int flags;
            public int iItem;
            public int iSubItem;
            public int iGroup;
        }

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref LVHITTESTINFO ht);

        public static int LowWord(int word) { return word & 0xFFFF; }
        public static int HighWord(int word) { return word >> 16; }
        public static int GetXLParam(int lparam) { return LowWord(lparam); }
        public static int GetYLParam(int lparam) { return HighWord(lparam); }

        protected override void WndProc(ref Message m)
        {
            switch(m.Msg)
            {
                case 0x201:
                case 0x204:
                    {
                        LVHITTESTINFO ht = new LVHITTESTINFO() { pt_x = GetXLParam(m.LParam.ToInt32()), pt_y = GetYLParam(m.LParam.ToInt32()) };
                        int value = SendMessage(Handle, LVM_HITTEST, -1, ref ht);
                        if ((value != -1) && ((ht.flags & LVHT_EX_GROUP_HEADER) != 0))
                        {
                            //Console.WriteLine("LVHITTESTINFO: {0} {1} {2} {3}", ht.flags, ht.iItem, ht.iSubItem, ht.iGroup);
                            TasualListView.OnGroupHeaderClick(new MouseEventArgs(Control.MouseButtons, ht.iGroup, 0, 0, 0));
                        }
                        else
                        {
                            base.WndProc(ref m);
                        }
                        break;
                    }

                default:
                case 0x203:
                    {
                        base.WndProc(ref m);
                        break;
                    }
            }
        }
    }

    public class TasualListView : ListView
    {
        public static MouseEventHandler GroupHeaderClick;
        public static void OnGroupHeaderClick(MouseEventArgs e)
        {
            GroupHeaderClick?.Invoke(null, e);
        }
    }
}
