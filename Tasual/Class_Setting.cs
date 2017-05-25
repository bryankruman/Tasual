﻿namespace Tasual
{
	public class Setting
	{
		public Styles Style { get; set; }
		public Protocols Protocol { get; set; }
		public string TextFile { get; set; }
		public bool LaunchOnStartup { get; set; }
		public bool MinimizeToTray { get; set; }
		public bool ConfirmClear { get; set; }
		public bool ConfirmDelete { get; set; }
		public bool AlwaysOnTop { get; set; }

		public enum TimeFormat
		{
			Elapsed,
			Due,
			Short,
			Medium,
			Long
		}

		public enum Styles
		{
			Custom,
			Simple,
			Detailed
		}

		public enum Protocols
		{
			Tasual,
			JSON,
			XML,
			RTM,
			Text
		}
	}
}