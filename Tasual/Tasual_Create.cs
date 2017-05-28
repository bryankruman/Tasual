using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using BrightIdeasSoftware;

namespace Tasual
{
	public partial class Tasual_Create : Form
	{
		private readonly Tasual_Main _Tasual_Main;
		private readonly Task TaskToEdit;
		bool EditMode = false;

		public List<Label> SelectionLabels_Days = new List<Label>();
		public List<Label> SelectionLabels_Weeks = new List<Label>();
		public List<Label> SelectionLabels_Months = new List<Label>();

		public string Notes = "";

		public void Tasual_Create_CommonSetup()
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
		}

		public void Tasual_Create_SetSpecificDay()
		{
			Tasual_Create_Label_DaySel_Specific.Text = TimeInfo.Ordinal(Tasual_Create_DateTimePicker_StartDate.Value.Day);
		}

		public void Tasual_Create_PopulateCategories()
		{
			foreach (Task Task in _Tasual_Main.TaskArray)
			{
				if (!string.IsNullOrEmpty(Task.Group))
				{
					if (!Tasual_Create_ComboBox_Category.Items.Contains(Task.Group))
					{
						Tasual_Create_ComboBox_Category.Items.Add(Task.Group);
					}
				}
			}
			if (Tasual_Create_ComboBox_Category.Items.Count == 0)
			{
				Tasual_Create_ComboBox_Category.Items.Add("Tasks");
			}
			Tasual_Create_ComboBox_Category.SelectedIndex = 0;
		}

		public Tasual_Create(Tasual_Main Tasual_Main)
		{
			// Treat as normal "Create" dialog
			InitializeComponent();
			_Tasual_Main = Tasual_Main;
			EditMode = false;

			// Common setup
			Tasual_Create_CommonSetup();

			// Categories
			Tasual_Create_PopulateCategories();

			Tasual_Create_DateTimePicker_StartDate.MinDate = DateTime.Now;

			DateTime BaseTime = DateTime.Now.AddMinutes(15);
			DateTime RoundedUp = new DateTime(BaseTime.Year, BaseTime.Month, BaseTime.Day, BaseTime.Hour, 0, 0);
			if ((BaseTime.Minute > 0) || (BaseTime.Second > 0))
			{
				RoundedUp = RoundedUp.AddHours(1);
			}
			Tasual_Create_DateTimePicker_StartTime.Value = RoundedUp;

			Tasual_Create_DateTimePicker_EndDate.MinDate = DateTime.Now;
			Tasual_Create_SetSpecificDay();

			Tasual_Create_ComboBox_RepeatSimple.SelectedIndex = 0;
			Tasual_Create_ComboBox_Dismiss.SelectedIndex = 0;
			Tasual_Create_ComboBox_Priority.SelectedIndex = 1;

			Tasual_Create_CheckGroupBoxSize();
			Tasual_Create_UpdateSummaryLabel();
		}

