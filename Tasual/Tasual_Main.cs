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
using System.Text.RegularExpressions;

namespace Tasual
{
    public partial class Tasual_Main : Form
    {
        // ==============
        //  Declarations
        // ==============

        public List<Task> TaskArray = new List<Task>();
        //public List<string> GroupArray = new List<string>(); // currently unused // TODO: Add all known groups from taskarray tasks into here
        public List<string> HiddenGroups = new List<string>();
        public List<string> HiddenGroupHasStub = new List<string>();

        Styles Tasual_Setting_Style = Styles.Custom;
        string Tasual_Setting_TextFile = "localdb.txt";
        bool Tasual_Setting_ConfirmClear = true; // currently unused
        bool Tasual_Setting_ConfirmDelete = true; // currently unused
        bool Tasual_Setting_AlwaysOnTop = false; // currently unused

        ListViewHitTestInfo CalendarPopout = null;
        ListViewHitTestInfo Tasual_ListView_FirstClickInfo = null;
        bool Tasual_ListView_PreviouslySelected = false;

        public enum TimeFormat
        {
            Elapsed,
            Due,
            Short,
            Medium,
            Long
        }

        public enum Styles
        {
            Custom,
            Simple,
            Detailed
        }

        public enum Protocols
        {
            Tasual,
            JSON,
            XML,
            RTM,
            Text
        }


        // =============================
        //  Common/Supporting Functions
        // =============================

        public void Tasual_ClearAll()
        {
            TaskArray.Clear();
            Tasual_Array_Save_Text();
            Tasual_Array_Load_Text();
            Tasual_ListView_PopulateFromArray();
        }

        public void Tasual_DeleteTask(ref Task Task, ListViewItem Item)
        {
            ListViewGroup OldGroup = Item.Group;
            TaskArray.Remove(Task);
            Tasual_Array_Save_Text();
            if (Item != null) { Tasual_ListView.Items.Remove(Item); }
            if ((OldGroup != null) && (OldGroup.Items.Count == 0))
            {
                Tasual_ListView.Groups.Remove(OldGroup);
            }
        }

        public void Tasual_DeleteTask(ListViewItem Item)
        {
            ListViewGroup OldGroup = Item.Group;
            TaskArray.Remove((Task)Item.Tag);
            Tasual_Array_Save_Text();
            if (Item != null) { Tasual_ListView.Items.Remove(Item); }
            if ((OldGroup != null) && (OldGroup.Items.Count == 0))
            {
                Tasual_ListView.Groups.Remove(OldGroup);
            }
        }

        private void Tasual_ReAssignGroup(string OldTaskGroup, string NewTaskGroup)
        {
            foreach (Task Task in TaskArray)
            {
                if (Task == null) { break; }
                if (Task.Group == OldTaskGroup)
                {
                    Task.Group = NewTaskGroup;
                    Tasual_ListView_AssignGroup(NewTaskGroup, ref Task.Item);
                }
            }
        }

        public void Tasual_PrintTaskToConsole(Task TaskItem)
        {
            Console.WriteLine(
                "TaskItem: '{0}', '{1}', '{2}', '{3}', '{4}', ('{5}', '{6}', '{7}')",
                TaskItem.Type,
                TaskItem.Priority,
                TaskItem.Status,
                TaskItem.Group,
                TaskItem.Description,
                TaskItem.Time.Started,
                TaskItem.Time.Ending,
                TaskItem.Time.Next
            );
        }

