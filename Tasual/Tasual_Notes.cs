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
		private readonly Tasual_Create _Tasual_Create;
		private readonly Tasual_Main _Tasual_Main;

		private int Index; 

		//private string Notes;

		private int Origination; // 1 = Main, 2 = Create

		public Tasual_Notes(Tasual_Main PassedForm, int PassedIndex)
		{
			InitializeComponent();
			_Tasual_Main = PassedForm;
			Index = PassedIndex;
			Tasual_Notes_TextBox.Text = _Tasual_Main.TaskArray[Index].Notes;
			Origination = 1;
			Console.WriteLine("Notes_Main: '{0}'", Tasual_Notes_TextBox.Text);
		}

		public Tasual_Notes(Tasual_Create PassedForm)
		{
			InitializeComponent();
			_Tasual_Create = PassedForm;
			Tasual_Notes_TextBox.Text = _Tasual_Create.Notes;
			Origination = 2;
			Console.WriteLine("Notes_Create: '{0}'", Tasual_Notes_TextBox.Text);
		}

		private void Tasual_Notes_Load(object sender, EventArgs e)
		{
			//
		}

		private void Tasual_Notes_Done_Click(object sender, EventArgs e)
		{
			if (Tasual_Notes_TextBox.Text != "")
			{
				if (Origination == 1)
				{
					_Tasual_Main.TaskArray[Index].Notes = Tasual_Notes_TextBox.Text;
					_Tasual_Main.Tasual_Array_Save_Text();
				}
				else
				{
					_Tasual_Create.Notes = Tasual_Notes_TextBox.Text;
				}
			}
		}
	}
}
