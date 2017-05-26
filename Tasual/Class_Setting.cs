using Newtonsoft.Json;

namespace Tasual
{
	public class Setting
	{
		[JsonProperty("style")]
		public Styles Style { get; set; }

		[JsonProperty("protocol")]
		public Protocols Protocol { get; set; }

		[JsonProperty("textfile")]
		public string TextFile { get; set; }

		[JsonProperty("launchonstartup")]
		public bool LaunchOnStartup { get; set; }

		[JsonProperty("minimizetotray")]
		public bool MinimizeToTray { get; set; }

		[JsonProperty("confirmclear")]
		public bool ConfirmClear { get; set; }

		[JsonProperty("confirmdelete")]
		public bool ConfirmDelete { get; set; }

		[JsonProperty("alwaysontop")]
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
