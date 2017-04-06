using System;
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
            ListViewGroup[] ListView_Groups = new ListViewGroup[20];
            ListViewItem[] ListView_Items = new ListViewItem[100];

            // initialize columns
            Tasual_ListView.Columns.Add("Description");
            Tasual_ListView.Columns.Add("Time");

            // initialize groups
            ListView_Groups[0] = new ListViewGroup();
            ListView_Groups[0].Name = "Work";
            ListView_Groups[0].Header = "Work";
            Tasual_ListView.Groups.Add(ListView_Groups[0]);

            // initialize items
            string[] ListView_Item_S = new string[2];
            ListView_Item_S[0] = "Create some tasks";
            ListView_Item_S[1] = DateTime.Now.ToLongDateString();
            ListView_Items[0] = new ListViewItem(ListView_Item_S);
            ListView_Items[0].Group = ListView_Groups[0];
            ListView_Items[0].Checked = true;
            ListView_Items[0].ForeColor = Color.FromArgb(255, 189, 208, 230); //Color.FromArgb(255, 36, 90, 150);
            ListView_Items[0].BackColor = Color.FromArgb(255, 0, 0, 0);
            Tasual_ListView.Items.Add(ListView_Items[0]);

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
            task_form.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tasual_Main_Status_MenuStrip.Show(linkLabel1, new Point(0, linkLabel1.Height));
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tasual_About about_form = new Tasual_About();
            about_form.Show();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tasual_Confirm_Clear confirm_form = new Tasual_Confirm_Clear();
            confirm_form.Show();
        }
    }
    public class TaskItem_C
    {
        public struct xyztime
        {
            double start;
            double end;
            double next;
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

        // todo: make custom categories possible with an array/configuration adjustment
        public enum taskcategory_t
        {
            CATE_HOME,
            CATE_WORK,
            CATE_SCHOOL,
            CATE_HOBBY
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
        public int ti_cate;
        public string ti_desc;
        public xyztime ti_time;

        // constructors
        //taskitem_c();
        //taskitem_c(int, int, int, int, string, xyztime);

        // deconstructor
        //~taskitem_c();
    }
}
