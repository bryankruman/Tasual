﻿using System;
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
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using BrightIdeasSoftware;

namespace Tasual
{
	public partial class Tasual_Main : Form
	{
		// ==============
		//  Declarations
		// ==============

		public List<Task> TaskArray = new List<Task>();
		public Setting Settings = new Setting();

		OlvListViewHitTestInfo CalendarPopout = null;
		OlvListViewHitTestInfo Tasual_ListView_FirstClickInfo = null;
		bool Tasual_ListView_PreviouslySelected = false;


		// =============================
		//  Common/Supporting Functions
		// =============================

		public void Tasual_ClearAll()
		{
			TaskArray.Clear();
			Tasual_Array_Save_Text();
			Tasual_Array_Load_Text();
			Tasual_ListView.BuildList();
		}

		/* // TODO: Re-write
		private void Tasual_ReAssignGroup(string OldTaskGroup, string NewTaskGroup)
		{
			foreach (Task Task in TaskArray)
			{
				if (Task == null) { break; }
				if (Task.Group == OldTaskGroup)
				{
					Task.Group = NewTaskGroup;
					Tasual_ListView_AssignGroup(NewTaskGroup, ref Task.Item);
				}
			}
		} */

		public void Tasual_PrintTaskToConsole(Task TaskItem)
		{
			Console.WriteLine(
				"TaskItem: '{0}', '{1}', '{2}', '{3}', '{4}', ('{5}', '{6}', '{7}')",
				TaskItem.Checked,
				TaskItem.Priority,
				TaskItem.Status,
				TaskItem.Group,
				TaskItem.Description,
				TaskItem.Time.Start,
				TaskItem.Time.End,
				TaskItem.Time.Next
			);
		}

		public void Tasual_StatusLabel_UpdateCounts()
		{
			int Complete = Tasual_ListView.CheckedItems.Count;
			int Total = Tasual_ListView.GetItemCount();

			if (Complete == Total)
			{
				Tasual_StatusLabel.Text = "All tasks complete";
			}
			else
			{
				Tasual_StatusLabel.Text = string.Format("{0} of {1} tasks complete", Complete, Total);
			}
		}


		// =================
		//  Array Functions
		// =================


		public void Tasual_Array_Save_JSON()
		{
			try
			{
				string Output = JsonConvert.SerializeObject(TaskArray, Formatting.Indented);
				//Console.WriteLine("{0}", Output);

				using (FileStream OutputFile = File.Open("local.json", FileMode.CreateNew))
				using (StreamWriter OutputStream = new StreamWriter(OutputFile))
				using (JsonWriter OutputJson = new JsonTextWriter(OutputStream))
				{
					OutputJson.Formatting = Formatting.Indented;

					JsonSerializer Serializer = new JsonSerializer();
					Serializer.Serialize(OutputJson, TaskArray);
				}

			}
			catch (Exception e)
			{
				Console.WriteLine("Tasual_Array_Save_JSON(): {0}\nTrace: {1}", e.Message, e.StackTrace);
			}
		}

