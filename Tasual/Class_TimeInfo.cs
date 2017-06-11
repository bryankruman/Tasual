// ===========================================
//  Copyright (c) 2017 Bryan Kruman
//
//  See LICENSE.rtf file in the project root
//  for full license information.
// ===========================================

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
			FirstThruLast = 39, // (Skips 45th and 5th options as they can't happen via dialog)
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
			Short,
			Medium,
			Long
		}

		public enum RepeatType
		{
			Singular,
			SimpleRepeat,
			ComplexRepeat
		}

		public enum DismissType
		{
			Never,
			Immediate,
			OneHour,
			TwelveHours,
			OneDay,
			OneWeek,
			OneMonth
		}

		// for all tasks
		[JsonProperty("summary")]
		public string Summary; // description of this tasks schedule and how it repeats

		[JsonProperty("checkedtime")]
		public DateTime CheckedTime; // date of task being "checked"/completed

		[JsonProperty("created")]
		public DateTime Created; // date of creation

		[JsonProperty("modified")]
		public DateTime Modified; // date of last modification

		[JsonProperty("start")]
		public DateTime Start; // date of first occurence

		[JsonProperty("next")]
		public DateTime Next; // date of next occurence

		[JsonProperty("dismiss")]
		public DismissType Dismiss; // time in seconds after due date ("Next") to remove task (-1 for instant)

		[JsonProperty ("expired")]
		public bool Expired; // boolean for if this task has expired already 


		// for all recurring tasks
		[JsonProperty("end")]
		public DateTime End; // date when task stops recurring

		[JsonProperty("iterations")]
		public int Iterations; // number of total occurences allowed 

		[JsonProperty("count")]
		public int Count; // instance count of this task (starts at 1)


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

		// singular constructor
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

		// simple repeating constructor
		public TimeInfo(
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
			TimeSpan _TimeOfDay)
		{
			Summary = null;
			CheckedTime = _CheckedTime;
			Created = _Created;
			Modified = _Modified;
			Start = _Start;
			Next = _Next;
			Dismiss = _Dismiss;
			Expired = _Expired;

			End = _End;
			Iterations = _Iterations;
			Count = _Count;

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
			TimeSpan _TimeOfDay,
			MonthFlag _MonthFilter,
			WeekFlag _WeekFilter,
			DayFlag _DayFilter,
			int _SpecificDay)
		{
			Summary = null;
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

		public static bool Scheduled(TimeInfo Time)
		{
			if (Time.Next != DateTime.MinValue)
			{
				return true;
			}
			else
			{
				return false;
			}
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

		public enum GroupTypes
		{
			Overdue = 1,
			Today = 2,
			Standard = 3,
			Completed = 4
		}

		public enum DueTypes
		{
			Overdue = 1,
			Today = 2,
			Tomorrow = 3,
			Weekday = 4,
			OneWeek = 5, 
			TwoWeeks = 6,
			ThreeWeeks = 7, 
			OneMonth = 8,
			Future = 9, 
			Completed = 10
		}

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

		public static GroupTypes GetGroupTypeFromTask(Task Task, Setting Settings)
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

		public static bool CompareGroupFromTasks(Task Source, Task Target, Setting Settings)
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

		public static string GetGroupStringFromTask(Task Task, Setting Settings)
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

		public static int FindIterationCount(TimeInfo Rules)
		{
			return FindIterationCount(Rules.Start, DateTime.Now, Rules);
		}

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

		public static DateTime FindNextIteration(TimeInfo Rules)
		{
			return FindNextIteration(DateTime.Now, Rules);
		}

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
