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
    public partial class Tasual_Main : Form
    {
        //***********************************************************
        //This gives us the ability to resize the borderless from any borders instead of just the lower right corner
        protected override void WndProc(ref Message m)
        {
            const int wmNcHitTest = 0x84;
            const int htLeft = 10;
            const int htRight = 11;
            const int htTop = 12;
            const int htTopLeft = 13;
            const int htTopRight = 14;
            const int htBottom = 15;
            const int htBottomLeft = 16;
            const int htBottomRight = 17;
            int padding = 10;

            if ((m.Msg == wmNcHitTest) && (FormBorderStyle == FormBorderStyle.None))
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
                }
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
            DrawGripper(e);
        }
        public void DrawGripper(PaintEventArgs e)
        {
            if ((VisualStyleRenderer.IsElementDefined(VisualStyleElement.Status.Gripper.Normal)) && (FormBorderStyle == FormBorderStyle.None))
            {
                VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.Status.Gripper.Normal);
                Rectangle rectangle1 = new Rectangle((Width) - 18, (Height) - 20, 20, 20);
                renderer.DrawBackground(e.Graphics, rectangle1);
            }
        }
        public Tasual_Main()
        {
            InitializeComponent();
            Tasual_SizeColumns();
            if (FormBorderStyle == FormBorderStyle.None)
            {
                Button_Exit.Visible = true;
            }
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
            if (FormBorderStyle == FormBorderStyle.None)
            {
                Invalidate();
            }
            Tasual_SizeColumns();
            /*listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.None);
            listView1.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            listView1.Columns[0].Width = Math.Max(100, (listView1.Width - listView1.Columns[1].Width));
            */
        }

        private void keepOnTToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Tasual_Main_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Tasual_ListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            //Tasual_SizeColumns();
            e.NewWidth = this.Tasual_ListView.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }
    }
}
