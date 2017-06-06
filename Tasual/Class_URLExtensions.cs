// ===========================================
//  Copyright (c) 2017 Bryan Kruman
//
//  See LICENSE.txt file in the project root
//  for full license information.
// ===========================================

using System;
using System.Net;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Linq;

namespace Tasual
{
	class URLExtensions
	{
		// TODO: Expand this to be a bit smarter
		// Currently, it doesn't detect foobar.com as an address. I want that to work.
		// Also: IP addresses.
		// Perhaps create a flag field for allowable options/search methods?
		// Perhaps Uri already supports this better than I could make myself?
		// bool IsUri = Uri.IsWellFormedUriString(TextBox.Text, UriKind.RelativeOrAbsolute);

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

		public static bool ValidateIPv4(string IPString)
		{
			if (IPString.Count(c => c == '.') != 3) { return false; }
			IPAddress address;
			return IPAddress.TryParse(IPString, out address);
		}

		public static void Follow(string Input)
		{
			if (UrlMatch.IsMatch(Input))
			{
				try
				{
					// If it's not formatted as a URL already
					if (!Input.Contains("http://"))
					{
						// Check to see if it's an IP address
						if (ValidateIPv4(Input))
						{
							// Prefix http:// so it'll launch correctly
							Process.Start(("http://" + Input));
						}
						else
						{
							// It wasn't an IP address, lets just launch it as normal
							// (It could very well be a file or executable command or anything like that)
							Process.Start(Input);
						}
					}
					// Already formatted as an HTTP URL, just pass it through
					else 
					{
						Process.Start(Input);
					}
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
