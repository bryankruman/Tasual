// ===========================================
//  Copyright (c) 2017 Bryan Kruman
//
//  See LICENSE.rtf file in the project root
//  for full license information.
// ===========================================

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


		[JsonProperty("storagefolder")]
		public string StorageFolder { get; set; } // this will get set by Load()

		[JsonIgnore]
		private string SettingsPath { get; set; } // this will get set by Load()

		[JsonProperty("launchonstartup")]
		public bool LaunchOnStartup { get; set; } = false;

		[JsonProperty("minimizetotray")]
		public bool MinimizeToTray { get; set; } = false;

		[JsonProperty("alwaysontop")]
		public bool AlwaysOnTop { get; set; } = false;

		[JsonProperty("savewindowpos")]
		public bool SaveWindowPos { get; set; } = false;

		[JsonProperty("windowstate")]
		public FormWindowState WindowState { get; set; } // don't set defaults on these

		[JsonProperty("location")]
		public Point Location { get; set; } // don't set defaults on these

		[JsonProperty("size")]
		public Size Size { get; set; } // don't set defaults on these


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
				using (FileStream OutputFile = File.Open(Path.Combine(Settings.SettingsPath, "settings.cfg"), FileMode.Create))
				using (StreamWriter OutputStream = new StreamWriter(OutputFile))
				using (JsonWriter OutputJson = new JsonTextWriter(OutputStream))
				{
					OutputJson.Formatting = Formatting.Indented;
					JsonSerializer Serializer = new JsonSerializer();
					Serializer.Serialize(OutputJson, Settings);
				}

			}
			catch (Exception Args)
			{
				Console.WriteLine("Could not write to settings.cfg! Message: {0}", Args.Message);
			}
		}

		public static void Load(ref Setting Settings)
		{
			try
			{
				// Check to see three things:
				// - If this is the first time we're loading the settings this instance (i.e. application load)
				// - If the settings file that was selected still exists
				// - If we're able to write to the settings file
				// If either of these two conditions fail, then lets search our default directories to see if any
				// of those contain settings files. Finally, if not, then lets just create a new settings file
				// based upon the default and save the file appropriately.
				string BasePath = AppDomain.CurrentDomain.BaseDirectory;
				string AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tasual");

				string SettingsPath;

				if (string.IsNullOrEmpty(Settings.SettingsPath) || !File.Exists(Path.Combine(Settings.SettingsPath, "settings.cfg")))
				{
					// 1st Priority: Base directory
					if (File.Exists(Path.Combine(BasePath, "settings.cfg")))
					{
						Settings.SettingsPath = BasePath;
					}

					// 2nd Priority: Application Data directory
					else if (File.Exists(Path.Combine(AppDataPath, "settings.cfg")))
					{
						Settings.SettingsPath = AppDataPath;
					}

					// Both directories don't have settings already
					// Lets create a new settings file in the Application Data directory
					else
					{ 
						if (File.Exists(Path.Combine(BasePath, "tasks.db")))
						{
							// If a tasks database already exists in the Base directory, choose that
							Settings.StorageFolder = BasePath;
						}
						else
						{
							// Otherwise just pick the Application Data directory
							Settings.StorageFolder = AppDataPath;
						}

						Settings.SettingsPath = AppDataPath;

						Save(ref Settings);
						return;
					}
				}

				// Load in the settings from the selected settings.cfg file
				using (StreamReader InputFile = File.OpenText(Path.Combine(Settings.SettingsPath, "settings.cfg")))
				using (JsonReader InputJson = new JsonTextReader(InputFile))
				{
					SettingsPath = Settings.SettingsPath;

					JsonSerializer Serializer = new JsonSerializer();
					Settings = (Setting)Serializer.Deserialize(InputJson, typeof(Setting));

					Settings.SettingsPath = SettingsPath;
				}

				// Check if there is a StorageFolder specified and whether it will work for us
				if (!string.IsNullOrWhiteSpace(Settings.StorageFolder) && Directory.Exists(Settings.StorageFolder))
				{
					// Directory is fine, do nothing
				}
				else if (File.Exists(Path.Combine(BasePath, "tasks.db")))
				{
					// If a tasks database already exists in the Base directory, choose that
					Settings.StorageFolder = BasePath;
					Save(ref Settings);
				}
				else if (File.Exists(Path.Combine(AppDataPath, "tasks.db")))
				{
					// If a tasks database already exists in the Application Data directory, choose that
					Settings.StorageFolder = AppDataPath;
					Save(ref Settings);
				}
				else
				{
					// If no database was found already, just save to the same location as the settings file
					Settings.StorageFolder = Settings.SettingsPath;
					Save(ref Settings);
				}
			}
			catch (Exception Args)
			{
				Console.WriteLine("Could not load settings.cfg! Message: {0}", Args.Message);
			}
		}
	}
}
