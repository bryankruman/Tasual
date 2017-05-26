using Newtonsoft.Json;
using System.Windows.Forms;

namespace Tasual
{
	public class Task
	{
		[JsonProperty("checked")]
		public bool Checked { get; set; }

		[JsonProperty("priority")]
		public int Priority { get; set; }

		[JsonProperty("status")]
		public int Status { get; set; }

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

		[JsonProperty("timer")]
		public Timer Timer { get; set; }


/*		public void PrintToConsole(Task Task)
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
		}*/

		public enum Arguments
		{
			Checked,
			Priority,
			Status,
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

		// blank constructor
		public Task()
		{
			Checked = false;
			Priority = (int)Priorities.Normal;
			Time = new TimeInfo();
		}

		// specific constructor
		public Task(
			bool Checked,
			int Priority,
			int Status,
			string Group,
			string Description,
			TimeInfo Time,
			Timer Timer)
		{

			this.Checked = Checked;
			this.Priority = Priority;
			this.Status = Status;
			this.Group = Group;
			this.Description = Description;
			this.Time = Time;
			this.Timer = Timer;
		}
	}
}
