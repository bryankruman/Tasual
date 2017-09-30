// ===========================================
//  Copyright (c) 2017 Bryan Kruman
//
//  See LICENSE.rtf file in the project root
//  for full license information.
// ===========================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tasual
{
	/// <summary>
	/// Create and edit form for the Tasual application.
	/// </summary>
	public partial class Form_Create : Form
	{
		/// <summary>Passed main form set upon initialization, used to refer to main application form functions.</summary>
		private readonly Form_Main MainForm;

		/// <summary>Passed task set upon initialization, used to refer to a task from the main application task array.</summary>
		private readonly Task TaskToEdit;

		/// <summary>Whether or not the form was launched in edit mode. True = Edit dialog, False = Create dialog.</summary>
		bool EditMode = false;
		bool SelectGroupTextBox = false;

		/// <summary>Array containing list of selection label objects for the day labels.</summary>
		public List<Label> SelectionLabels_Days = new List<Label>();

		/// <summary>Array containing list of selection label objects for the day labels.</summary>
		public List<Label> SelectionLabels_Weeks = new List<Label>();

		/// <summary>Array containing list of selection label objects for the day labels.</summary>
		public List<Label> SelectionLabels_Months = new List<Label>();

		/// <summary>Local storage of the notes field.</summary>
		public string Notes = "";

		private void AllNoneSelection(Label AllLabel, List<Label> LabelList)
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
				TagLabel(SelectionLabel, Selected);
			}
		}

		private void CheckGroupBoxSize()
		{
			int Offset = 0;
			int Height = 190;
			int Location = 308;

			if (Panel_SelectionLabels.Visible) { Offset = Offset + 92; }
			if (Panel_Ends.Visible) { Offset = Offset + 51; }

			GroupBox_Scheduled.Size = new Size(GroupBox_Scheduled.Size.Width, Height + Offset);
			Button_Create.Location = new Point(Button_Create.Location.X, Location + Offset);
			Button_Cancel.Location = new Point(Button_Cancel.Location.X, Location + Offset);
		}

		private void CheckSelected(List<Label> LabelList, Label AffectedLabel)
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


		public void CommonSetup()
		{
			SelectionLabels_Days.Add(Label_DaySel_Mon);
			SelectionLabels_Days.Add(Label_DaySel_Tue);
			SelectionLabels_Days.Add(Label_DaySel_Wed);
			SelectionLabels_Days.Add(Label_DaySel_Thu);
			SelectionLabels_Days.Add(Label_DaySel_Fri);
			SelectionLabels_Days.Add(Label_DaySel_Sat);
			SelectionLabels_Days.Add(Label_DaySel_Sun);

			SelectionLabels_Weeks.Add(Label_WeekSel_1st);
			SelectionLabels_Weeks.Add(Label_WeekSel_2nd);
			SelectionLabels_Weeks.Add(Label_WeekSel_3rd);
			SelectionLabels_Weeks.Add(Label_WeekSel_Last);

			SelectionLabels_Months.Add(Label_MonthSel_Jan);
			SelectionLabels_Months.Add(Label_MonthSel_Feb);
			SelectionLabels_Months.Add(Label_MonthSel_Mar);
			SelectionLabels_Months.Add(Label_MonthSel_Apr);
			SelectionLabels_Months.Add(Label_MonthSel_May);
			SelectionLabels_Months.Add(Label_MonthSel_Jun);
			SelectionLabels_Months.Add(Label_MonthSel_Jul);
			SelectionLabels_Months.Add(Label_MonthSel_Aug);
			SelectionLabels_Months.Add(Label_MonthSel_Sep);
			SelectionLabels_Months.Add(Label_MonthSel_Oct);
			SelectionLabels_Months.Add(Label_MonthSel_Nov);
			SelectionLabels_Months.Add(Label_MonthSel_Dec);
		}

		public void PopulateCategories()
		{
			foreach (Task Task in MainForm.TaskArray)
			{
				if (!string.IsNullOrEmpty(Task.Group))
				{
					if (!ComboBox_Category.Items.Contains(Task.Group))
					{
						ComboBox_Category.Items.Add(Task.Group);
					}
				}
			}
			if (ComboBox_Category.Items.Count == 0)
			{
				ComboBox_Category.Items.Add("Tasks");
			}
			ComboBox_Category.SelectedIndex = 0;
		}

		public void SetSpecificDay()
		{
			Label_DaySel_Specific.Text = TimeInfo.Ordinal(DateTimePicker_StartDate.Value.Day);
		}

		private void TagLabel(Label Label, bool Tagged)
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

		// TODO: Clean this fustercluck up
		// TODO: Move to separate function in TimeInfo which reads based off of TimeInfo
		private void UpdateSummaryLabel()
		{
			string TimeInsert = "";
			if (RadioButton_Time_Specific.Checked == true)
			{
				TimeInsert = " at " + DateTimePicker_StartTime.Value.ToString("h:mm tt");
			}

			string StartYearInsert = "";
			DateTime StartDate = DateTimePicker_StartDate.Value;
			if (StartDate.Year != DateTime.Now.Year)
			{
				StartYearInsert = " " + StartDate.Year.ToString();
			}

			if (RadioButton_Type_Singular.Checked == true)
			{
				// "Scheduled for Mon, Jun 13th at 6:50 PM"
				// "Scheduled for Mon, Jun 13th 2018 at 6:50 PM"
				// "Scheduled for Mon, Jun 13th"
				Label_Summary.Text = String.Format(
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
				if (RadioButton_Ends_Never.Checked == true)
				{
					EndsCount = 2; // make it > 1 so that we treat it as "every" instead of "once"
					EndsInsert = " forever";
				}
				else if (RadioButton_Ends_Occurences.Checked == true)
				{
					EndsCount = (int)NumericUpDown_Ends_Occurences.Value;
					if (EndsCount > 1)
					{
						EndsInsert = " for " + EndsCount.ToString() + " occurences";
					}

				}
				else if (RadioButton_Ends_OnDate.Checked == true)
				{
					EndsCount = 2; // make it > 1 so that we treat it as "every" instead of "once"
					string EndsYearInsert = "";
					DateTime EndDate = DateTimePicker_EndDate.Value;
					if (EndDate.Year != DateTime.Now.Year)
					{
						EndsYearInsert = " " + EndDate.Year.ToString();
					}

					EndsInsert = String.Format(
						" until {0} {1}{2}",
						EndDate.ToString("ddd, MMM"),
						TimeInfo.Ordinal(EndDate.Day),
						EndsYearInsert
					);
				}

				if (RadioButton_Type_RepeatSimple.Checked == true)
				{
					// "Repeats every 3 days from Jun 13th for 13 occurences"
					// "Repeats every 2 weeks from Jun 13th forever"
					// "Repeats every 2 weeks from Jun 13th until Jun 13th, 2017"
					// "Repeats 2 weeks from Jun 13th at 6:00 PM"
					string IncrementInsert = "";
					int IncrementValue = (int)NumericUpDown_Type_RepeatSimple.Value;
					switch (ComboBox_RepeatSimple.SelectedIndex)
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
					else if (EndsCount <= 1)
					{
						IncrementInsert = "in 1 " + IncrementInsert;
					}


					string EveryOrOnce = "once";//"Repeats {0} {1} from now";
					if (EndsCount > 1)
					{
						EveryOrOnce = "every";
					}

					Label_Summary.Text = String.Format(
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
				else if (RadioButton_Type_RepeatCustom.Checked == true)
				{
					// Repeats on Mon, Tue, and Fri in the 2nd and 3rd weeks of May, Jun, and Jul for 12 occurences
					// Repeats on Mon in Jun and Jul for 12 occurences
					// Repeats once on the first Mon, Tue, or Wed in the 2nd week of any month
					// Repeats on Mon and Tue in the last week of every month until Jun 13th, 2018
					string Format, Preposition, AllMonths, AllDays;
					if (EndsCount > 1)
					{
						//"Repeats on <DAYS> in (WEEKS OF) <EVERY MONTHS> <ENDING>"
						Format = "Repeats on {0} in {1}{2}{3}";
						Preposition = " and ";
						AllMonths = "every month";
						AllDays = "everyday";
					}
					else
					{
						//"Repeats once on the first <DAYS> in (WEEKS OF) <ANY MONTHS> <ENDING>"
						Format = "Repeats once on the first {0} in {1}{2}{3}";
						Preposition = " or ";
						AllMonths = "any month";
						AllDays = "weekday";
					}

					string DayString;
					if (Label_DaySel_Specific.Tag != null)
					{
						if (EndsCount > 1)
						{
							DayString = "the " + Label_DaySel_Specific.Text;
						}
						else
						{
							DayString = Label_DaySel_Specific.Text;
						}
					}
					else
					{
						List<string> Days = new List<string>();
						if (Label_DaySel_Mon.Tag != null) { Days.Add("Mon"); }
						if (Label_DaySel_Tue.Tag != null) { Days.Add("Tue"); }
						if (Label_DaySel_Wed.Tag != null) { Days.Add("Wed"); }
						if (Label_DaySel_Thu.Tag != null) { Days.Add("Thu"); }
						if (Label_DaySel_Fri.Tag != null) { Days.Add("Fri"); }
						if (Label_DaySel_Sat.Tag != null) { Days.Add("Sat"); }
						if (Label_DaySel_Sun.Tag != null) { Days.Add("Sun"); }

						if (Days.Count == 0)
						{
							Label_Summary.Text = "Can't repeat without at least one day selected";
							return;
						}
						else if (Days.Count == 7)
						{
							DayString = AllDays;
						}
						else
						{
							DayString = String.Join(", ", Days);
							int Index = DayString.LastIndexOf(", ");

							if (Index != -1)
							{
								DayString = DayString.Remove(Index, 2).Insert(Index, Preposition);
							}
						}
					}

					string WeekString;
					List<string> Weeks = new List<string>();
					if (Label_WeekSel_1st.Tag != null) { Weeks.Add("1st"); }
					if (Label_WeekSel_2nd.Tag != null) { Weeks.Add("2nd"); }
					if (Label_WeekSel_3rd.Tag != null) { Weeks.Add("3rd"); }
					if (Label_WeekSel_Last.Tag != null) { Weeks.Add("last"); }

					if (Weeks.Count == 0)
					{
						Label_Summary.Text = "Can't repeat without at least one week selected";
						return;
					}
					else if (Weeks.Count == 4)
					{
						WeekString = "";
					}
					else
					{
						WeekString = String.Join(", ", Weeks);
						int Index = WeekString.LastIndexOf(", ");

						if (Index != -1)
						{
							WeekString = WeekString.Remove(Index, 2).Insert(Index, Preposition);
						}

						if (Weeks.Count > 1)
						{
							WeekString = "the " + WeekString + " weeks of ";
						}
						else
						{
							WeekString = "the " + WeekString + " week of ";
						}
					}

					string MonthString;
					List<string> Months = new List<string>();
					if (Label_MonthSel_Jan.Tag != null) { Months.Add("Jan"); }
					if (Label_MonthSel_Feb.Tag != null) { Months.Add("Feb"); }
					if (Label_MonthSel_Mar.Tag != null) { Months.Add("Mar"); }
					if (Label_MonthSel_Apr.Tag != null) { Months.Add("Apr"); }
					if (Label_MonthSel_May.Tag != null) { Months.Add("May"); }
					if (Label_MonthSel_Jun.Tag != null) { Months.Add("Jun"); }
					if (Label_MonthSel_Jul.Tag != null) { Months.Add("Jul"); }
					if (Label_MonthSel_Aug.Tag != null) { Months.Add("Aug"); }
					if (Label_MonthSel_Sep.Tag != null) { Months.Add("Sep"); }
					if (Label_MonthSel_Oct.Tag != null) { Months.Add("Oct"); }
					if (Label_MonthSel_Nov.Tag != null) { Months.Add("Nov"); }
					if (Label_MonthSel_Dec.Tag != null) { Months.Add("Dec"); }

					if (Months.Count == 0)
					{
						Label_Summary.Text = "Can't repeat without at least one month selected";
						return;
					}
					else if (Months.Count == 12)
					{
						MonthString = AllMonths;
					}
					else
					{
						MonthString = String.Join(", ", Months);
						int Index = MonthString.LastIndexOf(", ");

						if (Index != -1)
						{
							MonthString = MonthString.Remove(Index, 2).Insert(Index, Preposition);
						}
					}

					Label_Summary.Text = String.Format(
						Format,
						DayString,
						WeekString,
						MonthString,
						EndsInsert
					);
				}
			}
		}

		public Form_Create(Form_Main PassedForm)
		{
			// Treat as normal "Create" dialog
			InitializeComponent();
			MainForm = PassedForm;
			EditMode = false;
			SelectGroupTextBox = false;

			// Common setup
			CommonSetup();

			// Categories
			PopulateCategories();

			DateTimePicker_StartDate.MinDate = DateTime.Now;

			DateTime BaseTime = DateTime.Now.AddMinutes(15);
			DateTime RoundedUp = new DateTime(BaseTime.Year, BaseTime.Month, BaseTime.Day, BaseTime.Hour, 0, 0);
			if ((BaseTime.Minute > 0) || (BaseTime.Second > 0))
			{
				RoundedUp = RoundedUp.AddHours(1);
			}
			DateTimePicker_StartTime.Value = RoundedUp;

			DateTimePicker_EndDate.MinDate = DateTime.Now;
			SetSpecificDay();

			ComboBox_RepeatSimple.SelectedIndex = 0;
			ComboBox_Dismiss.SelectedIndex = 0;
			ComboBox_Priority.SelectedIndex = 1;
		}

		public Form_Create(Form_Main PassedForm, int PassedIndex, bool PassedSelectGroup)
		{
			// Treat as "Edit" dialog
			InitializeComponent();
			MainForm = PassedForm;
			TaskToEdit = MainForm.TaskArray[PassedIndex];
			EditMode = true;
			SelectGroupTextBox = PassedSelectGroup;
			Text = "Edit";
			Button_Create.Text = "Save";

			// Common setup
			CommonSetup();

			// Text fields
			TextBox_Description.Text = TaskToEdit.Description;
			Notes = TaskToEdit.Notes;
			if (!string.IsNullOrEmpty(TaskToEdit.Link))
			{
				TextBox_Link.Text = TaskToEdit.Link;
			}
			if (!string.IsNullOrEmpty(TaskToEdit.Location))
			{
				TextBox_Location.Text = TaskToEdit.Location;
			}

			// Categories
			ComboBox_Category.Items.Add(TaskToEdit.Group);
			PopulateCategories();
			ComboBox_Category.Select();

			// Priority
			ComboBox_Priority.SelectedIndex = TaskToEdit.Priority;

			// Scheduled
			CheckBox_Scheduled.Checked = TimeInfo.Scheduled(TaskToEdit.Time);

			// Date and Time
			DateTimePicker_StartDate.MinDate = DateTime.Now;
			if (TaskToEdit.Time.Next > DateTime.Now)
			{
				DateTimePicker_StartDate.Value = TaskToEdit.Time.Next;
				DateTimePicker_StartTime.Value = TaskToEdit.Time.Next;
				if (TaskToEdit.Time.TimeOfDay != TimeSpan.FromSeconds(86399))
				{
					RadioButton_Time_AllDay.Checked = false;
					RadioButton_Time_Specific.Checked = true;
					DateTimePicker_StartTime.Enabled = true;
				}
			}
			else
			{
				DateTime BaseTime = DateTime.Now.AddMinutes(15);
				DateTime RoundedUp = new DateTime(BaseTime.Year, BaseTime.Month, BaseTime.Day, BaseTime.Hour, 0, 0);
				if ((BaseTime.Minute > 0) || (BaseTime.Second > 0))
				{
					RoundedUp = RoundedUp.AddHours(1);
				}
				DateTimePicker_StartTime.Value = RoundedUp;
			}
			DateTimePicker_EndDate.MinDate = DateTime.Now;
			SetSpecificDay();

			// Dismiss
			ComboBox_Dismiss.SelectedIndex = (int)TaskToEdit.Time.Dismiss;

			// Repeating
			switch (TimeInfo.GetRepeatType(TaskToEdit.Time))
			{
				case TimeInfo.RepeatType.ComplexRepeat:
					{
						Panel_SelectionLabels.Visible = true;
						Panel_Ends.Visible = true;
						RadioButton_Type_RepeatCustom.Checked = true;
						RadioButton_Type_RepeatSimple.Checked = false;
						RadioButton_Type_Singular.Checked = false;
						NumericUpDown_Type_RepeatSimple.Enabled = false;
						ComboBox_RepeatSimple.Enabled = false;
						ComboBox_RepeatSimple.SelectedIndex = 0;

						// DAYS
						if (TaskToEdit.Time.SpecificDay != 0)
						{
							TagLabel(Label_DaySel_Specific, true);
						}
						else if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Everyday) == TimeInfo.DayFlag.Everyday)
						{
							foreach (Label SelectionLabel in SelectionLabels_Days)
							{
								TagLabel(SelectionLabel, true);
							}
							CheckSelected(SelectionLabels_Days, Label_DaySel_All);
						}
						else
						{
							if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Monday) != 0) { TagLabel(Label_DaySel_Mon, true); }
							if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Tuesday) != 0) { TagLabel(Label_DaySel_Tue, true); }
							if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Wednesday) != 0) { TagLabel(Label_DaySel_Wed, true); }
							if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Thursday) != 0) { TagLabel(Label_DaySel_Thu, true); }
							if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Friday) != 0) { TagLabel(Label_DaySel_Fri, true); }
							if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Saturday) != 0) { TagLabel(Label_DaySel_Sat, true); }
							if ((TaskToEdit.Time.DayFilter & TimeInfo.DayFlag.Sunday) != 0) { TagLabel(Label_DaySel_Sun, true); }
						}

						// WEEKS
						if ((TaskToEdit.Time.WeekFilter & TimeInfo.WeekFlag.FirstThruLast) == TimeInfo.WeekFlag.FirstThruLast)
						{
							TagLabel(Label_WeekSel_1st, true);
							TagLabel(Label_WeekSel_2nd, true);
							TagLabel(Label_WeekSel_3rd, true);
							TagLabel(Label_WeekSel_Last, true);
							CheckSelected(SelectionLabels_Weeks, Label_WeekSel_All);
						}
						else
						{
							if ((TaskToEdit.Time.WeekFilter & TimeInfo.WeekFlag.First) != 0) { TagLabel(Label_WeekSel_1st, true); }
							if ((TaskToEdit.Time.WeekFilter & TimeInfo.WeekFlag.Second) != 0) { TagLabel(Label_WeekSel_2nd, true); }
							if ((TaskToEdit.Time.WeekFilter & TimeInfo.WeekFlag.Third) != 0) { TagLabel(Label_WeekSel_3rd, true); }
							if ((TaskToEdit.Time.WeekFilter & TimeInfo.WeekFlag.Last) != 0) { TagLabel(Label_WeekSel_Last, true); }
						}

						// MONTHS
						if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.Everymonth) == TimeInfo.MonthFlag.Everymonth)
						{
							foreach (Label SelectionLabel in SelectionLabels_Months)
							{
								TagLabel(SelectionLabel, true);
							}
							CheckSelected(SelectionLabels_Months, Label_MonthSel_All);
						}
						else
						{
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.January) != 0) { TagLabel(Label_MonthSel_Jan, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.February) != 0) { TagLabel(Label_MonthSel_Feb, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.March) != 0) { TagLabel(Label_MonthSel_Mar, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.April) != 0) { TagLabel(Label_MonthSel_Apr, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.May) != 0) { TagLabel(Label_MonthSel_May, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.June) != 0) { TagLabel(Label_MonthSel_Jun, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.July) != 0) { TagLabel(Label_MonthSel_Jul, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.August) != 0) { TagLabel(Label_MonthSel_Aug, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.September) != 0) { TagLabel(Label_MonthSel_Sep, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.October) != 0) { TagLabel(Label_MonthSel_Oct, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.November) != 0) { TagLabel(Label_MonthSel_Nov, true); }
							if ((TaskToEdit.Time.MonthFilter & TimeInfo.MonthFlag.December) != 0) { TagLabel(Label_MonthSel_Dec, true); }
						}

						break;
					}

				case TimeInfo.RepeatType.SimpleRepeat:
					{
						Panel_SelectionLabels.Visible = false;
						Panel_Ends.Visible = true;
						RadioButton_Type_RepeatCustom.Checked = false;
						RadioButton_Type_RepeatSimple.Checked = true;
						RadioButton_Type_Singular.Checked = false;
						NumericUpDown_Type_RepeatSimple.Enabled = true;
						ComboBox_RepeatSimple.Enabled = true;
						if (TaskToEdit.Time.Daily != 0)
						{
							NumericUpDown_Type_RepeatSimple.Value = TaskToEdit.Time.Daily;
							ComboBox_RepeatSimple.SelectedIndex = 0;
						}
						else if (TaskToEdit.Time.Weekly != 0)
						{
							NumericUpDown_Type_RepeatSimple.Value = TaskToEdit.Time.Weekly;
							ComboBox_RepeatSimple.SelectedIndex = 1;
						}
						else if (TaskToEdit.Time.Monthly != 0)
						{
							NumericUpDown_Type_RepeatSimple.Value = TaskToEdit.Time.Monthly;
							ComboBox_RepeatSimple.SelectedIndex = 3;
						}
						else // Must be yearly (TaskToEdit.Time.Yearly != 0)
						{
							NumericUpDown_Type_RepeatSimple.Value = TaskToEdit.Time.Yearly;
							ComboBox_RepeatSimple.SelectedIndex = 3;
						}
						break;
					}

				case TimeInfo.RepeatType.Singular:
					{
						Panel_SelectionLabels.Visible = false;
						Panel_Ends.Visible = false;
						RadioButton_Type_RepeatCustom.Checked = false;
						RadioButton_Type_RepeatSimple.Checked = false;
						RadioButton_Type_Singular.Checked = true;
						NumericUpDown_Type_RepeatSimple.Enabled = false;
						ComboBox_RepeatSimple.Enabled = false;
						ComboBox_RepeatSimple.SelectedIndex = 0;
						break;
					}
			}

			// Ends
			if (TaskToEdit.Time.End != DateTime.MinValue)
			{
				RadioButton_Ends_Never.Checked = false;
				RadioButton_Ends_Occurences.Checked = false;
				RadioButton_Ends_OnDate.Checked = true;
				NumericUpDown_Ends_Occurences.Enabled = false;
				DateTimePicker_EndDate.Enabled = true;

				DateTimePicker_EndDate.Value = TaskToEdit.Time.End;
			}
			else if (TaskToEdit.Time.Iterations != 0)
			{
				RadioButton_Ends_Never.Checked = false;
				RadioButton_Ends_Occurences.Checked = true;
				RadioButton_Ends_OnDate.Checked = false;
				NumericUpDown_Ends_Occurences.Enabled = true;
				DateTimePicker_EndDate.Enabled = false;

				NumericUpDown_Ends_Occurences.Value = TaskToEdit.Time.Iterations;
			}
			else
			{
				RadioButton_Ends_Never.Checked = true;
				RadioButton_Ends_Occurences.Checked = false;
				RadioButton_Ends_OnDate.Checked = false;
				NumericUpDown_Ends_Occurences.Enabled = false;
				DateTimePicker_EndDate.Enabled = false;
			}
		}

		private void FormLoad(object Sender, EventArgs Args)
		{
			CheckGroupBoxSize();
			UpdateSummaryLabel();
		}

		private void TextBox_Description_TextChanged(object Sender, EventArgs Args)
		{
			// TODO: Parse this text to make sure we don't get any unwanted characters
			// Note: This is probably the wrong eventhandler for this... we might need keydown or such
		}

		private void TextBox_Link_TextChanged(object Sender, EventArgs Args)
		{
			// TODO: Parse this text to make sure we don't get any unwanted characters
			// Note: This is probably the wrong eventhandler for this... we might need keydown or such
		}

		private void TextBox_Location_TextChanged(object Sender, EventArgs Args)
		{
			// TODO: Parse this text to make sure we don't get any unwanted characters
			// Note: This is probably the wrong eventhandler for this... we might need keydown or such
		}

		private void ComboBox_RepeatSimple_SelectedIndexChanged(object Sender, EventArgs Args)
		{
			UpdateSummaryLabel();
		}

		private void DateTimePicker_StartDate_ValueChanged(object Sender, EventArgs Args)
		{
			// This didn't quite work as desired, instead lets just check when clicking the create 
			// button whether or not the dates are acceptable.
			/*if (DateTimePicker_StartDate.Value < DateTimePicker_StartTime.Value)
			{
				Console.WriteLine("date before time!");
			}*/
			SetSpecificDay();
			UpdateSummaryLabel();
		}

		private void DateTimePicker_StartTime_ValueChanged(object Sender, EventArgs Args)
		{
			// This didn't quite work as desired, instead lets just check when clicking the create 
			// button whether or not the dates are acceptable.
			/*DateTime Readjust = DateTimePicker_StartDate.Value;
			Readjust = Readjust - Readjust.TimeOfDay;
			Readjust = Readjust + DateTimePicker_StartTime.Value.TimeOfDay;
			if (Readjust < DateTimePicker_StartDate.MinDate)
			{
				Readjust = Readjust.AddDays(1);
			}
			DateTimePicker_StartDate.Value = Readjust;*/
			UpdateSummaryLabel();
		}

		private void DateTimePicker_EndDate_ValueChanged(object Sender, EventArgs Args)
		{
			UpdateSummaryLabel();
		}

		private void NumericUpDown_Type_RepeatSimple_ValueChanged(object Sender, EventArgs Args)
		{
			UpdateSummaryLabel();
		}

		private void NumericUpDown_Ends_Occurences_ValueChanged(object Sender, EventArgs Args)
		{
			UpdateSummaryLabel();
		}

		private void RadioButton_Time_AllDay_CheckedChanged(object Sender, EventArgs Args)
		{
			UpdateSummaryLabel();
		}

		private void RadioButton_Time_Specific_CheckedChanged(object Sender, EventArgs Args)
		{
			if (RadioButton_Time_Specific.Checked)
			{
				DateTimePicker_StartTime.Enabled = true;
			}
			else
			{
				DateTimePicker_StartTime.Enabled = false;
			}
			UpdateSummaryLabel();
		}

		private void RadioButton_Type_Singular_CheckedChanged(object Sender, EventArgs Args)
		{
			if (RadioButton_Type_Singular.Checked)
			{
				Panel_Ends.Visible = false;
			}
			else
			{
				Panel_Ends.Visible = true;
			}
			CheckGroupBoxSize();
			UpdateSummaryLabel();
		}

		private void RadioButton_Type_RepeatSimple_CheckedChanged(object Sender, EventArgs Args)
		{
			if (RadioButton_Type_RepeatSimple.Checked)
			{
				NumericUpDown_Type_RepeatSimple.Enabled = true;
				ComboBox_RepeatSimple.Enabled = true;
			}
			else
			{
				NumericUpDown_Type_RepeatSimple.Enabled = false;
				ComboBox_RepeatSimple.Enabled = false;
			}
			CheckGroupBoxSize();
			UpdateSummaryLabel();
		}

		private void RadioButton_Type_RepeatCustom_CheckedChanged(object Sender, EventArgs Args)
		{
			if (RadioButton_Type_RepeatCustom.Checked)
			{
				Panel_SelectionLabels.Visible = true;
			}
			else
			{
				Panel_SelectionLabels.Visible = false;
			}
			CheckGroupBoxSize();
			UpdateSummaryLabel();
		}

		private void RadioButton_Ends_Never_CheckedChanged(object Sender, EventArgs Args)
		{
			UpdateSummaryLabel();
		}

		private void RadioButton_Ends_Occurences_CheckedChanged(object Sender, EventArgs Args)
		{
			if (RadioButton_Ends_Occurences.Checked)
			{
				NumericUpDown_Ends_Occurences.Enabled = true;
			}
			else
			{
				NumericUpDown_Ends_Occurences.Enabled = false;
			}
			UpdateSummaryLabel();
		}

		private void RadioButton_Ends_OnDate_CheckedChanged(object Sender, EventArgs Args)
		{
			if (RadioButton_Ends_OnDate.Checked)
			{
				DateTimePicker_EndDate.Enabled = true;
			}
			else
			{
				DateTimePicker_EndDate.Enabled = false;
			}
			UpdateSummaryLabel();
		}

		private void CheckBox_Scheduled_CheckedChanged(object Sender, EventArgs Args)
		{
			if (CheckBox_Scheduled.Checked)
			{
				GroupBox_Scheduled.Enabled = true;
			}
			else
			{
				GroupBox_Scheduled.Enabled = false;
			}
			UpdateSummaryLabel();
		}

		private void Label_DaySel_Specific_Click(object Sender, EventArgs Args)
		{
			Label Label = Sender as Label;

			bool Selected = false;
			if (Label.Tag == null)
			{
				// previously not selected, lets select it
				TagLabel(Label, true);
				Selected = false;
			}
			else
			{
				// previously selected, lets unselect it
				TagLabel(Label, false);
				Selected = true;
			}

			foreach (Label SelectionLabel in SelectionLabels_Days)
			{
				TagLabel(SelectionLabel, Selected);
			}

			CheckSelected(SelectionLabels_Days, Label_DaySel_All);
			UpdateSummaryLabel();
		}

		private void Label_DaySel_All_Click(object Sender, EventArgs Args)
		{
			AllNoneSelection((Label)Sender, SelectionLabels_Days);
			TagLabel(Label_DaySel_Specific, false);
			UpdateSummaryLabel();
		}

		private void Label_WeekSel_All_Click(object Sender, EventArgs Args)
		{
			AllNoneSelection((Label)Sender, SelectionLabels_Weeks);
			UpdateSummaryLabel();
		}

		private void Label_MonthSel_All_Click(object Sender, EventArgs Args)
		{
			AllNoneSelection((Label)Sender, SelectionLabels_Months);
			UpdateSummaryLabel();
		}

		private void Label_DaySel_ClickHandler(object Sender, EventArgs Args)
		{
			Label Label = Sender as Label;

			if (Label.Tag == null)
			{
				// previously not selected, lets select it
				TagLabel(Label, true);
				TagLabel(Label_DaySel_Specific, false);
				CheckSelected(SelectionLabels_Days, Label_DaySel_All);
			}
			else
			{
				// previously selected, lets unselect it
				TagLabel(Label, false);
				Label_DaySel_All.Text = "All";
			}

			UpdateSummaryLabel();
		}

		private void Label_WeekSel_ClickHandler(object Sender, EventArgs Args)
		{
			Label Label = Sender as Label;

			if (Label.Tag == null)
			{
				// previously not selected, lets select it
				TagLabel(Label, true);
				CheckSelected(SelectionLabels_Weeks, Label_WeekSel_All);
			}
			else
			{
				// previously selected, lets unselect it
				TagLabel(Label, false);
				Label_WeekSel_All.Text = "All";
			}

			UpdateSummaryLabel();
		}

		private void Label_MonthSel_ClickHandler(object Sender, EventArgs Args)
		{
			Label Label = Sender as Label;

			if (Label.Tag == null)
			{
				// previously not selected, lets select it
				TagLabel(Label, true);
				CheckSelected(SelectionLabels_Months, Label_MonthSel_All);
			}
			else
			{
				// previously selected, lets unselect it
				TagLabel(Label, false);
				Label_MonthSel_All.Text = "All";
			}

			UpdateSummaryLabel();
		}

		private void Button_Notes_Click(object Sender, EventArgs Args)
		{
			Form_Notes NotesForm = new Form_Notes(MainForm, this);
			NotesForm.ShowDialog(this);
		}

		private void Button_Create_Click(object Sender, EventArgs Args)
		{
			Task Task = new Task();

			Task.Description = TextBox_Description.Text;

			if (string.IsNullOrWhiteSpace(Task.Description) || (Task.Description == "Description"))
			{ 
				Console.WriteLine("Button_Create_Click(): Description cannot be blank!");
				MessageBox.Show(
					"Task description cannot be blank",
					"Tasual",
					MessageBoxButtons.OK,
					MessageBoxIcon.Asterisk
				);
				return;
			}

			Task.Priority = ComboBox_Priority.SelectedIndex;
			Task.Group = ComboBox_Category.Text;
			Task.Notes = Notes;
			if (TextBox_Link.Text != "Link")
			{
				Task.Link = TextBox_Link.Text;
			}

			if (TextBox_Location.Text != "Location")
			{
				Task.Location = TextBox_Location.Text;
			}

			TimeInfo TimeInfo = new TimeInfo();
			TimeInfo.Created = DateTime.Now; // TODO: Should we check EditMode here and use the old date?
			TimeInfo.Modified = DateTime.Now;

			if (CheckBox_Scheduled.Checked)
			{
				TimeInfo.Start = DateTimePicker_StartDate.Value;
				TimeInfo.Start = TimeInfo.Start - TimeInfo.Start.TimeOfDay;

				if (RadioButton_Time_AllDay.Checked)
				{
					TimeInfo.TimeOfDay = TimeSpan.FromSeconds(86399);
				}
				else
				{
					TimeInfo.TimeOfDay = DateTimePicker_StartTime.Value.TimeOfDay;
				}

				TimeInfo.Start = TimeInfo.Start + TimeInfo.TimeOfDay;

				if (DateTime.Now >= TimeInfo.Start)
				{
					// Picked time is before current time
					// Show warning message and cancel edit/creation
					Console.WriteLine("Button_Create_Click(): Start time cannot be before current time!");
					MessageBox.Show(
						"Start time cannot be in the past",
						"Tasual",
						MessageBoxButtons.OK,
						MessageBoxIcon.Asterisk
					);
					return;
				}

				TimeInfo.Dismiss = (TimeInfo.DismissType)ComboBox_Dismiss.SelectedIndex;

				if (RadioButton_Type_Singular.Checked == false)
				{
					if (RadioButton_Type_RepeatSimple.Checked == true)
					{
						int RepeatValue = (int)NumericUpDown_Type_RepeatSimple.Value;
						switch (ComboBox_RepeatSimple.SelectedIndex)
						{
							case 0: { TimeInfo.Daily = RepeatValue; break; }
							case 1: { TimeInfo.Weekly = RepeatValue; break; }
							case 2: { TimeInfo.Monthly = RepeatValue; break; }
							case 3: { TimeInfo.Yearly = RepeatValue; break; }
						}
					}
					else if (RadioButton_Type_RepeatCustom.Checked == true)
					{
						// day filters
						if (Label_DaySel_Specific.Tag != null)
						{
							TimeInfo.SpecificDay = DateTimePicker_StartDate.Value.Day;
						}
						else
						{
							if (Label_DaySel_Mon.Tag != null) { TimeInfo.DayFilter |= TimeInfo.DayFlag.Monday; }
							if (Label_DaySel_Tue.Tag != null) { TimeInfo.DayFilter |= TimeInfo.DayFlag.Tuesday; }
							if (Label_DaySel_Wed.Tag != null) { TimeInfo.DayFilter |= TimeInfo.DayFlag.Wednesday; }
							if (Label_DaySel_Thu.Tag != null) { TimeInfo.DayFilter |= TimeInfo.DayFlag.Thursday; }
							if (Label_DaySel_Fri.Tag != null) { TimeInfo.DayFilter |= TimeInfo.DayFlag.Friday; }
							if (Label_DaySel_Sat.Tag != null) { TimeInfo.DayFilter |= TimeInfo.DayFlag.Saturday; }
							if (Label_DaySel_Sun.Tag != null) { TimeInfo.DayFilter |= TimeInfo.DayFlag.Sunday; }
						}

						if (Label_WeekSel_1st.Tag != null) { TimeInfo.WeekFilter |= TimeInfo.WeekFlag.First; }
						if (Label_WeekSel_2nd.Tag != null) { TimeInfo.WeekFilter |= TimeInfo.WeekFlag.Second; }
						if (Label_WeekSel_3rd.Tag != null) { TimeInfo.WeekFilter |= TimeInfo.WeekFlag.Third; }
						if (Label_WeekSel_Last.Tag != null) { TimeInfo.WeekFilter |= TimeInfo.WeekFlag.Last; }

						if (Label_MonthSel_Jan.Tag != null) { TimeInfo.MonthFilter |= TimeInfo.MonthFlag.January; }
						if (Label_MonthSel_Feb.Tag != null) { TimeInfo.MonthFilter |= TimeInfo.MonthFlag.February; }
						if (Label_MonthSel_Mar.Tag != null) { TimeInfo.MonthFilter |= TimeInfo.MonthFlag.March; }
						if (Label_MonthSel_Apr.Tag != null) { TimeInfo.MonthFilter |= TimeInfo.MonthFlag.April; }
						if (Label_MonthSel_May.Tag != null) { TimeInfo.MonthFilter |= TimeInfo.MonthFlag.May; }
						if (Label_MonthSel_Jun.Tag != null) { TimeInfo.MonthFilter |= TimeInfo.MonthFlag.June; }
						if (Label_MonthSel_Jul.Tag != null) { TimeInfo.MonthFilter |= TimeInfo.MonthFlag.July; }
						if (Label_MonthSel_Aug.Tag != null) { TimeInfo.MonthFilter |= TimeInfo.MonthFlag.August; }
						if (Label_MonthSel_Sep.Tag != null) { TimeInfo.MonthFilter |= TimeInfo.MonthFlag.September; }
						if (Label_MonthSel_Oct.Tag != null) { TimeInfo.MonthFilter |= TimeInfo.MonthFlag.October; }
						if (Label_MonthSel_Nov.Tag != null) { TimeInfo.MonthFilter |= TimeInfo.MonthFlag.November; }
						if (Label_MonthSel_Dec.Tag != null) { TimeInfo.MonthFilter |= TimeInfo.MonthFlag.December; }
					}
				}

				if (RadioButton_Ends_Occurences.Checked == true)
				{
					TimeInfo.Iterations = (int)NumericUpDown_Ends_Occurences.Value;
				}
				else if (RadioButton_Ends_OnDate.Checked == true)
				{
					TimeInfo.End = DateTimePicker_EndDate.Value;
					if (DateTime.Now >= TimeInfo.End)
					{
						// Picked time is before current time
						// Show warning message and cancel edit/creation
						Console.WriteLine("Button_Create_Click(): End time cannot be before current time!");
						MessageBox.Show(
							"End time cannot be in the past",
							"Tasual",
							MessageBoxButtons.OK,
							MessageBoxIcon.Asterisk
						);
						return;
					}
				}
			}

			DateTime Next = TimeInfo.FindNextIteration(TimeInfo);
			//int Count = TimeInfo.FindIterationCount(ref TimeInfo);
			if (Next != DateTime.MinValue)
			{
				TimeInfo.Next = Next;
			}
			else // our next iteration is the start iteration
			{
				TimeInfo.Next = TimeInfo.Start;
			}

			TimeInfo.Summary = Label_Summary.Text;

			Task.Time = TimeInfo;

			if (EditMode)
			{
				MainForm.TaskArray.Remove(TaskToEdit);
			}

			MainForm.TaskArray.Add(Task);
			MainForm.Array_Save();
			MainForm.UpdateGroupKeys(Task);
			MainForm.CheckCollapsedGroup(Task);
			MainForm.ListView.BuildList();
			MainForm.ListView.EnsureModelVisible(Task);
			MainForm.ListView.SelectObject(Task);
			MainForm.UpdateStatusLabel();

			Close();
		}

		private void Button_Cancel_Click(object Sender, EventArgs Args)
		{
			Close();
		}

		private void TextBox_Description_CheckColor()
		{
			if (TextBox_Description.Text == "Description")
			{
				TextBox_Description.ForeColor = Color.Gray;
			}
			else
			{
				TextBox_Description.ForeColor = Color.Black;
			}
		}

		private void TextBox_Link_CheckColor()
		{
			if (TextBox_Link.Text == "Link")
			{
				TextBox_Link.ForeColor = Color.Gray;
			}
			else
			{
				TextBox_Link.ForeColor = Color.FromArgb(255, 36, 90, 150);
			}
		}

		private void TextBox_Location_CheckColor()
		{
			if (TextBox_Location.Text == "Location")
			{
				TextBox_Location.ForeColor = Color.Gray;
			}
			else
			{
				TextBox_Location.ForeColor = Color.FromArgb(255, 36, 120, 90);
			}
		}

		private void TextBox_Link_Enter(object Sender, EventArgs Args)
		{
			if (TextBox_Link.Text == "Link")
			{
				TextBox_Link.Text = "";
			}
			TextBox_Link_CheckColor();
		}

		private void TextBox_Link_Leave(object Sender, EventArgs Args)
		{
			if (TextBox_Link.Text == "")
			{
				TextBox_Link.Text = "Link";
			}
			TextBox_Link_CheckColor();
		}

		private void TextBox_Location_Enter(object Sender, EventArgs Args)
		{
			if (TextBox_Location.Text == "Location")
			{
				TextBox_Location.Text = "";
			}
			TextBox_Location_CheckColor();
		}

		private void TextBox_Location_Leave(object Sender, EventArgs Args)
		{
			if (TextBox_Location.Text == "")
			{
				TextBox_Location.Text = "Location";
			}
			TextBox_Location_CheckColor();
		}

		private void TextBox_Description_Enter(object Sender, EventArgs Args)
		{
			if (TextBox_Description.Text == "Description")
			{
				TextBox_Description.Text = "";
			}
			TextBox_Description_CheckColor();
		}

		private void TextBox_Description_Leave(object Sender, EventArgs Args)
		{
			if (TextBox_Description.Text == "")
			{
				TextBox_Description.Text = "Description";
			}
			TextBox_Description_CheckColor();
		}
	}
}
