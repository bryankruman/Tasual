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
		
		private void Tasual_Settings_UpdateGroupTasks()
		{
			if (Tasual_Settings_CheckBox_GroupTasks.Checked) // TODO: perhaps simplify this by putting all into an object array?
			{
				Tasual_Settings_ComboBox_GroupStyle.Enabled = true;
				Tasual_Settings_CheckBox_ShowItemCounts.Enabled = true;

				if (Tasual_Settings_ComboBox_GroupStyle.SelectedIndex == (int)Setting.GroupStyles.DueTime)
				{
					Tasual_Settings_CheckBox_AlwaysShowCompletedGroup.Checked = true;
					Tasual_Settings_CheckBox_AlwaysShowOverdueGroup.Checked = true;
					Tasual_Settings_CheckBox_AlwaysShowTodayGroup.Checked = true;
					Tasual_Settings_CheckBox_AlwaysShowCompletedGroup.Enabled = false;
					Tasual_Settings_CheckBox_AlwaysShowOverdueGroup.Enabled = false;
					Tasual_Settings_CheckBox_AlwaysShowTodayGroup.Enabled = false;
				}
				else
				{
					Tasual_Settings_CheckBox_AlwaysShowCompletedGroup.Enabled = true;
					Tasual_Settings_CheckBox_AlwaysShowOverdueGroup.Enabled = true;
					Tasual_Settings_CheckBox_AlwaysShowTodayGroup.Enabled = true;
				}
			}
			else
			{
				Tasual_Settings_ComboBox_GroupStyle.Enabled = false;
				Tasual_Settings_CheckBox_AlwaysShowCompletedGroup.Enabled = false;
				Tasual_Settings_CheckBox_AlwaysShowOverdueGroup.Enabled = false;
				Tasual_Settings_CheckBox_AlwaysShowTodayGroup.Enabled = false;
				Tasual_Settings_CheckBox_ShowItemCounts.Enabled = false;
			}
		}

		private void Tasual_Settings_Load(object sender, EventArgs e)
		{
			Tasual_Settings_CheckBox_LaunchOnStartup.Checked = MainForm.Settings.LaunchOnStartup;
			Tasual_Settings_CheckBox_MinimizeToTray.Checked = MainForm.Settings.MinimizeToTray;
			Tasual_Settings_CheckBox_AlwaysOnTop.Checked = MainForm.Settings.AlwaysOnTop;
			Tasual_Settings_CheckBox_SaveWindowPos.Checked = MainForm.Settings.SaveWindowPos;
			Tasual_Settings_CheckBox_PromptClear.Checked = MainForm.Settings.PromptClear;
			Tasual_Settings_CheckBox_PromptDelete.Checked = MainForm.Settings.PromptDelete;
			Tasual_Settings_CheckBox_EnterToSave.Checked = MainForm.Settings.EnterToSave;

			Tasual_Settings_CheckBox_GroupTasks.Checked = MainForm.Settings.GroupTasks;
			Tasual_Settings_ComboBox_GroupStyle.SelectedIndex = (int)MainForm.Settings.GroupStyle;
			Tasual_Settings_CheckBox_AlwaysShowCompletedGroup.Checked = MainForm.Settings.AlwaysShowCompletedGroup;
			Tasual_Settings_CheckBox_AlwaysShowOverdueGroup.Checked = MainForm.Settings.AlwaysShowOverdueGroup;
			Tasual_Settings_CheckBox_AlwaysShowTodayGroup.Checked = MainForm.Settings.AlwaysShowTodayGroup;
			Tasual_Settings_CheckBox_ShowItemCounts.Checked = MainForm.Settings.ShowItemCounts;

			Tasual_Settings_UpdateGroupTasks();

			Tasual_Settings_ListBox_EnabledColumns.SetSelected(0, ((MainForm.Settings.EnabledColumns & Setting.Columns.Notes) != 0));
			Tasual_Settings_ListBox_EnabledColumns.SetSelected(1, ((MainForm.Settings.EnabledColumns & Setting.Columns.Category) != 0));
			Tasual_Settings_ListBox_EnabledColumns.SetSelected(2, ((MainForm.Settings.EnabledColumns & Setting.Columns.Due) != 0));
			Tasual_Settings_ListBox_EnabledColumns.SetSelected(3, ((MainForm.Settings.EnabledColumns & Setting.Columns.Time) != 0));
		}

		private void Tasual_Settings_CheckBox_GroupTasks_CheckedChanged(object sender, EventArgs e)
		{
			Tasual_Settings_UpdateGroupTasks();
		}

		private void Tasual_Settings_ComboBox_GroupStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			Tasual_Settings_UpdateGroupTasks();
		}

		private void Tasual_Settings_Button_Save_Click(object sender, EventArgs e)
		{
			MainForm.Settings.LaunchOnStartup = Tasual_Settings_CheckBox_LaunchOnStartup.Checked;
			MainForm.Settings.MinimizeToTray = Tasual_Settings_CheckBox_MinimizeToTray.Checked;
			MainForm.Settings.AlwaysOnTop = Tasual_Settings_CheckBox_AlwaysOnTop.Checked;
			MainForm.Settings.SaveWindowPos = Tasual_Settings_CheckBox_SaveWindowPos.Checked;
			MainForm.Settings.PromptClear = Tasual_Settings_CheckBox_PromptClear.Checked;
			MainForm.Settings.PromptDelete = Tasual_Settings_CheckBox_PromptDelete.Checked;
			MainForm.Settings.EnterToSave = Tasual_Settings_CheckBox_EnterToSave.Checked;

			MainForm.Settings.GroupTasks = Tasual_Settings_CheckBox_GroupTasks.Checked;
			MainForm.Settings.GroupStyle = (Setting.GroupStyles)Tasual_Settings_ComboBox_GroupStyle.SelectedIndex; // WONDER IF THIS WORKS
			MainForm.Settings.AlwaysShowCompletedGroup = Tasual_Settings_CheckBox_AlwaysShowCompletedGroup.Checked;
			MainForm.Settings.AlwaysShowOverdueGroup = Tasual_Settings_CheckBox_AlwaysShowOverdueGroup.Checked;
			MainForm.Settings.AlwaysShowTodayGroup = Tasual_Settings_CheckBox_AlwaysShowTodayGroup.Checked;
			MainForm.Settings.ShowItemCounts = Tasual_Settings_CheckBox_ShowItemCounts.Checked;

			MainForm.Settings.EnabledColumns = Setting.Columns.Description; // always enable the description column

			if (Tasual_Settings_ListBox_EnabledColumns.SelectedIndices.Contains(0))
			{
				MainForm.Settings.EnabledColumns |= Setting.Columns.Notes;
			}
			if (Tasual_Settings_ListBox_EnabledColumns.SelectedIndices.Contains(1))
			{
				MainForm.Settings.EnabledColumns |= Setting.Columns.Category;
			}
			if (Tasual_Settings_ListBox_EnabledColumns.SelectedIndices.Contains(2))
			{
				MainForm.Settings.EnabledColumns |= Setting.Columns.Due;
			}
			if (Tasual_Settings_ListBox_EnabledColumns.SelectedIndices.Contains(3))
			{
				MainForm.Settings.EnabledColumns |= Setting.Columns.Time;
			}

			MainForm.Tasual_Settings_Save();
			MainForm.Tasual_Settings_Apply();
			MainForm.Tasual_ListView_UpdateColumnSettings();
			MainForm.Tasual_UpdateGroupKeys();
			MainForm.Tasual_ListView.BuildList();
			MainForm.Tasual_ListView.RebuildColumns();
		}
	}
}