        public void Tasual_StatusLabel_UpdateCounts()
        {
            // TODO: Get total from TaskArray size
            int Complete = 0;
            int Total = 0;

            foreach (Task Task in TaskArray)
            {
                if (Task == null) { break; }

                ++Total;
                if (Task.Status == (int)Task.Statuses.Complete) { ++Complete; }
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

        public void Tasual_Array_Save_Text()
        {
            try
            {
                using (StreamWriter OutputFile = new StreamWriter(Tasual_Setting_TextFile))
                {
                    foreach (Task Task in TaskArray)
                    {
                        string Line;
                        Line = Task.Type.ToString();
                        Line = Line + (char)29 + Task.Priority.ToString();
                        Line = Line + (char)29 + Task.Status.ToString();
                        Line = Line + (char)29 + Task.Group;
                        Line = Line + (char)29 + Task.Description;

                        DateTimeOffset TimeOffset;
                        TimeOffset = new DateTimeOffset(Task.Time.Started.ToLocalTime());
                        Line = Line + (char)29 + TimeOffset.ToUnixTimeSeconds();

                        TimeOffset = new DateTimeOffset(Task.Time.Ending.ToLocalTime());
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

        public void Tasual_Array_Load_Text()
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
                        Task NewItem = new Task();
                        //NewItem.Time = new Task.TimeInfo();
                        int argtype = 0;
                        string[] segments = Line.Split((char)29);
                        double UnixTime;

                        foreach (string token in segments)
                        {
                            // lets do something with this data now
                            switch (argtype)
                            {
                                case (int)Task.Arguments.Type: { Int32.TryParse(token, out NewItem.Type); break; }
                                case (int)Task.Arguments.Priority: { Int32.TryParse(token, out NewItem.Priority); break; }
                                case (int)Task.Arguments.Status: { Int32.TryParse(token, out NewItem.Status); break; }
                                case (int)Task.Arguments.Group: { NewItem.Group = token; break; }
                                case (int)Task.Arguments.Description: { NewItem.Description = token; break; }
                                case (int)Task.Arguments.Created:
                                    {
                                        Double.TryParse(token, out UnixTime);
                                        NewItem.Time.Started = DateTimeOffset.FromUnixTimeSeconds((long)UnixTime).DateTime.ToLocalTime();
                                        break;
                                    }
                                case (int)Task.Arguments.Ending:
                                    {
                                        Double.TryParse(token, out UnixTime);
                                        NewItem.Time.Ending = DateTimeOffset.FromUnixTimeSeconds((long)UnixTime).DateTime.ToLocalTime();
                                        break;
                                    }
                                case (int)Task.Arguments.Next:
                                    {
                                        Double.TryParse(token, out UnixTime);
                                        NewItem.Time.Next = DateTimeOffset.FromUnixTimeSeconds((long)UnixTime).DateTime.ToLocalTime();
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("TOO MANY ARGUMENTS IN FILE !!!!!!");
                                        break;
                                    }
                            }

                            ++argtype;
                            //Console.WriteLine(token);
                        }

                        if (argtype == (int)Task.Arguments.Count)
                            TaskArray.Add(NewItem);

                        //Console.WriteLine(line);
                        counter++;
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

        public static string Ordinal(int number)
        {
            string suffix = String.Empty;

            int ones = number % 10;
            int tens = (int)Math.Floor(number / 10M) % 10;

            if (tens == 1)
            {
                suffix = "th";
            }
            else
            {
                switch (ones)
                {
                    case 1:
                        suffix = "st";
                        break;

                    case 2:
                        suffix = "nd";
                        break;

                    case 3:
                        suffix = "rd";
                        break;

                    default:
                        suffix = "th";
                        break;
                }
            }
            return String.Format("{0}{1}", number, suffix);
        }

        public void Tasual_ListView_BeginEdit(ListViewItem Item)
        {
            if (Item != null)
            {
                Tasual_ListView.LabelEdit = true;
                Item.BeginEdit();
            }
        }

        public string Tasual_ListView_FormatTime(DateTime Time, TimeFormat Format)
        {
            switch (Format)
            {
                case TimeFormat.Elapsed:
                    {
                        TimeSpan TS = DateTime.Now - Time;
                        int intYears = DateTime.Now.Year - Time.Year;
                        int intMonths = DateTime.Now.Month - Time.Month;
                        int intDays = DateTime.Now.Day - Time.Day;
                        int intHours = DateTime.Now.Hour - Time.Hour;
                        int intMinutes = DateTime.Now.Minute - Time.Minute;
                        int intSeconds = DateTime.Now.Second - Time.Second;
                        if (intYears > 0) return String.Format("{0} {1} ago", intYears, (intYears == 1) ? "year" : "years");
                        else if (intMonths > 0) return String.Format("{0} {1} ago", intMonths, (intMonths == 1) ? "month" : "months");
                        else if (intDays > 0) return String.Format("{0} {1} ago", intDays, (intDays == 1) ? "day" : "days");
                        else if (intHours > 0) return String.Format("{0} {1} ago", intHours, (intHours == 1) ? "hour" : "hours");
                        else if (intMinutes > 0) return String.Format("{0} {1} ago", intMinutes, (intMinutes == 1) ? "minute" : "minutes");
                        else if (intSeconds > 1) return String.Format("{0} {1} ago", intSeconds, (intSeconds == 1) ? "second" : "seconds");
                        else if (intSeconds >= 0) return "Just now";
                        else
                        {
                            return String.Format("{0} {1}", Time.ToShortDateString(), Time.ToShortTimeString());
                        }
                    }

                case TimeFormat.Due:
                    {
                        return "";
                    }

                case TimeFormat.Short: // "6/6 - Tue 10pm"
                    {
                        string TimeStamp = "";
                        if (Time.TimeOfDay != TimeSpan.Zero)
                        {
                            string Minutes = "";
                            if (Time.Minute != 0) { Minutes = ":" + Time.Minute.ToString("00"); }

                            if (Time.Hour > 12)
                            {
                                TimeStamp = (Time.Hour - 12).ToString();
                                TimeStamp = TimeStamp + Minutes + "pm";
                            }
                            else
                            {
                                TimeStamp = Time.Hour.ToString();
                                TimeStamp = TimeStamp + Minutes + "am";
                            }
                        }

                        return String.Format(
                            "{0}/{1} - {2} {3}",
                            Time.Month,
                            Time.Day,
                            Time.DayOfWeek.ToString().Substring(0, 3),
                            TimeStamp);
                    }

                case TimeFormat.Medium: // "Sat, Jun 6th at 10:00pm"
                    {
                        string TimeStamp;
                        if (Time.TimeOfDay == TimeSpan.Zero)
                        {
                            TimeStamp = "";
                        }
                        else
                        {
                            TimeStamp = "at ";
                            TimeStamp = TimeStamp + Time.Hour.ToString() + ":" + Time.Minute.ToString();
                            if (Time.Hour <= 12) { TimeStamp = TimeStamp + "am"; }
                            else { TimeStamp = TimeStamp + "pm"; }
                        }

                        return String.Format(
                            "{0}, {1} {2} {3}",
                            Time.DayOfWeek.ToString().Substring(0, 3),
                            Time.Month.ToString().Substring(0, 3),
                            Time.Day,
                            TimeStamp);
                    }

                case TimeFormat.Long: // "Tuesday, June 6th at 10:00pm"
                    {
                        string TimeStamp;
                        if (Time.TimeOfDay == TimeSpan.Zero)
                        {
                            TimeStamp = "";
                        }
                        else
                        {
                            TimeStamp = "at ";
                            TimeStamp = TimeStamp + Time.Hour.ToString() + ":" + Time.Minute.ToString();
                            if (Time.Hour <= 12) { TimeStamp = TimeStamp + "am"; }
                            else { TimeStamp = TimeStamp + "pm"; }
                        }

                        return String.Format(
                            "{0}, {1} {2} {3}",
                            Time.DayOfWeek.ToString(),
                            Time.Month.ToString(),
                            Time.Day,
                            TimeStamp);
                    }

                default: return "";
            }
        }

        private Color Tasual_ListView_ForeColor(int Status, bool Selected)
        {
            switch (Status)
            {
                case (int)Task.Statuses.Complete:
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
                case (int)Task.Statuses.New:
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
                case (int)Task.Statuses.Complete:
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
                case (int)Task.Statuses.New:
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
            Task Task = (Task)Item.Tag;

            switch (Status)
            {
                case (int)Task.Statuses.Toggle:
                    {
                        if (Task.Status == (int)Task.Statuses.Complete)
                        {
                            Status = (int)Task.Statuses.New;
                            goto case (int)Task.Statuses.New;
                        }
                        else if (Task.Status == (int)Task.Statuses.New)
                        {
                            Status = (int)Task.Statuses.Complete;
                            goto case (int)Task.Statuses.Complete;
                        }
                        break;
                    }

                case (int)Task.Statuses.Complete:
                    {
                        Item.ForeColor = Tasual_ListView_ForeColor((int)Task.Statuses.Complete, Item.Selected);//Color.FromArgb(255, 189, 208, 230);
                        Item.ImageIndex = 0;
                        break;
                    }

                case (int)Task.Statuses.New:
                    {
                        Item.ForeColor = Tasual_ListView_ForeColor((int)Task.Statuses.New, Item.Selected);//Color.FromArgb(255, 36, 90, 150);
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

        public void Tasual_ListView_SizeColumns()
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

        private void Tasual_ListView_AssignGroup(string GroupName, ref ListViewItem Item)
        {
            ListViewGroup Found = Tasual_ListView_FindGroup(GroupName);
            if (Found == null)
            {
                ListViewGroup NewGroup = new ListViewGroup();
                NewGroup.Name = GroupName;
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

        public ListViewItem Tasual_ListView_CreateListViewItem(ref Task Task)
        {
            string[] ItemColumnData;

            // determine which columns we're going to have
            switch (Tasual_Setting_Style)
            {
                // "CUSTOM" STYLE:
                // - groups: overdue at top, normal group listings below, completed at bottom
                // - columns: Description, Time
                case Styles.Custom:
                    {
                        ItemColumnData = new string[2];
                        ItemColumnData[0] = Task.Description;
                        ItemColumnData[1] = Tasual_ListView_FormatTime(Task.Time.Started.ToLocalTime(), TimeFormat.Short);
                        break;
                    }

                // "SIMPLE" STYLE:
                // - groups: overdue at top, today, tomorrow, this week, next week, future, completed at bottom
                // - columns: Description, Time
                case Styles.Simple:
                    {
                        ItemColumnData = new string[2];
                        ItemColumnData[0] = Task.Description;
                        ItemColumnData[1] = Task.Group.ToString();
                        break;
                    }

                // "DETAILED" STYLE:
                // - groups: overdue at top, today, tomorrow, this week, next week, future, completed at bottom
                // - columns: Description, Category, Time
                case Styles.Detailed:
                    {
                        ItemColumnData = new string[3];
                        ItemColumnData[0] = Task.Description;
                        ItemColumnData[1] = Task.Group.ToString();
                        ItemColumnData[2] = Tasual_ListView_FormatTime(Task.Time.Started.ToLocalTime(), TimeFormat.Elapsed);
                        break;
                    }

                default:
                    {
                        throw new Exception("Tasual_ListView_CreateListViewItem(): Invalid style setting!");
                    }
            }

            ListViewItem Item = new ListViewItem(ItemColumnData);
            Item.Tag = Task;
            Task.Item = Item;

            Tasual_ListView_AssignGroup(Task.Group.ToString(), ref Item);
            Tasual_ListView_ChangeStatus(ref Item, Task.Status);
            Tasual_ListView.Items.Add(Item);

            return Item;
        }

        public void Tasual_ListView_PopulateFromArray()
        {
            Tasual_ListView_ClearAll();

            switch (Tasual_Setting_Style)
            {
                // See Tasual_ListView_CreateListViewItem() for details
                case Styles.Custom:
                    {
                        Tasual_ListView.Columns.Add("Description");
                        Tasual_ListView.Columns.Add("Time");
                        break;
                    }

                case Styles.Simple:
                    {
                        Tasual_ListView.Columns.Add("Description");
                        Tasual_ListView.Columns.Add("Time");
                        break;
                    }

                case Styles.Detailed:
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

            foreach (Task Task in TaskArray)
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
                        Task Stub = new Task();
                        Stub.Status = (int)Task.Statuses.New;
                        Stub.Group = "Testing";
                        Stub.Description = "This item has been hidden";
                        Task.TimeInfo Time = new Task.TimeInfo();
                        Time.Started = DateTime.Now.ToLocalTime();
                        Stub.Time = Time;
                        Tasual_ListView_CreateListViewItem(ref Stub);
                    }
                }
                else
                {
                    Task Referral = Task;
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
            Tasual_Create TaskForm = new Tasual_Create(this);
            TaskForm.ShowDialog(this);
        }

        private void Tasual_MenuStrip_Create_Quick_Click(object sender, EventArgs e)
        {
            string Group = "";
            if(Tasual_ListView.Groups.Count != 0)
            {
                Group = Tasual_ListView.Groups[0].Name;
            }
            else
            {
                Group = "Tasks";
            }

            Task Task = new Task(
                0, 
                0, 
                0, 
                Group, 
                "New task", 
                new Task.TimeInfo(), 
                new Timer()
            );

            TaskArray.Add(Task);
            Tasual_Array_Save_Text();
            ListViewItem Item = Tasual_ListView_CreateListViewItem(ref Task);
            Tasual_StatusLabel_UpdateCounts();
            Tasual_ListView_SizeColumns();
            Tasual_ListView_BeginEdit(Item);
        }

        private void Tasual_MenuStrip_Edit_Click(object sender, EventArgs e)
        {
            Tasual_ListView_BeginEdit(Tasual_ListView.FocusedItem);
        }

        private void Tasual_MenuStrip_Settings_AlwaysOnTop_Click(object sender, EventArgs e)
        {
            Tasual_ReAssignGroup("Testing", "");
            Tasual_Array_Save_Text();
            Tasual_Array_Load_Text();
            Tasual_ListView_PopulateFromArray();
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
                    Task Task = (Task)Item.Tag;
                    if ((Task != null) && (Item != null))
                    {
                        Task.Description = Item.Text.ToString();
                        Tasual_Array_Save_Text();
                    }
                });
            }
        }

        private void Tasual_ListView_SingleClick(MouseEventArgs e)
        {
            if (Tasual_ListView_PreviouslySelected == true)
            {
                Tasual_ListView_BeginEdit(Tasual_ListView.FocusedItem);
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
                        Tasual_ListView_BeginEdit(Tasual_ListView.FocusedItem);
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
                                    Tasual_ListView_ChangeStatus(ref SelectedItem, (int)Task.Statuses.Toggle);
                                    Tasual_StatusLabel_UpdateCounts();
                                    Tasual_Array_Save_Text();
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
                                        // Set up popout calendar
                                        // We have to wait until MouseUp so that the window gets proper focus when loading
                                        if (Info.SubItem != null)
                                        {
                                            if (CalendarPopout == null)
                                            {
                                                CalendarPopout = Info;
                                            }
                                            else { CalendarPopout = null; }
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
            /* else if (Info != null)
            {
                // TODO: Should we handle clicking blank space in any particular way?
            }*/
        }

        private void Tasual_ListView_MouseUp(object sender, MouseEventArgs e)
        {
            if (CalendarPopout != null)
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
                    Task Task = (Task)Item.Tag;
                    Item.BackColor = Tasual_ListView_BackColor(Task.Status, false);
                    Item.ForeColor = Tasual_ListView_ForeColor(Task.Status, false);
                });
            Tasual_ListView.SelectedItems.Cast<ListViewItem>()
                .ToList().ForEach(Item =>
                {
                    Task Task = (Task)Item.Tag;
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
            // TODO select which method of array acquisition here
            Tasual_Array_Load_Text();

            //HiddenGroups.Add("Testing");

            // load tasks into Tasual_ListView
            Tasual_ListView_PopulateFromArray();
        }

        public static Tasual_Main ReturnFormInstance()
        {
            return Application.OpenForms[0] as Tasual_Main;
        }

        private void Tasual_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Tasual_Notify.Dispose();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tasual_DeleteTask(Tasual_ListView.FocusedItem);
        }
    }

    public class Task
    {
        public class TimeInfo
        {
            // for all tasks
            public DateTime Started;
            public DateTime Ending;
            public DateTime Next;

            // for recurring tasks
            public int Iterations; // number of total iterations allowed 
            public int Count; // instance count of this task (starts at 1)
            public int Expire; // time in seconds after due date to remove task (-1 for instant)

            // simple recurring tasks
            public int Yearly;
            public int Monthly;
            public int Weekly;
            public int Daily;

            // complex recurring tasks
            public TimeSpan TimeOfDay;
            public MonthFlag MonthFilter;
            public WeekFlag WeekFilter;
            public DayFlag DayFilter;
            public int SpecificDay;

            // blank constructor
            public TimeInfo()
            {
                Started = DateTime.MinValue;
                Ending = DateTime.MinValue;
                Next = DateTime.MinValue;
                Iterations = 0;
                Count = 0;
                Expire = 0;
                Yearly = 0;
                Monthly = 0;
                Weekly = 0;
                Daily = 0;
                TimeOfDay = TimeSpan.Zero;
                MonthFilter = 0;
                WeekFilter = 0;
                DayFilter = 0;
                SpecificDay = 0;
            }

            // singular constructor
            public TimeInfo(
                DateTime _Started,
                DateTime _Ending,
                DateTime _Next)
            {
                Started = _Started;
                Ending = _Ending;
                Next = _Next;
                Iterations = 0;
                Count = 0;
                Expire = 0;
                Yearly = 0;
                Monthly = 0;
                Weekly = 0;
                Daily = 0;
                TimeOfDay = TimeSpan.Zero;
                MonthFilter = 0;
                WeekFilter = 0;
                DayFilter = 0;
                SpecificDay = 0;
            }

            // simple repeating constructor
            public TimeInfo(
                DateTime _Started,
                DateTime _Ending,
                DateTime _Next,
                int _Iterations,
                int _Count,
                int _Expire,
                int _Yearly,
                int _Monthly,
                int _Weekly,
                int _Daily,
                TimeSpan _TimeOfDay)
            {
                Started = _Started;
                Ending = _Ending;
                Next = _Next;
                Iterations = _Iterations;
                Count = _Count;
                Expire = _Expire;
                Yearly = _Yearly;
                Monthly = _Monthly;
                Weekly = _Weekly;
                Daily = _Daily;
                TimeOfDay = _TimeOfDay;
                MonthFilter = 0;
                WeekFilter = 0;
                DayFilter = 0;
                SpecificDay = 0;
            }

            // complex repeating constructor
            public TimeInfo(
                DateTime _Started,
                DateTime _Ending,
                DateTime _Next,
                int _Iterations,
                int _Count,
                int _Expire,
                TimeSpan _TimeOfDay,
                MonthFlag _MonthFilter,
                WeekFlag _WeekFilter,
                DayFlag _DayFilter,
                int _SpecificDay)
            {
                Started = _Started;
                Ending = _Ending;
                Next = _Next;
                Iterations = _Iterations;
                Count = _Count;
                Expire = _Expire;
                Yearly = 0;
                Monthly = 0;
                Weekly = 0;
                Daily = 0;
                TimeOfDay = _TimeOfDay;
                MonthFilter = _MonthFilter;
                WeekFilter = _WeekFilter;
                DayFilter = _DayFilter;
                SpecificDay = _SpecificDay;
            }

            // full constructor
            public TimeInfo(
                DateTime _Started,
                DateTime _Ending,
                DateTime _Next,
                int _Iterations,
                int _Count,
                int _Expire,
                int _Yearly,
                int _Monthly,
                int _Weekly,
                int _Daily,
                TimeSpan _TimeOfDay,
                MonthFlag _MonthFilter,
                WeekFlag _WeekFilter,
                DayFlag _DayFilter,
                int _SpecificDay)
            {
                Started = _Started;
                Ending = _Ending;
                Next = _Next;
                Iterations = _Iterations;
                Count = _Count;
                Expire = _Expire;
                Yearly = _Yearly;
                Monthly = _Monthly;
                Weekly = _Weekly;
                Daily = _Daily;
                TimeOfDay = _TimeOfDay;
                MonthFilter = _MonthFilter;
                WeekFilter = _WeekFilter;
                DayFilter = _DayFilter;
                SpecificDay = _SpecificDay;
            }
        }

        [Flags]
        public enum MonthFlag
        {
            January = 1,
            February = 2,
            March = 4,
            April = 8,
            May = 16,
            June = 32,
            July = 64,
            August = 128,
            September = 256,
            October = 512,
            November = 1024,
            December = 2048,
            Everymonth = 4095
        }

        [Flags]
        public enum WeekFlag
        {
            First = 1,
            Second = 2,
            Third = 4,
            Fourth = 8,
            Fifth = 16,
            Last = 32,
            Everyweek = 31
        }

        [Flags]
        public enum DayFlag
        {
            Monday = 1,
            Tuesday = 2,
            Wednesday = 4,
            Thursday = 8,
            Friday = 16,
            Saturday = 32,
            Sunday = 64,
            Everyday = 127
        }

        private static MonthFlag FromMonthToFlag(int Input)
        {
            switch (Input)
            {
                case 1: return MonthFlag.January;
                case 2: return MonthFlag.February;
                case 3: return MonthFlag.March;
                case 4: return MonthFlag.April;
                case 5: return MonthFlag.May;
                case 6: return MonthFlag.June;
                case 7: return MonthFlag.July;
                case 8: return MonthFlag.August;
                case 9: return MonthFlag.September;
                case 10: return MonthFlag.October;
                case 11: return MonthFlag.November;
                case 12: return MonthFlag.December;
                default: return 0;
            }
        }

        private static WeekFlag FromWeekToFlag(int Input)
        {
            switch (Math.Ceiling((double)Input / 7))
            {
                case 1: return WeekFlag.First;
                case 2: return WeekFlag.Second;
                case 3: return WeekFlag.Third;
                case 4: return WeekFlag.Fourth;
                case 5: return WeekFlag.Fifth;
                default: return 0;
            }
        }

        private static DayFlag FromDayToFlag(DayOfWeek Input)
        {
            switch (Input)
            {
                case DayOfWeek.Monday: return DayFlag.Monday;
                case DayOfWeek.Tuesday: return DayFlag.Tuesday;
                case DayOfWeek.Wednesday: return DayFlag.Wednesday;
                case DayOfWeek.Thursday: return DayFlag.Thursday;
                case DayOfWeek.Friday: return DayFlag.Friday;
                case DayOfWeek.Saturday: return DayFlag.Saturday;
                case DayOfWeek.Sunday: return DayFlag.Sunday;
                default: return 0;
            }
        }

        public static DateTime FindNextIteration(DateTime BaseTime, ref TimeInfo Rules)
        {
            DateTime NextTime = new DateTime();
            NextTime = BaseTime;

            // set hours/minutes/seconds
            NextTime = NextTime - NextTime.TimeOfDay;
            NextTime = NextTime + Rules.TimeOfDay;

            // SIMPLE RECURRANCE
            // Increment from "start date"
            if ((Rules.Yearly + Rules.Monthly + Rules.Weekly + Rules.Daily) > 0)
            {
                NextTime = Rules.Started;

                for (int hops = 1; hops <= 3650; ++hops)
                {
                    Rules.Count = hops;
                    if (NextTime > BaseTime)
                    {
                        return NextTime;
                    }

                    NextTime = NextTime.AddYears(Rules.Yearly);
                    NextTime = NextTime.AddMonths(Rules.Monthly);
                    NextTime = NextTime.AddDays(Rules.Weekly * 7);
                    NextTime = NextTime.AddDays(Rules.Daily);
                }
            }

            // todo: find some good way to count occurences of complex recurrances 

            // COMPLEX RECURRANCE: Filter month/week/day by flags
            // Increment NextTime until we find a day that matches all of our criteria
            else if ((Rules.MonthFilter != 0) || (Rules.WeekFilter != 0) || (Rules.DayFilter != 0))
            {
                for (int hops = 1; hops <= 3650; ++hops) // maximum search of 10 years
                {
                    if ((Rules.MonthFilter & FromMonthToFlag(NextTime.Month)) != 0)
                    {
                        // if we're searching for the last week, automatically jump to the last 7 days of the month
                        bool FindingLastWeek = false;
                        if ((Rules.WeekFilter & WeekFlag.Last) != 0)
                        {
                            FindingLastWeek = true;
                            NextTime = NextTime.AddDays(DateTime.DaysInMonth(NextTime.Year, NextTime.Month) - 7);
                            //Console.WriteLine("Last week {0} {1}", DateTime.DaysInMonth(NextTime.Year, NextTime.Month), DateTime.DaysInMonth(NextTime.Year, NextTime.Month) - 7);
                        }

                        if (((Rules.WeekFilter & FromWeekToFlag(NextTime.Day)) != 0) || FindingLastWeek)
                        {
                            if ((Rules.DayFilter & FromDayToFlag(NextTime.DayOfWeek)) != 0)
                            {
                                if (NextTime > BaseTime)
                                {
                                    if (Rules.SpecificDay != 0)
                                    {
                                        if (NextTime.Day == Rules.SpecificDay)
                                        {
                                            return NextTime;
                                        }
                                    }
                                    else
                                    {
                                        return NextTime;
                                    }
                                }
                            }
                        }
                    }

                    NextTime = NextTime.AddDays(1);
                }
            }

            // NON-RECURRANCE: Item has no rules that allow it to recur
            return DateTime.MinValue;
        }

        public int Type;
        public int Priority;
        public int Status;
        public string Group;
        public string Description;
        public string Notes;
        public string Link;
        public string Location;
        public TimeInfo Time;
        public Timer Timer;
        public ListViewItem Item;

        public enum Arguments
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

        public enum Types
        {
            TYPE_USER_SINGLE,
            TYPE_USER_RECURRING,
            TYPE_USER_DEBT_OWED,
            TYPE_USER_DEBT_LENT,
            TYPE_SYNDICATION_SINGLE,
            TYPE_SYNDICATION_RECURRING
        }

        public enum Priorities
        {
            Low,
            Normal,
            High
        }

        public enum Statuses
        {
            New,
            Complete,
            Toggle
        }

        // blank constructor
        public Task()
        {
            Type = 0;
            Priority = (int)Priorities.Normal;
            Time = new TimeInfo();
        }

        // specific constructor
        public Task(
            int _Type, 
            int _Priority, 
            int _Status, 
            string _Group, 
            string _Description, 
            TimeInfo _Time, 
            Timer _Timer)
        {

            Type = _Type;
            Priority = _Priority;
            Status = _Status;
            Group = _Group;
            Description = _Description;
            Time = _Time;
            Timer = _Timer;
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

    // A textbox that supports a watermark hint
    public class WatermarkTextBox : TextBox
    {
        // The text that will be presented as the watermark hint
        private string _WatermarkText = "Type here";

        // Gets or Sets the text that will be presented as the watermark hint
        public string WatermarkText
        {
            get { return _WatermarkText; }
            set { _WatermarkText = value; }
        }

        // Whether watermark effect is enabled or not
        private bool _WatermarkActive = true;

        // Gets or Sets whether watermark effect is enabled or not
        public bool WatermarkActive
        {
            get { return _WatermarkActive; }
            set { _WatermarkActive = value; }
        }

        // Create a new TextBox that supports watermark hint
        public WatermarkTextBox()
        {
            _WatermarkActive = true;
            Text = _WatermarkText;
            ForeColor = Color.Gray;

            GotFocus += (source, e) =>
            {
                RemoveWatermark();
            };

            LostFocus += (source, e) =>
            {
                ApplyWatermark();
            };

        }

        // Remove watermark from the textbox
        public void RemoveWatermark()
        {
            if (_WatermarkActive)
            {
                _WatermarkActive = false;
                Text = "";
                ForeColor = Color.Black;
            }
        }

        // Applywatermark immediately
        public void ApplyWatermark()
        {
            if (!_WatermarkActive && string.IsNullOrEmpty(Text)
                || ForeColor == Color.Gray)
            {
                _WatermarkActive = true;
                Text = _WatermarkText;
                ForeColor = Color.Gray;
            }
        }

        // Apply watermark to the textbox
        public void ApplyWatermark(string newText)
        {
            WatermarkText = newText;
            ApplyWatermark();
        }

    }

    // TODO: We're really not doing much with this, is it even worth it to subclass?
    public class TasualListView : ListView
    {
        public static MouseEventHandler GroupHeaderClick;
        public static void OnGroupHeaderClick(MouseEventArgs e)
        {
            GroupHeaderClick?.Invoke(null, e);
        }
    }
}
