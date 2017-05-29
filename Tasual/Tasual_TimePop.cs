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
	public partial class Tasual_TimePop : Form
	{
		private readonly Tasual_Main _Tasual_Main;
		private readonly Task _Task;

		private void Tasual_TimePop_CheckEnableStatus()
		{
			TimeInfo.RepeatType RepeatType = TimeInfo.GetRepeatType(_Task.Time);
			if (Tasual_TimePop_CheckBox.Checked)
			{
				switch (RepeatType)
				{
					case TimeInfo.RepeatType.ComplexRepeat:
					case TimeInfo.RepeatType.SimpleRepeat:
						{
							Tasual_TimePop_CheckBox.Enabled = false; // TODO: Allow scheduling to be cancelled from here
							Tasual_TimePop_Calendar.Enabled = false;
							Tasual_TimePop_RadioButton_AllDay.Enabled = false;
							Tasual_TimePop_RadioButton_Specific.Enabled = false;
							Tasual_TimePop_DateTimePicker.Enabled = false;
							Tasual_TimePop_Button_Save.Enabled = false;
							Tasual_TimePop_Label_CantEdit.Visible = true;
							break;
						}
					case TimeInfo.RepeatType.Singular:
						{
							Tasual_TimePop_Calendar.Enabled = true;
							Tasual_TimePop_Button_Save.Enabled = true;
							Tasual_TimePop_RadioButton_AllDay.Enabled = true;
							Tasual_TimePop_RadioButton_Specific.Enabled = true;
							Tasual_TimePop_Label_CantEdit.Visible = false;
							if (Tasual_TimePop_RadioButton_Specific.Checked)
							{
								Tasual_TimePop_DateTimePicker.Enabled = true;
							}
							else
							{
								Tasual_TimePop_DateTimePicker.Enabled = false;
							}
							break;
						}
				}
			}
			else
			{
				Tasual_TimePop_Calendar.Enabled = false;
				Tasual_TimePop_RadioButton_AllDay.Enabled = false;
				Tasual_TimePop_RadioButton_Specific.Enabled = false;
				Tasual_TimePop_DateTimePicker.Enabled = false;

				if (RepeatType == TimeInfo.RepeatType.Singular)
				{
					Tasual_TimePop_Button_Save.Enabled = true;
					Tasual_TimePop_Label_CantEdit.Visible = false;
				}
				else
				{
					Tasual_TimePop_Button_Save.Enabled = false;
				}
			}
		}

		public Tasual_TimePop(Tasual_Main PassedForm, int PassedIndex)
		{
			InitializeComponent();
			_Tasual_Main = PassedForm;
			_Task = _Tasual_Main.TaskArray[PassedIndex];

			Tasual_TimePop_Calendar.MinDate = DateTime.Now;

			DateTime NextOrNow = new DateTime(Math.Max(_Task.Time.Next.Ticks, DateTime.Now.Ticks));
			Tasual_TimePop_Calendar.SetDate(NextOrNow);

			if (TimeInfo.Scheduled(_Task.Time))
			{
				Tasual_TimePop_CheckBox.Checked = true;
				if (_Task.Time.TimeOfDay == TimeSpan.FromSeconds(86399))
				{
					Tasual_TimePop_RadioButton_AllDay.Checked = true;
					Tasual_TimePop_RadioButton_Specific.Checked = false;
					Tasual_TimePop_DateTimePicker.Value = TimeInfo.PickRoundedUpTime(DateTime.Now);
				}
				else
				{
					Tasual_TimePop_RadioButton_AllDay.Checked = false;
					Tasual_TimePop_RadioButton_Specific.Checked = true;
					Tasual_TimePop_DateTimePicker.Value = NextOrNow;
				}
			}
			else
			{
				Tasual_TimePop_CheckBox.Checked = false;
				Tasual_TimePop_RadioButton_AllDay.Checked = true;
				Tasual_TimePop_RadioButton_Specific.Checked = false;
				Tasual_TimePop_DateTimePicker.Value = TimeInfo.PickRoundedUpTime(DateTime.Now);
			}

			Tasual_TimePop_CheckEnableStatus();
		}

		private void Tasual_CalendarPopout_Deactivate(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Tasual_TimePop_CheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Tasual_TimePop_CheckEnableStatus();
		}

		private void Tasual_TimePop_RadioButton_AllDay_CheckedChanged(object sender, EventArgs e)
		{
			Tasual_TimePop_CheckEnableStatus();
		}

		private void Tasual_TimePop_RadioButton_Specific_CheckedChanged(object sender, EventArgs e)
		{
			Tasual_TimePop_CheckEnableStatus();
		}

		private void Tasual_TimePop_LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Tasual_Create CreateForm = new Tasual_Create(_Tasual_Main, _Tasual_Main.TaskArray.IndexOf(_Task));
			CreateForm.ShowDialog(_Tasual_Main);
			Close();
		}

		private void Tasual_TimePop_Button_Save_Click(object sender, EventArgs e)
		{
			DateTime NewTime = new DateTime();
			NewTime = Tasual_TimePop_Calendar.SelectionStart;

			TimeSpan TimeOfDay = new TimeSpan();

			Console.WriteLine("DateTime: {0} - {1}", NewTime.ToShortDateString(), NewTime.ToLongTimeString());

			NewTime = NewTime - NewTime.TimeOfDay;

			if (Tasual_TimePop_CheckBox.Checked)
			{
				if (Tasual_TimePop_RadioButton_Specific.Checked)
				{
					NewTime = NewTime + Tasual_TimePop_DateTimePicker.Value.TimeOfDay;
					TimeOfDay = Tasual_TimePop_DateTimePicker.Value.TimeOfDay;
					//_Task.Time.TimeOfDay = Tasual_TimePop_DateTimePicker.Value.TimeOfDay;
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
					_Task.Time.Created,
					DateTime.Now,
					DateTime.MinValue,
					DateTime.MinValue,
					DateTime.MinValue);
			}

			_Tasual_Main.Tasual_Array_Save();
			_Tasual_Main.Tasual_ListView.BuildList();
			_Tasual_Main.Tasual_ListView.EnsureModelVisible(_Task);
			Close();
		}

		private void Tasual_TimePop_Button_Cancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
