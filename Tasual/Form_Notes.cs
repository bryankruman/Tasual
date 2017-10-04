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
		private readonly Task Task;
		private readonly int Origination; // 1 = Main, 2 = Create

		public Form_Notes(int PassedIndex)
		{
			try
			{
				InitializeComponent();

				Origination = 1;
				Task = ArrayHandler.Tasks[PassedIndex];

				TextBox.Text = Task.Notes;
			}
			catch (Exception Args)
			{
				Console.WriteLine(Args);
				Close();
			}
		}

		public Form_Notes(Form_Create PassedCreate)
		{
			try
			{
				InitializeComponent();

				Origination = 2;
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
					ArrayHandler.Save();
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
			Settings.Config.EnterToSave = CheckBox.Checked;
			Settings.Save();
		}

		private void FormLoad(object Sender, EventArgs Args)
		{
			TextBox.AcceptsReturn = !Settings.Config.EnterToSave;
			CheckBox.Checked = Settings.Config.EnterToSave;
		}
	}
}
