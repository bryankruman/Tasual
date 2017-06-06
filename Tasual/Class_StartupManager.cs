using System;
using Microsoft.Win32;
using System.Security.Principal;

namespace Tasual
{
	class StartupManager
	{
		public static void SetStartupStatus(bool ShouldStartup)
		{
			try
			{
				if (IsUserAdministrator())
				{
					using (RegistryKey Key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
					{
						bool CurrentlyStarting = (Key.GetValue("Tasual") != null);
						if (!CurrentlyStarting && ShouldStartup)
						{
							AddApplicationToAllUserStartup();
						}
						else if (CurrentlyStarting && !ShouldStartup)
						{
							RemoveApplicationFromAllUserStartup();
						}
					}
				}
				else
				{
					using (RegistryKey Key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
					{
						bool CurrentlyStarting = (Key.GetValue("Tasual") != null);
						if (!CurrentlyStarting && ShouldStartup)
						{
							AddApplicationToCurrentUserStartup();
						}
						else if (CurrentlyStarting && !ShouldStartup)
						{
							RemoveApplicationFromCurrentUserStartup();
						}
					}
				}
			}
			catch (Exception Ex)
			{
				Console.WriteLine(Ex);
			}
		}

		public static void AddApplicationToCurrentUserStartup()
		{
			using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
			{
				key.SetValue("Tasual", "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
			}
			Console.WriteLine("AddApplicationToCurrentUserStartup");
		}

		public static void AddApplicationToAllUserStartup()
		{
			using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
			{
				key.SetValue("Tasual", "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
			}
			Console.WriteLine("AddApplicationToAllUserStartup");
		}

		public static void RemoveApplicationFromCurrentUserStartup()
		{
			using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
			{
				key.DeleteValue("Tasual", false);
			}
			Console.WriteLine("RemoveApplicationFromCurrentUserStartup");
		}

		public static void RemoveApplicationFromAllUserStartup()
		{
			using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
			{
				key.DeleteValue("Tasual", false);
			}
			Console.WriteLine("RemoveApplicationFromAllUserStartup");
		}

		public static bool IsUserAdministrator()
		{
			// Bool value to hold our return value
			bool Admin;

			try
			{
				// Get the currently logged in user
				WindowsIdentity User = WindowsIdentity.GetCurrent();
				WindowsPrincipal Principal = new WindowsPrincipal(User);
				Admin = Principal.IsInRole(WindowsBuiltInRole.Administrator);
			}
			catch (UnauthorizedAccessException)
			{
				// Custom handling here
				Admin = false;
			}
			catch (Exception)
			{
				// Custom handling here
				Admin = false;
			}

			return Admin;
		}
	}
}
