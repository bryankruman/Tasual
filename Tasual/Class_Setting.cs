using System;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Drawing;

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

		[JsonProperty("savewindowpos")]
		public bool SaveWindowPos { get; set; } = false;

		[JsonProperty("windowstate")]
		public FormWindowState WindowState { get; set; }

		[JsonProperty("location")]
		public Point Location { get; set; }

		[JsonProperty("size")]
		public Size Size { get; set; }


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


		[JsonProperty("removecompleted")]
		public RemoveType RemoveCompleted { get; set; } = RemoveType.Never; // TODO: Add option to settings dialog for this

		[JsonProperty("subitemheaderalign")]
		public HorizontalAlignment SubItemHeaderAlign { get; set; } = HorizontalAlignment.Center;

		[JsonProperty("subitemtextalign")]
		public HorizontalAlignment SubItemTextAlign { get; set; } = HorizontalAlignment.Left;


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

		public enum RemoveType
		{
			Never,
			Immediate,
			OneHour,
			TwelveHours,
			OneDay,
			TwoDays,
			OneWeek,
			TwoWeeks,
			OneMonth,
			ThreeMonths,
			SixMonths,
			OneYear
		}

		public static TimeSpan GetRemoveTimeSpan(RemoveType Type)
		{
			switch (Type)
			{
				case RemoveType.Never:        { return TimeSpan.Zero; }
				case RemoveType.Immediate:    { return TimeSpan.Zero; }
				case RemoveType.OneHour:      { return new TimeSpan(0, 1, 0, 0); }
				case RemoveType.TwelveHours:  { return new TimeSpan(0, 12, 0, 0); }
				case RemoveType.OneDay:       { return new TimeSpan(1, 0, 0, 0); }
				case RemoveType.TwoDays:      { return new TimeSpan(2, 0, 0, 0); }
				case RemoveType.OneWeek:      { return new TimeSpan(7, 0, 0, 0); }
				case RemoveType.TwoWeeks:     { return new TimeSpan(14, 0, 0, 0); }
				case RemoveType.OneMonth:     { return new TimeSpan(30, 0, 0, 0); }
				case RemoveType.ThreeMonths:  { return new TimeSpan(90, 0, 0, 0); }
				case RemoveType.SixMonths:    { return new TimeSpan(180, 0, 0, 0); }
				case RemoveType.OneYear:      { return new TimeSpan(365, 0, 0, 0); }
				default:                      { return TimeSpan.Zero; }
			}
		}

		public static void Save(ref Setting Settings)
		{
			try
			{
				using (FileStream OutputFile = File.Open("settings.cfg", FileMode.Create))
				using (StreamWriter OutputStream = new StreamWriter(OutputFile))
				using (JsonWriter OutputJson = new JsonTextWriter(OutputStream))
				{
					OutputJson.Formatting = Formatting.Indented;
					JsonSerializer Serializer = new JsonSerializer();
					Serializer.Serialize(OutputJson, Settings);
				}

			}
			catch (Exception e)
			{
				Console.WriteLine("Could not write to settings.cfg! Message: {0}", e.Message);
			}
		}

		public static void Load(ref Setting Settings)
		{
			try
			{
				using (StreamReader InputFile = File.OpenText("settings.cfg"))
				using (JsonReader InputJson = new JsonTextReader(InputFile))
				{
					JsonSerializer Serializer = new JsonSerializer();
					Settings = (Setting)Serializer.Deserialize(InputJson, typeof(Setting));
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(
					"Could not load settings.cfg! " +
					"Proceeding with defaults and writing blank new config! " +
					"Message: {0}",
					e.Message
				);
				Save(ref Settings);
			}
		}
	}
}
