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
    public partial class Tasual_Confirm_Clear : Form
    {
		private readonly Tasual_Main _Tasual_Main;

        public Tasual_Confirm_Clear(Tasual_Main main)
        {
            InitializeComponent();
			this._Tasual_Main = main;
        }

        private void Tasual_Confirm_Clear_Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void Tasual_Confirm_Clear_Button_Confirm_Click(object sender, EventArgs e)
		{
			this._Tasual_Main.Tasual_Array_ClearAll();
			this.Close();
		}
	}
}
