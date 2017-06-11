// ===========================================
//  Copyright (c) 2017 Bryan Kruman
//
//  See LICENSE.rtf file in the project root
//  for full license information.
// ===========================================

using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Tasual
{
	public class Task
	{
		[JsonProperty("checked")]
		public bool Checked { get; set; }

		[JsonProperty("priority")]
		public int Priority { get; set; }

		[JsonProperty("group")]
		public string Group { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("notes")]
		public string Notes { get; set; }

		[JsonProperty("link")]
		public string Link { get; set; }

		[JsonProperty("location")]
		public string Location { get; set; }

		[JsonProperty("time")]
		public TimeInfo Time { get; set; }


		// Used only for display/grouping in ObjectListView
		[JsonIgnore]
		public string CategoryGroupKey { get; set; } = "";

		[JsonIgnore]
		public string DueGroupKey { get; set; } = "";


		public enum Arguments
		{
			Checked,
			Priority,
			Group,
			Description,
			Created,
			Ending,
			Next,
			Count
		}

		public enum Types
		{
			TYPE_USER_SINGLE,
			TYPE_USER_RECURRING,
			TYPE_USER_DEBT_OWED,
			TYPE_USER_DEBT_LENT,
			TYPE_SYNDICATION_SINGLE,
			TYPE_SYNDICATION_RECURRING
		}

		public enum Priorities
		{
			Low,
			Normal,
			High
		}

		public enum Statuses
		{
			New,
			Complete,
			Toggle
		}

		// Blank Constructor
		public Task()
		{
			Checked = false;
			Priority = (int)Priorities.Normal;
			Time = new TimeInfo();
		}

		// Specific Constructor
		public Task(
			bool Checked,
			int Priority,
			string Group,
			string Description,
			string Notes,
			string Link,
			string Location,
			TimeInfo Time)
		{

			this.Checked = Checked;
			this.Priority = Priority;
			this.Group = Group;
			this.Description = Description;
			this.Notes = Notes;
			this.Link = Link;
			this.Location = Location;
			this.Time = Time;
		}

		// Supporting Functions
		public static void PrintToConsoleBasic(Task Task)
		{
			Console.WriteLine(
				"TaskItem: '{0}', '{1}', '{2}', '{3}', ('{4}', '{5}', '{6}')",
				Task.Checked,
				Task.Priority,
				Task.Group,
				Task.Description,
				Task.Time.Start,
				Task.Time.End,
				Task.Time.Next
			);
		}

		public static void PrintToConsoleDetailed(Task Task)
		{
			foreach (PropertyDescriptor Descriptor in TypeDescriptor.GetProperties(Task))
			{
				string Name = Descriptor.Name;
				object Value = Descriptor.GetValue(Task);
				Console.WriteLine("[{0}] = '{1}'", Name, Value);
			}
		}
	}
}