		public void Tasual_Array_Load_JSON()
		{
			try
			{
				TaskArray.Clear();

				using (StreamReader InputFile = File.OpenText("local.json"))
				{
					JsonSerializer Serializer = new JsonSerializer();
					//TaskArray = (List<Task>)Serializer.Deserialize(InputFile);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Tasual_Array_Load_JSON(): {0}\nTrace: {1}", e.Message, e.StackTrace);
			}
		}

		public void Tasual_Array_Save_Text()
		{
			Console.WriteLine("Tasual_Array_Save_Text();");
			try
			{
				using (StreamWriter OutputFile = new StreamWriter(Settings.TextFile))
				{
					foreach (Task Task in TaskArray)
					{
						string Line;
						Line = Task.Checked.ToString();
						Line = Line + (char)29 + Task.Priority.ToString();
						Line = Line + (char)29 + Task.Status.ToString();
						Line = Line + (char)29 + Task.Group;
						Line = Line + (char)29 + Task.Description;

						DateTimeOffset TimeOffset;
						TimeOffset = new DateTimeOffset(Task.Time.Start.ToLocalTime());
						Line = Line + (char)29 + TimeOffset.ToUnixTimeSeconds();

						TimeOffset = new DateTimeOffset(Task.Time.End.ToLocalTime());
						Line = Line + (char)29 + TimeOffset.ToUnixTimeSeconds();

						TimeOffset = new DateTimeOffset(Task.Time.Next.ToLocalTime());
						Line = Line + (char)29 + TimeOffset.ToUnixTimeSeconds();
						OutputFile.WriteLine(Line);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Tasual_Array_Save_Text(): {0}\nTrace: {1}", e.Message, e.StackTrace);
			}
		}

		public void Tasual_Array_Load_Text()
		{
				Console.WriteLine("Tasual_Array_Load_Text();");
				try
			{
				TaskArray.Clear();

				using (StreamReader InputFile = new StreamReader(Settings.TextFile))
				{
					int counter = 0;
					string Line;

					while ((Line = InputFile.ReadLine()) != null)
					{
						Task NewItem = new Task();
						//NewItem.Time = new Task.TimeInfo();
						int argtype = 0;
						string[] segments = Line.Split((char)29);
						double UnixTime;

						foreach (string token in segments)
						{
							// lets do something with this data now
							int Temp = 0;
								bool TempBool = false;
							switch (argtype)
							{
								case (int)Task.Arguments.Checked: { Boolean.TryParse(token, out TempBool); NewItem.Checked = TempBool; break; }
								case (int)Task.Arguments.Priority: { Int32.TryParse(token, out Temp); NewItem.Priority = Temp; break; }
								case (int)Task.Arguments.Status: { Int32.TryParse(token, out Temp); NewItem.Status = Temp; break; }
								case (int)Task.Arguments.Group: { NewItem.Group = token; break; }
								case (int)Task.Arguments.Description: { NewItem.Description = token; break; }
								case (int)Task.Arguments.Created:
									{
										Double.TryParse(token, out UnixTime);
										NewItem.Time.Start = DateTimeOffset.FromUnixTimeSeconds((long)UnixTime).DateTime.ToLocalTime();
										break;
									}
								case (int)Task.Arguments.Ending:
									{
										Double.TryParse(token, out UnixTime);
										NewItem.Time.End = DateTimeOffset.FromUnixTimeSeconds((long)UnixTime).DateTime.ToLocalTime();
										break;
									}
								case (int)Task.Arguments.Next:
									{
										Double.TryParse(token, out UnixTime);
										NewItem.Time.Next = DateTimeOffset.FromUnixTimeSeconds((long)UnixTime).DateTime.ToLocalTime();
										break;
									}
								default:
									{
										Console.WriteLine("TOO MANY ARGUMENTS IN FILE !!!!!!");
										break;
									}
							}

							++argtype;
							//Console.WriteLine(token);
						}

						if (argtype == (int)Task.Arguments.Count)
							TaskArray.Add(NewItem);

						//Console.WriteLine(line);
						counter++;
					}
				}

			}
			catch (Exception e)
			{
				Console.WriteLine("Tasual_Array_Load_Text(): {0}\nTrace: {1}", e.Message, e.StackTrace);
			}
		}


		// ====================
		//  ListView Functions
		// ====================

		public static string Ordinal(int number)
		{
			string suffix = String.Empty;

			int ones = number % 10;
			int tens = (int)Math.Floor(number / 10M) % 10;

			if (tens == 1)
			{
				suffix = "th";
			}
			else
			{
				switch (ones)
				{
					case 1:
						suffix = "st";
						break;

					case 2:
						suffix = "nd";
						break;

					case 3:
						suffix = "rd";
						break;

					default:
						suffix = "th";
						break;
				}
			}
			return String.Format("{0}{1}", number, suffix);
		}

		private void Tasual_ListView_AddColumns()
		{
			/*
			switch (Tasual_Setting_Style)
			{
				// "CUSTOM" STYLE:
				// - groups: overdue at top, normal group listings below, completed at bottom
				// - columns: Description, Time
				case Styles.Custom:
					{
						ItemColumnData = new string[2];
						ItemColumnData[0] = Task.Description;
						ItemColumnData[1] = Tasual_ListView_FormatTime(Task.Time.Start.ToLocalTime(), TimeFormat.Short);
						break;
					}

				// "SIMPLE" STYLE:
				// - groups: overdue at top, today, tomorrow, this week, next week, future, completed at bottom
				// - columns: Description, Time
				case Styles.Simple:
					{
						ItemColumnData = new string[2];
						ItemColumnData[0] = Task.Description;
						ItemColumnData[1] = Task.Group.ToString();
						break;
					}

				// "DETAILED" STYLE:
				// - groups: overdue at top, today, tomorrow, this week, next week, future, completed at bottom
				// - columns: Description, Category, Time
				case Styles.Detailed:
					{
						ItemColumnData = new string[3];
						ItemColumnData[0] = Task.Description;
						ItemColumnData[1] = Task.Group.ToString();
						ItemColumnData[2] = Tasual_ListView_FormatTime(Task.Time.Start.ToLocalTime(), TimeFormat.Elapsed);
						break;
					}

				default:
					{
						throw new Exception("Tasual_ListView_CreateListViewItem(): Invalid style setting!");
					}
			}*/

			OLVColumn DescriptionColumn = new OLVColumn("Description", "Description");
			//DescriptionColumn.AspectName = "Description";
			//DescriptionColumn.Sortable = true;
			DescriptionColumn.MinimumWidth = 100;
			DescriptionColumn.FillsFreeSpace = true;
			DescriptionColumn.IsVisible = true;
			DescriptionColumn.IsEditable = true;
			//DescriptionColumn.HasFilterIndicator = false;
			DescriptionColumn.Sortable = true;
			DescriptionColumn.DisplayIndex = 1;
			DescriptionColumn.LastDisplayIndex = 1;
			//DescriptionColumn.header = false;
			//DescriptionColumn.IsEditable = false;
			//DescriptionColumn.Edit
			//DescriptionColumn.ImageGetter = new
			//DescriptionColumn.AspectToStringConverter = 
			DescriptionColumn.HeaderTextAlign = HorizontalAlignment.Center;
			Tasual_ListView.AllColumns.Add(DescriptionColumn);
			Tasual_ListView.Columns.AddRange(new ColumnHeader[] { DescriptionColumn });

			OLVColumn IconColumn = new OLVColumn("Icons", "");
			IconColumn.Renderer = new ImageRenderer();
			IconColumn.AspectGetter = delegate (object Input)
			{
				Task Task = (Task)Input;
				int[] Images = new int[3];

				if (Task.Location != null) { Images[0] = 1; }
				if (Task.Link != null) { Images[1] = 2; }
				if (Task.Notes != null) { Images[2] = 3; }

				// TODO: Make this work
				if (Images.Count() == 0)
				{
					Images[0] = 1;
				}

				return Images;
			};
			IconColumn.MinimumWidth = 30;
			IconColumn.IsVisible = true;
			IconColumn.IsEditable = false;
			IconColumn.Sortable = false;
			IconColumn.DisplayIndex = 2;
			IconColumn.LastDisplayIndex = 2;
			IconColumn.TextAlign = HorizontalAlignment.Right;
			IconColumn.HeaderTextAlign = HorizontalAlignment.Center;
			IconColumn.ShowTextInHeader = false;
			Tasual_ListView.AllColumns.Add(IconColumn);
			Tasual_ListView.Columns.AddRange(new ColumnHeader[] { IconColumn });

			OLVColumn CategoryColumn = new OLVColumn("Category", "Group");
			//CategoryColumn.AspectName = "Group";
			//CategoryColumn.Sortable = false;
			CategoryColumn.MinimumWidth = 100;
			CategoryColumn.IsVisible = false;
			CategoryColumn.IsEditable = true;
			CategoryColumn.DisplayIndex = 3;
			CategoryColumn.LastDisplayIndex = 3;
			//DescriptionColumn.
			//DescriptionColumn.AspectToStringConverter = 
			CategoryColumn.TextAlign = HorizontalAlignment.Center;
			CategoryColumn.HeaderTextAlign = HorizontalAlignment.Center;
			Tasual_ListView.AllColumns.Add(CategoryColumn);
			Tasual_ListView.Columns.AddRange(new ColumnHeader[] { CategoryColumn });

			OLVColumn DueColumn = new OLVColumn("Due", "Time");
			//TimeColumn.AspectName = "Time";
			//TimeColumn.Sortable = false;
			//TimeColumn.Width = -1;
			DueColumn.MinimumWidth = 80;
			DueColumn.IsVisible = false;
			DueColumn.IsEditable = false;
			DueColumn.DisplayIndex = 4;
			DueColumn.LastDisplayIndex = 4;
			DueColumn.TextAlign = HorizontalAlignment.Center;
			DueColumn.HeaderTextAlign = HorizontalAlignment.Center;
			DueColumn.AspectToStringConverter = delegate (object Input)
			{
				TimeInfo Time = (TimeInfo)Input;
				return Tasual_ListView_FormatTime(Time.Start.ToLocalTime(), Setting.TimeFormat.Due);
			};
			Tasual_ListView.AllColumns.Add(DueColumn);
			Tasual_ListView.Columns.AddRange(new ColumnHeader[] { DueColumn });

			OLVColumn TimeColumn = new OLVColumn("Time", "Time");
			//TimeColumn.AspectName = "Time";
			//TimeColumn.Sortable = false;
			//TimeColumn.Width = -1;
			TimeColumn.MinimumWidth = 130;
			TimeColumn.IsVisible = true;
			TimeColumn.IsEditable = false;
			TimeColumn.DisplayIndex = 5;
			TimeColumn.LastDisplayIndex = 5;
			TimeColumn.TextAlign = HorizontalAlignment.Center;
			TimeColumn.HeaderTextAlign = HorizontalAlignment.Center;
			TimeColumn.AspectToStringConverter = delegate(object Input)
			{
				TimeInfo Time = (TimeInfo)Input;
				return Tasual_ListView_FormatTime(Time.Start.ToLocalTime(), Setting.TimeFormat.Short);
			};
			Tasual_ListView.AllColumns.Add(TimeColumn);
			Tasual_ListView.Columns.AddRange(new ColumnHeader[] { TimeColumn });

			Tasual_ListView.AlwaysGroupByColumn = CategoryColumn;
		}

		public string Tasual_ListView_FormatTime(DateTime Time, Setting.TimeFormat Format)
		{
			switch (Format)
			{
				case Setting.TimeFormat.Elapsed:
					{
						TimeSpan TS = DateTime.Now - Time;
						int intYears = DateTime.Now.Year - Time.Year;
						int intMonths = DateTime.Now.Month - Time.Month;
						int intDays = DateTime.Now.Day - Time.Day;
						int intHours = DateTime.Now.Hour - Time.Hour;
						int intMinutes = DateTime.Now.Minute - Time.Minute;
						int intSeconds = DateTime.Now.Second - Time.Second;
						if (intYears > 0) return String.Format("{0} {1} ago", intYears, (intYears == 1) ? "year" : "years");
						else if (intMonths > 0) return String.Format("{0} {1} ago", intMonths, (intMonths == 1) ? "month" : "months");
						else if (intDays > 0) return String.Format("{0} {1} ago", intDays, (intDays == 1) ? "day" : "days");
						else if (intHours > 0) return String.Format("{0} {1} ago", intHours, (intHours == 1) ? "hour" : "hours");
						else if (intMinutes > 0) return String.Format("{0} {1} ago", intMinutes, (intMinutes == 1) ? "minute" : "minutes");
						else if (intSeconds > 1) return String.Format("{0} {1} ago", intSeconds, (intSeconds == 1) ? "second" : "seconds");
						else if (intSeconds >= 0) return "Just now";
						else
						{
							return String.Format("{0} {1}", Time.ToShortDateString(), Time.ToShortTimeString());
						}
					}

				case Setting.TimeFormat.Due:
					{
						// Overdue
						// Today
						// Tomorrow
						// Friday
						// Next Week
						// 2 Weeks
						// Next Month
						// Future

						if (DateTime.Now > Time)
						{
							return "Overdue";
						}
						else
						{
							DateTime Today = DateTime.Now - DateTime.Now.TimeOfDay;
							DateTime TargetDay = Time - Time.TimeOfDay;
							TimeSpan Span = TargetDay - Today;
							if (TargetDay.Day == Today.Day)
							{
								return "Today";
							}
							else if (Span.Days <= 1)
							{
								return "Tomorrow";
							}
							else if (Span.Days <= 7)
							{
								return Time.DayOfWeek.ToString();
							}
							else if (Span.Days <= 14)
							{
								return "Next Week";
							}
							else if (Span.Days <= 21)
							{
								return "2 Weeks";
							}
							else if (Span.Days <= 30)
							{
								return "3 Weeks";
							}
							else if (TargetDay.Month == Today.AddMonths(1).Month)
							{
								return "Next Month";
							}
							else
							{
								return "Future";
							}
						}
					}

				case Setting.TimeFormat.Short: // "6/6 - Tue 10pm"
					{
						string TimeStamp = "";
						if (Time.TimeOfDay != TimeSpan.Zero)
						{
							string Minutes = "";
							if (Time.Minute != 0) { Minutes = ":" + Time.Minute.ToString("00"); }

							if (Time.Hour > 12)
							{
								TimeStamp = " - " + (Time.Hour - 12).ToString();
								TimeStamp = TimeStamp + Minutes + "pm";
							}
							else
							{
								TimeStamp = " - " + Time.Hour.ToString();
								TimeStamp = TimeStamp + Minutes + "am";
							}
						}

						return String.Format(
							"{0}{1}",
							Time.ToString("M/d - ddd"),
							TimeStamp);
					}

				case Setting.TimeFormat.Medium: // "Sat, Jun 6th at 10:00pm"
					{
						string TimeStamp;
						if (Time.TimeOfDay == TimeSpan.Zero)
						{
							TimeStamp = "";
						}
						else
						{
							TimeStamp = "at ";
							TimeStamp = TimeStamp + Time.Hour.ToString() + ":" + Time.Minute.ToString();
							if (Time.Hour <= 12) { TimeStamp = TimeStamp + "am"; }
							else { TimeStamp = TimeStamp + "pm"; }
						}

						return String.Format(
							"{0} {1} {2}",
							Time.ToString("ddd, MMM"),
							Ordinal(Time.Day),
							TimeStamp);
					}

				case Setting.TimeFormat.Long: // "Tuesday, June 6th at 10:00pm"
					{
						string TimeStamp;
						if (Time.TimeOfDay == TimeSpan.Zero)
						{
							TimeStamp = "";
						}
						else
						{
							TimeStamp = "at ";
							TimeStamp = TimeStamp + Time.Hour.ToString() + ":" + Time.Minute.ToString();
							if (Time.Hour <= 12) { TimeStamp = TimeStamp + "am"; }
							else { TimeStamp = TimeStamp + "pm"; }
						}

						return String.Format(
							"{0} {1} {2}",
							Time.ToString("dddd, MMMM"),
							Ordinal(Time.Day),
							TimeStamp);
					}

				default: return "";
			}
		}


		// ================
		//  Event Handlers
		// ================

		// MenuStrips
		private void Tasual_MenuStrip_Create_Advanced_Click(object sender, EventArgs e)
		{
			Tasual_Create TaskForm = new Tasual_Create(this);
			TaskForm.ShowDialog(this);
		}

		private void Tasual_MenuStrip_Create_Quick_Click(object sender, EventArgs e)
		{
			// TODO: Try to find the "first" group first, if nothing is found then just use the default group of "Tasks"
			//if (Tasual_ListView.OLVGroups.FirstOrDefault() != null)
			//{
			//    GroupName = Tasual_ListView.OLVGroups.FirstOrDefault().Name;
			//}

			Task Task = new Task(
				false,
				0,
				0,
				"Tasks",
				"New task",
				new TimeInfo(),
				new Timer()
			);

			TaskArray.Add(Task);
			Tasual_Array_Save_Text();
			Tasual_ListView.BuildList();
			Tasual_StatusLabel_UpdateCounts();
			Tasual_ListView.EditModel(Task);
		}

		private void Tasual_MenuStrip_Edit_Click(object sender, EventArgs e)
		{
			if (Tasual_ListView.SelectedItem != null)
			{
				Tasual_ListView.EditModel(Tasual_ListView.SelectedItem.RowObject);
			}
		}

		private void Tasual_MenuStrip_Settings_AlwaysOnTop_Click(object sender, EventArgs e)
		{
			//Tasual_ReAssignGroup("Testing", "");
			Tasual_Array_Save_Text();
			Tasual_Array_Load_Text();
			Tasual_ListView.BuildList();
		}

		private void Tasual_MenuStrip_Sources_Click(object sender, EventArgs e)
		{
			Tasual_Array_Save_JSON();
		}

		private void Tasual_StatusLabel_MenuStrip_Clear_Click(object sender, EventArgs e)
		{
			Tasual_Confirm_Clear ConfirmForm = new Tasual_Confirm_Clear(this);
			ConfirmForm.ShowDialog(this);
		}

		// Notification Icon
		private void Tasual_Notify_Click(object sender, EventArgs e)
		{
			ReturnFormInstance().Activate();
		}

		// Labels
		private void Tasual_StatusLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Tasual_MenuStrip_Status.Show(Tasual_StatusLabel, new Point(0, Tasual_StatusLabel.Height));
		}

		private void Tasual_AboutLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Tasual_About AboutForm = new Tasual_About();
			AboutForm.ShowDialog(this);
		}

		// ListView
		private void Tasual_ListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			//e.NewWidth = this.Tasual_ListView.Columns[e.ColumnIndex].Width;
			//e.Cancel = true;
		}

