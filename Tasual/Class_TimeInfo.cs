using System;
using Newtonsoft.Json;

namespace Tasual
{
	public class TimeInfo
	{
		// flags
		[Flags]
		public enum MonthFlag
		{
			January = 1,
			February = 2,
			March = 4,
			April = 8,
			May = 16,
			June = 32,
			July = 64,
			August = 128,
			September = 256,
			October = 512,
			November = 1024,
			December = 2048,
			Everymonth = 4095
		}

		[Flags]
		public enum WeekFlag
		{
			First = 1,
			Second = 2,
			Third = 4,
			Fourth = 8,
			Fifth = 16,
			Last = 32,
			Everyweek = 31
		}

		[Flags]
		public enum DayFlag
		{
			Monday = 1,
			Tuesday = 2,
			Wednesday = 4,
			Thursday = 8,
			Friday = 16,
			Saturday = 32,
			Sunday = 64,
			Everyday = 127
		}

		public enum TimeFormat
		{
			Elapsed,
			Due,
			Short,
			Medium,
			Long
		}

		// for all tasks
		[JsonProperty("created")]
		public DateTime Created; // date of creation

		[JsonProperty("start")]
		public DateTime Start; // date of first occurence


		// for recurring tasks
		[JsonProperty("next")]
		public DateTime Next; // date of next occurence

		[JsonProperty("end")]
		public DateTime End; // date when task stops recurring

		[JsonProperty("iterations")]
		public int Iterations; // number of total occurences allowed 

		[JsonProperty("count")]
		public int Count; // instance count of this task (starts at 1)

		[JsonProperty("dismiss")]
		public int Dismiss; // time in seconds after due date ("Next") to remove task (-1 for instant)


		// simple recurring tasks
		[JsonProperty("yearly")]
		public int Yearly;

		[JsonProperty("monthly")]
		public int Monthly;

		[JsonProperty("weekly")]
		public int Weekly;

		[JsonProperty("daily")]
		public int Daily;


		// complex recurring tasks
		[JsonProperty("timeofday")]
		public TimeSpan TimeOfDay;

		[JsonProperty("monthfilter")]
		public MonthFlag MonthFilter;

		[JsonProperty("weekfilter")]
		public WeekFlag WeekFilter;

		[JsonProperty("dayfilter")]
		public DayFlag DayFilter;

		[JsonProperty("specificday")]
		public int SpecificDay;

		// blank constructor
		public TimeInfo()
		{
			Created = DateTime.MinValue;
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
			TimeOfDay = TimeSpan.Zero;
			MonthFilter = 0;
			WeekFilter = 0;
			DayFilter = 0;
			SpecificDay = 0;
		}

		// singular constructor
		public TimeInfo(
			DateTime _Created,
			DateTime _Start,
			DateTime _Next,
			DateTime _End)
		{
			Created = _Created;
			Start = _Start;
			Next = _Next;
			End = _End;

			Iterations = 0;
			Count = 0;
			Dismiss = 0;

			Yearly = 0;
			Monthly = 0;
			Weekly = 0;
			Daily = 0;

			TimeOfDay = TimeSpan.Zero;
			MonthFilter = 0;
			WeekFilter = 0;
			DayFilter = 0;
			SpecificDay = 0;
		}

		// simple repeating constructor
		public TimeInfo(
			DateTime _Created,
			DateTime _Start,
			DateTime _Next,
			DateTime _End,
			int _Iterations,
			int _Count,
			int _Dismiss,
			int _Yearly,
			int _Monthly,
			int _Weekly,
			int _Daily,
			TimeSpan _TimeOfDay)
		{
			Created = _Created;
			Start = _Start;
			Next = _Next;
			End = _End;

			Iterations = _Iterations;
			Count = _Count;
			Dismiss = _Dismiss;

			Yearly = _Yearly;
			Monthly = _Monthly;
			Weekly = _Weekly;
			Daily = _Daily;

			TimeOfDay = _TimeOfDay;
			MonthFilter = 0;
			WeekFilter = 0;
			DayFilter = 0;
			SpecificDay = 0;
		}

		// complex repeating constructor
		public TimeInfo(
			DateTime _Created,
			DateTime _Start,
			DateTime _Next,
			DateTime _End,
			int _Iterations,
			int _Count,
			int _Expire,
			TimeSpan _TimeOfDay,
			MonthFlag _MonthFilter,
			WeekFlag _WeekFilter,
			DayFlag _DayFilter,
			int _SpecificDay)
		{
			Created = _Created;
			Start = _Start;
			Next = _Next;
			End = _End;

			Iterations = _Iterations;
			Count = _Count;
			Dismiss = _Expire;

			Yearly = 0;
			Monthly = 0;
			Weekly = 0;
			Daily = 0;

			TimeOfDay = _TimeOfDay;
			MonthFilter = _MonthFilter;
			WeekFilter = _WeekFilter;
			DayFilter = _DayFilter;
			SpecificDay = _SpecificDay;
		}

