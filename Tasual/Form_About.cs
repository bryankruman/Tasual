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
		public Form_About()
		{
			InitializeComponent();
			Text = String.Format("About {0}", AssemblyInfo.Title);
			labelProductName.Text = AssemblyInfo.Product;
			labelVersion.Text = String.Format("Version {0}", AssemblyInfo.Version);
			labelCopyright.Text = AssemblyInfo.Copyright;
			LinkLabel.Text = AssemblyInfo.Company;
			//this.textBoxDescription.Text = AssemblyDescription;
		}

		private void Button_Close_Click(object Sender, EventArgs Args)
		{
			this.Close();
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
	}
}
