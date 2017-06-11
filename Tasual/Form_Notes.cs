// ===========================================
//  Copyright (c) 2017 Bryan Kruman
//
//  See LICENSE.rtf file in the project root
//  for full license information.
// ===========================================

using System;
using System.Windows.Forms;

namespace Tasual
{
	public partial class Form_Notes : Form
	{
		private readonly Form_Create CreateForm;
		private readonly Form_Main MainForm;
		private readonly Task Task;
		private readonly int Origination; // 1 = Main, 2 = Create

		public Form_Notes(Form_Main PassedMain, int PassedIndex)
		{
			try
			{
				InitializeComponent();

				Origination = 1;
				MainForm = PassedMain;
				Task = MainForm.TaskArray[PassedIndex];

				TextBox.Text = Task.Notes;
			}
			catch (Exception Args)
			{
				Console.WriteLine(Args);
				Close();
			}
		}

		public Form_Notes(Form_Main PassedMain, Form_Create PassedCreate)
		{
			try
			{
				InitializeComponent();

				Origination = 2;
				MainForm = PassedMain;
				CreateForm = PassedCreate;

				TextBox.Text = CreateForm.Notes;
			}
			catch (Exception Args)
			{
				Console.WriteLine(Args);
				Close();
			}
		}

		private void Save_Click(object Sender, EventArgs Args)
		{
			if (Origination == 1)
			{
				if (Task != null)
				{
					Task.Notes = TextBox.Text;
					MainForm.Array_Save();
					//ArrayHandler.Save(ref MainForm.TaskArray, MainForm.Settings);
					//MainForm.Tasual_Array_Save();
				}
				else
				{
					Console.WriteLine("Done_Click(): Somehow Task was null!");
				}
			}
			else
			{
				CreateForm.Notes = TextBox.Text;
			}
		}

		private void CheckBox_CheckedChanged(object Sender, EventArgs Args)
		{
			TextBox.AcceptsReturn = !CheckBox.Checked;
			MainForm.Settings.EnterToSave = CheckBox.Checked;
			MainForm.Settings_Save();
		}

		private void FormLoad(object Sender, EventArgs Args)
		{
			TextBox.AcceptsReturn = !MainForm.Settings.EnterToSave;
			CheckBox.Checked = MainForm.Settings.EnterToSave;
		}
	}
}
