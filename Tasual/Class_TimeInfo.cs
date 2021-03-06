﻿// ===========================================
//  Copyright (c) 2017 Bryan Kruman
//
//  See LICENSE.rtf file in the project root
//  for full license information.
// ===========================================

using System;
using Newtonsoft.Json;

namespace Tasual
{
	/// <summary>
	/// Definitions and supporting functions for TimeInfo.
	/// </summary>
	public class TimeInfo
	{
		/// <summary>
		/// Complex recurrance filter for months.
		/// </summary>
		[Flags]
		public enum MonthFlag
		{
			/// <summary>Allow complex recurrances matching January.</summary>
			January = 1,
			/// <summary>Allow complex recurrances matching February.</summary>
			February = 2,
			/// <summary>Allow complex recurrances matching March.</summary>
			March = 4,
			/// <summary>Allow complex recurrances matching April.</summary>
			April = 8,
			/// <summary>Allow complex recurrances matching May.</summary>
			May = 16,
			/// <summary>Allow complex recurrances matching June.</summary>
			June = 32,
			/// <summary>Allow complex recurrances matching July.</summary>
			July = 64,
			/// <summary>Allow complex recurrances matching August.</summary>
			August = 128,
			/// <summary>Allow complex recurrances matching September.</summary>
			September = 256,
			/// <summary>Allow complex recurrances matching October.</summary>
			October = 512,
			/// <summary>Allow complex recurrances matching November.</summary>
			November = 1024,
			/// <summary>Allow complex recurrances matching December.</summary>
			December = 2048,
			/// <summary>Allow complex recurrances matching all months.</summary>
			Everymonth = 4095
		}

		/// <summary>
		/// Complex recurrance filter for weeks.
		/// </summary>
		[Flags]
		public enum WeekFlag
		{
			/// <summary>Allow complex recurrances matching the first week of the month.</summary>
			First = 1,
			/// <summary>Allow complex recurrances matching the second week of the month.</summary>
			Second = 2,
			/// <summary>Allow complex recurrances matching the third week of the month.</summary>
			Third = 4,
			/// <summary>Allow complex recurrances matching the fourth week of the month.</summary>
			Fourth = 8,
			/// <summary>Allow complex recurrances matching the fifth week of the month.</summary>
			Fifth = 16,
			/// <summary>Allow complex recurrances matching the third, fourth, or fifth week of the month.</summary>
			Last = 32,
			/// <summary>Combines First, Second, and Last flags.</summary>
			/// <remarks>Skips 45th and 5th options as they can't happen via dialog.</remarks>
			FirstThruLast = 39,
			/// <summary>Combines First, Second, Third, Fourth, and Fifth flags.</summary>
			Everyweek = 31
		}

		/// <summary>
		/// Complex recurrance filter for days.
		/// </summary>
		[Flags]
		public enum DayFlag
		{
			/// <summary>Allow complex recurrances matching Monday.</summary>
			Monday = 1,
			/// <summary>Allow complex recurrances matching Tuesday.</summary>
			Tuesday = 2,
			/// <summary>Allow complex recurrances matching Wednesday.</summary>
			Wednesday = 4,
			/// <summary>Allow complex recurrances matching Thursday.</summary>
			Thursday = 8,
			/// <summary>Allow complex recurrances matching Friday.</summary>
			Friday = 16,
			/// <summary>Allow complex recurrances matching Saturday.</summary>
			Saturday = 32,
			/// <summary>Allow complex recurrances matching Sunday.</summary>
			Sunday = 64,
			/// <summary>Allow complex recurrances matching all days.</summary>
			Everyday = 127
		}

		/// <summary>
		/// Time formatting style.
		/// </summary>
		public enum TimeFormat
		{
			/// <summary>Time string formatted to show elapsed time.</summary>
			Elapsed,
			/// <summary>Time string formatted in a short summary: "6/6 - Tue 10pm"</summary>
			Short,
			/// <summary>Time string formatted in a medium summary: "Sat, Jun 6th at 10:00pm"</summary>
			Medium,
			/// <summary>Time string formatted in a long summary: "Tuesday, June 6th at 10:00pm"</summary>
			Long
		}