		private void Tasual_ListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			/* // TODO: Re-write
			Tasual_ListView.LabelEdit = false;

			ListViewItem Item = Tasual_ListView.Items[e.Item];

			if (Item != null)
			{
				Item.Selected = false;

				this.BeginInvoke((MethodInvoker)delegate
				{
					Task Task = (Task)Item.Tag;
					if ((Task != null) && (Item != null))
					{
						Task.Description = Item.Text.ToString();
						Tasual_Array_Save_Text();
					}
				});
			}*/
		}

		private void Tasual_ListView_SingleClick(MouseEventArgs e)
		{
			if (Tasual_ListView_PreviouslySelected == true)
			{
				if (Tasual_ListView.SelectedItem != null)
				{
					Tasual_ListView.EditModel(Tasual_ListView.SelectedItem.RowObject);
				}
			}
			else
			{
				// do nothing
			}
		}

		private void Tasual_ListView_DoubleClick(MouseEventArgs e)
		{
			OlvListViewHitTestInfo SecondClickInfo = Tasual_ListView.OlvHitTest(e.X, e.Y);

			if ((Tasual_ListView_FirstClickInfo.Item != null) && (SecondClickInfo.Item != null))
			{
				if (Tasual_ListView_FirstClickInfo.Item == SecondClickInfo.Item)
				{
					if (Tasual_ListView.SelectedItem != null)
					{
						this.BeginInvoke((MethodInvoker)delegate
						{
							Tasual_ListView.EditModel(Tasual_ListView.SelectedItem.RowObject);
						});
					}
				}
			}
		}

