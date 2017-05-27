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

		private void Tasual_Settings_Load(object sender, EventArgs e)
		{
			Tasual_Settings_CheckBox_LaunchOnStartup.Checked = _Tasual_Main.Settings.LaunchOnStartup;
			Tasual_Settings_CheckBox_MinimizeToTray.Checked = _Tasual_Main.Settings.MinimizeToTray;
			Tasual_Settings_CheckBox_AlwaysOnTop.Checked = _Tasual_Main.Settings.AlwaysOnTop;
			Tasual_Settings_CheckBox_PromptClear.Checked = _Tasual_Main.Settings.PromptClear;
			Tasual_Settings_CheckBox_PromptDelete.Checked = _Tasual_Main.Settings.PromptDelete;
			Tasual_Settings_CheckBox_EnterToSave.Checked = _Tasual_Main.Settings.EnterToSave;
			Tasual_Settings_CheckBox_.Checked = _Tasual_Main.Settings.;
			Tasual_Settings_CheckBox_.Checked = _Tasual_Main.Settings.;

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

		}

		private void Tasual_Settings_ComboBox_GroupStyle_SelectedIndexChanged(object sender, EventArgs e)
		{

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

		}
	}
}
