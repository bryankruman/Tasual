using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Tasual
{
	//vector<taskitem_c> tasklist;


	public partial class Tasual_Main : Form
	{
		public void Tasual_PrintTaskToConsole(TaskItem_C TaskItem)
		{
			/*cout
					<< "tasqing_item: {'" << taskitem.ti_type << "', '"
					<< taskitem.ti_prio << "', '"
					<< taskitem.ti_stat << "', '"
					<< taskitem.ti_cate << "', '"
					<< taskitem.ti_desc << "', '"
					<< taskitem.ti_time.start << "', '"
					<< taskitem.ti_time.end << "', '"
					<< taskitem.ti_time.next << "'}\n";
					*/

			Console.WriteLine("TaskItem: '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'",
			TaskItem.ti_type,
			TaskItem.ti_prio,
			TaskItem.ti_stat,
			TaskItem.ti_grou,
			TaskItem.ti_desc,
			TaskItem.ti_time.xstart,
			TaskItem.ti_time.yend,
			TaskItem.ti_time.znext
			);
		}

		//***********************************************************
		//This gives us the ability to resize the borderless from any borders instead of just the lower right corner
		protected override void WndProc(ref Message m)
		{
			const int wmNcHitTest = 0x84;
			//const int htLeft = 10;
			//const int htRight = 11;
			//const int htTop = 12;
			//const int htTopLeft = 13;
			//const int htTopRight = 14;
			//const int htBottom = 15;
			const int htBottomLeft = 16;
			const int htBottomRight = 17;
			int padding = 15;

			if (m.Msg == wmNcHitTest)
			{
				int x = (int)(m.LParam.ToInt64() & 0xFFFF);
				int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
				Point pt = PointToClient(new Point(x, y));
				Size clientSize = ClientSize;
				///allow resize on the lower right corner
				if (pt.X >= clientSize.Width - padding && pt.Y >= clientSize.Height - padding && clientSize.Height >= padding)
				{
					m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight);
					//Invalidate();
					return;
				}
				/*
                ///allow resize on the lower left corner
                if (pt.X <= padding && pt.Y >= clientSize.Height - padding && clientSize.Height >= padding)
                {
                    m.Result = (IntPtr)(IsMirrored ? htBottomRight : htBottomLeft);
                    return;
                }
                ///allow resize on the upper right corner
                if (pt.X <= padding && pt.Y <= padding && clientSize.Height >= padding)
                {
                    m.Result = (IntPtr)(IsMirrored ? htTopRight : htTopLeft);
                    return;
                }
                ///allow resize on the upper left corner
                if (pt.X >= clientSize.Width - padding && pt.Y <= padding && clientSize.Height >= padding)
                {
                    m.Result = (IntPtr)(IsMirrored ? htTopLeft : htTopRight);
                    return;
                }
                ///allow resize on the top border
                if (pt.Y <= padding && clientSize.Height >= padding)
                {
                    m.Result = (IntPtr)(htTop);
                    return;
                }
                ///allow resize on the bottom border
                if (pt.Y >= clientSize.Height - padding && clientSize.Height >= padding)
                {
                    m.Result = (IntPtr)(htBottom);
                    return;
                }
                ///allow resize on the left border
                if (pt.X <= padding && clientSize.Height >= padding)
                {
                    m.Result = (IntPtr)(htLeft);
                    return;
                }
                ///allow resize on the right border
                if (pt.X >= clientSize.Width - padding && clientSize.Height >= padding)
                {
                    m.Result = (IntPtr)(htRight);
                    return;
                }*/
			}
			base.WndProc(ref m);
		}

		//***********************************************************
		//This gives us the ability to drag the borderless form to a new location
		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;

		[DllImportAttribute("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[DllImportAttribute("user32.dll")]
		public static extern bool ReleaseCapture();

		private void Tasual_Main_MouseDown(object sender, MouseEventArgs e)
		{
			//ctrl-leftclick anywhere on the control to drag the form to a new location 
			if (e.Button == MouseButtons.Left) // && Control.ModifierKeys == Keys.Control)
			{
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}

		//***********************************************************
		//This gives us the drop shadow behind the borderless form
		private const int CS_DROPSHADOW = 0x20000;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				if (FormBorderStyle == FormBorderStyle.None)
				{
					cp.ClassStyle |= CS_DROPSHADOW;
				}
				return cp;
			}
		}
		//***********************************************************

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			//DrawGripper(e);

			//System.Drawing.Graphics graphics = this.CreateGraphics();
			//Rectangle gripperbounds = new Rectangle((Width) - 18, (Height) - 20, 20, 20);
			//ControlPaint.DrawSizeGrip(e.Graphics, BackColor,
			//    (Width) - 18, (Height) - 20, 20, 20);
			//MessageBox.Show("fuck you");
		}
		public void DrawGripper(PaintEventArgs e)
		{
			//if (VisualStyleRenderer.IsElementDefined(VisualStyleElement.Status.Gripper.Normal))
			//{
			VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.Status.Gripper.Normal);
			Rectangle gripperbounds = new Rectangle((Width) - 22, (Height) - 22, 20, 20);
			renderer.DrawBackground(e.Graphics, gripperbounds);
			//}
		}

		public void Tasual_LoadArray(TaskItem_C[] Array)
		{
			try
			{
				using (StreamReader inputfile = new StreamReader("localdb.txt"))
				{
					int counter = 0;
					string line;

					while ((line = inputfile.ReadLine()) != null)
					{
						TaskItem_C temp_item = new TaskItem_C();
						int argtype = 0;
						string[] segments = line.Split((char)29);

						foreach (string token in segments)
						{
							// lets do something with this data now
							switch (argtype)
							{
								case (int)TaskItem_C.protocol_text_arg_t.ARG_TYPE: { Int32.TryParse(token, out temp_item.ti_type); break; }
								case (int)TaskItem_C.protocol_text_arg_t.ARG_PRIO: { Int32.TryParse(token, out temp_item.ti_prio); break; }
								case (int)TaskItem_C.protocol_text_arg_t.ARG_STAT: { Int32.TryParse(token, out temp_item.ti_stat); break; }
								case (int)TaskItem_C.protocol_text_arg_t.ARG_CATE: { temp_item.ti_grou = token; break; }
								case (int)TaskItem_C.protocol_text_arg_t.ARG_DESC: { temp_item.ti_desc = token; break; }
								case (int)TaskItem_C.protocol_text_arg_t.ARG_START: { Double.TryParse(token, out temp_item.ti_time.xstart); break; }
								case (int)TaskItem_C.protocol_text_arg_t.ARG_END: { Double.TryParse(token, out temp_item.ti_time.yend); break; }
								case (int)TaskItem_C.protocol_text_arg_t.ARG_NEXT: { Double.TryParse(token, out temp_item.ti_time.znext); break; }
								default:
									{
										Console.WriteLine("TOO MANY ARGUMENTS IN FILE !!!!!!");
										break;
									}
							}

							++argtype;
							//Console.WriteLine(token);
						}

						if (argtype == (int)TaskItem_C.protocol_text_arg_t.ARG_COUNT)
							Array[counter] = temp_item;

						//Console.WriteLine(line);
						counter++;
					}
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public Tasual_Main()
		{
			InitializeComponent();

			// if we're using borderless mode, add an exit button
			if (FormBorderStyle == FormBorderStyle.None)
			{
				Button_Exit.Visible = true;
			}

			// setup listview control
			// todo: add ability to choose how to display categories -- in column, or in groups

			// clear any existing items
			// /*
			Tasual_ListView.Columns.Clear();
			Tasual_ListView.Groups.Clear();
			Tasual_ListView.Items.Clear();
			Tasual_ListView.Update();
			Tasual_ListView.Refresh();
			// */

			// declarations
			//ListViewGroup[] ListView_Groups = new ListViewGroup[20];
			TaskItem_C[] TaskArray = new TaskItem_C[100]; // TODO: change this to data list later

			// load task array
			Tasual_LoadArray(TaskArray);

			// initialize columns
			Tasual_ListView.Columns.Add("Description");
			var displaymode = 2;
			if (displaymode == 1)
			{
				Tasual_ListView.Columns.Add("Time");
			}
			else if (displaymode == 2)
			{
				Tasual_ListView.Columns.Add("Category");
			}

			// load tasks into Tasual_ListView
			foreach (TaskItem_C task in TaskArray)
			{
				if (task == null) { break; }
				Tasual_PrintTaskToConsole(task);

				// create listviewitem basic info
				string[] Item_S = new string[2];
				Item_S[0] = task.ti_desc;

				if (displaymode == 1)
				{
					var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
					epoch.AddSeconds(task.ti_time.xstart);
					Item_S[1] = epoch.ToLongDateString();
				}
				else if (displaymode == 2)
				{
					Item_S[1] = task.ti_grou.ToString();
				}
				ListViewItem Item = new ListViewItem(Item_S);

				// create listviewgroup for item
				ListViewGroup found = null;
				foreach (ListViewGroup group in Tasual_ListView.Groups)
				{
					if (group.Name == task.ti_grou.ToString())
					{
						found = group;
						break;
					}
				}

				if (found == null)
				{
					ListViewGroup NewGroup = new ListViewGroup();
					NewGroup.Name = task.ti_grou.ToString();
					NewGroup.Header = NewGroup.Name;
					Tasual_ListView.Groups.Add(NewGroup);
					Item.Group = NewGroup;
				}
				else
				{
					Item.Group = found;
				}

				Item.Checked = true;
				Item.ForeColor = Color.FromArgb(255, 189, 208, 230); //Color.FromArgb(255, 36, 90, 150);
				//Item.BackColor = Color.FromArgb(255, 0, 0, 0);
				Tasual_ListView.Items.Add(Item);
			}

			// initialize groups
			//ListView_Groups[0] = new ListViewGroup();
			//ListView_Groups[0].Name = "Work";
			//ListView_Groups[0].Header = "Work";
			//Tasual_ListView.Groups.Add(ListView_Groups[0]);

			// initialize items
			//string[] ListView_Item_S = new string[2];
			//ListView_Item_S[0] = "Create some tasks";
			//ListView_Item_S[1] = DateTime.Now.ToLongDateString();
			//ListView_Items[0] = new ListViewItem(ListView_Item_S);
			//ListView_Items[0].Group = ListView_Groups[0];
			//ListView_Items[0].Checked = true;
			//ListView_Items[0].ForeColor = Color.FromArgb(255, 189, 208, 230); //Color.FromArgb(255, 36, 90, 150);
			//ListView_Items[0].BackColor = Color.FromArgb(255, 0, 0, 0);
			//Tasual_ListView.Items.Add(ListView_Items[0]);

			// init array of items
			TaskItem_C taskitem = new TaskItem_C();

			taskitem.ti_type = (int)TaskItem_C.tasktype_t.TYPE_USER_SINGLE;

			// finally now that the listview is loaded, resize its columns
			Tasual_SizeColumns();
		}

		private void Tasual_SizeColumns()
		{
			Tasual_ListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.None);
			Tasual_ListView.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

			Tasual_ListView.Columns[0].Width = Math.Max(100, (Tasual_ListView.Width - Tasual_ListView.Columns[1].Width));
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void toolStripStatusLabel1_Click(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void Button_Exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Tasual_Main_Resize(object sender, EventArgs e)
		{
			Invalidate();
			Tasual_SizeColumns();
			/*listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.None);
            listView1.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            listView1.Columns[0].Width = Math.Max(100, (listView1.Width - listView1.Columns[1].Width));
            */
		}

		private void keepOnTToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		/*private void Tasual_Main_Paint(object sender, PaintEventArgs e)
        {
            //base.OnPaint(e);
            DrawGripper(e);
        }*/

		private void Tasual_ListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			//Tasual_SizeColumns();
			e.NewWidth = this.Tasual_ListView.Columns[e.ColumnIndex].Width;
			e.Cancel = true;
		}

		private void taskToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Tasual_Create_Task task_form = new Tasual_Create_Task();
			task_form.Show(this);
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Tasual_Main_Status_MenuStrip.Show(linkLabel1, new Point(0, linkLabel1.Height));
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Tasual_About about_form = new Tasual_About();
			about_form.ShowDialog(this);
		}

		private void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Tasual_Confirm_Clear confirm_form = new Tasual_Confirm_Clear();
			confirm_form.ShowDialog(this);
		}
	}
	public class TaskItem_C
	{
		public struct xyztime
		{
			public double xstart;
			public double yend;
			public double znext;
		}

		public enum protocol_text_arg_t
		{
			ARG_TYPE,
			ARG_PRIO,
			ARG_STAT,
			ARG_CATE,
			ARG_DESC,
			ARG_START,
			ARG_END,
			ARG_NEXT,
			ARG_COUNT
		}

		public enum tasktype_t
		{
			TYPE_USER_SINGLE,
			TYPE_USER_RECURRING,
			TYPE_USER_DEBT_OWED,
			TYPE_USER_DEBT_LENT,
			TYPE_SYNDICATION_SINGLE,
			TYPE_SYNDICATION_RECURRING
		}

		public enum taskpriority_t
		{
			PRIO_LOW,
			PRIO_MED,
			PRIO_HIGH
		}

		public enum taskstatus_t
		{
			STAT_NEW,
			STAT_COMPLETED
		}

		public enum protocol_t
		{
			PRO_TEXT,
			PRO_XML,
			PRO_RTM,
			PRO_TASUAL
		}

		public enum loader_t
		{
			TASUAL_INIT,   // called upon first initialization
			TASUAL_RELOAD  // called when a user prompts the application to reload configurations
		}

		public int ti_type;
		public int ti_prio;
		public int ti_stat;
		public string ti_grou;
		public string ti_desc;
		public xyztime ti_time;


		// constructors
		//taskitem_c();
		//taskitem_c(int, int, int, int, string, xyztime);

		// deconstructor
		//~taskitem_c();
	}
}
