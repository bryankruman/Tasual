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
	public partial class Tasual_Settings : Form
	{
		private readonly Tasual_Main _Tasual_Main;

		public Tasual_Settings(Tasual_Main PassedForm)
		{
			InitializeComponent();

			_Tasual_Main = PassedForm;
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
			Tasual_Settings_CheckBox_LaunchOnStartup.Checked = _Tasual_Main.Settings.LaunchOnStartup;
			Tasual_Settings_CheckBox_MinimizeToTray.Checked = _Tasual_Main.Settings.MinimizeToTray;
			Tasual_Settings_CheckBox_AlwaysOnTop.Checked = _Tasual_Main.Settings.AlwaysOnTop;
			Tasual_Settings_CheckBox_PromptClear.Checked = _Tasual_Main.Settings.PromptClear;
			Tasual_Settings_CheckBox_PromptDelete.Checked = _Tasual_Main.Settings.PromptDelete;
			Tasual_Settings_CheckBox_EnterToSave.Checked = _Tasual_Main.Settings.EnterToSave;

			Tasual_Settings_CheckBox_GroupTasks.Checked = _Tasual_Main.Settings.GroupTasks;
			Tasual_Settings_ComboBox_GroupStyle.SelectedIndex = (int)_Tasual_Main.Settings.GroupStyle;
			Tasual_Settings_CheckBox_AlwaysShowCompletedGroup.Checked = _Tasual_Main.Settings.AlwaysShowCompletedGroup;
			Tasual_Settings_CheckBox_AlwaysShowOverdueGroup.Checked = _Tasual_Main.Settings.AlwaysShowOverdueGroup;
			Tasual_Settings_CheckBox_AlwaysShowTodayGroup.Checked = _Tasual_Main.Settings.AlwaysShowTodayGroup;
			Tasual_Settings_CheckBox_ShowItemCounts.Checked = _Tasual_Main.Settings.ShowItemCounts;

			Tasual_Settings_UpdateGroupTasks();

			Tasual_Settings_ListBox_EnabledColumns.SetSelected(0, ((_Tasual_Main.Settings.EnabledColumns & Setting.Columns.Notes) != 0));
			Tasual_Settings_ListBox_EnabledColumns.SetSelected(1, ((_Tasual_Main.Settings.EnabledColumns & Setting.Columns.Category) != 0));
			Tasual_Settings_ListBox_EnabledColumns.SetSelected(2, ((_Tasual_Main.Settings.EnabledColumns & Setting.Columns.Due) != 0));
			Tasual_Settings_ListBox_EnabledColumns.SetSelected(3, ((_Tasual_Main.Settings.EnabledColumns & Setting.Columns.Time) != 0));
		}

		private void Tasual_Settings_CheckBox_LaunchOnStartup_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void Tasual_Settings_CheckBox_MinimizeToTray_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void Tasual_Settings_CheckBox_AlwaysOnTop_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void Tasual_Settings_CheckBox_PromptClear_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void Tasual_Settings_CheckBox_PromptDelete_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void Tasual_Settings_CheckBox_EnterToSave_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void Tasual_Settings_CheckBox_GroupTasks_CheckedChanged(object sender, EventArgs e)
		{
			Tasual_Settings_UpdateGroupTasks();
		}

		private void Tasual_Settings_ComboBox_GroupStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			Tasual_Settings_UpdateGroupTasks();
		}

		private void Tasual_Settings_CheckBox_AlwaysShowCompletedGroup_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void Tasual_Settings_CheckBox_AlwaysShowOverdueGroup_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void Tasual_Settings_CheckBox_AlwaysShowTodayGroup_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void Tasual_Settings_CheckBox_ShowItemCounts_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void Tasual_Settings_ListBox_EnabledColumns_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void Tasual_Settings_Button_Save_Click(object sender, EventArgs e)
		{
			_Tasual_Main.Settings.LaunchOnStartup = Tasual_Settings_CheckBox_LaunchOnStartup.Checked;
			_Tasual_Main.Settings.MinimizeToTray = Tasual_Settings_CheckBox_MinimizeToTray.Checked;
			_Tasual_Main.Settings.AlwaysOnTop = Tasual_Settings_CheckBox_AlwaysOnTop.Checked;
			_Tasual_Main.Settings.PromptClear = Tasual_Settings_CheckBox_PromptClear.Checked;
			_Tasual_Main.Settings.PromptDelete = Tasual_Settings_CheckBox_PromptDelete.Checked;
			_Tasual_Main.Settings.EnterToSave = Tasual_Settings_CheckBox_EnterToSave.Checked;

			_Tasual_Main.Settings.GroupTasks = Tasual_Settings_CheckBox_GroupTasks.Checked;
			_Tasual_Main.Settings.GroupStyle = (Setting.GroupStyles)Tasual_Settings_ComboBox_GroupStyle.SelectedIndex; // WONDER IF THIS WORKS
			_Tasual_Main.Settings.AlwaysShowCompletedGroup = Tasual_Settings_CheckBox_AlwaysShowCompletedGroup.Checked;
			_Tasual_Main.Settings.AlwaysShowOverdueGroup = Tasual_Settings_CheckBox_AlwaysShowOverdueGroup.Checked;
			_Tasual_Main.Settings.AlwaysShowTodayGroup = Tasual_Settings_CheckBox_AlwaysShowTodayGroup.Checked;
			_Tasual_Main.Settings.ShowItemCounts = Tasual_Settings_CheckBox_ShowItemCounts.Checked;

			_Tasual_Main.Settings.EnabledColumns = Setting.Columns.Description; // always enable the description column

			if (Tasual_Settings_ListBox_EnabledColumns.SelectedIndices.Contains(0))
			{
				_Tasual_Main.Settings.EnabledColumns |= Setting.Columns.Notes;
			}
			if (Tasual_Settings_ListBox_EnabledColumns.SelectedIndices.Contains(1))
			{
				_Tasual_Main.Settings.EnabledColumns |= Setting.Columns.Category;
			}
			if (Tasual_Settings_ListBox_EnabledColumns.SelectedIndices.Contains(2))
			{
				_Tasual_Main.Settings.EnabledColumns |= Setting.Columns.Due;
			}
			if (Tasual_Settings_ListBox_EnabledColumns.SelectedIndices.Contains(3))
			{
				_Tasual_Main.Settings.EnabledColumns |= Setting.Columns.Time;
			}

			_Tasual_Main.Tasual_Settings_Save();
		}
	}
}