		private void Tasual_Timer_ListViewClick_Tick(object sender, EventArgs e)
		{
			if ((MouseButtons & MouseButtons.Left) == 0)
			{
				Tasual_ListView_SingleClick((MouseEventArgs)Tasual_Timer_ListViewClick.Tag);
			}
			Tasual_ListView_FirstClickInfo = null;
			Tasual_ListView_PreviouslySelected = false;
			Tasual_Timer_ListViewClick.Stop();
		}

		private void Tasual_ListView_MouseDown(object sender, MouseEventArgs e)
		{
			OlvListViewHitTestInfo Info = Tasual_ListView.OlvHitTest(e.X, e.Y);

			if (Info.Group != null)
			{
				// TODO: Fix the issue where clicking blank space inside of the group still triggers the group
				if (e.Button == MouseButtons.Right)
				{
					Tasual_MenuStrip_Group.Tag = Info.Group;
					Tasual_MenuStrip_Group.Show(Cursor.Position.X, Cursor.Position.Y);
				}
			}
			else if (Info.Item != null)
			{
				switch (e.Button)
				{
					case MouseButtons.Left:
						{
							if (Tasual_Timer_ListViewClick.Enabled) // second click
							{
								Tasual_ListView_DoubleClick(e);
								Tasual_ListView_PreviouslySelected = false;
								Tasual_ListView_FirstClickInfo = null;
								Tasual_Timer_ListViewClick.Stop();
							}
							else // first click
							{
								switch (Info.Column.AspectName)
								{
									case "Description":
										{
											Tasual_Timer_ListViewClick.Start();

											if (Tasual_ListView.SelectedItem == Info.Item)
											{
												Tasual_ListView_PreviouslySelected = true;
											}

											Tasual_ListView_FirstClickInfo = Info;
											Tasual_Timer_ListViewClick.Tag = e;
											break;
										}
									case "Group":
										{
											Console.WriteLine("Clicked on a category!");
											break;
										}
									case "Time":
										{
											if (Info.SubItem != null)
											{
												if (CalendarPopout == null)
												{
													CalendarPopout = Info;
												}
												else { CalendarPopout = null; }
											}
											break;
										}
								}
								/*if (e.X <= 20) // clicked checkbox area
								{
									ListViewItem SelectedItem = Info.Item;
									//Tasual_ListView_ChangeStatus(ref SelectedItem, (int)Task.Statuses.Toggle);
									Tasual_StatusLabel_UpdateCounts();
									Tasual_Array_Save_Text();
								}*/
							}
							break;
						}

					case MouseButtons.Right:
						{
							Tasual_MenuStrip_Item.Tag = Info.Item.RowObject;
							Tasual_MenuStrip_Item.Show(Cursor.Position.X, Cursor.Position.Y);
							break;
						}

					case MouseButtons.Middle:
						{
							// todo: perhaps duplicate item if middle clicked?
							break;
						}

					default:
						{
							// can this even happen?
							Console.WriteLine("unknown button");
							break;
						}
				}
			}
			/* else if (Info != null)
			{
				// TODO: Should we handle clicking blank space in any particular way?
			}*/
		}

