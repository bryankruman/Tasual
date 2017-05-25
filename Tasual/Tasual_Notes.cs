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
	public partial class Tasual_Notes : Form
	{
		//private readonly Tasual_Create _Tasual_Create;

		private string Notes;

		public Tasual_Notes(string Notes)
		{
			InitializeComponent();
			this.Notes = Notes;
			//this._Tasual_Create = Tasual_Create;
		}

		private void Tasual_Notes_Load(object sender, EventArgs e)
		{
			Tasual_Notes_WatermarkTextBox.Text = Notes;
		}

		private void Tasual_Notes_Done_Click(object sender, EventArgs e)
		{
			if (Tasual_Notes_WatermarkTextBox.Text != Tasual_Notes_WatermarkTextBox.WatermarkText)
			{
				Notes = Tasual_Notes_WatermarkTextBox.Text;
			}
		}
	}
}
