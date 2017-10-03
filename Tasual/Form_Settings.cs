// ===========================================
//  Copyright (c) 2017 Bryan Kruman
//
//  See LICENSE.rtf file in the project root
//  for full license information.
// ===========================================

using System;
using System.IO;
using System.Windows.Forms;

namespace Tasual
{
	public partial class Form_Settings : Form
	{
		private readonly Form_Main MainForm;
		private bool ChangedStorageFolder;

		public Form_Settings(Form_Main PassedForm)
		{
			InitializeComponent();

			MainForm = PassedForm;
		}
		
		private void UpdateGroupTasks()
		{
			if (CheckBox_GroupTasks.Checked) // TODO: perhaps simplify this by putting all into an object array?
			{
				ComboBox_GroupStyle.Enabled = true;
				CheckBox_ShowItemCounts.Enabled = true;

				if (ComboBox_GroupStyle.SelectedIndex == (int)Settings.GroupStyles.DueTime)
				{
					CheckBox_AlwaysShowCompletedGroup.Checked = true;
					CheckBox_AlwaysShowOverdueGroup.Checked = true;
					CheckBox_AlwaysShowTodayGroup.Checked = true;
					CheckBox_AlwaysShowCompletedGroup.Enabled = false;
					CheckBox_AlwaysShowOverdueGroup.Enabled = false;
					CheckBox_AlwaysShowTodayGroup.Enabled = false;
				}
				else
				{
					CheckBox_AlwaysShowCompletedGroup.Enabled = true;
					CheckBox_AlwaysShowOverdueGroup.Enabled = true;
					CheckBox_AlwaysShowTodayGroup.Enabled = true;
				}
			}
			else
			{
				ComboBox_GroupStyle.Enabled = false;
				CheckBox_AlwaysShowCompletedGroup.Enabled = false;
				CheckBox_AlwaysShowOverdueGroup.Enabled = false;
				CheckBox_AlwaysShowTodayGroup.Enabled = false;
				CheckBox_ShowItemCounts.Enabled = false;
			}
		}

		private void FormLoad(object Sender, EventArgs Args)
		{
			CheckBox_LaunchOnStartup.Checked = Settings.Config.LaunchOnStartup;
			CheckBox_MinimizeToTray.Checked = Settings.Config.MinimizeToTray;
			CheckBox_AlwaysOnTop.Checked = Settings.Config.AlwaysOnTop;
			CheckBox_SaveWindowPos.Checked = Settings.Config.SaveWindowPos;
			CheckBox_PromptClear.Checked = Settings.Config.PromptClear;
			CheckBox_PromptDelete.Checked = Settings.Config.PromptDelete;
			CheckBox_EnterToSave.Checked = Settings.Config.EnterToSave;

			TextBox_Folder.Text = Settings.Config.StorageFolder;

			CheckBox_GroupTasks.Checked = Settings.Config.GroupTasks;
			ComboBox_GroupStyle.SelectedIndex = (int)Settings.Config.GroupStyle;
			CheckBox_AlwaysShowCompletedGroup.Checked = Settings.Config.AlwaysShowCompletedGroup;
			CheckBox_AlwaysShowOverdueGroup.Checked = Settings.Config.AlwaysShowOverdueGroup;
			CheckBox_AlwaysShowTodayGroup.Checked = Settings.Config.AlwaysShowTodayGroup;
			CheckBox_ShowItemCounts.Checked = Settings.Config.ShowItemCounts;

			UpdateGroupTasks();

			ListBox_EnabledColumns.SetSelected(0, ((Settings.Config.EnabledColumns & Settings.Columns.Notes) != 0));
			ListBox_EnabledColumns.SetSelected(1, ((Settings.Config.EnabledColumns & Settings.Columns.Category) != 0));
			ListBox_EnabledColumns.SetSelected(2, ((Settings.Config.EnabledColumns & Settings.Columns.Due) != 0));
			ListBox_EnabledColumns.SetSelected(3, ((Settings.Config.EnabledColumns & Settings.Columns.Time) != 0));
		}

