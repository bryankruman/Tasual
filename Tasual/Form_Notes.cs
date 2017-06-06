using System;
using System.Windows.Forms;

namespace Tasual
{
	public partial class Form_Notes : Form
	{
		private readonly Form_Create _Tasual_Create;
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
			catch (Exception e)
			{
				Console.WriteLine(e);
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
				_Tasual_Create = PassedCreate;

				TextBox.Text = _Tasual_Create.Notes;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				Close();
			}
		}

		private void Save_Click(object sender, EventArgs e)
		{
			if (Origination == 1)
			{
				if (Task != null)
				{
					Task.Notes = TextBox.Text;
					MainForm.Tasual_Array_Save();
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
				_Tasual_Create.Notes = TextBox.Text;
			}
		}

		private void CheckBox_CheckedChanged(object sender, EventArgs e)
		{
			TextBox.AcceptsReturn = !CheckBox.Checked;
			MainForm.Settings.EnterToSave = CheckBox.Checked;
			MainForm.Save();
		}

		private void FormLoad(object sender, EventArgs e)
		{
			TextBox.AcceptsReturn = !MainForm.Settings.EnterToSave;
			CheckBox.Checked = MainForm.Settings.EnterToSave;
		}
	}
}
