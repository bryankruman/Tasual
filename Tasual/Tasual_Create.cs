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

		public List<Label> SelectionLabels_Days = new List<Label>();
		public List<Label> SelectionLabels_Weeks = new List<Label>();
		public List<Label> SelectionLabels_Months = new List<Label>();

		public string Notes = "";

		public Tasual_Create(Tasual_Main Tasual_Main)
		{
			InitializeComponent();
			this._Tasual_Main = Tasual_Main;
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
			Tasual_Create_UpdateSummaryLabel();

			Tasual_Create_ComboBox_RepeatSimple.SelectedIndex = 0;
			Tasual_Create_ComboBox_Dismiss.SelectedIndex = 0;
			Tasual_Create_ComboBox_Priority.SelectedIndex = 1;

			// TODO: DataSource will need to be different for other display styles
			// Perhaps build an array of groups? Or just search through every task item in TaskArray worst case scenario
			if (_Tasual_Main.Tasual_ListView.OLVGroups.Count != 0)
			{
				Tasual_Create_ComboBox_Category.DataSource = _Tasual_Main.Tasual_ListView.OLVGroups;
			}
			else
			{
				Tasual_Create_ComboBox_Category.Items.Add("Tasks");
			}
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
			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_DateTimePicker_StartDate_ValueChanged(object sender, EventArgs e)
		{
			Tasual_Create_UpdateSummaryLabel();
		}

		private void Tasual_Create_DateTimePicker_StartTime_ValueChanged(object sender, EventArgs e)
		{
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
			Tasual_Notes NotesForm = new Tasual_Notes(this);
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
					Tasual_Main.Ordinal(StartDate.Day),
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
						Tasual_Main.Ordinal(EndDate.Day),
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
						Tasual_Main.Ordinal(StartDate.Day),
						StartYearInsert,
						TimeInsert,
						EndsInsert
					);
				}
				else if (Tasual_Create_RadioButton_Type_RepeatCustom.Checked == true)
				{
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
		Regex AllowedCharacters = new Regex("^[\\w\\s]+$"); // todo: Make this not suck as much

		private void Tasual_Create_Button_Create_Click(object sender, EventArgs e)
		{
			Task Task = new Task();
			Task.Status = (int)Task.Statuses.New;
			//Task.Checked = (int)Task.Types.TYPE_USER_SINGLE;

			if (
				(Tasual_Create_TextBox_Description.Text != Tasual_Create_TextBox_Description.WatermarkText)
				&&
				!string.IsNullOrWhiteSpace(Tasual_Create_TextBox_Description.Text)
				)
			{
				if (AllowedCharacters.IsMatch(Tasual_Create_TextBox_Description.Text))
				{
					Task.Description = Tasual_Create_TextBox_Description.Text;
				}
				else
				{
					Console.WriteLine("Tasual_Create_Button_Create_Click(): Invalid characters!");
					return;
				}
			}
			else
			{
				Console.WriteLine("Tasual_Create_Button_Create_Click(): Description cannot be blank!");
				return;
			}

			Task.Priority = Tasual_Create_ComboBox_Priority.SelectedIndex;
			Task.Group = ((OLVGroup)Tasual_Create_ComboBox_Category.SelectedItem).Name;

			Task.Notes = Notes;
			if (Tasual_Create_TextBox_Link.Text != Tasual_Create_TextBox_Link.WatermarkText)
			{
				Task.Link = Tasual_Create_TextBox_Link.Text;
			}

			if (Tasual_Create_TextBox_Location.Text != Tasual_Create_TextBox_Location.WatermarkText)
			{
				Task.Location = Tasual_Create_TextBox_Location.Text;
			}

			Task.TimeInfo TimeInfo = new Task.TimeInfo();
			TimeInfo.Start = DateTime.Now;
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
					if (Tasual_Create_Label_MonthSel_Dec.Tag != null) { TimeInfo.MonthFilter &= Task.MonthFlag.December; }
				}
			}
			Task.Time = TimeInfo;

			_Tasual_Main.TaskArray.Add(Task);
			_Tasual_Main.Tasual_Array_Save_Text();
			_Tasual_Main.Tasual_ListView.BuildList();
			_Tasual_Main.Tasual_StatusLabel_UpdateCounts();
			this.Close();
		}

		private void Tasual_Create_Button_Cancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
