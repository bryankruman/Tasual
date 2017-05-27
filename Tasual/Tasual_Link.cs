using System;
using System.Windows.Forms;


namespace Tasual
{
	public partial class Tasual_Link : Form
	{
		private readonly Tasual_Main _Tasual_Main;
		private readonly Task Task;

		private void Tasual_Link_Button_Follow_CheckURLValid()
		{
			if (URLExtensions.Valid(Tasual_Link_TextBox.Text))
			{
				Tasual_Link_Button_Follow.Enabled = true;
			}
			else
			{
				Tasual_Link_Button_Follow.Enabled = false;
			}
		}

		public Tasual_Link(Tasual_Main PassedForm, int PassedIndex)
		{
			try
			{
				InitializeComponent();

				_Tasual_Main = PassedForm;
				Task = _Tasual_Main.TaskArray[PassedIndex];

				Tasual_Link_TextBox.Text = Task.Link;
				Tasual_Link_Button_Follow_CheckURLValid();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				Close();
			}
		}

		private void Tasual_Link_Button_Save_Click(object sender, EventArgs e)
		{
			if (Task != null)
			{
				Task.Link = Tasual_Link_TextBox.Text;
				_Tasual_Main.Tasual_Main_Save();
			}
			else
			{
				Console.WriteLine("Tasual_Link_Button_Save_Click(): Somehow Task was null!");
			}
		}

		private void Tasual_Link_Button_Follow_Click(object sender, EventArgs e)
		{
			URLExtensions.Follow(Tasual_Link_TextBox.Text);
		}

		private void Tasual_Link_TextBox_TextChanged(object sender, EventArgs e)
		{
			Tasual_Link_Button_Follow_CheckURLValid();
		}
	}
}
