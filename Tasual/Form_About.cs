// ===========================================
//  Copyright (c) 2017 Bryan Kruman
//
//  See LICENSE.rtf file in the project root
//  for full license information.
// ===========================================

using System;
using System.Windows.Forms;

namespace Tasual
{
	partial class Form_About : Form
	{
		private bool UpdateAvailable = false;

		public bool ShouldUpdate()
		{
			var LatestVersion = new Version(Settings.Config.LatestVersion);
			var CurrentVersion = new Version(AssemblyInfo.Version);

			int Result = LatestVersion.CompareTo(CurrentVersion);

			return (Result > 0);
		}

		private void CheckUpdateButtonStatus()
		{
			if (ShouldUpdate())
			{
				Button_Update.Text = String.Format("Download latest &update ({0})", Settings.Config.LatestVersion);
				UpdateAvailable = true;
			}
			else
			{
				Button_Update.Text = "Check for &updates";
				UpdateAvailable = false;
			}
		}

		public void VersionCheck_Callback_OnLoad(Interface.VersionCheck.RequestResult RequestResult)
		{
			// Thread safe invoke handling
			if (InvokeRequired)
			{
				Invoke(new Action<Interface.VersionCheck.RequestResult>(VersionCheck_Callback_OnLoad), RequestResult);
				return;
			}

			if (RequestResult == Interface.VersionCheck.RequestResult.UpdateFound)
			{

				if (!Settings.Config.PromptUpdate)
				{
					CheckUpdateButtonStatus();
					return;
				}

				DialogResult Choice = MessageBox.Show(
					string.Format(
						"New update (version {0}) available!\nDo you want to download the update now?",
						Settings.Config.LatestVersion
					),
					"Tasual",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Information);

				if (Choice == DialogResult.Yes)
				{
					Close();
					Form_Main.DownloadUpdate();
				}
				else
				{
					CheckUpdateButtonStatus();
				}
			}
		}

		public void VersionCheck_Callback_FromButton(Interface.VersionCheck.RequestResult RequestResult)
		{
			// Thread safe invoke handling
			if (InvokeRequired)
			{
				Invoke(new Action<Interface.VersionCheck.RequestResult>(VersionCheck_Callback_FromButton), RequestResult);
				return;
			}

			switch (RequestResult)
			{
				case Interface.VersionCheck.RequestResult.UpdateFound:
					{
						if (!Settings.Config.PromptUpdate) { return; }

						DialogResult Choice = MessageBox.Show(
							string.Format(
								"New update (version {0}) available!\nDo you want to download the update now?",
								Settings.Config.LatestVersion
							),
							"Tasual",
							MessageBoxButtons.YesNo,
							MessageBoxIcon.Information);

						if (Choice == DialogResult.Yes)
						{
							Close();
							Form_Main.DownloadUpdate();
						}
						else
						{
							CheckUpdateButtonStatus();
						}
						break;
					}

				case Interface.VersionCheck.RequestResult.UpToDate:
					{
						MessageBox.Show(
							"Already up to date",
							"Tasual",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information
						);
						break;
					}

				case Interface.VersionCheck.RequestResult.NotCompleted:
				case Interface.VersionCheck.RequestResult.BadRequest:
					{
						MessageBox.Show(
							"Failed to check for updates (check network connection)",
							"Tasual",
							MessageBoxButtons.OK,
							MessageBoxIcon.Error
						);
						break;
					}
			}
		}

		public Form_About()
		{
			InitializeComponent();
			Text = String.Format("About {0}", AssemblyInfo.Title);
			labelProductName.Text = AssemblyInfo.Product;
			labelVersion.Text = String.Format("Version {0}", AssemblyInfo.Version);
			labelCopyright.Text = AssemblyInfo.Copyright;
			LinkLabel.Text = AssemblyInfo.Company;

			CheckUpdateButtonStatus();

			Interface.VersionCheck.Request(VersionCheck_Callback_OnLoad);
		}

		private void Button_Close_Click(object Sender, EventArgs Args)
		{
			Close();
		}

		private void Button_Donate_Click(object Sender, EventArgs Args)
		{
			try
			{
				System.Diagnostics.Process.Start("https://www.paypal.com/donate/?token=7XZwBMQUS8SN3-2ZubZjcPKkGITV5K2ljLmc6X_baq2MO37KEKIa2AFU7rLAPprJG2U6HG");
			}
			catch { }
		}

		private void LinkLabel_LinkClicked(object Sender, LinkLabelLinkClickedEventArgs Args)
		{
			try
			{
				System.Diagnostics.Process.Start("http://www.bryankruman.com/tasual");
			}
			catch { }
		}

		private void Button_Update_Click(object sender, EventArgs e)
		{
			if (UpdateAvailable)
			{
				Form_Main.DownloadUpdate();
			}
			else
			{
				Interface.VersionCheck.Request(VersionCheck_Callback_FromButton);
			}
		}
	}
}
