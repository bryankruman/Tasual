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
				Task = ArrayHandler.Tasks[PassedIndex];

				TextBox.Text = Task.Link;
				Button_Follow_CheckURLValid();
			}
			catch (Exception Args)
			{
				Console.WriteLine(Args);
				Close();
			}
		}

		private void Button_Save_Click(object Sender, EventArgs Args)
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

		private void Button_Follow_Click(object Sender, EventArgs Args)
		{
			URLExtensions.Follow(TextBox.Text);
		}

		private void TextBox_TextChanged(object Sender, EventArgs Args)
		{
			Button_Follow_CheckURLValid();
		}
	}
}
