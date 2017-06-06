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
	public partial class Form_Location : Form
	{
		private readonly Form_Main MainForm;
		private readonly Task Task;

		private void Tasual_Location_Button_GoogleMaps_CheckStatus()
		{
			if (!string.IsNullOrEmpty(Tasual_Location_TextBox.Text))
			{
				Tasual_Location_Button_GoogleMaps.Enabled = true;
			}
			else
			{
				Tasual_Location_Button_GoogleMaps.Enabled = false;
			}
		}

		public Form_Location(Form_Main PassedForm, int PassedIndex)
		{
			try
			{
				InitializeComponent();

				MainForm = PassedForm;
				Task = MainForm.TaskArray[PassedIndex];

				Tasual_Location_TextBox.Text = Task.Location;
				Tasual_Location_Button_GoogleMaps_CheckStatus();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				Close();
			}
		}

		private void Tasual_Location_Button_Save_Click(object sender, EventArgs e)
		{
			if (Task != null)
			{
				Task.Location = Tasual_Location_TextBox.Text;
				MainForm.Tasual_Array_Save();
			}
			else
			{
				Console.WriteLine("Tasual_Location_Button_Done_Click(): Somehow Task was null!");
			}
		}

		private void Tasual_Location_TextBox_TextChanged(object sender, EventArgs e)
		{
			Tasual_Location_Button_GoogleMaps_CheckStatus();
		}

		private void Tasual_Location_Button_GoogleMaps_Click(object sender, EventArgs e)
		{
			URLExtensions.Follow(string.Format("http://maps.google.com/?q={0}", Uri.EscapeDataString(Tasual_Location_TextBox.Text)));
		}
	}
}
