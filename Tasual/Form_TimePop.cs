// ===========================================
//  Copyright (c) 2017 Bryan Kruman
//
//  See LICENSE.txt file in the project root
//  for full license information.
// ===========================================

using System;
using System.Windows.Forms;

namespace Tasual
{
	public partial class Form_TimePop : Form
	{
		private readonly Form_Main MainForm;
		private readonly Task _Task;

		private void CheckEnableStatus()
		{
			TimeInfo.RepeatType RepeatType = TimeInfo.GetRepeatType(_Task.Time);
			if (CheckBox.Checked)
			{
				switch (RepeatType)
				{
					case TimeInfo.RepeatType.ComplexRepeat:
					case TimeInfo.RepeatType.SimpleRepeat:
						{
							CheckBox.Enabled = false; // TODO: Allow scheduling to be cancelled from here
							Calendar.Enabled = false;
							RadioButton_AllDay.Enabled = false;
							RadioButton_Specific.Enabled = false;
							DateTimePicker.Enabled = false;
							Button_Save.Enabled = false;
							Label_CantEdit.Visible = true;
							break;
						}
					case TimeInfo.RepeatType.Singular:
						{
							Calendar.Enabled = true;
							Button_Save.Enabled = true;
							RadioButton_AllDay.Enabled = true;
							RadioButton_Specific.Enabled = true;
							Label_CantEdit.Visible = false;
							if (RadioButton_Specific.Checked)
							{
								DateTimePicker.Enabled = true;
							}
							else
							{
								DateTimePicker.Enabled = false;
							}
							break;
						}
				}
			}
			else
			{
				Calendar.Enabled = false;
				RadioButton_AllDay.Enabled = false;
				RadioButton_Specific.Enabled = false;
				DateTimePicker.Enabled = false;

				if (RepeatType == TimeInfo.RepeatType.Singular)
				{
					Button_Save.Enabled = true;
					Label_CantEdit.Visible = false;
				}
				else
				{
					Button_Save.Enabled = false;
				}
			}
		}

		public Form_TimePop(Form_Main PassedForm, int PassedIndex)
		{
			InitializeComponent();
			MainForm = PassedForm;
			_Task = MainForm.TaskArray[PassedIndex];

			Calendar.MinDate = DateTime.Now;

			DateTime NextOrNow = new DateTime(Math.Max(_Task.Time.Next.Ticks, DateTime.Now.Ticks));
			Calendar.SetDate(NextOrNow);

			if (TimeInfo.Scheduled(_Task.Time))
			{
				CheckBox.Checked = true;
				if (_Task.Time.TimeOfDay == TimeSpan.FromSeconds(86399))
				{
					RadioButton_AllDay.Checked = true;
					RadioButton_Specific.Checked = false;
					DateTimePicker.Value = TimeInfo.PickRoundedUpTime(DateTime.Now);
				}
				else
				{
					RadioButton_AllDay.Checked = false;
					RadioButton_Specific.Checked = true;
					DateTimePicker.Value = NextOrNow;
				}
			}
			else
			{
				CheckBox.Checked = false;
				RadioButton_AllDay.Checked = true;
				RadioButton_Specific.Checked = false;
				DateTimePicker.Value = TimeInfo.PickRoundedUpTime(DateTime.Now);
			}

			CheckEnableStatus();
		}

		private void Tasual_CalendarPopout_Deactivate(object Sender, EventArgs Args)
		{
			this.Close();
		}

		private void CheckBox_CheckedChanged(object Sender, EventArgs Args)
		{
			CheckEnableStatus();
		}

		private void RadioButton_AllDay_CheckedChanged(object Sender, EventArgs Args)
		{
			CheckEnableStatus();
		}

		private void RadioButton_Specific_CheckedChanged(object Sender, EventArgs Args)
		{
			CheckEnableStatus();
		}

		private void LinkLabel_LinkClicked(object Sender, LinkLabelLinkClickedEventArgs Args)
		{
			Form_Create CreateForm = new Form_Create(MainForm, MainForm.TaskArray.IndexOf(_Task));
			CreateForm.ShowDialog(MainForm);
			Close();
		}

		private void Button_Save_Click(object Sender, EventArgs Args)
		{
			DateTime NewTime = new DateTime();
			NewTime = Calendar.SelectionStart;

			TimeSpan TimeOfDay = new TimeSpan();

			Console.WriteLine("DateTime: {0} - {1}", NewTime.ToShortDateString(), NewTime.ToLongTimeString());

			NewTime = NewTime - NewTime.TimeOfDay;

			if (CheckBox.Checked)
			{
				if (RadioButton_Specific.Checked)
				{
					NewTime = NewTime + DateTimePicker.Value.TimeOfDay;
					TimeOfDay = DateTimePicker.Value.TimeOfDay;
					//_Task.Time.TimeOfDay = DateTimePicker.Value.TimeOfDay;
				}
				else
				{
					NewTime = NewTime + TimeSpan.FromSeconds(86399);
					TimeOfDay = TimeSpan.FromSeconds(86399);
					//_Task.Time.TimeOfDay = TimeSpan.FromSeconds(86399);
				}

				if (NewTime < DateTime.Now)
				{
					Console.WriteLine("Can't have a date that is before now!");
					MessageBox.Show(MainForm,
						"Scheduled time cannot be in the past",
						"Tasual",
						MessageBoxButtons.OK,
						MessageBoxIcon.Asterisk
					);
					return;
				}
				else
				{
					_Task.Time.Modified = DateTime.Now;
					_Task.Time.Next = NewTime;
					_Task.Time.TimeOfDay = TimeOfDay;
				}
			}
			else
			{
				_Task.Time = new TimeInfo(
					DateTime.MinValue,
					_Task.Time.Created,
					DateTime.Now,
					DateTime.MinValue,
					DateTime.MinValue,
					DateTime.MinValue);
			}

			MainForm.Array_Save();
			MainForm.UpdateGroupKeys(_Task);
			MainForm.ListView.BuildList();
			MainForm.ListView.EnsureModelVisible(_Task);
			Close();
		}

		private void Button_Cancel_Click(object Sender, EventArgs Args)
		{
			Close();
		}
	}
}
