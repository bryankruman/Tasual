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
        public Tasual_Confirm_Clear()
        {
            InitializeComponent();
        }

        private void Tasual_Confirm_Clear_Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
