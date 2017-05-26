using System;
using Newtonsoft.Json;

namespace Tasual
{
	public class Setting
	{
		[JsonProperty("protocol")]
		public Protocols Protocol { get; set; }


		[JsonProperty("textfile")]
		public string TextFile { get; set; }

		[JsonProperty("launchonstartup")]
		public bool LaunchOnStartup { get; set; }

		[JsonProperty("minimizetotray")]
		public bool MinimizeToTray { get; set; }

		[JsonProperty("alwaysontop")]
		public bool AlwaysOnTop { get; set; }


		[JsonProperty("promptclear")]
		public bool PromptClear { get; set; }

		[JsonProperty("promptdelete")]
		public bool PromptDelete { get; set; }


		[JsonProperty("grouptasks")]
		public bool GroupTasks { get; set; }

		[JsonProperty("groupstyle")]
		public GroupStyles GroupStyle { get; set; }

		[JsonProperty("alwaysshowcompletedgroup")]
		public bool AlwaysShowCompletedGroup { get; set; }

		[JsonProperty("alwaysshowoverduegroup")]
		public bool AlwaysShowOverdueGroup { get; set; }

		[JsonProperty("alwaysshowtodaygroup")]
		public bool AlwaysShowTodayGroup { get; set; }

		[Flags]
		public enum Columns
		{
			Description = 1,
			Notes = 2,
			Category = 4,
			Due = 8,
			Time = 16
		}

		public enum Protocols
		{
			Tasual,
			JSON,
			XML,
			RTM,
			Text
		}

		public enum GroupStyles
		{
			Category,
			DueTime
		}
	}
}
