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
		private readonly Tasual_Main _tasual_main;

        public Tasual_Confirm_Clear(Tasual_Main main)
        {
            InitializeComponent();
			this._tasual_main = main;
        }

        private void Tasual_Confirm_Clear_Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void Tasual_Confirm_Clear_Button_Confirm_Click(object sender, EventArgs e)
		{
			this._tasual_main.TaskArray.Clear();
			// TODO: Create a function to directly destroy the taskarray and localdb text file
			this._tasual_main.Tasual_Array_Save_Text(ref this._tasual_main.TaskArray);
			this._tasual_main.Tasual_Array_Load_Text(ref this._tasual_main.TaskArray);
			this._tasual_main.Tasual_ListView_PopulateFromArray(ref this._tasual_main.TaskArray);
			this.Close();
		}
	}
}
