using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasual
{
	public class Setting
	{
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

		public Styles Style;
		public string TextFile;
		public bool ConfirmClear;
		public bool ConfirmDelete;
		public bool AlwaysOnTop;
	}
}
