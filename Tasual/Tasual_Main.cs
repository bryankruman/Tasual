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
	public partial class Tasual_Main : Form
	{
		public List<TaskItem> TaskArray = new List<TaskItem>();
		bool Tasual_Setting_TimeGroups = false;
		string Tasual_Setting_TextFile = "localdb.txt";

		int Tasual_Tasks_Complete = 0;
		int Tasual_Tasks_Total = 0;

		public enum protocol_text_arg_t
		{
			ARG_TYPE,
			ARG_PRIO,
			ARG_STAT,
			ARG_GROU,
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
			PRO_TEXT//,
					//PRO_XML,
					//PRO_RTM,
					//PRO_TASUAL
		}

		public void Tasual_PrintTaskToConsole(TaskItem TaskItem)
		{
			Console.WriteLine(
				"TaskItem: '{0}', '{1}', '{2}', '{3}', '{4}', ('{5}', '{6}', '{7}')",
				TaskItem.Type,
				TaskItem.Priority,
				TaskItem.Status,
				TaskItem.Group,
				TaskItem.Description,
				TaskItem.Time.Start,
				TaskItem.Time.End,
				TaskItem.Time.Next
			);
		}

		//***********************************************************
		//This gives us the ability to resize the borderless from any borders instead of just the lower right corner
		protected override void WndProc(ref Message m)
		{
			const int wmNcHitTest = 0x84;
			const int htBottomLeft = 16;
			const int htBottomRight = 17;
			int padding = 15;

			if (m.Msg == wmNcHitTest)
			{
				int x = (int)(m.LParam.ToInt64() & 0xFFFF);
				int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
				Point pt = PointToClient(new Point(x, y));
				Size clientSize = ClientSize;

				///allow resize on the lower right corner // TODO: Fix this with multiple monitors
				if (pt.X >= clientSize.Width - padding && pt.Y >= clientSize.Height - padding && clientSize.Height >= padding)
				{
					m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight);
					//Invalidate();
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

		public void Tasual_Array_AddTask(
			ref List<TaskItem> Array,
			int Type,
			int Priority,
			int Status,
			string Group,
			string Description,
			TaskItem.TaskTime Time)
		{
			TaskItem NewItem = new TaskItem();

			NewItem.Type = Type;
			NewItem.Priority = Priority;
			NewItem.Status = Status;
			NewItem.Group = Group;
			NewItem.Description = Description;
			NewItem.Time = Time;

			Array.Add(NewItem);
			Tasual_Array_Save_Text(ref Array);

			// create listviewitem basic info
			string[] Item_S = new string[2];
			Item_S[0] = NewItem.Description;

			if (Tasual_Setting_TimeGroups)
			{
				Item_S[1] = NewItem.Group.ToString();
			}
			else
			{
				var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				epoch.AddSeconds(NewItem.Time.Start);
				Item_S[1] = epoch.ToLongTimeString();
			}
			ListViewItem Item = new ListViewItem(Item_S);
			Item.Tag = NewItem;

			// create listviewgroup for item
			Tasual_ListView_AssignGroup(NewItem.Group.ToString(), ref Item);

			// check status of task to see if it is checked or not
			if (NewItem.Status == (int)taskstatus_t.STAT_COMPLETED)
			{
				++Tasual_Tasks_Complete;
				Item.Checked = true;
			}
			else
			{
				Item.Checked = false;
			}

			//Item.BackColor = Color.FromArgb(255, 0, 0, 0);
			Tasual_ListView.Items.Add(Item);

			++Tasual_Tasks_Total;
			Tasual_StatusLabel_UpdateCounts();
			Tasual_ListView_SizeColumns();
		}

		public void Tasual_Array_Save_Text(ref List<TaskItem> Array)
		{
			try
			{
				using (StreamWriter OutputFile = new StreamWriter(Tasual_Setting_TextFile))
				{
					foreach (TaskItem Task in Array)
					{
						string Line;
						Line = Task.Type.ToString();
						Line = Line + (char)29 + Task.Priority.ToString();
						Line = Line + (char)29 + Task.Status.ToString();
						Line = Line + (char)29 + Task.Group;
						Line = Line + (char)29 + Task.Description;
						Line = Line + (char)29 + Task.Time.Start.ToString();
						Line = Line + (char)29 + Task.Time.End.ToString();
						Line = Line + (char)29 + Task.Time.Next.ToString();
						OutputFile.WriteLine(Line);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void Tasual_Array_Load_Text(ref List<TaskItem> Array)
		{
			try
			{
				TaskArray.Clear();

				using (StreamReader InputFile = new StreamReader(Tasual_Setting_TextFile))
				{
					int counter = 0;
					string Line;

					while ((Line = InputFile.ReadLine()) != null)
					{
						TaskItem NewItem = new TaskItem();
						int argtype = 0;
						string[] segments = Line.Split((char)29);

						foreach (string token in segments)
						{
							// lets do something with this data now
							switch (argtype)
							{
								case (int)protocol_text_arg_t.ARG_TYPE: { Int32.TryParse(token, out NewItem.Type); break; }
								case (int)protocol_text_arg_t.ARG_PRIO: { Int32.TryParse(token, out NewItem.Priority); break; }
								case (int)protocol_text_arg_t.ARG_STAT: { Int32.TryParse(token, out NewItem.Status); break; }
								case (int)protocol_text_arg_t.ARG_GROU: { NewItem.Group = token; break; }
								case (int)protocol_text_arg_t.ARG_DESC: { NewItem.Description = token; break; }
								case (int)protocol_text_arg_t.ARG_START: { Double.TryParse(token, out NewItem.Time.Start); break; }
								case (int)protocol_text_arg_t.ARG_END: { Double.TryParse(token, out NewItem.Time.End); break; }
								case (int)protocol_text_arg_t.ARG_NEXT: { Double.TryParse(token, out NewItem.Time.Next); break; }
								default:
									{
										Console.WriteLine("TOO MANY ARGUMENTS IN FILE !!!!!!");
										break;
									}
							}

							++argtype;
							//Console.WriteLine(token);
						}

						if (argtype == (int)protocol_text_arg_t.ARG_COUNT)
							TaskArray.Add(NewItem);

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

		private void Tasual_StatusLabel_UpdateCounts()
		{
			if (Tasual_Tasks_Complete == Tasual_Tasks_Total)
			{
				Tasual_StatusLabel.Text = "All tasks complete";
			}
			else
			{
				Tasual_StatusLabel.Text = string.Format("{0} of {1} tasks complete", Tasual_Tasks_Complete, Tasual_Tasks_Total);
			}
		}

		private void Tasual_ListView_SizeColumns()
		{
			Tasual_ListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.None);
			Tasual_ListView.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

			Tasual_ListView.Columns[0].Width = Math.Max(100, (Tasual_ListView.Width - Tasual_ListView.Columns[1].Width) - 17);
		}

		private void Tasual_ListView_ClearAll()
		{
			Tasual_ListView.Columns.Clear();
			Tasual_ListView.Groups.Clear();
			Tasual_ListView.Items.Clear();
			Tasual_ListView.Update();
			Tasual_ListView.Refresh();
		}

		private void Tasual_ListView_AssignGroup(string TaskGroup, ref ListViewItem Item)
		{
			ListViewGroup Found = null;
			foreach (ListViewGroup Group in Tasual_ListView.Groups)
			{
				if (Group.Name == TaskGroup)
				{
					Found = Group;
					break;
				}
			}

			if (Found == null)
			{
				ListViewGroup NewGroup = new ListViewGroup();
				NewGroup.Name = TaskGroup;
				NewGroup.Header = NewGroup.Name;
				Tasual_ListView.Groups.Add(NewGroup);
				Item.Group = NewGroup;
			}
			else
			{
				Item.Group = Found;
			}
		}

		public void Tasual_ListView_PopulateFromArray(ref List<TaskItem> TaskArray)
		{
			Tasual_ListView_ClearAll();

			Tasual_Tasks_Total = Tasual_Tasks_Complete = 0;

			Tasual_ListView.Columns.Add("Description");
			if (Tasual_Setting_TimeGroups) { Tasual_ListView.Columns.Add("Category"); }
			else { Tasual_ListView.Columns.Add("Time"); }

			foreach (TaskItem Task in TaskArray)
			{
				if (Task == null) { break; }
				Tasual_PrintTaskToConsole(Task);

				// create listviewitem basic info
				string[] Item_S = new string[2];
				Item_S[0] = Task.Description;
				if (Tasual_Setting_TimeGroups)
				{
					Item_S[1] = Task.Group.ToString();
				}
				else
				{
					var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
					epoch.AddSeconds(Task.Time.Start);
					Item_S[1] = epoch.ToLongDateString();
				}
				ListViewItem Item = new ListViewItem(Item_S);
				Item.Tag = Task;

				// create listviewgroup for item
				Tasual_ListView_AssignGroup(Task.Group.ToString(), ref Item);

				// check status of task to see if it is checked or not
				if (Task.Status == (int)taskstatus_t.STAT_COMPLETED)
				{
					++Tasual_Tasks_Complete;
					Item.Checked = true;
				}
				else
				{
					Item.Checked = false;
				}

				++Tasual_Tasks_Total;
				//Item.BackColor = Color.FromArgb(255, 0, 0, 0);
				Tasual_ListView.Items.Add(Item);
			}

			Tasual_StatusLabel_UpdateCounts();
			Tasual_ListView_SizeColumns();
		}

		public Tasual_Main()
		{
			InitializeComponent();

			// if we're using borderless mode, add an exit button
			if (FormBorderStyle == FormBorderStyle.None)
			{
				Button_Exit.Visible = true;
			}
		}

		private void Tasual_Main_Load(object sender, EventArgs e)
		{
			// load task array
			Tasual_Array_Load_Text(ref TaskArray);

			// load tasks into Tasual_ListView
			Tasual_ListView_PopulateFromArray(ref TaskArray);
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void toolStripStatusLabel1_Click(object sender, EventArgs e)
		{

		}

		private void Button_Exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Tasual_Main_Resize(object sender, EventArgs e)
		{
			Invalidate();
			Tasual_ListView_SizeColumns();
		}

		private void keepOnTToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Tasual_Array_Save_Text(ref TaskArray);
			Tasual_Array_Load_Text(ref TaskArray);
			Tasual_ListView_PopulateFromArray(ref TaskArray);
		}

		private void Tasual_ListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			//Tasual_SizeColumns();
			e.NewWidth = this.Tasual_ListView.Columns[e.ColumnIndex].Width;
			e.Cancel = true;
		}

		private void taskToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Tasual_Create_Task TaskForm = new Tasual_Create_Task();
			TaskForm.Show(this);
		}

		private void Tasual_StatusLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Tasual_StatusLabel_MenuStrip.Show(Tasual_StatusLabel, new Point(0, Tasual_StatusLabel.Height));
		}

		private void Tasual_AboutLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Tasual_About AboutForm = new Tasual_About();
			AboutForm.ShowDialog(this);
		}

		private void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Tasual_Confirm_Clear ConfirmForm = new Tasual_Confirm_Clear(this);
			ConfirmForm.ShowDialog(this);
		}

		private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//Tasual_ListView_ClearAll();
			//Tasual_ListView_PopulateFromArray(ref TaskArray, ref timegroups);
			TaskItem.TaskTime foobar = new TaskItem.TaskTime();
			foobar.Start = 1;
			foobar.End = 2;
			foobar.Next = 3;
			Tasual_Array_AddTask(ref TaskArray, 0, 0, 0, "Cows", "Testing 123", foobar);
		}

		private void Tasual_ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			TaskItem Task = (TaskItem)e.Item.Tag;

			if (e.Item.Checked)
			{
				e.Item.ForeColor = Color.FromArgb(255, 189, 208, 230);
				if (Task.Status != (int)taskstatus_t.STAT_COMPLETED)
				{
					++Tasual_Tasks_Complete;
					Task.Status = (int)taskstatus_t.STAT_COMPLETED;
					Tasual_Array_Save_Text(ref TaskArray);
				}

			}
			else
			{
				e.Item.ForeColor = Color.FromArgb(255, 36, 90, 150);
				if (Task.Status != (int)taskstatus_t.STAT_NEW)
				{
					--Tasual_Tasks_Complete;
					Task.Status = (int)taskstatus_t.STAT_NEW;
					Tasual_Array_Save_Text(ref TaskArray);
				}
			}

			Tasual_StatusLabel_UpdateCounts();
		}
	}
	public class TaskItem
	{
		public struct TaskTime
		{
			public double Start;
			public double End;
			public double Next;
		}

		public int Type;
		public int Priority;
		public int Status;
		public string Group;
		public string Description;
		public TaskTime Time;


		// constructors
		//taskitem_c();
		//taskitem_c(int, int, int, int, string, xyztime);

		// deconstructor
		//~taskitem_c();
	}
}