		/// <summary>
		/// Style of repeating task.
		/// </summary>
		public enum RepeatType
		{
			/// <summary>Task only has one occurence/does not repeat.</summary>
			Singular,
			/// <summary>Task repeats on simple terms (i.e. once a week).</summary>
			SimpleRepeat,
			/// <summary>Task repeats on complex terms (i.e. on specific days every few months).</summary>
			ComplexRepeat
		}

		/// <summary>
		/// Task dismissal time.
		/// </summary>
		public enum DismissType
		{
			/// <summary>Never dismiss task.</summary>
			Never,
			/// <summary>Dismiss task immediately.</summary>
			Immediate,
			/// <summary>Dismiss task after one hour.</summary>
			OneHour,
			/// <summary>Dismiss task after twelve hours.</summary>
			TwelveHours,
			/// <summary>Dismiss task after one day.</summary>
			OneDay,
			/// <summary>Dismiss task after one week.</summary>
			OneWeek,
			/// <summary>Dismiss task after one month.</summary>
			OneMonth
		}

		// For all tasks
		/// <summary>Description of this tasks schedule and how it repeats.</summary>
		[JsonProperty("summary")]
		public string Summary;

		/// <summary>Date of task being checked/completed.</summary>
		[JsonProperty("checkedtime")]
		public DateTime CheckedTime;

		/// <summary>Date of task creation.</summary>
		[JsonProperty("created")]
		public DateTime Created;

		/// <summary>Date of the last modification to this task.</summary>
		[JsonProperty("modified")]
		public DateTime Modified;

		/// <summary>Date of the first occurence for this task.</summary>
		[JsonProperty("start")]
		public DateTime Start;

		/// <summary>Date of the next occurence for this task.</summary>
		[JsonProperty("next")]
		public DateTime Next;

		/// <summary>DissmissType timespan after due date ("Next") to remove task.</summary>
		[JsonProperty("dismiss")]
		public DismissType Dismiss;

		/// <summary>Boolean for if this task has expired already.</summary>
		[JsonProperty ("expired")]
		public bool Expired;


		// For all recurring tasks
		/// <summary>Date when task stops recurring.</summary>
		[JsonProperty("end")]
		public DateTime End;

		/// <summary>Number of total occurences allowed.</summary>
		[JsonProperty("iterations")]
		public int Iterations;

		/// <summary>Instance count of this task (starts at 1).</summary>
		[JsonProperty("count")]
		public int Count;


		// Simple recurring tasks
		/// <summary>Number of years to iterate for simple recurrance.</summary>
		[JsonProperty("yearly")]
		public int Yearly;

		/// <summary>Number of months to iterate for simple recurrance.</summary>
		[JsonProperty("monthly")]
		public int Monthly;

		/// <summary>Number of weeks to iterate for simple recurrance.</summary>
		[JsonProperty("weekly")]
		public int Weekly;

		/// <summary>Number of days to iterate for simple recurrance.</summary>
		[JsonProperty("daily")]
		public int Daily;


		// Complex recurring tasks
		/// <summary>Time of day for complex recurrances.</summary>
		[JsonProperty("timeofday")]
		public TimeSpan TimeOfDay;

		/// <summary>Selected months for complex recurrances.</summary>
		[JsonProperty("monthfilter")]
		public MonthFlag MonthFilter;

		/// <summary>Selected weeks for complex recurrances.</summary>
		[JsonProperty("weekfilter")]
		public WeekFlag WeekFilter;

		/// <summary>Selected days for complex recurrances.</summary>
		[JsonProperty("dayfilter")]
		public DayFlag DayFilter;

		/// <summary>Specific day for complex recurrances.</summary>
		[JsonProperty("specificday")]
		public int SpecificDay;

		/// <summary>
		/// Blank TimeInfo constructor.
		/// </summary>
		public TimeInfo()
		{
			Summary = null;
			CheckedTime = DateTime.MinValue;
			Created = DateTime.MinValue;
			Modified = DateTime.MinValue;
			Start = DateTime.MinValue;
			Next = DateTime.MinValue;
			End = DateTime.MinValue;
			Iterations = 0;
			Count = 0;
			Dismiss = 0;
			Yearly = 0;
			Monthly = 0;
			Weekly = 0;
			Daily = 0;
			TimeOfDay = TimeSpan.FromSeconds(86399);
			MonthFilter = 0;
			WeekFilter = 0;
			DayFilter = 0;
			SpecificDay = 0;
		}