		// full constructor
		public TimeInfo(
			DateTime _Created,
			DateTime _Start,
			DateTime _Next,
			DateTime _End,
			int _Iterations,
			int _Count,
			int _Dismiss,
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
			Created = _Created;
			Start = _Start;
			Next = _Next;
			End = _End;

			Iterations = _Iterations;
			Count = _Count;
			Dismiss = _Dismiss;

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

		public static string GetGroupStringFromTask(Task Task, Setting Settings)
		{
			if (Task.Checked && Settings.AlwaysShowCompletedGroup)
			{
				return "Completed";
			}
			else if ((DateTime.Now > Task.Time.Start) && Settings.AlwaysShowOverdueGroup)
			{
				return "Overdue";
			}
			else if (Settings.AlwaysShowTodayGroup)
			{
				DateTime Today = DateTime.Now - DateTime.Now.TimeOfDay;
				DateTime TargetDay = Task.Time.Start - Task.Time.Start.TimeOfDay;
				if (TargetDay == Today)
				{
					return "Today";
				}
				else
				{
					return Task.Group;
				}
			}
			else
			{
				return Task.Group;
			}
		}

		public static string GetDueStringFromTimeInfo(TimeInfo TimeInfo)
		{
			DateTime Time = TimeInfo.Start.ToLocalTime();
			if (DateTime.Now > Time)
			{
				return "Overdue";
			}
			else
			{
				DateTime Today = DateTime.Now - DateTime.Now.TimeOfDay;
				DateTime TargetDay = Time - Time.TimeOfDay;
				TimeSpan Span = TargetDay - Today;
				if (TargetDay == Today)
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

		public static string GetDueStringFromInt(int Key)
		{
			switch (Key)
			{
				case 1: return "Overdue";
				case 2: return "Today";
				case 3: return "Tomorrow";
				case 4: return "Sunday";
				case 5: return "Monday";
				case 6: return "Tuesday";
				case 7: return "Wednesday";
				case 8: return "Thursday";
				case 9: return "Friday";
				case 10: return "Saturday";
				case 11: return "Next Week";
				case 12: return "2 Weeks";
				case 13: return "3 Weeks";
				case 14: return "Next Month";
				case 15: return "Future";
				case 16: return "Completed";
				default: return "Broken!";
			}
		}

		public static int GetDueIntFromTask(Task Task)
		{
			DateTime Time = Task.Time.Start.ToLocalTime();
			//TimeInfo Time = (TimeInfo)Input;
			if (Task.Checked)
			{
				return 16; // "Completed";
			}
			else if (DateTime.Now > Time)
			{
				return 1; // "Overdue";
			}
			else
			{
				DateTime Today = DateTime.Now - DateTime.Now.TimeOfDay;
				DateTime TargetDay = Time - Time.TimeOfDay;
				TimeSpan Span = TargetDay - Today;
				if (TargetDay == Today)
				{
					return 2; // "Today";
				}
				else if (Span.Days <= 1)
				{
					return 3; //  "Tomorrow";
				}
				else if (Span.Days <= 7)
				{
					return 4 + (int)Time.DayOfWeek; //Time.DayOfWeek.ToString();
				}
				else if (Span.Days <= 14)
				{
					return 11; //"Next Week";
				}
				else if (Span.Days <= 21)
				{
					return 12; // "2 Weeks";
				}
				else if (Span.Days <= 30)
				{
					return 13; // "3 Weeks";
				}
				else if (TargetDay.Month == Today.AddMonths(1).Month)
				{
					return 14; // "Next Month";
				}
				else
				{
					return 15; // "Future";
				}
			}
		}

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

				case TimeFormat.Due:
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
							if (TargetDay == Today)
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

				case TimeFormat.Short: // "6/6 - Tue 10pm"
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

				case TimeFormat.Medium: // "Sat, Jun 6th at 10:00pm"
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

				case TimeFormat.Long: // "Tuesday, June 6th at 10:00pm"
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

		public static DateTime FindNextIteration(DateTime BaseTime, ref TimeInfo Rules)
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
					Rules.Count = hops;
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

			// todo: find some good way to count occurences of complex recurrances 

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
							FindingLastWeek = true;
							NextTime = NextTime.AddDays(DateTime.DaysInMonth(NextTime.Year, NextTime.Month) - 7);
							/*Console.WriteLine(
							 * "Last week {0} {1}", 
							 * DateTime.DaysInMonth(NextTime.Year, NextTime.Month), 
							 * DateTime.DaysInMonth(NextTime.Year, NextTime.Month) - 7
							);*/
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
