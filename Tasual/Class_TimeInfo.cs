﻿using System;

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

		// for all tasks
		public DateTime Created; // date of creation
		public DateTime Start; // date of first occurence

		// for recurring tasks
		public DateTime Next; // date of next occurence
		public DateTime End; // date when task stops recurring
		public int Iterations; // number of total occurences allowed 
		public int Count; // instance count of this task (starts at 1)
		public int Dismiss; // time in seconds after due date ("Next") to remove task (-1 for instant)

		// simple recurring tasks
		public int Yearly;
		public int Monthly;
		public int Weekly;
		public int Daily;

		// complex recurring tasks
		public TimeSpan TimeOfDay;
		public MonthFlag MonthFilter;
		public WeekFlag WeekFilter;
		public DayFlag DayFilter;
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