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
	public partial class Tasual_Link : Form
	{
		private readonly Tasual_Main _Tasual_Main;
		private readonly Task Task;

		public Tasual_Link(Tasual_Main PassedForm, int PassedIndex)
		{
			try
			{
				InitializeComponent();

				_Tasual_Main = PassedForm;
				Task = _Tasual_Main.TaskArray[PassedIndex];

				Tasual_Link_TextBox.Text = Task.Link;
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
				_Tasual_Main.Tasual_Array_Save();
			}
			else
			{
				Console.WriteLine("Tasual_Link_Button_Save_Click(): Somehow Task was null!");
			}
		}
	}
}