		/// <summary>
		/// TimeInfo constructor for singular tasks.
		/// </summary>
		/// <param name="_CheckedTime">Date of task being checked/completed.</param>
		/// <param name="_Created">Date of task creation.</param>
		/// <param name="_Modified">Date of the last modification to this task.</param>
		/// <param name="_Start">Date of the first occurence for this task.</param>
		/// <param name="_Next">Date of the next occurence for this task.</param>
		/// <param name="_End">Date when task stops recurring.</param>
		public TimeInfo(
			DateTime _CheckedTime,
			DateTime _Created,
			DateTime _Modified,
			DateTime _Start,
			DateTime _Next,
			DateTime _End)
		{
			Summary = null;
			CheckedTime = _CheckedTime;
			Created = _Created;
			Modified = _Modified;
			Start = _Start;
			Next = _Next;
			Dismiss = DismissType.Never;
			Expired = false;

			End = _End;
			Iterations = 0;
			Count = 0;

			Yearly = 0;
			Monthly = 0;
			Weekly = 0;
			Daily = 0;

			TimeOfDay = TimeSpan.FromSeconds(86399);
			MonthFilter = 0;
			WeekFilter = 0;
			DayFilter = 0;
			SpecificDay = 0;
		}

		/// <summary>
		/// Explicit TimeInfo constructor.
		/// </summary>
		/// <param name="_Summary">Description of this tasks schedule and how it repeats.</param>
		/// <param name="_CheckedTime">Date of task being checked/completed.</param>
		/// <param name="_Created">Date of task creation.</param>
		/// <param name="_Modified">Date of the last modification to this task.</param>
		/// <param name="_Start">Date of the first occurence for this task.</param>
		/// <param name="_Next">Date of the next occurence for this task.</param>
		/// <param name="_End">Date when task stops recurring.</param>
		/// <param name="_Dismiss">DissmissType timespan after due date ("Next") to remove task.</param>
		/// <param name="_Expired">Boolean for if this task has expired already.</param>
		/// <param name="_Iterations">Number of total occurences allowed.</param>
		/// <param name="_Count">Instance count of this task (starts at 1).</param>
		/// <param name="_Yearly">Number of years to iterate for simple recurrance.</param>
		/// <param name="_Monthly">Number of months to iterate for simple recurrance.</param>
		/// <param name="_Weekly">Number of weeks to iterate for simple recurrance.</param>
		/// <param name="_Daily">Number of days to iterate for simple recurrance.</param>
		/// <param name="_TimeOfDay">Time of day for complex recurrances.</param>
		/// <param name="_MonthFilter">Selected months for complex recurrances.</param>
		/// <param name="_WeekFilter">Selected weeks for complex recurrances.</param>
		/// <param name="_DayFilter">Selected days for complex recurrances.</param>
		/// <param name="_SpecificDay">Specific day for complex recurrances.</param>
		public TimeInfo(
			string _Summary,
			DateTime _CheckedTime,
			DateTime _Created,
			DateTime _Modified,
			DateTime _Start,
			DateTime _Next,
			DateTime _End,
			DismissType _Dismiss,
			bool _Expired,
			int _Iterations,
			int _Count,
			int _Yearly,
			int _Monthly,
			int _Weekly,
			int _Daily,
			TimeSpan _TimeOfDay,
			MonthFlag _MonthFilter,
			WeekFlag _WeekFilter,
			DayFlag _DayFilter,
			int _SpecificDay)
		{
			Summary = _Summary;
			CheckedTime = _CheckedTime;
			Created = _Created;
			Modified = _Modified;
			Start = _Start;
			Next = _Next;
			End = _End;
			Dismiss = _Dismiss;
			Expired = _Expired;

			Iterations = _Iterations;
			Count = _Count;

			Yearly = _Yearly;
			Monthly = _Monthly;
			Weekly = _Weekly;
			Daily = _Daily;

			TimeOfDay = _TimeOfDay;
			MonthFilter = _MonthFilter;
			WeekFilter = _WeekFilter;
			DayFilter = _DayFilter;
			SpecificDay = _SpecificDay;
		}

