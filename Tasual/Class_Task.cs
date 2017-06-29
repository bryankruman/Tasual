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
	/// <summary>
	/// Definitions and supporting functions for tasks.
	/// </summary>
	public class Task
	{
		/// <summary>Unique identifier for the task.</summary>
		[JsonProperty("id")]
		public string ID { get; set; }

		/// <summary>Whether or not the task is hidden/removed from the list.</summary>
		[JsonProperty("removed")]
		public bool Removed { get; set; }

		/// <summary>Checked status for the task.</summary>
		[JsonProperty("checked")]
		public bool Checked { get; set; }

		/// <summary>Priority level for the task.</summary>
		[JsonProperty("priority")]
		public int Priority { get; set; }

		/// <summary>Group name for the task.</summary>
		[JsonProperty("group")]
		public string Group { get; set; }

		/// <summary>Description of the task.</summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>Notes field for the task.</summary>
		[JsonProperty("notes")]
		public string Notes { get; set; }

		/// <summary>Link field for the task.</summary>
		[JsonProperty("link")]
		public string Link { get; set; }

		/// <summary>Location field for the task.</summary>
		[JsonProperty("location")]
		public string Location { get; set; }

		/// <summary>Collection of time information for the task.</summary>
		[JsonProperty("time")]
		public TimeInfo Time { get; set; }

		/// <summary>Internal category group key for the task used by ObjectListView.</summary>
		[JsonIgnore]
		public string CategoryGroupKey { get; set; } = "";

		/// <summary>Internal due group key for the task used by ObjectListView.</summary>
		[JsonIgnore]
		public string DueGroupKey { get; set; } = "";

		/// <summary>Priority level for the task.</summary>
		public enum Priorities
		{
			/// <summary>Low priority setting.</summary>
			Low,
			/// <summary>Normal priority setting.</summary>
			Normal,
			/// <summary>High priority setting.</summary>
			High
		}

		/// <summary>
		/// Blank constructor for the task class.
		/// </summary>
		public Task()
		{
			ID = GenerateID();
			Removed = false;
			Checked = false;
			Priority = (int)Priorities.Normal;
			Time = new TimeInfo();
		}

		/// <summary>
		/// Specific constructor for the task class.
		/// </summary>
		/// <param name="ID">Unique identifier for the task.</param>
		/// <param name="Removed">Whether or not the task is hidden/removed from the list.</param>
		/// <param name="Checked">Checked status for the task.</param>
		/// <param name="Priority">Priority level for the task.</param>
		/// <param name="Group">Group name for the task.</param>
		/// <param name="Description">Description of the task.</param>
		/// <param name="Notes">Notes field for the task.</param>
		/// <param name="Link">Link field for the task.</param>
		/// <param name="Location">Location field for the task.</param>
		/// <param name="Time">Collection of time information for the task.</param>
		public Task(
			string ID,
			bool Removed,
			bool Checked,
			int Priority,
			string Group,
			string Description,
			string Notes,
			string Link,
			string Location,
			TimeInfo Time)
		{
			this.ID = ID;
			this.Removed = Removed;
			this.Checked = Checked;
			this.Priority = Priority;
			this.Group = Group;
			this.Description = Description;
			this.Notes = Notes;
			this.Link = Link;
			this.Location = Location;
			this.Time = Time;
		}

		/// <summary>
		/// Generate unique identifier for task.
		/// </summary>
		/// <returns>Unique identifier using Guid.NewGuid().</returns>
		public static string GenerateID()
		{
			return Guid.NewGuid().ToString("N");
		}

		/// <summary>
		/// Basic/simplified console debug print.
		/// </summary>
		/// <param name="Task">Task to print out.</param>
		public static void PrintToConsoleBasic(Task Task)
		{
			Console.WriteLine(
				"TaskItem: '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', ('{6}', '{7}', '{8}')",
				Task.ID,
				Task.Removed,
				Task.Checked,
				Task.Priority,
				Task.Group,
				Task.Description,
				Task.Time.Start,
				Task.Time.End,
				Task.Time.Next
			);
		}

		/// <summary>
		/// Verbose/detailed console debug print.
		/// </summary>
		/// <param name="Task">Task to print out.</param>
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
