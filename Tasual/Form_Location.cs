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
	public partial class Form_Location : Form
	{
		private readonly Form_Main MainForm;
		private readonly Task Task;

		private void Button_GoogleMaps_CheckStatus()
		{
			if (!string.IsNullOrEmpty(TextBox.Text))
			{
				Button_GoogleMaps.Enabled = true;
			}
			else
			{
				Button_GoogleMaps.Enabled = false;
			}
		}

		public Form_Location(Form_Main PassedForm, int PassedIndex)
		{
			try
			{
				InitializeComponent();

				MainForm = PassedForm;
				Task = MainForm.TaskArray[PassedIndex];

				TextBox.Text = Task.Location;
				Button_GoogleMaps_CheckStatus();
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
				Task.Location = TextBox.Text;
				MainForm.Array_Save();
			}
			else
			{
				Console.WriteLine("Button_Done_Click(): Somehow Task was null!");
			}
		}

		private void TextBox_TextChanged(object Sender, EventArgs Args)
		{
			Button_GoogleMaps_CheckStatus();
		}

		private void Button_GoogleMaps_Click(object Sender, EventArgs Args)
		{
			URLExtensions.Follow(string.Format("http://maps.google.com/?q={0}", Uri.EscapeDataString(TextBox.Text)));
		}
	}
}