		private void CheckBox_GroupTasks_CheckedChanged(object Sender, EventArgs Args)
		{
			UpdateGroupTasks();
		}

		private void ComboBox_GroupStyle_SelectedIndexChanged(object Sender, EventArgs Args)
		{
			UpdateGroupTasks();
		}

		private void Button_Save_Click(object Sender, EventArgs Args)
		{
			Settings.Config.LaunchOnStartup = CheckBox_LaunchOnStartup.Checked;
			Settings.Config.MinimizeToTray = CheckBox_MinimizeToTray.Checked;
			Settings.Config.AlwaysOnTop = CheckBox_AlwaysOnTop.Checked;
			Settings.Config.SaveWindowPos = CheckBox_SaveWindowPos.Checked;
			Settings.Config.PromptClear = CheckBox_PromptClear.Checked;
			Settings.Config.PromptDelete = CheckBox_PromptDelete.Checked;
			Settings.Config.EnterToSave = CheckBox_EnterToSave.Checked;

			Settings.Config.StorageFolder = TextBox_Folder.Text;

			Settings.Config.GroupTasks = CheckBox_GroupTasks.Checked;
			Settings.Config.GroupStyle = (Settings.GroupStyles)ComboBox_GroupStyle.SelectedIndex; // WONDER IF THIS WORKS
			Settings.Config.AlwaysShowCompletedGroup = CheckBox_AlwaysShowCompletedGroup.Checked;
			Settings.Config.AlwaysShowOverdueGroup = CheckBox_AlwaysShowOverdueGroup.Checked;
			Settings.Config.AlwaysShowTodayGroup = CheckBox_AlwaysShowTodayGroup.Checked;
			Settings.Config.ShowItemCounts = CheckBox_ShowItemCounts.Checked;

			Settings.Config.EnabledColumns = Settings.Columns.Description; // always enable the description column

			if (ListBox_EnabledColumns.SelectedIndices.Contains(0))
			{
				Settings.Config.EnabledColumns |= Settings.Columns.Notes;
			}
			if (ListBox_EnabledColumns.SelectedIndices.Contains(1))
			{
				Settings.Config.EnabledColumns |= Settings.Columns.Category;
			}
			if (ListBox_EnabledColumns.SelectedIndices.Contains(2))
			{
				Settings.Config.EnabledColumns |= Settings.Columns.Due;
			}
			if (ListBox_EnabledColumns.SelectedIndices.Contains(3))
			{
				Settings.Config.EnabledColumns |= Settings.Columns.Time;
			}

			if (ChangedStorageFolder)
			{
				ArrayHandler.Save();
			}

			Settings.Save();
			MainForm.Settings_Apply();
			MainForm.ListView_UpdateColumnSettings();
			MainForm.UpdateGroupKeys();
			MainForm.ListView.BuildList();
			MainForm.ListView.RebuildColumns();
		}

		private void Button_ChangeFolder_Click(object sender, EventArgs e)
		{
			MenuStrip_ChangeFolder.Show(MousePosition);
		}

		private void MenuStrip_ChangeFolder_Custom_Click(object sender, EventArgs e)
		{
			DialogResult Result = FolderBrowserDialog.ShowDialog();
			if (Result != DialogResult.OK)
			{
				return;
			}

			TextBox_Folder.Text = FolderBrowserDialog.SelectedPath;
			ChangedStorageFolder = true;

			// TODO: Check to make sure we have read AND write access to this directory
			//       Show messagebox if not and reset back to previous selection.
		}

		private void MenuStrip_ChangeFolder_BaseDirectory_Click(object sender, EventArgs e)
		{
			TextBox_Folder.Text = AppDomain.CurrentDomain.BaseDirectory;
			ChangedStorageFolder = true;
		}

		private void MenuStrip_ChangeFolder_AppData_Click(object sender, EventArgs e)
		{
			TextBox_Folder.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tasual");
			ChangedStorageFolder = true;
		}
	}
}
