using System;
using Newtonsoft.Json;

namespace Tasual
{
	public class Setting
	{
		[JsonProperty("protocol")]
		public Protocols Protocol { get; set; } = Protocols.JSON;


		[JsonProperty("textfile")]
		public string TextFile { get; set; } = "tasks.db";

		[JsonProperty("launchonstartup")]
		public bool LaunchOnStartup { get; set; } = false;

		[JsonProperty("minimizetotray")]
		public bool MinimizeToTray { get; set; } = false;

		[JsonProperty("alwaysontop")]
		public bool AlwaysOnTop { get; set; } = false;


		[JsonProperty("promptclear")]
		public bool PromptClear { get; set; } = true;

		[JsonProperty("promptdelete")]
		public bool PromptDelete { get; set; } = true;

		[JsonProperty("entertosave")]
		public bool EnterToSave { get; set; } = true;


		[JsonProperty("columns")]
		public Columns EnabledColumns { get; set; } = Columns.Normal;

		[JsonProperty("grouptasks")]
		public bool GroupTasks { get; set; } = true;

		[JsonProperty("groupstyle")]
		public GroupStyles GroupStyle { get; set; } = GroupStyles.Category;

		[JsonProperty("alwaysshowcompletedgroup")]
		public bool AlwaysShowCompletedGroup { get; set; } = true;

		[JsonProperty("alwaysshowoverduegroup")]
		public bool AlwaysShowOverdueGroup { get; set; } = true;

		[JsonProperty("alwaysshowtodaygroup")]
		public bool AlwaysShowTodayGroup { get; set; } = true;

		[JsonProperty("showitemcounts")]
		public bool ShowItemCounts { get; set; } = true;


		[Flags]
		public enum Columns
		{
			Description = 1,
			Notes = 2,
			Category = 4,
			Due = 8,
			Time = 16,
			Normal = 19,
			All = 31
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
