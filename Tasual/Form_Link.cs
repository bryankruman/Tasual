using System;
using System.Windows.Forms;


namespace Tasual
{
	public partial class Form_Link : Form
	{
		private readonly Form_Main MainForm;
		private readonly Task Task;

		private void Button_Follow_CheckURLValid()
		{
			if (URLExtensions.Valid(TextBox.Text))
			{
				Button_Follow.Enabled = true;
			}
			else
			{
				Button_Follow.Enabled = false;
			}
		}

		public Form_Link(Form_Main PassedForm, int PassedIndex)
		{
			try
			{
				InitializeComponent();

				MainForm = PassedForm;
				Task = MainForm.TaskArray[PassedIndex];

				TextBox.Text = Task.Link;
				Button_Follow_CheckURLValid();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				Close();
			}
		}

		private void Button_Save_Click(object sender, EventArgs e)
		{
			if (Task != null)
			{
				Task.Link = TextBox.Text;
				MainForm.Array_Save();
			}
			else
			{
				Console.WriteLine("Button_Save_Click(): Somehow Task was null!");
			}
		}

		private void Button_Follow_Click(object sender, EventArgs e)
		{
			URLExtensions.Follow(TextBox.Text);
		}

		private void TextBox_TextChanged(object sender, EventArgs e)
		{
			Button_Follow_CheckURLValid();
		}
	}
}
