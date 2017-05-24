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
	public partial class Tasual_Popout_Calendar : Form
	{
		public Tasual_Popout_Calendar()
		{
			InitializeComponent();
			//this.LostFocus += new EventHandler(this.Tasual_CalendarPopout_LostFocus);
		}

		private void Tasual_CalendarPopout_Leave(object sender, EventArgs e)
		{
			//this.Close();
		}

		private void Tasual_CalendarPopout_Deactivate(object sender, EventArgs e)
		{

			this.Close();
		}
	}
}
