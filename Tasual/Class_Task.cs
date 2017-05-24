using System;
using System.Windows.Forms;

namespace Tasual
{
	public class Task
	{
		public bool Checked { get; set; }
		public int Priority { get; set; }
		public int Status { get; set; }
		public string Group { get; set; }
		public string Description { get; set; }
		public string Notes { get; set; }
		public string Link { get; set; }
		public string Location { get; set; }
		public TimeInfo Time { get; set; }
		public Timer Timer { get; set; }

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