		public Tasual_Create(Tasual_Main Tasual_Main, int PassedIndex)
		{
			// Treat as "Edit" dialog
			InitializeComponent();
			_Tasual_Main = Tasual_Main;
			TaskToEdit = _Tasual_Main.TaskArray[PassedIndex];
			EditMode = true;
			Text = "Edit";
			Tasual_Create_Button_Create.Text = "Edit";

			// Common setup
			Tasual_Create_CommonSetup();

			// Notes
			Notes = TaskToEdit.Notes;
			Tasual_Create_TextBox_Link.Text = TaskToEdit.Link;
			Tasual_Create_TextBox_Location.Text = TaskToEdit.Location;

			// Categories
			Tasual_Create_ComboBox_Category.Items.Add(TaskToEdit.Group);
			Tasual_Create_PopulateCategories();

			// Priority
			Tasual_Create_ComboBox_Priority.SelectedIndex = TaskToEdit.Priority;

			// Scheduled
			Tasual_Create_CheckBox_Scheduled.Checked = TimeInfo.Scheduled(TaskToEdit.Time);

			// Date and Time
			Tasual_Create_DateTimePicker_StartDate.MinDate = DateTime.Now;
			if (TaskToEdit.Time.Start > DateTime.Now)
			{
				Tasual_Create_DateTimePicker_StartDate.Value = TaskToEdit.Time.Start;
				Tasual_Create_DateTimePicker_StartTime.Value = TaskToEdit.Time.Start;
			}
			else
			{
				DateTime BaseTime = DateTime.Now.AddMinutes(15);
				DateTime RoundedUp = new DateTime(BaseTime.Year, BaseTime.Month, BaseTime.Day, BaseTime.Hour, 0, 0);
				if ((BaseTime.Minute > 0) || (BaseTime.Second > 0))
				{
					RoundedUp = RoundedUp.AddHours(1);
				}
				Tasual_Create_DateTimePicker_StartTime.Value = RoundedUp;
			}
			Tasual_Create_DateTimePicker_EndDate.MinDate = DateTime.Now;
			Tasual_Create_SetSpecificDay();

			// Dismiss
			Tasual_Create_ComboBox_Dismiss.SelectedIndex = TaskToEdit.Time.Dismiss;

			// Repeating
			switch (TimeInfo.GetRepeatType(TaskToEdit.Time))
			{
				case TimeInfo.RepeatType.ComplexRepeat:
					{
						Tasual_Create_RadioButton_Type_RepeatCustom.Checked = true;
						Tasual_Create_RadioButton_Type_RepeatSimple.Checked = false;
						Tasual_Create_RadioButton_Type_Singular.Checked = false;
						Tasual_Create_NumericUpDown_Type_RepeatSimple.Enabled = false;
						Tasual_Create_ComboBox_RepeatSimple.Enabled = false;

						// DAYS
						if (TaskToEdit.Time.SpecificDay != 0)
						{
							Tasual_Create_TagLabel(Tasual_Create_Label_DaySel_Specific, true);
						}
						else if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Everyday) != 0)
						{
							foreach (Label SelectionLabel in SelectionLabels_Days)
							{
								Tasual_Create_TagLabel(SelectionLabel, true);
							}
							Tasual_Create_CheckSelected(SelectionLabels_Days, Tasual_Create_Label_DaySel_All);
						}
						else
						{
							if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Monday) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_DaySel_Mon, true); }
							if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Tuesday) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_DaySel_Tue, true); }
							if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Wednesday) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_DaySel_Wed, true); }
							if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Thursday) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_DaySel_Thu, true); }
							if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Friday) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_DaySel_Fri, true); }
							if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Saturday) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_DaySel_Sat, true); }
							if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Sunday) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_DaySel_Sun, true); }
						}

						// WEEKS
						if ((TaskToEdit.Time.WeekFilter & TimeInfo.WeekFlag.FirstThruLast) != 0)
						{
							Tasual_Create_TagLabel(Tasual_Create_Label_WeekSel_1st, true);
							Tasual_Create_TagLabel(Tasual_Create_Label_WeekSel_2nd, true);
							Tasual_Create_TagLabel(Tasual_Create_Label_WeekSel_3rd, true);
							Tasual_Create_TagLabel(Tasual_Create_Label_WeekSel_Last, true);
							Tasual_Create_CheckSelected(SelectionLabels_Weeks, Tasual_Create_Label_WeekSel_All);
						}
						else
						{
							if ((TaskToEdit.Time.WeekFilter & TimeInfo.WeekFlag.First) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_WeekSel_1st, true); }
							if ((TaskToEdit.Time.WeekFilter & TimeInfo.WeekFlag.Second) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_WeekSel_2nd, true); }
							if ((TaskToEdit.Time.WeekFilter & TimeInfo.WeekFlag.Third) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_WeekSel_3rd, true); }
							if ((TaskToEdit.Time.WeekFilter & TimeInfo.WeekFlag.Last) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_WeekSel_Last, true); }
						}

						// MONTHS
						if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.Everymonth) != 0)
						{
							foreach (Label SelectionLabel in SelectionLabels_Months)
							{
								Tasual_Create_TagLabel(SelectionLabel, true);
							}
							Tasual_Create_CheckSelected(SelectionLabels_Months, Tasual_Create_Label_MonthSel_All);
						}
						else
						{
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.January) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_MonthSel_Jan, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.February) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_MonthSel_Feb, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.March) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_MonthSel_Mar, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.April) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_MonthSel_Apr, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.May) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_MonthSel_May, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.June) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_MonthSel_Jun, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.July) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_MonthSel_Jul, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.August) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_MonthSel_Aug, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.September) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_MonthSel_Sep, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.October) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_MonthSel_Oct, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.November) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_MonthSel_Nov, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.December) != 0) { Tasual_Create_TagLabel(Tasual_Create_Label_MonthSel_Dec, true); }
						}

						break;
					}

				case TimeInfo.RepeatType.SimpleRepeat:
					{
						Tasual_Create_RadioButton_Type_RepeatCustom.Checked = false;
						Tasual_Create_RadioButton_Type_RepeatSimple.Checked = true;
						Tasual_Create_RadioButton_Type_Singular.Checked = false;
						Tasual_Create_NumericUpDown_Type_RepeatSimple.Enabled = true;
						Tasual_Create_ComboBox_RepeatSimple.Enabled = true;
						if (TaskToEdit.Time.Daily != 0)
						{
							Tasual_Create_NumericUpDown_Type_RepeatSimple.Value = TaskToEdit.Time.Daily;
							Tasual_Create_ComboBox_RepeatSimple.SelectedIndex = 0;
						}
						else if (TaskToEdit.Time.Weekly != 0)
						{
							Tasual_Create_NumericUpDown_Type_RepeatSimple.Value = TaskToEdit.Time.Weekly;
							Tasual_Create_ComboBox_RepeatSimple.SelectedIndex = 1;
						}
						else if (TaskToEdit.Time.Monthly != 0)
						{
							Tasual_Create_NumericUpDown_Type_RepeatSimple.Value = TaskToEdit.Time.Monthly;
							Tasual_Create_ComboBox_RepeatSimple.SelectedIndex = 3;
						}
						else // Must be yearly (TaskToEdit.Time.Yearly != 0)
						{
							Tasual_Create_NumericUpDown_Type_RepeatSimple.Value = TaskToEdit.Time.Yearly;
							Tasual_Create_ComboBox_RepeatSimple.SelectedIndex = 3;
						}
						break;
					}

				case TimeInfo.RepeatType.Singular:
					{
						Tasual_Create_RadioButton_Type_RepeatCustom.Checked = false;
						Tasual_Create_RadioButton_Type_RepeatSimple.Checked = false;
						Tasual_Create_RadioButton_Type_Singular.Checked = true;
						Tasual_Create_NumericUpDown_Type_RepeatSimple.Enabled = false;
						Tasual_Create_ComboBox_RepeatSimple.Enabled = false;
						break;
					}
			}

			// Ends
			if (TaskToEdit.Time.End != DateTime.MinValue)
			{
				Tasual_Create_RadioButton_Ends_Never.Checked = false;
				Tasual_Create_RadioButton_Ends_Occurences.Checked = false;
				Tasual_Create_RadioButton_Ends_OnDate.Checked = true;
				Tasual_Create_NumericUpDown_Ends_Occurences.Enabled = false;
				Tasual_Create_DateTimePicker_EndDate.Enabled = true;

				Tasual_Create_DateTimePicker_EndDate.Value = TaskToEdit.Time.End;
			}
			else if (TaskToEdit.Time.Iterations != 0)
			{
				Tasual_Create_RadioButton_Ends_Never.Checked = false;
				Tasual_Create_RadioButton_Ends_Occurences.Checked = true;
				Tasual_Create_RadioButton_Ends_OnDate.Checked = false;
				Tasual_Create_NumericUpDown_Ends_Occurences.Enabled = true;
				Tasual_Create_DateTimePicker_EndDate.Enabled = false;

				Tasual_Create_NumericUpDown_Ends_Occurences.Value = TaskToEdit.Time.Iterations;
			}
			else
			{
				Tasual_Create_RadioButton_Ends_Never.Checked = true;
				Tasual_Create_RadioButton_Ends_Occurences.Checked = false;
				Tasual_Create_RadioButton_Ends_OnDate.Checked = false;
				Tasual_Create_NumericUpDown_Ends_Occurences.Enabled = false;
				Tasual_Create_DateTimePicker_EndDate.Enabled = false;
			}

			Tasual_Create_CheckGroupBoxSize();
			Tasual_Create_UpdateSummaryLabel();
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

		private void Tasual_Create_ComboBox_RepeatSimple_SelectedIndexChanged(object sender, EventArgs e)
		{
			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_DateTimePicker_StartDate_ValueChanged(object sender, EventArgs e)
		{
			if (Tasual_Create_DateTimePicker_StartDate.Value < Tasual_Create_DateTimePicker_StartTime.Value)
			{
				Console.WriteLine("date before time!");
			}
			Tasual_Create_SetSpecificDay();
			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_DateTimePicker_StartTime_ValueChanged(object sender, EventArgs e)
		{
			DateTime Readjust = Tasual_Create_DateTimePicker_StartDate.Value;
			Readjust = Readjust - Readjust.TimeOfDay;
			Readjust = Readjust + Tasual_Create_DateTimePicker_StartTime.Value.TimeOfDay;
			if (Readjust < Tasual_Create_DateTimePicker_StartDate.MinDate)
			{
				Readjust = Readjust.AddDays(1);
			}
			Tasual_Create_DateTimePicker_StartDate.Value = Readjust;
			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_DateTimePicker_EndDate_ValueChanged(object sender, EventArgs e)
		{
			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_NumericUpDown_Type_RepeatSimple_ValueChanged(object sender, EventArgs e)
		{
			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_NumericUpDown_Ends_Occurences_ValueChanged(object sender, EventArgs e)
		{
			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_RadioButton_Time_AllDay_CheckedChanged(object sender, EventArgs e)
		{
			Tasual_Create_UpdateSummaryLabel();
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
			Tasual_Create_UpdateSummaryLabel();
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
			Tasual_Create_UpdateSummaryLabel();
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
			Tasual_Create_UpdateSummaryLabel();
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
			Tasual_Create_UpdateSummaryLabel();
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
			Tasual_Create_UpdateSummaryLabel();
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
			Tasual_Create_UpdateSummaryLabel();
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
			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_AllNoneSelection(Label AllLabel, List<Label> LabelList)
		{
			bool Selected = true;

			if (AllLabel.Text == "All")
			{
				AllLabel.Text = "None";
				Selected = true;
			}
			else
			{
				AllLabel.Text = "All";
				Selected = false;
			}

			foreach (Label SelectionLabel in LabelList)
			{
				Tasual_Create_TagLabel(SelectionLabel, Selected);
			}
		}

		private void Tasual_Create_CheckSelected(List<Label> LabelList, Label AffectedLabel)
		{
			bool AllSelected = true;
			foreach (Label SelectionLabel in LabelList)
			{
				if (SelectionLabel.Tag == null)
				{
					AllSelected = false;
					break;
				}
			}

			if (AllSelected)
			{
				AffectedLabel.Text = "None";
			}
			else
			{
				AffectedLabel.Text = "All";
			}
		}
		private void Tasual_Create_TagLabel(Label Label, bool Tagged)
		{
			if (Tagged)
			{
				Label.ForeColor = Color.Black;
				Label.BackColor = Color.LightBlue;
				Label.Tag = true;
			}
			else
			{
				Label.ForeColor = Color.White;
				Label.BackColor = Color.Silver;
				Label.Tag = null;
			}
		}

		private void Tasual_Create_Label_DaySel_Specific_Click(object sender, EventArgs e)
		{
			Label Label = sender as Label;

			bool Selected = false;
			if (Label.Tag == null)
			{
				// previously not selected, lets select it
				Tasual_Create_TagLabel(Label, true);
				Selected = false;
			}
			else
			{
				// previously selected, lets unselect it
				Tasual_Create_TagLabel(Label, false);
				Selected = true;
			}

			foreach (Label SelectionLabel in SelectionLabels_Days)
			{
				Tasual_Create_TagLabel(SelectionLabel, Selected);
			}

			Tasual_Create_CheckSelected(SelectionLabels_Days, Tasual_Create_Label_DaySel_All);
			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_Label_DaySel_All_Click(object sender, EventArgs e)
		{
			Tasual_Create_AllNoneSelection((Label)sender, SelectionLabels_Days);
			Tasual_Create_TagLabel(Tasual_Create_Label_DaySel_Specific, false);
			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_Label_WeekSel_All_Click(object sender, EventArgs e)
		{
			Tasual_Create_AllNoneSelection((Label)sender, SelectionLabels_Weeks);
			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_Label_MonthSel_All_Click(object sender, EventArgs e)
		{
			Tasual_Create_AllNoneSelection((Label)sender, SelectionLabels_Months);
			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_Label_DaySel_ClickHandler(object sender, EventArgs e)
		{
			Label Label = sender as Label;

			if (Label.Tag == null)
			{
				// previously not selected, lets select it
				Tasual_Create_TagLabel(Label, true);
				Tasual_Create_TagLabel(Tasual_Create_Label_DaySel_Specific, false);
				Tasual_Create_CheckSelected(SelectionLabels_Days, Tasual_Create_Label_DaySel_All);
			}
			else
			{
				// previously selected, lets unselect it
				Tasual_Create_TagLabel(Label, false);
				Tasual_Create_Label_DaySel_All.Text = "All";
			}

			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_Label_WeekSel_ClickHandler(object sender, EventArgs e)
		{
			Label Label = sender as Label;

			if (Label.Tag == null)
			{
				// previously not selected, lets select it
				Tasual_Create_TagLabel(Label, true);
				Tasual_Create_CheckSelected(SelectionLabels_Weeks, Tasual_Create_Label_WeekSel_All);
			}
			else
			{
				// previously selected, lets unselect it
				Tasual_Create_TagLabel(Label, false);
				Tasual_Create_Label_WeekSel_All.Text = "All";
			}

			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_Label_MonthSel_ClickHandler(object sender, EventArgs e)
		{
			Label Label = sender as Label;

			if (Label.Tag == null)
			{
				// previously not selected, lets select it
				Tasual_Create_TagLabel(Label, true);
				Tasual_Create_CheckSelected(SelectionLabels_Months, Tasual_Create_Label_MonthSel_All);
			}
			else
			{
				// previously selected, lets unselect it
				Tasual_Create_TagLabel(Label, false);
				Tasual_Create_Label_MonthSel_All.Text = "All";
			}

			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_Button_Notes_Click(object sender, EventArgs e)
		{
			Tasual_Notes NotesForm = new Tasual_Notes(_Tasual_Main, this);
			NotesForm.ShowDialog(this);
		}

		private void Tasual_Create_UpdateSummaryLabel()
		{
			string TimeInsert = "";
			if (Tasual_Create_RadioButton_Time_Specific.Checked == true)
			{
				TimeInsert = " at " + Tasual_Create_DateTimePicker_StartTime.Value.ToString("h:mm tt");
			}

			string StartYearInsert = "";
			DateTime StartDate = Tasual_Create_DateTimePicker_StartDate.Value;
			if (StartDate.Year != DateTime.Now.Year)
			{
				StartYearInsert = " " + StartDate.Year.ToString();
			}

			if (Tasual_Create_RadioButton_Type_Singular.Checked == true)
			{
				// "Scheduled for Mon, Jun 13th at 6:50 PM"
				// "Scheduled for Mon, Jun 13th 2018 at 6:50 PM"
				// "Scheduled for Mon, Jun 13th"

				Tasual_Create_Label_Summary.Text = String.Format(
					"Scheduled for {0} {1}{2}{3}",
					StartDate.ToString("ddd, MMM"),
					TimeInfo.Ordinal(StartDate.Day),
					StartYearInsert,
					TimeInsert
				);
			}
			else
			{
				int EndsCount = 0;
				string EndsInsert = "";
				if (Tasual_Create_RadioButton_Ends_Never.Checked == true)
				{
					EndsCount = 2; // make it > 1 so that we treat it as "every" instead of "once"
					EndsInsert = " forever";
				}
				else if (Tasual_Create_RadioButton_Ends_Occurences.Checked == true)
				{
					EndsCount = (int)Tasual_Create_NumericUpDown_Ends_Occurences.Value;
					if (EndsCount > 1)
					{
						EndsInsert = " for " + EndsCount.ToString() + " occurences";
					}

				}
				else if (Tasual_Create_RadioButton_Ends_OnDate.Checked == true)
				{
					EndsCount = 2; // make it > 1 so that we treat it as "every" instead of "once"
					string EndsYearInsert = "";
					DateTime EndDate = Tasual_Create_DateTimePicker_EndDate.Value;
					if (EndDate.Year != DateTime.Now.Year)
					{
						EndsYearInsert = " " + EndDate.Year.ToString();
					}

					EndsInsert = String.Format(
						" until {0} {1}{2}",
						EndDate.ToString("ddd, MMM"),
						TimeInfo.Ordinal(EndDate.Day),
						EndsYearInsert);
				}

				if (Tasual_Create_RadioButton_Type_RepeatSimple.Checked == true)
				{
					// "Repeats every 3 days from Jun 13th for 13 occurences"
					// "Repeats every 2 weeks from Jun 13th forever"
					// "Repeats every 2 weeks from Jun 13th until Jun 13th, 2017"
					// "Repeats 2 weeks from Jun 13th at 6:00 PM"
					string IncrementInsert = "";
					int IncrementValue = (int)Tasual_Create_NumericUpDown_Type_RepeatSimple.Value;
					switch (Tasual_Create_ComboBox_RepeatSimple.SelectedIndex)
					{
						case 0: { IncrementInsert = "day"; break; }
						case 1: { IncrementInsert = "week"; break; }
						case 2: { IncrementInsert = "month"; break; }
						case 3: { IncrementInsert = "year"; break; }
					}
					if (IncrementValue > 1)
					{
						// make it plural
						IncrementInsert = IncrementValue.ToString() + " " + IncrementInsert + "s";
					}


					string EveryOrOnce = "once";//"Repeats {0} {1} from now";
					if (EndsCount > 1)
					{
						EveryOrOnce = "every";
					}

					Tasual_Create_Label_Summary.Text = String.Format(
						"Repeats {0} {1} from {2} {3}{4}{5}{6}",
						EveryOrOnce,
						IncrementInsert,
						StartDate.ToString("ddd, MMM"),
						TimeInfo.Ordinal(StartDate.Day),
						StartYearInsert,
						TimeInsert,
						EndsInsert
					);
				}
				else if (Tasual_Create_RadioButton_Type_RepeatCustom.Checked == true)
				{
					// Repeats on Mon, Tue, and Fri in the 2nd and 3rd weeks of May, Jun, and Jul for 12 occurences.
					// Repeats on Mon in Jun and Jul for 12 occurences.
					// Repeats once on the first Mon, Tue, or Wed in the 2nd week of any month. 
					// Repeats on Mon and Tue in the last week of every month until Jun 13th, 2018
					/*Tasual_Create_Label_Summary.Text = String.Format(
						"Repeats on {1} from {2} {3}{4}{5}{6}",
						StartDate.ToString("ddd, MMM"),
						TimeInfo.Ordinal(StartDate.Day),
						StartYearInsert,
						TimeInsert,
						EndsInsert
					);*/
					/*
					// day filters
					if (Tasual_Create_Label_DaySel_Specific.Tag != null)
					{
						TimeInfo.SpecificDay = Tasual_Create_DateTimePicker_StartDate.Value.Day;
					}
					else
					{
						if (Tasual_Create_Label_DaySel_Mon.Tag != null) { TimeInfo.DayFilter &= Task.DayFlag.Monday; }
						if (Tasual_Create_Label_DaySel_Tue.Tag != null) { TimeInfo.DayFilter &= Task.DayFlag.Tuesday; }
						if (Tasual_Create_Label_DaySel_Wed.Tag != null) { TimeInfo.DayFilter &= Task.DayFlag.Wednesday; }
						if (Tasual_Create_Label_DaySel_Thu.Tag != null) { TimeInfo.DayFilter &= Task.DayFlag.Thursday; }
						if (Tasual_Create_Label_DaySel_Fri.Tag != null) { TimeInfo.DayFilter &= Task.DayFlag.Friday; }
						if (Tasual_Create_Label_DaySel_Sat.Tag != null) { TimeInfo.DayFilter &= Task.DayFlag.Saturday; }
						if (Tasual_Create_Label_DaySel_Sun.Tag != null) { TimeInfo.DayFilter &= Task.DayFlag.Sunday; }
					}

					if (Tasual_Create_Label_WeekSel_1st.Tag != null) { TimeInfo.WeekFilter &= Task.WeekFlag.First; }
					if (Tasual_Create_Label_WeekSel_2nd.Tag != null) { TimeInfo.WeekFilter &= Task.WeekFlag.Second; }
					if (Tasual_Create_Label_WeekSel_3rd.Tag != null) { TimeInfo.WeekFilter &= Task.WeekFlag.Third; }
					if (Tasual_Create_Label_WeekSel_Last.Tag != null) { TimeInfo.WeekFilter &= Task.WeekFlag.Last; }

					if (Tasual_Create_Label_MonthSel_Jan.Tag != null) { TimeInfo.MonthFilter &= Task.MonthFlag.January; }
					if (Tasual_Create_Label_MonthSel_Feb.Tag != null) { TimeInfo.MonthFilter &= Task.MonthFlag.February; }
					if (Tasual_Create_Label_MonthSel_Mar.Tag != null) { TimeInfo.MonthFilter &= Task.MonthFlag.March; }
					if (Tasual_Create_Label_MonthSel_Apr.Tag != null) { TimeInfo.MonthFilter &= Task.MonthFlag.April; }
					if (Tasual_Create_Label_MonthSel_May.Tag != null) { TimeInfo.MonthFilter &= Task.MonthFlag.May; }
					if (Tasual_Create_Label_MonthSel_Jun.Tag != null) { TimeInfo.MonthFilter &= Task.MonthFlag.June; }
					if (Tasual_Create_Label_MonthSel_Jul.Tag != null) { TimeInfo.MonthFilter &= Task.MonthFlag.July; }
					if (Tasual_Create_Label_MonthSel_Aug.Tag != null) { TimeInfo.MonthFilter &= Task.MonthFlag.August; }
					if (Tasual_Create_Label_MonthSel_Sep.Tag != null) { TimeInfo.MonthFilter &= Task.MonthFlag.September; }
					if (Tasual_Create_Label_MonthSel_Oct.Tag != null) { TimeInfo.MonthFilter &= Task.MonthFlag.October; }
					if (Tasual_Create_Label_MonthSel_Nov.Tag != null) { TimeInfo.MonthFilter &= Task.MonthFlag.November; }
					if (Tasual_Create_Label_MonthSel_Dec.Tag != null) { TimeInfo.MonthFilter &= Task.MonthFlag.December; }*/
				}
			}
		}

		// TODO: Filter unwanted characters from text input on Tasual_ListView and all other textboxes
		// TODO: Force DateTimePicker_EndDate.MinValue to be later than StartDate
		// TODO: Force DateTimePicker_StartDate.MinValue to be later than DateTime.Now
		// See: http://stackoverflow.com/questions/12607087/only-allow-specific-characters-in-textbox
		// Regex AllowedCharacters = new Regex("^[\\w\\s]+$"); // todo: Make this not suck as much

		private void Tasual_Create_Button_Create_Click(object sender, EventArgs e)
		{
			Task Task = new Task();

			if (!string.IsNullOrWhiteSpace(Tasual_Create_TextBox_Description.Text))
			{
				Task.Description = Tasual_Create_TextBox_Description.Text;
			}
			else
			{
				Console.WriteLine("Tasual_Create_Button_Create_Click(): Description cannot be blank!");
				return;
			}

			Task.Priority = Tasual_Create_ComboBox_Priority.SelectedIndex;
			Task.Group = Tasual_Create_ComboBox_Category.Text;
			Task.Notes = Notes;
			if (Tasual_Create_TextBox_Link.Text != Tasual_Create_TextBox_Link.WatermarkText)
			{
				Task.Link = Tasual_Create_TextBox_Link.Text;
			}

			if (Tasual_Create_TextBox_Location.Text != Tasual_Create_TextBox_Location.WatermarkText)
			{
				Task.Location = Tasual_Create_TextBox_Location.Text;
			}

			TimeInfo TimeInfo = new TimeInfo();
			//TimeInfo.Created = DateTime.Now(); // not necessary, constructor sets this
			if (Tasual_Create_CheckBox_Scheduled.Checked)
			{
				TimeInfo.Start = Tasual_Create_DateTimePicker_StartDate.Value;

				if (Tasual_Create_RadioButton_Time_AllDay.Checked)
				{
					TimeInfo.Start = TimeInfo.Start - TimeInfo.Start.TimeOfDay;
					TimeInfo.Start = TimeInfo.Start + TimeSpan.Zero;
				}
				else
				{
					TimeInfo.Start = TimeInfo.Start - TimeInfo.Start.TimeOfDay;
					TimeInfo.Start = TimeInfo.Start + Tasual_Create_DateTimePicker_StartTime.Value.TimeOfDay;
				}
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
							if (Tasual_Create_Label_DaySel_Mon.Tag != null) { TimeInfo.DayFilter &= TimeInfo.DayFlag.Monday; }
							if (Tasual_Create_Label_DaySel_Tue.Tag != null) { TimeInfo.DayFilter &= TimeInfo.DayFlag.Tuesday; }
							if (Tasual_Create_Label_DaySel_Wed.Tag != null) { TimeInfo.DayFilter &= TimeInfo.DayFlag.Wednesday; }
							if (Tasual_Create_Label_DaySel_Thu.Tag != null) { TimeInfo.DayFilter &= TimeInfo.DayFlag.Thursday; }
							if (Tasual_Create_Label_DaySel_Fri.Tag != null) { TimeInfo.DayFilter &= TimeInfo.DayFlag.Friday; }
							if (Tasual_Create_Label_DaySel_Sat.Tag != null) { TimeInfo.DayFilter &= TimeInfo.DayFlag.Saturday; }
							if (Tasual_Create_Label_DaySel_Sun.Tag != null) { TimeInfo.DayFilter &= TimeInfo.DayFlag.Sunday; }
						}

						if (Tasual_Create_Label_WeekSel_1st.Tag != null) { TimeInfo.WeekFilter &= TimeInfo.WeekFlag.First; }
						if (Tasual_Create_Label_WeekSel_2nd.Tag != null) { TimeInfo.WeekFilter &= TimeInfo.WeekFlag.Second; }
						if (Tasual_Create_Label_WeekSel_3rd.Tag != null) { TimeInfo.WeekFilter &= TimeInfo.WeekFlag.Third; }
						if (Tasual_Create_Label_WeekSel_Last.Tag != null) { TimeInfo.WeekFilter &= TimeInfo.WeekFlag.Last; }

						if (Tasual_Create_Label_MonthSel_Jan.Tag != null) { TimeInfo.MonthFilter &= TimeInfo.MonthFlag.January; }
						if (Tasual_Create_Label_MonthSel_Feb.Tag != null) { TimeInfo.MonthFilter &= TimeInfo.MonthFlag.February; }
						if (Tasual_Create_Label_MonthSel_Mar.Tag != null) { TimeInfo.MonthFilter &= TimeInfo.MonthFlag.March; }
						if (Tasual_Create_Label_MonthSel_Apr.Tag != null) { TimeInfo.MonthFilter &= TimeInfo.MonthFlag.April; }
						if (Tasual_Create_Label_MonthSel_May.Tag != null) { TimeInfo.MonthFilter &= TimeInfo.MonthFlag.May; }
						if (Tasual_Create_Label_MonthSel_Jun.Tag != null) { TimeInfo.MonthFilter &= TimeInfo.MonthFlag.June; }
						if (Tasual_Create_Label_MonthSel_Jul.Tag != null) { TimeInfo.MonthFilter &= TimeInfo.MonthFlag.July; }
						if (Tasual_Create_Label_MonthSel_Aug.Tag != null) { TimeInfo.MonthFilter &= TimeInfo.MonthFlag.August; }
						if (Tasual_Create_Label_MonthSel_Sep.Tag != null) { TimeInfo.MonthFilter &= TimeInfo.MonthFlag.September; }
						if (Tasual_Create_Label_MonthSel_Oct.Tag != null) { TimeInfo.MonthFilter &= TimeInfo.MonthFlag.October; }
						if (Tasual_Create_Label_MonthSel_Nov.Tag != null) { TimeInfo.MonthFilter &= TimeInfo.MonthFlag.November; }
						if (Tasual_Create_Label_MonthSel_Dec.Tag != null) { TimeInfo.MonthFilter &= TimeInfo.MonthFlag.December; }
					}
				}
			}
			Task.Time = TimeInfo;

			if (EditMode)
			{
				_Tasual_Main.TaskArray.Remove(TaskToEdit);
			}
			_Tasual_Main.TaskArray.Add(Task);
			_Tasual_Main.Tasual_Array_Save();
			_Tasual_Main.Tasual_ListView.BuildList();
			_Tasual_Main.Tasual_ListView.EnsureModelVisible(Task);
			_Tasual_Main.Tasual_ListView.SelectObject(Task);
			_Tasual_Main.Tasual_StatusLabel_UpdateCounts();
			this.Close();
		}

		private void Tasual_Create_Button_Cancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