		private void Tasual_ListView_MouseUp(object sender, MouseEventArgs e)
		{
			if (CalendarPopout != null)
			{
				Tasual_Popout_Calendar Calendar = new Tasual_Popout_Calendar();

				Rectangle Bounds = CalendarPopout.SubItem.Bounds;
				Calendar.Location = PointToScreen(new Point(Bounds.Left, Bounds.Bottom + Bounds.Height + 5));
				Calendar.Show(this);

				CalendarPopout = null;
			}
		}

#if false
		ListViewItem UnderlinedItem;
		private void Tasual_ListView_MouseMove(object sender, MouseEventArgs e)
		{
			ListViewHitTestInfo Info = Tasual_ListView.HitTest(e.X, e.Y);

			if (Info.Item != null)
			{
				Tasual_ListView.Cursor = Cursors.Hand;
				if (Info.Item != UnderlinedItem)
				{
					if (UnderlinedItem != null)
						{ UnderlinedItem.Font = new Font(UnderlinedItem.Font.Name, 9); }

					Info.Item.Font = new Font(Info.Item.Font.Name, 9, FontStyle.Underline);
					UnderlinedItem = Info.Item;
				}
			}
			else
			{
				Tasual_ListView.Cursor = Cursors.Default;
			}
		}
#endif


