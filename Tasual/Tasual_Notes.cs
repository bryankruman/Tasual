using System;
using System.Windows.Forms;

namespace Tasual
{
	public partial class Tasual_Notes : Form
	{
		private readonly Tasual_Create _Tasual_Create;
		private readonly Tasual_Main _Tasual_Main;
		private readonly Task Task;
		private readonly int Origination; // 1 = Main, 2 = Create

		public Tasual_Notes(Tasual_Main PassedForm, int PassedIndex)
		{
			try
			{
				InitializeComponent();

				Origination = 1;
				_Tasual_Main = PassedForm;
				Task = _Tasual_Main.TaskArray[PassedIndex];

				Tasual_Notes_TextBox.Text = Task.Notes;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				Close();
			}
		}

		public Tasual_Notes(Tasual_Create PassedForm)
		{
			try
			{
				InitializeComponent();

				Origination = 2;
				_Tasual_Create = PassedForm;

				Tasual_Notes_TextBox.Text = _Tasual_Create.Notes;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				Close();
			}
		}

		private void Tasual_Notes_Save_Click(object sender, EventArgs e)
		{
			if (Origination == 1)
			{
				if (Task != null)
				{
					Task.Notes = Tasual_Notes_TextBox.Text;
					_Tasual_Main.Tasual_Array_Save();
				}
				else
				{
					Console.WriteLine("Tasual_Notes_Done_Click(): Somehow Task was null!");
				}
			}
			else
			{
				_Tasual_Create.Notes = Tasual_Notes_TextBox.Text;
			}
		}

		private void Tasual_Notes_CheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Tasual_Notes_TextBox.AcceptsReturn = !Tasual_Notes_CheckBox.Checked;
			_Tasual_Main.Settings.EnterToSave = Tasual_Notes_CheckBox.Checked;
			Setting.Save(_Tasual_Main.Settings);
		}

		private void Tasual_Notes_Load(object sender, EventArgs e)
		{
			Tasual_Notes_TextBox.AcceptsReturn = !_Tasual_Main.Settings.EnterToSave;
			Tasual_Notes_CheckBox.Checked = _Tasual_Main.Settings.EnterToSave;
		}
	}
}
