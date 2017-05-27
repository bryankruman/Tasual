using System;
using System.Text.RegularExpressions;

namespace Tasual
{
	class URLExtensions
	{
		// TODO: Expand this to be a bit smarter
		// Currently, it doesn't detect foobar.com as an address. I want that to work.
		// Also: IP addresses.
		// Perhaps create a flag field for allowable options/search methods?
		// Perhaps Uri already supports this better than I could make myself?
		// bool IsUri = Uri.IsWellFormedUriString(Tasual_Link_TextBox.Text, UriKind.RelativeOrAbsolute);

		private static Regex UrlMatch = new Regex(@"(?i)(http(s)?:\/\/)?(\w{2,25}\.)+\w{3}([a-z0-9\-?=$-_.+!*()]+)(?i)", RegexOptions.Singleline);

		public static bool Valid(string Input)
		{
			if (Input != null)
			{
				if (UrlMatch.IsMatch(Input))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		public static void Follow(string Input)
		{
			if (UrlMatch.IsMatch(Input))
			{
				try
				{
					//Console.WriteLine("We tried this");
					System.Diagnostics.Process.Start(Input);
				}
				catch { }
			}
			else
			{
				Console.WriteLine("Invalid URI!");
				// TODO: Throw a messagebox
			}
		}
	}
}