		// ================
		//  Core Functions
		// ================

		public Tasual_Main()
		{
			InitializeComponent();

			Tasual_Timer_ListViewClick.Interval = SystemInformation.DoubleClickTime;
		}

		private void Tasual_ListView_FormatRow(object sender, FormatRowEventArgs e)
		{
			if (e.Item.Checked)
			{
				e.Item.ForeColor = Color.FromArgb(255, 189, 208, 230);
			}
			else
			{
				e.Item.ForeColor = Color.FromArgb(255, 36, 90, 150);
			}
		}

		private void Tasual_Main_Load(object sender, EventArgs e)
		{
			// TODO: Load settings from json file
			Settings.AlwaysOnTop = false; // currently unused
			Settings.ConfirmClear = true; // currently unused
			Settings.ConfirmDelete = true; // currently unused
			Settings.Style = Setting.Styles.Custom; // currently unused
			Settings.Protocol = Setting.Protocols.Text;
			Settings.TextFile = "localdb.txt";

			// load task array
			switch (Settings.Protocol)
			{
				case Setting.Protocols.JSON: { Tasual_Array_Load_JSON(); break; }

				default:
				case Setting.Protocols.Text: { Tasual_Array_Load_Text(); break; }
			}

			Tasual_ListView.ShowGroups = true;
			Tasual_ListView.ShowItemCountOnGroups = true;
			//Tasual_ListView.HotItemStyle = new HotItemStyle();
			//Tasual_ListView.HotItemStyle.FontStyle = FontStyle.Underline;
			Tasual_ListView.UseCustomSelectionColors = true;
			Tasual_ListView.SelectedBackColor = Color.FromArgb(255, 222, 232, 246);
			Tasual_ListView.SelectedForeColor = Color.FromArgb(255, 36, 90, 150);
			Tasual_ListView.UnfocusedSelectedBackColor = Color.FromArgb(255, 222, 232, 246);
			Tasual_ListView.UnfocusedSelectedForeColor = Color.FromArgb(255, 36, 90, 150);
			Tasual_ListView.FormatRow += new EventHandler<FormatRowEventArgs>(Tasual_ListView_FormatRow);
			//Tasual_ListView.TintSortColumn = false; // TODO: Add settings choice for this
			//Tasual_ListView.SelectedColumnTint = Color.FromArgb(255, 230, 230, 255);
			HeaderFormatStyle FormatStyle = new HeaderFormatStyle();
			FormatStyle.Normal.ForeColor = Color.FromArgb(255, 36, 90, 150);
			FormatStyle.Normal.BackColor = Color.FromArgb(255, 222, 232, 246);
			FormatStyle.Hot.ForeColor = Color.FromArgb(255, 36, 90, 150);
			FormatStyle.Hot.BackColor = Color.FromArgb(255, 240, 240, 255);
			FormatStyle.Pressed.ForeColor = Color.FromArgb(255, 36, 90, 150);
			FormatStyle.Pressed.BackColor = Color.FromArgb(255, 240, 240, 255);
			Tasual_ListView.HeaderUsesThemes = false;
			Tasual_ListView.HeaderFormatStyle = FormatStyle;
			Tasual_ListView.PersistentCheckBoxes = true;
			Tasual_ListView.CheckBoxes = true;
			Tasual_ListView.CheckedAspectName = "Checked";
			Tasual_ListView.CellEditUseWholeCell = true;
			//Tasual_ListView.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick;

			Tasual_ListView.IsSimpleDragSource = true;
			Tasual_ListView.IsSimpleDropSink = true;

			Tasual_ListView_AddColumns();

			Tasual_ListView.SetObjects(TaskArray);
			Tasual_ListView.RebuildColumns();
			//Tasual_ListView.AutoResizeColumns();

			Tasual_StatusLabel_UpdateCounts();
		}

		public static Tasual_Main ReturnFormInstance()
		{
			return Application.OpenForms[0] as Tasual_Main;
		}

		private void Tasual_Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			Tasual_Notify.Dispose();
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//ListViewGroup OldGroup = Item.Group;
			TaskArray.Remove((Task)Tasual_MenuStrip_Item.Tag);
			Tasual_Array_Save_Text();
			Tasual_ListView.BuildList();
			Tasual_StatusLabel_UpdateCounts();
		}

		private void Tasual_ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			Tasual_StatusLabel_UpdateCounts();
		}

		private void editToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Tasual_ListView.EditModel((Task)Tasual_MenuStrip_Item.Tag);
		}

		private void Tasual_ListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Tasual_ListView.SelectedItem != null)
			{
				Tasual_MenuStrip_Edit.Enabled = true;
			}
			else
			{
				Tasual_MenuStrip_Edit.Enabled = false;
			}
		}
	}
}
