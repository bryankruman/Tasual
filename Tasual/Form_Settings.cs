using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tasual
{
	public partial class Form_Settings : Form
	{
		private readonly Form_Main MainForm;

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

				if (ComboBox_GroupStyle.SelectedIndex == (int)Setting.GroupStyles.DueTime)
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

		private void Load(object sender, EventArgs e)
		{
			CheckBox_LaunchOnStartup.Checked = MainForm.Settings.LaunchOnStartup;
			CheckBox_MinimizeToTray.Checked = MainForm.Settings.MinimizeToTray;
			CheckBox_AlwaysOnTop.Checked = MainForm.Settings.AlwaysOnTop;
			CheckBox_SaveWindowPos.Checked = MainForm.Settings.SaveWindowPos;
			CheckBox_PromptClear.Checked = MainForm.Settings.PromptClear;
			CheckBox_PromptDelete.Checked = MainForm.Settings.PromptDelete;
			CheckBox_EnterToSave.Checked = MainForm.Settings.EnterToSave;

			CheckBox_GroupTasks.Checked = MainForm.Settings.GroupTasks;
			ComboBox_GroupStyle.SelectedIndex = (int)MainForm.Settings.GroupStyle;
			CheckBox_AlwaysShowCompletedGroup.Checked = MainForm.Settings.AlwaysShowCompletedGroup;
			CheckBox_AlwaysShowOverdueGroup.Checked = MainForm.Settings.AlwaysShowOverdueGroup;
			CheckBox_AlwaysShowTodayGroup.Checked = MainForm.Settings.AlwaysShowTodayGroup;
			CheckBox_ShowItemCounts.Checked = MainForm.Settings.ShowItemCounts;

			UpdateGroupTasks();

			ListBox_EnabledColumns.SetSelected(0, ((MainForm.Settings.EnabledColumns & Setting.Columns.Notes) != 0));
			ListBox_EnabledColumns.SetSelected(1, ((MainForm.Settings.EnabledColumns & Setting.Columns.Category) != 0));
			ListBox_EnabledColumns.SetSelected(2, ((MainForm.Settings.EnabledColumns & Setting.Columns.Due) != 0));
			ListBox_EnabledColumns.SetSelected(3, ((MainForm.Settings.EnabledColumns & Setting.Columns.Time) != 0));
		}

		private void CheckBox_GroupTasks_CheckedChanged(object sender, EventArgs e)
		{
			UpdateGroupTasks();
		}

		private void ComboBox_GroupStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateGroupTasks();
		}

		private void Button_Save_Click(object sender, EventArgs e)
		{
			MainForm.Settings.LaunchOnStartup = CheckBox_LaunchOnStartup.Checked;
			MainForm.Settings.MinimizeToTray = CheckBox_MinimizeToTray.Checked;
			MainForm.Settings.AlwaysOnTop = CheckBox_AlwaysOnTop.Checked;
			MainForm.Settings.SaveWindowPos = CheckBox_SaveWindowPos.Checked;
			MainForm.Settings.PromptClear = CheckBox_PromptClear.Checked;
			MainForm.Settings.PromptDelete = CheckBox_PromptDelete.Checked;
			MainForm.Settings.EnterToSave = CheckBox_EnterToSave.Checked;

			MainForm.Settings.GroupTasks = CheckBox_GroupTasks.Checked;
			MainForm.Settings.GroupStyle = (Setting.GroupStyles)ComboBox_GroupStyle.SelectedIndex; // WONDER IF THIS WORKS
			MainForm.Settings.AlwaysShowCompletedGroup = CheckBox_AlwaysShowCompletedGroup.Checked;
			MainForm.Settings.AlwaysShowOverdueGroup = CheckBox_AlwaysShowOverdueGroup.Checked;
			MainForm.Settings.AlwaysShowTodayGroup = CheckBox_AlwaysShowTodayGroup.Checked;
			MainForm.Settings.ShowItemCounts = CheckBox_ShowItemCounts.Checked;

			MainForm.Settings.EnabledColumns = Setting.Columns.Description; // always enable the description column

			if (ListBox_EnabledColumns.SelectedIndices.Contains(0))
			{
				MainForm.Settings.EnabledColumns |= Setting.Columns.Notes;
			}
			if (ListBox_EnabledColumns.SelectedIndices.Contains(1))
			{
				MainForm.Settings.EnabledColumns |= Setting.Columns.Category;
			}
			if (ListBox_EnabledColumns.SelectedIndices.Contains(2))
			{
				MainForm.Settings.EnabledColumns |= Setting.Columns.Due;
			}
			if (ListBox_EnabledColumns.SelectedIndices.Contains(3))
			{
				MainForm.Settings.EnabledColumns |= Setting.Columns.Time;
			}

			MainForm.Save();
			MainForm.Apply();
			MainForm.Tasual_ListView_UpdateColumnSettings();
			MainForm.Tasual_UpdateGroupKeys();
			MainForm.Tasual_ListView.BuildList();
			MainForm.Tasual_ListView.RebuildColumns();
		}
	}
}
