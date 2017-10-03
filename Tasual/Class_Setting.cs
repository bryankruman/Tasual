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
	/// <summary>
	/// Object and supporting functions for handling Tasual settings.
	/// </summary>
	public class Settings
	{
		/// <summary>Settings object containing current application settings.</summary>
		public static Settings Config = new Settings();

		/// <summary>Unique identifier hash for this instance of the Tasual application.</summary>
		[JsonProperty("hash")]
		public string Hash { get; set; }

		/// <summary>Storage/data protocol to use for data retrieval.</summary>
		[JsonProperty("protocol")]
		public Protocols Protocol { get; set; } = Protocols.JSON;

		/// <summary>Path to folder determining the location of data storage.</summary>
		/// <remarks>This field is checked and set when necessary by Load() automatically.</remarks>
		[JsonProperty("storagefolder")]
		public string StorageFolder { get; set; }

		/// <summary>Path to folder determining the location of the settings.cfg file.</summary>
		/// <remarks>This field is checked and set when necessary by Load() automatically.</remarks>
		[JsonIgnore]
		private string SettingsPath { get; set; }

		/// <summary>Setting for whether Tasual is launched on system startup.</summary>
		[JsonProperty("launchonstartup")]
		public bool LaunchOnStartup { get; set; } = false;

		/// <summary>Automatically check for updates on application launch.</summary>
		[JsonProperty("autoupdate")]
		public bool AutoUpdate { get; set; } = true;

		/// <summary>Setting for whether Tasual gets minimized to tray instead of taskbar.</summary>
		[JsonProperty("minimizetotray")]
		public bool MinimizeToTray { get; set; } = false;

		/// <summary>Setting for whether Tasual is forced to always be on top.</summary>
		[JsonProperty("alwaysontop")]
		public bool AlwaysOnTop { get; set; } = false;

		/// <summary>Setting for whether to save the window position of Tasual and resume on startup.</summary>
		[JsonProperty("savewindowpos")]
		public bool SaveWindowPos { get; set; } = false;

		/// <summary>Previously saved WindowState.</summary>
		/// <remarks>Don't set a default value for this field, this way the field is only set
		/// whenever the SaveWindowPos setting is applied.</remarks>
		[JsonProperty("windowstate")]
		public FormWindowState WindowState { get; set; }

		/// <summary>Previously saved window Location.</summary>
		/// <remarks>Don't set a default value for this field, this way the field is only set
		/// whenever the SaveWindowPos setting is applied.</remarks>
		[JsonProperty("location")]
		public Point Location { get; set; }

		/// <summary>Previously saved window Size.</summary>
		/// <remarks>Don't set a default value for this field, this way the field is only set
		/// whenever the SaveWindowPos setting is applied.</remarks>
		[JsonProperty("size")]
		public Size Size { get; set; }

		/// <summary>Prompt user with messagebox whenever clearing the task list.</summary>
		[JsonProperty("promptclear")]
		public bool PromptClear { get; set; } = true;

		/// <summary>Prompt user with messagebox whenever deleting a task.</summary>
		[JsonProperty("promptdelete")]
		public bool PromptDelete { get; set; } = true;

		/// <summary>Pressing enter in Notes textboxes causes the dialog to close and save.</summary>
		[JsonProperty("entertosave")]
		public bool EnterToSave { get; set; } = true;

		/// <summary>List of enabled columns in the Listview.</summary>
		// TODO: Add "see also"
		[JsonProperty("columns")]
		public Columns EnabledColumns { get; set; } = Columns.Normal;

		/// <summary>Setting to determine whether to split tasks into groups.</summary>
		[JsonProperty("grouptasks")]
		public bool GroupTasks { get; set; } = true;

		/// <summary>When grouping tasks, this setting decides which style of grouping is used.</summary>
		// TODO: Add "see also"
		[JsonProperty("groupstyle")]
		public GroupStyles GroupStyle { get; set; } = GroupStyles.Category;

		/// <summary>Always display a "Completed" group for tasks that are finished.</summary>
		[JsonProperty("alwaysshowcompletedgroup")]
		public bool AlwaysShowCompletedGroup { get; set; } = true;

		/// <summary>Always display a "Overdue" group for tasks that are overdue.</summary>
		[JsonProperty("alwaysshowoverduegroup")]
		public bool AlwaysShowOverdueGroup { get; set; } = true;

		/// <summary>Always display a "Today" group for tasks that are ending today.</summary>
		[JsonProperty("alwaysshowtodaygroup")]
		public bool AlwaysShowTodayGroup { get; set; } = true;

		/// <summary>Display item counts on group headers.</summary>
		[JsonProperty("showitemcounts")]
		public bool ShowItemCounts { get; set; } = true;

		/// <summary>Remove completed tasks automatically.</summary>
		// TODO: Add option to settings dialog for this.
		[JsonProperty("removecompleted")]
		public RemoveType RemoveCompleted { get; set; } = RemoveType.Never;

		/// <summary>Column header text alignment for subitems.</summary>
		[JsonProperty("subitemheaderalign")]
		public HorizontalAlignment SubItemHeaderAlign { get; set; } = HorizontalAlignment.Center;

		/// <summary>Sub item text alignment.</summary>
		[JsonProperty("subitemtextalign")]
		public HorizontalAlignment SubItemTextAlign { get; set; } = HorizontalAlignment.Left;

		/// <summary>Choice of columns to enable in the ListView.</summary>
		[Flags]
		public enum Columns
		{
			/// <summary>Flag field for the Description column.</summary>
			Description = 1,
			/// <summary>Flag field for the Notes column.</summary>
			Notes = 2,
			/// <summary>Flag field for the Category column.</summary>
			Category = 4,
			/// <summary>Flag field for the Due column.</summary>
			Due = 8,
			/// <summary>Flag field for the Time column.</summary>
			Time = 16,
			/// <summary>Combination of Description, Notes, and Time columns.</summary>
			Normal = 19,
			/// <summary>Combination of Description, Notes, Category, Due, and Time columns.</summary>
			All = 31
		}

		/// <summary>Storage/data protocol to use for data retrieval.</summary>
		public enum Protocols
		{
			/// <summary>File storage format using JSON.</summary>
			JSON,
			/// <summary>File export format using XML. NOTE: CURRENTLY UNUSED.</summary>
			XML,
			/// <summary>File export format using text formatting. NOTE: CURRENTLY UNUSED.</summary>
			Text
		}

		/// <summary>When grouping tasks, this settings decides which style of grouping is used.</summary>
		public enum GroupStyles
		{
			/// <summary>Group tasks by the category column.</summary>
			Category,
			/// <summary>Group tasks by the due column.</summary>
			DueTime
		}

		/// <summary>Setting for removing tasks automatically whenever they are completed.</summary>
		public enum RemoveType
		{
			/// <summary>Never remove completed tasks automatically.</summary>
			Never,
			/// <summary>Automatically remove completed tasks immediately.</summary>
			Immediate,
			/// <summary>Automatically remove completed tasks after one hour.</summary>
			OneHour,
			/// <summary>Automatically remove completed tasks after twelve hours.</summary>
			TwelveHours,
			/// <summary>Automatically remove completed tasks after one day.</summary>
			OneDay,
			/// <summary>Automatically remove completed tasks after two days.</summary>
			TwoDays,
			/// <summary>Automatically remove completed tasks after one week.</summary>
			OneWeek,
			/// <summary>Automatically remove completed tasks after two weeks.</summary>
			TwoWeeks,
			/// <summary>Automatically remove completed tasks after one month.</summary>
			OneMonth,
			/// <summary>Automatically remove completed tasks after three months.</summary>
			ThreeMonths,
			/// <summary>Automatically remove completed tasks after six months.</summary>
			SixMonths,
			/// <summary>Automatically remove completed tasks after one year.</summary>
			OneYear
		}

		/// <summary>
		/// Used to retrieve the timespan for when a task is completed and should be removed.
		/// Converts RemoveType to TimeSpan.
		/// </summary>
		/// <param name="Type">Removal setting.</param>
		/// <returns>TimeSpan of how long after a task is completed to remove it.</returns>
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

		/// <summary>
		/// Save settings to settings.cfg function.
		/// </summary>
		/// <param name="Settings">Internal settings object from which to write.</param>
		public static void Save()
		{
			try
			{
				using (FileStream OutputFile = File.Open(Path.Combine(Config.SettingsPath, "settings.cfg"), FileMode.Create))
				using (StreamWriter OutputStream = new StreamWriter(OutputFile))
				using (JsonWriter OutputJson = new JsonTextWriter(OutputStream))
				{
					OutputJson.Formatting = Formatting.Indented;
					JsonSerializer Serializer = new JsonSerializer();
					Serializer.Serialize(OutputJson, Config);
				}

			}
			catch (Exception Args)
			{
				Console.WriteLine("Could not write to settings.cfg! Message: {0}", Args.Message);
			}
		}

		/// <summary>
		/// Load settings from settings.cfg function.
		/// </summary>
		/// <remarks>
		/// TODO: Lay out function into the remarks here and move comments up from the function.
		/// </remarks>
		public static void Load()
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
				bool ShouldSave = false;

				if (string.IsNullOrEmpty(Config.SettingsPath) || !File.Exists(Path.Combine(Config.SettingsPath, "settings.cfg")))
				{
					// 1st Priority: Base directory
					if (File.Exists(Path.Combine(BasePath, "settings.cfg")))
					{
						Config.SettingsPath = BasePath;
					}

					// 2nd Priority: Application Data directory
					else if (File.Exists(Path.Combine(AppDataPath, "settings.cfg")))
					{
						Config.SettingsPath = AppDataPath;
					}

					// Both directories don't have settings already
					// Lets create a new settings file in the Application Data directory
					else
					{ 
						if (File.Exists(Path.Combine(BasePath, "tasks.db")))
						{
							// If a tasks database already exists in the Base directory, choose that
							Config.StorageFolder = BasePath;
						}
						else
						{
							// Otherwise just pick the Application Data directory
							Config.StorageFolder = AppDataPath;
						}

						Config.SettingsPath = AppDataPath;

						// Create identifier hash
						Config.Hash = Guid.NewGuid().ToString("N");
						
						// Save new settings file
						Save();
						return;
					}
				}

				// Load in the settings from the selected settings.cfg file
				using (StreamReader InputFile = File.OpenText(Path.Combine(Config.SettingsPath, "settings.cfg")))
				using (JsonReader InputJson = new JsonTextReader(InputFile))
				{
					SettingsPath = Config.SettingsPath;

					JsonSerializer Serializer = new JsonSerializer();
					Config = (Settings)Serializer.Deserialize(InputJson, typeof(Settings));

					Config.SettingsPath = SettingsPath;
				}

				// Create identifier hash if necessary
				if (string.IsNullOrWhiteSpace(Config.Hash))
				{
					Config.Hash = Guid.NewGuid().ToString("N");
					ShouldSave = true;
				}

				// Check if there is a StorageFolder specified and whether it will work for us
				if (!string.IsNullOrWhiteSpace(Config.StorageFolder) && Directory.Exists(Config.StorageFolder))
				{
					// Directory is fine, do nothing
				}
				else if (File.Exists(Path.Combine(BasePath, "tasks.db")))
				{
					// If a tasks database already exists in the Base directory, choose that
					Config.StorageFolder = BasePath;
					ShouldSave = true;
				}
				else if (File.Exists(Path.Combine(AppDataPath, "tasks.db")))
				{
					// If a tasks database already exists in the Application Data directory, choose that
					Config.StorageFolder = AppDataPath;
					ShouldSave = true;
				}
				else
				{
					// If no database was found already, just save to the same location as the settings file
					Config.StorageFolder = Config.SettingsPath;
					ShouldSave = true;
				}

				if (ShouldSave)
				{
					Save();
				}
			}
			catch (Exception Args)
			{
				Console.WriteLine("Could not load settings.cfg! Message: {0}", Args.Message);
			}
		}
	}
}