		/// <summary>
		/// Determine whether TimeInfo contains valid scheduling information.
		/// </summary>
		/// <param name="Time">TimeInfo to check.</param>
		/// <returns>True if TimeInfo.Next is not DateTime.MinValue, otherwise false.</returns>
		public static bool Scheduled(TimeInfo Time)
		{
			// TODO: Should a removed task be scheduled here?
			if (Time.Next != DateTime.MinValue)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Convert integer into ordinal string.
		/// </summary>
		/// <param name="number">Number to convert.</param>
		/// <returns>String containing number expressed with an ordinal (like 1st, 2nd, 3rd).</returns>
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

		/// <summary>
		/// Converts integer to MonthFlag selection.
		/// </summary>
		/// <param name="Input">Integer as DateTime.Month.</param>
		/// <returns>MonthFlag corresponding to the month provided in Input.</returns>
		private static MonthFlag FromMonthToFlag(int Input)
		{
			switch (Input)
			{
				case 1: return MonthFlag.January;
				case 2: return MonthFlag.February;
				case 3: return MonthFlag.March;
				case 4: return MonthFlag.April;
				case 5: return MonthFlag.May;
				case 6: return MonthFlag.June;
				case 7: return MonthFlag.July;
				case 8: return MonthFlag.August;
				case 9: return MonthFlag.September;
				case 10: return MonthFlag.October;
				case 11: return MonthFlag.November;
				case 12: return MonthFlag.December;
				default: return 0;
			}
		}

		/// <summary>
		/// Converts integer to WeekFlag selection.
		/// </summary>
		/// <param name="Input">Integer as DateTime.Day.</param>
		/// <returns>WeekFlag corresponding to the day of the month provided in Input divided by 7.</returns>
		private static WeekFlag FromWeekToFlag(int Input)
		{
			switch (Math.Ceiling((double)Input / 7))
			{
				case 1: return WeekFlag.First;
				case 2: return WeekFlag.Second;
				case 3: return WeekFlag.Third;
				case 4: return WeekFlag.Fourth;
				case 5: return WeekFlag.Fifth;
				default: return 0;
			}
		}

		/// <summary>
		/// Converts DayOfWeek to DayFlag selection.
		/// </summary>
		/// <param name="Input">DayOfWeek from DateTime.DayOfWeek.</param>
		/// <returns>DayFlag corresponding to the day of the week provided in Input.</returns>
		private static DayFlag FromDayToFlag(DayOfWeek Input)
		{
			switch (Input)
			{
				case DayOfWeek.Monday: return DayFlag.Monday;
				case DayOfWeek.Tuesday: return DayFlag.Tuesday;
				case DayOfWeek.Wednesday: return DayFlag.Wednesday;
				case DayOfWeek.Thursday: return DayFlag.Thursday;
				case DayOfWeek.Friday: return DayFlag.Friday;
				case DayOfWeek.Saturday: return DayFlag.Saturday;
				case DayOfWeek.Sunday: return DayFlag.Sunday;
				default: return 0;
			}
		}

		/// <summary>
		/// Converts DismissType into TimeSpan.
		/// </summary>
		/// <param name="Type">DismissType to check.</param>
		/// <returns>TimeSpan based upon DismissType selection.</returns>
		public static TimeSpan GetDismissTimeSpan(DismissType Type)
		{
			switch (Type)
			{
				case DismissType.Never: { return TimeSpan.Zero; }
				case DismissType.Immediate: { return TimeSpan.Zero; }
				case DismissType.OneHour: { return new TimeSpan(0, 1, 0, 0); }
				case DismissType.TwelveHours: { return new TimeSpan(0, 12, 0, 0); }
				case DismissType.OneDay: { return new TimeSpan(1, 0, 0, 0); }
				case DismissType.OneWeek: { return new TimeSpan(7, 0, 0, 0); }
				case DismissType.OneMonth: { return new TimeSpan(30, 0, 0, 0); }
				default: { return TimeSpan.Zero; }
			}
		}

		/// <summary>
		/// Find RepeatType for TimeInfo information.
		/// </summary>
		/// <param name="Time">TimeInfo to check.</param>
		/// <returns>RepeatType corresponding to the TimeInfo provided in Input.</returns>
		public static RepeatType GetRepeatType(TimeInfo Time)
		{
			if ((Time.MonthFilter != 0) || (Time.MonthFilter != 0) || (Time.MonthFilter != 0) || (Time.MonthFilter != 0))
			{
				return RepeatType.ComplexRepeat;
			}
			else if ((Time.Yearly != 0) || (Time.Monthly != 0) || (Time.Weekly != 0) || (Time.Daily != 0))
			{
				return RepeatType.SimpleRepeat;
			}
			else
			{
				return RepeatType.Singular;
			}
		}

		/// <summary>
		/// Types of special groups.
		/// </summary>
		public enum GroupTypes
		{
			/// <summary>Group for tasks that are overdue.</summary>
			Overdue = 1,
			/// <summary>Group for tasks that are due today.</summary>
			Today = 2,
			/// <summary>Group for tasks that don't fit into any forced/special categories.</summary>
			Standard = 3,
			/// <summary>Group for tasks that are completed.</summary>
			Completed = 4
		}

		/// <summary>
		/// Due time type.
		/// </summary>
		public enum DueTypes
		{
			/// <summary>Designation for tasks that are overdue.</summary>
			Overdue = 1,
			/// <summary>Designation for tasks that are due today.</summary>
			Today = 2,
			/// <summary>Designation for tasks that are due tomorrow.</summary>
			Tomorrow = 3,
			/// <summary>Designation for tasks that are due within a week.</summary>
			Weekday = 4,
			/// <summary>Designation for tasks that are due one week from now.</summary>
			OneWeek = 5,
			/// <summary>Designation for tasks that are due two weeks from now.</summary>
			TwoWeeks = 6,
			/// <summary>Designation for tasks that are due three weeks from now.</summary>
			ThreeWeeks = 7,
			/// <summary>Designation for tasks that are due one month from now.</summary>
			OneMonth = 8,
			/// <summary>Designation for tasks that are due later in the future.</summary>
			Future = 9,
			/// <summary>Designation for tasks that are completed.</summary>
			Completed = 10
		}

		/// <summary>
		/// Retrieve due time type from task information.
		/// </summary>
		/// <param name="Task">Task to check.</param>
		/// <returns>DueType for Task based upon when it is due.</returns>
		public static DueTypes GetDueTypeFromTask(Task Task)
		{
			DateTime Time = Task.Time.Next;
			if (Task.Checked)
			{
				return DueTypes.Completed;
			}

			if (Scheduled(Task.Time))
			{
				if (DateTime.Now > Time)
				{
					return DueTypes.Overdue;
				}
				else
				{
					DateTime Today = DateTime.Now - DateTime.Now.TimeOfDay;
					DateTime TargetDay = Time - Time.TimeOfDay;
					TimeSpan Span = TargetDay - Today;
					if (TargetDay == Today)
					{
						return DueTypes.Today;
					}
					else if (Span.Days <= 1)
					{
						return DueTypes.Tomorrow;
					}
					else if (Span.Days <= 7)
					{
						return DueTypes.Weekday;
					}
					else if (Span.Days <= 14)
					{
						return DueTypes.OneWeek;
					}
					else if (Span.Days <= 21)
					{
						return DueTypes.TwoWeeks;
					}
					else if (Span.Days <= 30)
					{
						return DueTypes.ThreeWeeks;
					}
					else if (TargetDay.Month == Today.AddMonths(1).Month)
					{
						return DueTypes.OneMonth;
					}
					else
					{
						return DueTypes.Future;
					}
				}
			}
			else
			{
				return DueTypes.Future;
			}
		}

		/// <summary>
		/// Retrieve group type from task information.
		/// </summary>
		/// <param name="Task">Task to check.</param>
		/// <param name="Settings">Program settings to determine which group types are enabled.</param>
		/// <returns>GroupType for task based upon supplied information.</returns>
		public static GroupTypes GetGroupTypeFromTask(Task Task, Settings Settings)
		{
			if (Task.Checked && Settings.AlwaysShowCompletedGroup)
			{
				return GroupTypes.Completed; // completed
			}
			else if (Scheduled(Task.Time))
			{
				if ((DateTime.Now > Task.Time.Next) && Settings.AlwaysShowOverdueGroup)
				{
					return GroupTypes.Overdue; // overdue
				}
				else if (Settings.AlwaysShowTodayGroup)
				{
					DateTime Today = DateTime.Now - DateTime.Now.TimeOfDay;
					DateTime TargetDay = Task.Time.Next - Task.Time.Next.TimeOfDay;
					if (TargetDay == Today)
					{
						return GroupTypes.Today; // today
					}
					else
					{
						return GroupTypes.Standard; 
					}
				}
				else
				{
					return GroupTypes.Standard;
				}
			}
			else
			{
				return GroupTypes.Standard;
			}
		}

		/// <summary>
		/// Check whether tasks from Source and Target have the same Group String.
		/// </summary>
		/// <param name="Source">Primary task object to compare.</param>
		/// <param name="Target">Secondary task object to compare.</param>
		/// <param name="Settings">Settings to pass to GetGroupStringFromTask().</param>
		/// <returns>True if Source and Target have the same Group String, false otherwise.</returns>
		public static bool CompareGroupFromTasks(Task Source, Task Target, Settings Settings)
		{
			if (GetGroupStringFromTask(Source, Settings) == GetGroupStringFromTask(Target, Settings))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Create group string based on task information and settings.
		/// </summary>
		/// <param name="Task">Task to acquire group information from.</param>
		/// <param name="Settings">Settings to use when checking for completed, overdue, and today tasks.</param>
		/// <returns>String formatted with a standardized group name for display in the listview.</returns>
		public static string GetGroupStringFromTask(Task Task, Settings Settings)
		{
			if (Task.Checked && Settings.AlwaysShowCompletedGroup)
			{
				return "4Completed";
			}
			else if (Scheduled(Task.Time))
			{
				if ((DateTime.Now > Task.Time.Next) && Settings.AlwaysShowOverdueGroup)
				{
					return "1Overdue";
				}
				else if (Settings.AlwaysShowTodayGroup)
				{
					DateTime Today = DateTime.Now - DateTime.Now.TimeOfDay;
					DateTime TargetDay = Task.Time.Next - Task.Time.Next.TimeOfDay;
					if (TargetDay == Today)
					{
						return "2Today";
					}
					else
					{
						return String.Format("3{0}", Task.Group);
					}
				}
				else
				{
					return String.Format("3{0}", Task.Group);
				}
			}
			else
			{
				return String.Format("3{0}", Task.Group);
			}
		}

		/// <summary>
		/// Check whether tasks from Source and Target have the same Due String.
		/// </summary>
		/// <param name="Source">Primary task object to compare.</param>
		/// <param name="Target">Secondary task object to compare.</param>
		/// <returns>True if Source and Target have the same Due String, false otherwise.</returns>
		public static bool CompareDueStringFromTasks(Task Source, Task Target)
		{
			if (GetDueStringFromTask(Source) == GetDueStringFromTask(Target))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Create due string based on task information.
		/// </summary>
		/// <param name="Task">Task to acquire group information from.</param>
		/// <returns>String formatted with a standardized due name for display in the listview.</returns>
		public static string GetDueStringFromTask(Task Task)
		{
			DateTime Time = Task.Time.Next;
			if (Task.Checked)
			{
				return "17Completed";
			}

			if (Scheduled(Task.Time))
			{
				if (DateTime.Now > Time)
				{
					return "01Overdue";
				}
				else
				{
					DateTime Today = DateTime.Now - DateTime.Now.TimeOfDay;
					DateTime TargetDay = Time - Time.TimeOfDay;
					TimeSpan Span = TargetDay - Today;
					if (TargetDay == Today)
					{
						return "02Today";
					}
					else if (Span.Days <= 1)
					{
						return "03Tomorrow";
					}
					else if (Span.Days <= 7)
					{
						return string.Format("{0}{1}", (4 + Span.Days).ToString("D2"), Time.DayOfWeek.ToString());
					}
					else if (Span.Days <= 14)
					{
						return "121 Week";
					}
					else if (Span.Days <= 21)
					{
						return "132 Weeks";
					}
					else if (Span.Days <= 30)
					{
						return "143 Weeks";
					}
					else if (TargetDay.Month == Today.AddMonths(1).Month)
					{
						return "151 Month";
					}
					else
					{
						return "16Future";
					}
				}
			}
			else
			{
				return "16Future";
			}
		}

		/// <summary>
		/// Special formatting for datetime strings.
		/// </summary>
		/// <param name="Time">DateTime to use as reference.</param>
		/// <param name="Format">Formatting of time string.</param>
		/// <returns>String with a time string formatted based upon the selected TimeFormat.</returns>
		public static string FormatTime(DateTime Time, TimeFormat Format)
		{
			switch (Format)
			{
				case TimeFormat.Elapsed:
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

				case TimeFormat.Short: // "6/6 - Tue 10pm"
					{
						string TimeStamp = "";
						if (Time.TimeOfDay != TimeSpan.FromSeconds(86399))
						{
							DateTime Offset = new DateTime();
							Offset = Offset + Time.TimeOfDay;

							string Minutes = "";
							if (Offset.TimeOfDay.Minutes != 0) { Minutes = ":" + Offset.Minute.ToString("00"); }

							if (Offset.Hour == 0)
							{
								if (Offset.Minute != 0)
								{
									TimeStamp = " - 12" + Minutes + "am";
								}
								else
								{
									TimeStamp = " - Midnight";
								}
							}
							else if (Offset.Hour == 12) // TODO: Fix this properly! 
							{
								if (Offset.Minute != 0)
								{
									TimeStamp = " - 12" + Minutes + "pm";
								}
								else
								{
									TimeStamp = " - Noon";
								}
							}
							else if (Offset.Hour > 12)
							{
								TimeStamp = " - " + (Offset.Hour - 12).ToString();
								TimeStamp = TimeStamp + Minutes + "pm";
							}
							else
							{
								TimeStamp = " - " + Offset.Hour.ToString();
								TimeStamp = TimeStamp + Minutes + "am";
							}
						}

						return String.Format(
							"{0}{1}",
							Time.ToString("M/d - ddd"),
							TimeStamp);
					}

				case TimeFormat.Medium: // "Sat, Jun 6th at 10:00pm"
					{
						string TimeStamp;
						DateTime Offset = new DateTime();
						Offset = Offset + Time.TimeOfDay;
						if (Offset.TimeOfDay == TimeSpan.FromSeconds(86399))
						{
							TimeStamp = "";
						}
						else
						{
							TimeStamp = "at ";
							TimeStamp = TimeStamp + Offset.Hour.ToString() + ":" + Offset.Minute.ToString();
							if (Offset.Hour <= 12) { TimeStamp = TimeStamp + "am"; }
							else { TimeStamp = TimeStamp + "pm"; }
						}

						return String.Format(
							"{0} {1} {2}",
							Time.ToString("ddd, MMM"),
							Ordinal(Time.Day),
							TimeStamp);
					}

				case TimeFormat.Long: // "Tuesday, June 6th at 10:00pm"
					{
						string TimeStamp;
						if (Time.TimeOfDay == TimeSpan.FromSeconds(86399))
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

		/// <summary>
		/// Pick a rounded up DateTime.
		/// </summary>
		/// <param name="BaseTime">Original time to base rounding up.</param>
		/// <returns>Rounded up DateTime by always rounding up to the nearest hour (within 45 minutes).</returns>
		public static DateTime PickRoundedUpTime(DateTime BaseTime)
		{
			BaseTime = BaseTime.AddMinutes(15);
			DateTime RoundedUp = new DateTime(BaseTime.Year, BaseTime.Month, BaseTime.Day, BaseTime.Hour, 0, 0);
			if ((BaseTime.Minute > 0) || (BaseTime.Second > 0))
			{
				RoundedUp = RoundedUp.AddHours(1);
			}
			return RoundedUp;
		}

		/// <summary>
		/// Simple version: Find the iteration count of a task based upon its TimeInfo.
		/// </summary>
		/// <param name="Rules">TimeInfo to base checking rules upon.</param>
		/// <returns>Integer for the number of iterations found.</returns>
		public static int FindIterationCount(TimeInfo Rules)
		{
			return FindIterationCount(Rules.Start, DateTime.Now, Rules);
		}

		/// <summary>
		/// Detailed version: Find the iteration count within a time span based upon its TimeInfo.
		/// </summary>
		/// <param name="From">Starting date from which to begin iteration search.</param>
		/// <param name="Until">Ending date to which iteration search finishes.</param>
		/// <param name="Rules">TimeInfo to base checking rules upon.</param>
		/// <returns>Integer for the number of iterations found.</returns>
		public static int FindIterationCount(DateTime From, DateTime Until, TimeInfo Rules)
		{
			int Count = 0;
			while (From < Until)
			{
				DateTime Next = FindNextIteration(From, Rules);
				if (Next != DateTime.MinValue)
				{
					++Count;
					From = Next;
				}
				else
				{
					break;
				}
			}
			return Count;
		}

		/// <summary>
		/// Simple version: Find the next iteration of a task based upon its TimeInfo.
		/// </summary>
		/// <param name="Rules">TimeInfo to base checking rules upon.</param>
		/// <returns>DateTime of next iteration.</returns>
		public static DateTime FindNextIteration(TimeInfo Rules)
		{
			return FindNextIteration(DateTime.Now, Rules);
		}

		/// <summary>
		/// Detailed version: Find the next iteration of a task starting from BaseTime based upon its TimeInfo.
		/// </summary>
		/// <param name="BaseTime">Starting date from which to begin iteration search.</param>
		/// <param name="Rules">TimeInfo to base checking rules upon.</param>
		/// <returns>DateTime of next iteration.</returns>
		public static DateTime FindNextIteration(DateTime BaseTime, TimeInfo Rules)
		{
			DateTime NextTime = new DateTime();
			NextTime = BaseTime;

			// set hours/minutes/seconds
			NextTime = NextTime - NextTime.TimeOfDay;
			NextTime = NextTime + Rules.TimeOfDay;

			// SIMPLE RECURRANCE
			// Increment from "start date"
			if ((Rules.Yearly + Rules.Monthly + Rules.Weekly + Rules.Daily) > 0)
			{
				NextTime = Rules.Start;

				for (int hops = 1; hops <= 3650; ++hops)
				{
					//Rules.Count = hops;
					if (NextTime > BaseTime)
					{
						return NextTime;
					}

					NextTime = NextTime.AddYears(Rules.Yearly);
					NextTime = NextTime.AddMonths(Rules.Monthly);
					NextTime = NextTime.AddDays(Rules.Weekly * 7);
					NextTime = NextTime.AddDays(Rules.Daily);
				}
			}

			// COMPLEX RECURRANCE: Filter month/week/day by flags
			// Increment NextTime until we find a day that matches all of our criteria
			else if ((Rules.MonthFilter != 0) || (Rules.WeekFilter != 0) || (Rules.DayFilter != 0))
			{
				for (int hops = 1; hops <= 3650; ++hops) // maximum search of 10 years
				{
					if ((Rules.MonthFilter & FromMonthToFlag(NextTime.Month)) != 0)
					{
						// if we're searching for the last week, automatically jump to the last 7 days of the month
						bool FindingLastWeek = false;
						if ((Rules.WeekFilter & WeekFlag.Last) != 0)
						{
							if (NextTime.Day >= (DateTime.DaysInMonth(NextTime.Year, NextTime.Month) - 7))
							{
								FindingLastWeek = true;
							}
						}

						if (((Rules.WeekFilter & FromWeekToFlag(NextTime.Day)) != 0) || FindingLastWeek)
						{
							if ((Rules.DayFilter & FromDayToFlag(NextTime.DayOfWeek)) != 0)
							{
								if (NextTime > BaseTime)
								{
									if (Rules.SpecificDay != 0)
									{
										if (NextTime.Day == Rules.SpecificDay)
										{
											return NextTime;
										}
									}
									else
									{
										return NextTime;
									}
								}
							}
						}
					}

					NextTime = NextTime.AddDays(1);
				}
			}

			// NON-RECURRANCE: Item has no rules that allow it to recur
			return DateTime.MinValue;
		}
	}
}
