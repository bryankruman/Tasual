using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tasual
{
    public partial class Tasual_Create : Form
    {
        private readonly Tasual_Main _Tasual_Main;

        public List<Label> SelectionLabels_Days = new List<Label>();
        public List<Label> SelectionLabels_Weeks = new List<Label>();
        public List<Label> SelectionLabels_Months = new List<Label>();

        public Tasual_Create(Tasual_Main Main)
        {
            InitializeComponent();
            this._Tasual_Main = Main;
        }

        private void Tasual_Create_Load(object sender, EventArgs e)
        {
            SelectionLabels_Days.Add(Tasual_Create_Label_DaySel_Mon);
            SelectionLabels_Days.Add(Tasual_Create_Label_DaySel_Tue);
            SelectionLabels_Days.Add(Tasual_Create_Label_DaySel_Wed);
            SelectionLabels_Days.Add(Tasual_Create_Label_DaySel_Thu);
            SelectionLabels_Days.Add(Tasual_Create_Label_DaySel_Fri);
            SelectionLabels_Days.Add(Tasual_Create_Label_DaySel_Sat);
            SelectionLabels_Days.Add(Tasual_Create_Label_DaySel_Sun);

            SelectionLabels_Weeks.Add(Tasual_Create_Label_WeekSel_1st);
            SelectionLabels_Weeks.Add(Tasual_Create_Label_WeekSel_2nd);
            SelectionLabels_Weeks.Add(Tasual_Create_Label_WeekSel_3rd);
            SelectionLabels_Weeks.Add(Tasual_Create_Label_WeekSel_Last);

            SelectionLabels_Months.Add(Tasual_Create_Label_MonthSel_Jan);
            SelectionLabels_Months.Add(Tasual_Create_Label_MonthSel_Feb);
            SelectionLabels_Months.Add(Tasual_Create_Label_MonthSel_Mar);
            SelectionLabels_Months.Add(Tasual_Create_Label_MonthSel_Apr);
            SelectionLabels_Months.Add(Tasual_Create_Label_MonthSel_May);
            SelectionLabels_Months.Add(Tasual_Create_Label_MonthSel_Jun);
            SelectionLabels_Months.Add(Tasual_Create_Label_MonthSel_Jul);
            SelectionLabels_Months.Add(Tasual_Create_Label_MonthSel_Aug);
            SelectionLabels_Months.Add(Tasual_Create_Label_MonthSel_Sep);
            SelectionLabels_Months.Add(Tasual_Create_Label_MonthSel_Oct);
            SelectionLabels_Months.Add(Tasual_Create_Label_MonthSel_Nov);
            SelectionLabels_Months.Add(Tasual_Create_Label_MonthSel_Dec);

            Tasual_Create_CheckGroupBoxSize();

            Tasual_Create_ComboBox_RepeatSimple.SelectedIndex = 0;
            Tasual_Create_ComboBox_Dismiss.SelectedIndex = 0;
            Tasual_Create_ComboBox_Priority.SelectedIndex = 1;

            // TODO: DataSource will need to be different for other display styles
            // Perhaps build an array of groups? Or just search through every task item in TaskArray worst case scenario
            Tasual_Create_ComboBox_Category.DataSource = _Tasual_Main.Tasual_ListView.Groups;
            Tasual_Create_ComboBox_Category.SelectedIndex = 0;

            // TODO: Populate Category list with existing categories
        }

        private void Tasual_Create_CheckGroupBoxSize()
        {
            int Offset = 0;
            int Height = 190;
            int Location = 308;

            if (Tasual_Create_Panel_SelectionLabels.Visible) { Offset = Offset + 92; }
            if (Tasual_Create_Panel_Ends.Visible) { Offset = Offset + 51; }

            Tasual_Create_GroupBox_Scheduled.Size = new Size(Tasual_Create_GroupBox_Scheduled.Size.Width, Height + Offset);
            Tasual_Create_Button_Create.Location = new Point(Tasual_Create_Button_Create.Location.X, Location + Offset);
            Tasual_Create_Button_Cancel.Location = new Point(Tasual_Create_Button_Cancel.Location.X, Location + Offset);
        }

        private void Tasual_Create_TextBox_Description_TextChanged(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_TextBox_Link_TextChanged(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_TextBox_Location_TextChanged(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_ComboBox_Category_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_ComboBox_Priority_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_ComboBox_Dismiss_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_ComboBox_RepeatSimple_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_DateTimePicker_StartDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_DateTimePicker_StartTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_DateTimePicker_EndDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_NumericUpDown_Type_RepeatSimple_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_NumericUpDown_Ends_Occurences_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_RadioButton_Time_AllDay_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Tasual_Create_RadioButton_Time_Specific_CheckedChanged(object sender, EventArgs e)
        {
            if (Tasual_Create_RadioButton_Time_Specific.Checked)
            {
                Tasual_Create_DateTimePicker_StartTime.Enabled = true;
            }
            else
            {
                Tasual_Create_DateTimePicker_StartTime.Enabled = false;
            }
        }

        private void Tasual_Create_RadioButton_Type_Singular_CheckedChanged(object sender, EventArgs e)
        {
            if (Tasual_Create_RadioButton_Type_Singular.Checked)
            {
                Tasual_Create_Panel_Ends.Visible = false;
            }
            else
            {
                Tasual_Create_Panel_Ends.Visible = true;
            }
            Tasual_Create_CheckGroupBoxSize();
        }

        private void Tasual_Create_RadioButton_Type_RepeatSimple_CheckedChanged(object sender, EventArgs e)
        {
            if (Tasual_Create_RadioButton_Type_RepeatSimple.Checked)
            {
                Tasual_Create_NumericUpDown_Type_RepeatSimple.Enabled = true;
                Tasual_Create_ComboBox_RepeatSimple.Enabled = true;
            }
            else
            {
                Tasual_Create_NumericUpDown_Type_RepeatSimple.Enabled = false;
                Tasual_Create_ComboBox_RepeatSimple.Enabled = false;
            }
            Tasual_Create_CheckGroupBoxSize();
        }

        private void Tasual_Create_RadioButton_Type_RepeatCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (Tasual_Create_RadioButton_Type_RepeatCustom.Checked)
            {
                Tasual_Create_Panel_SelectionLabels.Visible = true;
            }
            else
            {
                Tasual_Create_Panel_SelectionLabels.Visible = false;
            }
            Tasual_Create_CheckGroupBoxSize();
        }

        private void Tasual_Create_RadioButton_Ends_Never_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_RadioButton_Ends_Occurences_CheckedChanged(object sender, EventArgs e)
        {
            if (Tasual_Create_RadioButton_Ends_Occurences.Checked)
            {
                Tasual_Create_NumericUpDown_Ends_Occurences.Enabled = true;
            }
            else
            {
                Tasual_Create_NumericUpDown_Ends_Occurences.Enabled = false;
            }
        }

        private void Tasual_Create_RadioButton_Ends_OnDate_CheckedChanged(object sender, EventArgs e)
        {
            if (Tasual_Create_RadioButton_Ends_OnDate.Checked)
            {
                Tasual_Create_DateTimePicker_EndDate.Enabled = true;
            }
            else
            {
                Tasual_Create_DateTimePicker_EndDate.Enabled = false;
            }
        }

        private void Tasual_Create_CheckBox_Scheduled_CheckedChanged(object sender, EventArgs e)
        {
            if (Tasual_Create_CheckBox_Scheduled.Checked)
            {
                Tasual_Create_GroupBox_Scheduled.Enabled = true;
            }
            else
            {
                Tasual_Create_GroupBox_Scheduled.Enabled = false;
            }
        }

        private void Tasual_Create_Label_DaySel_Specific_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_DaySel_All_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_DaySel_Sun_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_DaySel_Mon_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_DaySel_Tue_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_DaySel_Wed_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_DaySel_Thu_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_DaySel_Fri_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_DaySel_Sat_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_WeekSel_All_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_WeekSel_1st_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_WeekSel_2nd_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_WeekSel_3rd_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_WeekSel_Last_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_MonthSel_All_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_MonthSel_Jan_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_MonthSel_Feb_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_MonthSel_Mar_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_MonthSel_Apr_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_MonthSel_May_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_MonthSel_Jun_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_MonthSel_Jul_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_MonthSel_Aug_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_MonthSel_Sep_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_MonthSel_Oct_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_MonthSel_Nov_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Label_MonthSel_Dec_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Button_Notes_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Create_Button_Create_Click(object sender, EventArgs e)
        {
            Task Task = new Task();
            Task.Description = Tasual_Create_TextBox_Description.Text;
            Task.Priority = Tasual_Create_ComboBox_Priority.SelectedIndex;
            Task.Group = Tasual_Create_ComboBox_Category.Text;
            Task.Status = (int)Tasual_Main.StatusEnum.New;
            Task.Type = (int)Tasual_Main.TypeEnum.TYPE_USER_SINGLE;
            
            Task.TimeInfo TimeInfo = new Task.TimeInfo();
            TimeInfo.Started = DateTime.Now;
            if (Tasual_Create_RadioButton_Type_Singular.Checked == false)
            {
                if (Tasual_Create_RadioButton_Type_RepeatSimple.Checked == true)
                {
                    switch (Tasual_Create_ComboBox_RepeatSimple.SelectedIndex)
                    {
                        case 0: // days
                            {
                                TimeInfo.Daily = (int)Tasual_Create_NumericUpDown_Type_RepeatSimple.Value;
                                break;
                            }
                        case 1: // weeks
                            {
                                TimeInfo.Weekly = (int)Tasual_Create_NumericUpDown_Type_RepeatSimple.Value;
                                break;
                            }
                        case 2: // months
                            {
                                TimeInfo.Monthly = (int)Tasual_Create_NumericUpDown_Type_RepeatSimple.Value;
                                break;
                            }
                        case 3: // years
                            {
                                TimeInfo.Yearly = (int)Tasual_Create_NumericUpDown_Type_RepeatSimple.Value;
                                break;
                            }
                    }
                }
                else if (Tasual_Create_RadioButton_Type_RepeatCustom.Checked == true)
                {
                    // day filters
                    if (Tasual_Create_Label_DaySel_Specific.Tag != null)
                    {
                        TimeInfo.SpecificDay = Tasual_Create_DateTimePicker_StartDate.Value.Day;
                    }
                    else
                    {
                        if (Tasual_Create_Label_DaySel_Mon.Tag != null) { TimeInfo.DayFilter &= Task.DayEnum.Monday; }
                        if (Tasual_Create_Label_DaySel_Tue.Tag != null) { TimeInfo.DayFilter &= Task.DayEnum.Tuesday; }
                        if (Tasual_Create_Label_DaySel_Wed.Tag != null) { TimeInfo.DayFilter &= Task.DayEnum.Wednesday; }
                        if (Tasual_Create_Label_DaySel_Thu.Tag != null) { TimeInfo.DayFilter &= Task.DayEnum.Thursday; }
                        if (Tasual_Create_Label_DaySel_Fri.Tag != null) { TimeInfo.DayFilter &= Task.DayEnum.Friday; }
                        if (Tasual_Create_Label_DaySel_Sat.Tag != null) { TimeInfo.DayFilter &= Task.DayEnum.Saturday; }
                        if (Tasual_Create_Label_DaySel_Sun.Tag != null) { TimeInfo.DayFilter &= Task.DayEnum.Sunday; }
                    }

                    if (Tasual_Create_Label_WeekSel_1st.Tag != null) { TimeInfo.WeekFilter &= Task.WeekEnum.First; }
                    if (Tasual_Create_Label_WeekSel_2nd.Tag != null) { TimeInfo.WeekFilter &= Task.WeekEnum.Second; }
                    if (Tasual_Create_Label_WeekSel_3rd.Tag != null) { TimeInfo.WeekFilter &= Task.WeekEnum.Third; }
                    if (Tasual_Create_Label_WeekSel_Last.Tag != null) { TimeInfo.WeekFilter &= Task.WeekEnum.Last; }

                    if (Tasual_Create_Label_MonthSel_Jan.Tag != null) { TimeInfo.MonthFilter &= Task.MonthEnum.January; }
                    if (Tasual_Create_Label_MonthSel_Feb.Tag != null) { TimeInfo.MonthFilter &= Task.MonthEnum.February; }
                    if (Tasual_Create_Label_MonthSel_Mar.Tag != null) { TimeInfo.MonthFilter &= Task.MonthEnum.March; }
                    if (Tasual_Create_Label_MonthSel_Apr.Tag != null) { TimeInfo.MonthFilter &= Task.MonthEnum.April; }
                    if (Tasual_Create_Label_MonthSel_May.Tag != null) { TimeInfo.MonthFilter &= Task.MonthEnum.May; }
                    if (Tasual_Create_Label_MonthSel_Jun.Tag != null) { TimeInfo.MonthFilter &= Task.MonthEnum.June; }
                    if (Tasual_Create_Label_MonthSel_Jul.Tag != null) { TimeInfo.MonthFilter &= Task.MonthEnum.July; }
                    if (Tasual_Create_Label_MonthSel_Aug.Tag != null) { TimeInfo.MonthFilter &= Task.MonthEnum.August; }
                    if (Tasual_Create_Label_MonthSel_Sep.Tag != null) { TimeInfo.MonthFilter &= Task.MonthEnum.September; }
                    if (Tasual_Create_Label_MonthSel_Oct.Tag != null) { TimeInfo.MonthFilter &= Task.MonthEnum.October; }
                    if (Tasual_Create_Label_MonthSel_Nov.Tag != null) { TimeInfo.MonthFilter &= Task.MonthEnum.November; }
                    if (Tasual_Create_Label_MonthSel_Dec.Tag != null) { TimeInfo.MonthFilter &= Task.MonthEnum.December; }
                }
            }
            Task.Time = TimeInfo;
            _Tasual_Main.Tasual_Array_CreateTask(Task.Type, Task.Priority, Task.Status, Task.Group, Task.Description, Task.Time, false);
            //Tasual_Main.Tasual_Array_Create
            this.Close();
        }

        private void Tasual_Create_Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
