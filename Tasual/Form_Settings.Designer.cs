namespace Tasual
{
	partial class Form_Settings
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Tasual_Settings_CheckBox_GroupTasks = new System.Windows.Forms.CheckBox();
			this.Tasual_Settings_ComboBox_GroupStyle = new System.Windows.Forms.ComboBox();
			this.Tasual_Settings_CheckBox_AlwaysShowCompletedGroup = new System.Windows.Forms.CheckBox();
			this.Tasual_Settings_CheckBox_AlwaysShowOverdueGroup = new System.Windows.Forms.CheckBox();
			this.Tasual_Settings_CheckBox_AlwaysShowTodayGroup = new System.Windows.Forms.CheckBox();
			this.Tasual_Settings_CheckBox_LaunchOnStartup = new System.Windows.Forms.CheckBox();
			this.Tasual_Settings_CheckBox_MinimizeToTray = new System.Windows.Forms.CheckBox();
			this.Tasual_Settings_CheckBox_PromptClear = new System.Windows.Forms.CheckBox();
			this.Tasual_Settings_CheckBox_PromptDelete = new System.Windows.Forms.CheckBox();
			this.Tasual_Settings_GroupBox_Application = new System.Windows.Forms.GroupBox();
			this.Tasual_Settings_CheckBox_EnterToSave = new System.Windows.Forms.CheckBox();
			this.Tasual_Settings_CheckBox_AlwaysOnTop = new System.Windows.Forms.CheckBox();
			this.Tasual_Settings_GroupBox_Display = new System.Windows.Forms.GroupBox();
			this.Tasual_Settings_CheckBox_ShowItemCounts = new System.Windows.Forms.CheckBox();
			this.Tasual_Settings_ListBox_EnabledColumns = new System.Windows.Forms.ListBox();
			this.Tasual_Settings_Label_Columns = new System.Windows.Forms.Label();
			this.Tasual_Settings_Button_Save = new System.Windows.Forms.Button();
			this.Tasual_Settings_Button_Cancel = new System.Windows.Forms.Button();
			this.Tasual_Settings_CheckBox_SaveWindowPos = new System.Windows.Forms.CheckBox();
			this.Tasual_Settings_GroupBox_Application.SuspendLayout();
			this.Tasual_Settings_GroupBox_Display.SuspendLayout();
			this.SuspendLayout();
			// 
			// Tasual_Settings_CheckBox_GroupTasks
			// 
			this.Tasual_Settings_CheckBox_GroupTasks.AutoSize = true;
			this.Tasual_Settings_CheckBox_GroupTasks.Location = new System.Drawing.Point(12, 30);
			this.Tasual_Settings_CheckBox_GroupTasks.Name = "Tasual_Settings_CheckBox_GroupTasks";
			this.Tasual_Settings_CheckBox_GroupTasks.Size = new System.Drawing.Size(83, 17);
			this.Tasual_Settings_CheckBox_GroupTasks.TabIndex = 0;
			this.Tasual_Settings_CheckBox_GroupTasks.Text = "Group tasks";
			this.Tasual_Settings_CheckBox_GroupTasks.UseVisualStyleBackColor = true;
			this.Tasual_Settings_CheckBox_GroupTasks.CheckedChanged += new System.EventHandler(this.Tasual_Settings_CheckBox_GroupTasks_CheckedChanged);
			// 
			// Tasual_Settings_ComboBox_GroupStyle
			// 
			this.Tasual_Settings_ComboBox_GroupStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Tasual_Settings_ComboBox_GroupStyle.FormattingEnabled = true;
			this.Tasual_Settings_ComboBox_GroupStyle.Items.AddRange(new object[] {
            "by category",
            "by due time"});
			this.Tasual_Settings_ComboBox_GroupStyle.Location = new System.Drawing.Point(111, 27);
			this.Tasual_Settings_ComboBox_GroupStyle.Name = "Tasual_Settings_ComboBox_GroupStyle";
			this.Tasual_Settings_ComboBox_GroupStyle.Size = new System.Drawing.Size(157, 21);
			this.Tasual_Settings_ComboBox_GroupStyle.TabIndex = 1;
			this.Tasual_Settings_ComboBox_GroupStyle.SelectedIndexChanged += new System.EventHandler(this.Tasual_Settings_ComboBox_GroupStyle_SelectedIndexChanged);
			// 
			// Tasual_Settings_CheckBox_AlwaysShowCompletedGroup
			// 
			this.Tasual_Settings_CheckBox_AlwaysShowCompletedGroup.AutoSize = true;
			this.Tasual_Settings_CheckBox_AlwaysShowCompletedGroup.Location = new System.Drawing.Point(26, 55);
			this.Tasual_Settings_CheckBox_AlwaysShowCompletedGroup.Name = "Tasual_Settings_CheckBox_AlwaysShowCompletedGroup";
			this.Tasual_Settings_CheckBox_AlwaysShowCompletedGroup.Size = new System.Drawing.Size(220, 17);
			this.Tasual_Settings_CheckBox_AlwaysShowCompletedGroup.TabIndex = 2;
			this.Tasual_Settings_CheckBox_AlwaysShowCompletedGroup.Text = "Split completed tasks into separate group";
			this.Tasual_Settings_CheckBox_AlwaysShowCompletedGroup.UseVisualStyleBackColor = true;
			// 
			// Tasual_Settings_CheckBox_AlwaysShowOverdueGroup
			// 
			this.Tasual_Settings_CheckBox_AlwaysShowOverdueGroup.AutoSize = true;
			this.Tasual_Settings_CheckBox_AlwaysShowOverdueGroup.Location = new System.Drawing.Point(26, 79);
			this.Tasual_Settings_CheckBox_AlwaysShowOverdueGroup.Name = "Tasual_Settings_CheckBox_AlwaysShowOverdueGroup";
			this.Tasual_Settings_CheckBox_AlwaysShowOverdueGroup.Size = new System.Drawing.Size(210, 17);
			this.Tasual_Settings_CheckBox_AlwaysShowOverdueGroup.TabIndex = 3;
			this.Tasual_Settings_CheckBox_AlwaysShowOverdueGroup.Text = "Split overdue tasks into separate group";
			this.Tasual_Settings_CheckBox_AlwaysShowOverdueGroup.UseVisualStyleBackColor = true;
			// 
			// Tasual_Settings_CheckBox_AlwaysShowTodayGroup
			// 
			this.Tasual_Settings_CheckBox_AlwaysShowTodayGroup.AutoSize = true;
			this.Tasual_Settings_CheckBox_AlwaysShowTodayGroup.Location = new System.Drawing.Point(26, 103);
			this.Tasual_Settings_CheckBox_AlwaysShowTodayGroup.Name = "Tasual_Settings_CheckBox_AlwaysShowTodayGroup";
			this.Tasual_Settings_CheckBox_AlwaysShowTodayGroup.Size = new System.Drawing.Size(218, 17);
			this.Tasual_Settings_CheckBox_AlwaysShowTodayGroup.TabIndex = 4;
			this.Tasual_Settings_CheckBox_AlwaysShowTodayGroup.Text = "Split tasks due today into separate group";
			this.Tasual_Settings_CheckBox_AlwaysShowTodayGroup.UseVisualStyleBackColor = true;
			// 
			// Tasual_Settings_CheckBox_LaunchOnStartup
			// 
			this.Tasual_Settings_CheckBox_LaunchOnStartup.AutoSize = true;
			this.Tasual_Settings_CheckBox_LaunchOnStartup.Location = new System.Drawing.Point(12, 23);
			this.Tasual_Settings_CheckBox_LaunchOnStartup.Name = "Tasual_Settings_CheckBox_LaunchOnStartup";
			this.Tasual_Settings_CheckBox_LaunchOnStartup.Size = new System.Drawing.Size(168, 17);
			this.Tasual_Settings_CheckBox_LaunchOnStartup.TabIndex = 5;
			this.Tasual_Settings_CheckBox_LaunchOnStartup.Text = "Start Tasual on system startup";
			this.Tasual_Settings_CheckBox_LaunchOnStartup.UseVisualStyleBackColor = true;
			// 
			// Tasual_Settings_CheckBox_MinimizeToTray
			// 
			this.Tasual_Settings_CheckBox_MinimizeToTray.AutoSize = true;
			this.Tasual_Settings_CheckBox_MinimizeToTray.Location = new System.Drawing.Point(12, 46);
			this.Tasual_Settings_CheckBox_MinimizeToTray.Name = "Tasual_Settings_CheckBox_MinimizeToTray";
			this.Tasual_Settings_CheckBox_MinimizeToTray.Size = new System.Drawing.Size(133, 17);
			this.Tasual_Settings_CheckBox_MinimizeToTray.TabIndex = 6;
			this.Tasual_Settings_CheckBox_MinimizeToTray.Text = "Minimize Tasual to tray";
			this.Tasual_Settings_CheckBox_MinimizeToTray.UseVisualStyleBackColor = true;
			// 
			// Tasual_Settings_CheckBox_PromptClear
			// 
			this.Tasual_Settings_CheckBox_PromptClear.AutoSize = true;
			this.Tasual_Settings_CheckBox_PromptClear.Location = new System.Drawing.Point(12, 123);
			this.Tasual_Settings_CheckBox_PromptClear.Name = "Tasual_Settings_CheckBox_PromptClear";
			this.Tasual_Settings_CheckBox_PromptClear.Size = new System.Drawing.Size(169, 17);
			this.Tasual_Settings_CheckBox_PromptClear.TabIndex = 7;
			this.Tasual_Settings_CheckBox_PromptClear.Text = "Prompt when clearing all tasks";
			this.Tasual_Settings_CheckBox_PromptClear.UseVisualStyleBackColor = true;
			// 
			// Tasual_Settings_CheckBox_PromptDelete
			// 
			this.Tasual_Settings_CheckBox_PromptDelete.AutoSize = true;
			this.Tasual_Settings_CheckBox_PromptDelete.Location = new System.Drawing.Point(12, 146);
			this.Tasual_Settings_CheckBox_PromptDelete.Name = "Tasual_Settings_CheckBox_PromptDelete";
			this.Tasual_Settings_CheckBox_PromptDelete.Size = new System.Drawing.Size(210, 17);
			this.Tasual_Settings_CheckBox_PromptDelete.TabIndex = 8;
			this.Tasual_Settings_CheckBox_PromptDelete.Text = "Prompt when deleting tasks individually";
			this.Tasual_Settings_CheckBox_PromptDelete.UseVisualStyleBackColor = true;
			// 
			// Tasual_Settings_GroupBox_Application
			// 
			this.Tasual_Settings_GroupBox_Application.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Tasual_Settings_GroupBox_Application.Controls.Add(this.Tasual_Settings_CheckBox_SaveWindowPos);
			this.Tasual_Settings_GroupBox_Application.Controls.Add(this.Tasual_Settings_CheckBox_EnterToSave);
			this.Tasual_Settings_GroupBox_Application.Controls.Add(this.Tasual_Settings_CheckBox_AlwaysOnTop);
			this.Tasual_Settings_GroupBox_Application.Controls.Add(this.Tasual_Settings_CheckBox_MinimizeToTray);
			this.Tasual_Settings_GroupBox_Application.Controls.Add(this.Tasual_Settings_CheckBox_PromptDelete);
			this.Tasual_Settings_GroupBox_Application.Controls.Add(this.Tasual_Settings_CheckBox_LaunchOnStartup);
			this.Tasual_Settings_GroupBox_Application.Controls.Add(this.Tasual_Settings_CheckBox_PromptClear);
			this.Tasual_Settings_GroupBox_Application.Location = new System.Drawing.Point(13, 13);
			this.Tasual_Settings_GroupBox_Application.Name = "Tasual_Settings_GroupBox_Application";
			this.Tasual_Settings_GroupBox_Application.Size = new System.Drawing.Size(391, 197);
			this.Tasual_Settings_GroupBox_Application.TabIndex = 9;
			this.Tasual_Settings_GroupBox_Application.TabStop = false;
			this.Tasual_Settings_GroupBox_Application.Text = "Application";
			// 
			// Tasual_Settings_CheckBox_EnterToSave
			// 
			this.Tasual_Settings_CheckBox_EnterToSave.AutoSize = true;
			this.Tasual_Settings_CheckBox_EnterToSave.Location = new System.Drawing.Point(12, 169);
			this.Tasual_Settings_CheckBox_EnterToSave.Name = "Tasual_Settings_CheckBox_EnterToSave";
			this.Tasual_Settings_CheckBox_EnterToSave.Size = new System.Drawing.Size(208, 17);
			this.Tasual_Settings_CheckBox_EnterToSave.TabIndex = 9;
			this.Tasual_Settings_CheckBox_EnterToSave.Text = "Press Enter to save while editing notes";
			this.Tasual_Settings_CheckBox_EnterToSave.UseVisualStyleBackColor = true;
			// 
			// Tasual_Settings_CheckBox_AlwaysOnTop
			// 
			this.Tasual_Settings_CheckBox_AlwaysOnTop.AutoSize = true;
			this.Tasual_Settings_CheckBox_AlwaysOnTop.Location = new System.Drawing.Point(12, 69);
			this.Tasual_Settings_CheckBox_AlwaysOnTop.Name = "Tasual_Settings_CheckBox_AlwaysOnTop";
			this.Tasual_Settings_CheckBox_AlwaysOnTop.Size = new System.Drawing.Size(154, 17);
			this.Tasual_Settings_CheckBox_AlwaysOnTop.TabIndex = 7;
			this.Tasual_Settings_CheckBox_AlwaysOnTop.Text = "Keep Tasual always on top";
			this.Tasual_Settings_CheckBox_AlwaysOnTop.UseVisualStyleBackColor = true;
			// 
			// Tasual_Settings_GroupBox_Display
			// 
			this.Tasual_Settings_GroupBox_Display.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Tasual_Settings_GroupBox_Display.Controls.Add(this.Tasual_Settings_CheckBox_ShowItemCounts);
			this.Tasual_Settings_GroupBox_Display.Controls.Add(this.Tasual_Settings_ListBox_EnabledColumns);
			this.Tasual_Settings_GroupBox_Display.Controls.Add(this.Tasual_Settings_Label_Columns);
			this.Tasual_Settings_GroupBox_Display.Controls.Add(this.Tasual_Settings_CheckBox_GroupTasks);
			this.Tasual_Settings_GroupBox_Display.Controls.Add(this.Tasual_Settings_ComboBox_GroupStyle);
			this.Tasual_Settings_GroupBox_Display.Controls.Add(this.Tasual_Settings_CheckBox_AlwaysShowCompletedGroup);
			this.Tasual_Settings_GroupBox_Display.Controls.Add(this.Tasual_Settings_CheckBox_AlwaysShowOverdueGroup);
			this.Tasual_Settings_GroupBox_Display.Controls.Add(this.Tasual_Settings_CheckBox_AlwaysShowTodayGroup);
			this.Tasual_Settings_GroupBox_Display.Location = new System.Drawing.Point(13, 216);
			this.Tasual_Settings_GroupBox_Display.Name = "Tasual_Settings_GroupBox_Display";
			this.Tasual_Settings_GroupBox_Display.Size = new System.Drawing.Size(391, 160);
			this.Tasual_Settings_GroupBox_Display.TabIndex = 10;
			this.Tasual_Settings_GroupBox_Display.TabStop = false;
			this.Tasual_Settings_GroupBox_Display.Text = "Display";
			// 
			// Tasual_Settings_CheckBox_ShowItemCounts
			// 
			this.Tasual_Settings_CheckBox_ShowItemCounts.AutoSize = true;
			this.Tasual_Settings_CheckBox_ShowItemCounts.Location = new System.Drawing.Point(26, 127);
			this.Tasual_Settings_CheckBox_ShowItemCounts.Name = "Tasual_Settings_CheckBox_ShowItemCounts";
			this.Tasual_Settings_CheckBox_ShowItemCounts.Size = new System.Drawing.Size(211, 17);
			this.Tasual_Settings_CheckBox_ShowItemCounts.TabIndex = 7;
			this.Tasual_Settings_CheckBox_ShowItemCounts.Text = "Show item counts next to group header";
			this.Tasual_Settings_CheckBox_ShowItemCounts.UseVisualStyleBackColor = true;
			// 
			// Tasual_Settings_ListBox_EnabledColumns
			// 
			this.Tasual_Settings_ListBox_EnabledColumns.FormattingEnabled = true;
			this.Tasual_Settings_ListBox_EnabledColumns.Items.AddRange(new object[] {
            "Notes",
            "Category",
            "Due",
            "Time"});
			this.Tasual_Settings_ListBox_EnabledColumns.Location = new System.Drawing.Point(292, 40);
			this.Tasual_Settings_ListBox_EnabledColumns.Name = "Tasual_Settings_ListBox_EnabledColumns";
			this.Tasual_Settings_ListBox_EnabledColumns.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.Tasual_Settings_ListBox_EnabledColumns.Size = new System.Drawing.Size(82, 95);
			this.Tasual_Settings_ListBox_EnabledColumns.TabIndex = 6;
			// 
			// Tasual_Settings_Label_Columns
			// 
			this.Tasual_Settings_Label_Columns.AutoSize = true;
			this.Tasual_Settings_Label_Columns.Location = new System.Drawing.Point(311, 22);
			this.Tasual_Settings_Label_Columns.Name = "Tasual_Settings_Label_Columns";
			this.Tasual_Settings_Label_Columns.Size = new System.Drawing.Size(47, 13);
			this.Tasual_Settings_Label_Columns.TabIndex = 5;
			this.Tasual_Settings_Label_Columns.Text = "Columns";
			// 
			// Tasual_Settings_Button_Save
			// 
			this.Tasual_Settings_Button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Tasual_Settings_Button_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Tasual_Settings_Button_Save.Location = new System.Drawing.Point(248, 382);
			this.Tasual_Settings_Button_Save.Name = "Tasual_Settings_Button_Save";
			this.Tasual_Settings_Button_Save.Size = new System.Drawing.Size(75, 23);
			this.Tasual_Settings_Button_Save.TabIndex = 11;
			this.Tasual_Settings_Button_Save.Text = "Save";
			this.Tasual_Settings_Button_Save.UseVisualStyleBackColor = true;
			this.Tasual_Settings_Button_Save.Click += new System.EventHandler(this.Tasual_Settings_Button_Save_Click);
			// 
			// Tasual_Settings_Button_Cancel
			// 
			this.Tasual_Settings_Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Tasual_Settings_Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Tasual_Settings_Button_Cancel.Location = new System.Drawing.Point(329, 382);
			this.Tasual_Settings_Button_Cancel.Name = "Tasual_Settings_Button_Cancel";
			this.Tasual_Settings_Button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.Tasual_Settings_Button_Cancel.TabIndex = 12;
			this.Tasual_Settings_Button_Cancel.Text = "Cancel";
			this.Tasual_Settings_Button_Cancel.UseVisualStyleBackColor = true;
			// 
			// Tasual_Settings_CheckBox_SaveWindowPos
			// 
			this.Tasual_Settings_CheckBox_SaveWindowPos.AutoSize = true;
			this.Tasual_Settings_CheckBox_SaveWindowPos.Location = new System.Drawing.Point(12, 92);
			this.Tasual_Settings_CheckBox_SaveWindowPos.Name = "Tasual_Settings_CheckBox_SaveWindowPos";
			this.Tasual_Settings_CheckBox_SaveWindowPos.Size = new System.Drawing.Size(155, 17);
			this.Tasual_Settings_CheckBox_SaveWindowPos.TabIndex = 10;
			this.Tasual_Settings_CheckBox_SaveWindowPos.Text = "Remember window position";
			this.Tasual_Settings_CheckBox_SaveWindowPos.UseVisualStyleBackColor = true;
			// 
			// Tasual_Settings
			// 
			this.AcceptButton = this.Tasual_Settings_Button_Save;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Tasual_Settings_Button_Cancel;
			this.ClientSize = new System.Drawing.Size(416, 417);
			this.Controls.Add(this.Tasual_Settings_Button_Cancel);
			this.Controls.Add(this.Tasual_Settings_Button_Save);
			this.Controls.Add(this.Tasual_Settings_GroupBox_Display);
			this.Controls.Add(this.Tasual_Settings_GroupBox_Application);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Tasual_Settings";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.Tasual_Settings_Load);
			this.Tasual_Settings_GroupBox_Application.ResumeLayout(false);
			this.Tasual_Settings_GroupBox_Application.PerformLayout();
			this.Tasual_Settings_GroupBox_Display.ResumeLayout(false);
			this.Tasual_Settings_GroupBox_Display.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.CheckBox Tasual_Settings_CheckBox_GroupTasks;
		private System.Windows.Forms.ComboBox Tasual_Settings_ComboBox_GroupStyle;
		private System.Windows.Forms.CheckBox Tasual_Settings_CheckBox_AlwaysShowCompletedGroup;
		private System.Windows.Forms.CheckBox Tasual_Settings_CheckBox_AlwaysShowOverdueGroup;
		private System.Windows.Forms.CheckBox Tasual_Settings_CheckBox_AlwaysShowTodayGroup;
		private System.Windows.Forms.CheckBox Tasual_Settings_CheckBox_LaunchOnStartup;
		private System.Windows.Forms.CheckBox Tasual_Settings_CheckBox_MinimizeToTray;
		private System.Windows.Forms.CheckBox Tasual_Settings_CheckBox_PromptClear;
		private System.Windows.Forms.CheckBox Tasual_Settings_CheckBox_PromptDelete;
		private System.Windows.Forms.GroupBox Tasual_Settings_GroupBox_Application;
		private System.Windows.Forms.CheckBox Tasual_Settings_CheckBox_AlwaysOnTop;
		private System.Windows.Forms.GroupBox Tasual_Settings_GroupBox_Display;
		private System.Windows.Forms.ListBox Tasual_Settings_ListBox_EnabledColumns;
		private System.Windows.Forms.Label Tasual_Settings_Label_Columns;
		private System.Windows.Forms.Button Tasual_Settings_Button_Save;
		private System.Windows.Forms.Button Tasual_Settings_Button_Cancel;
		private System.Windows.Forms.CheckBox Tasual_Settings_CheckBox_ShowItemCounts;
		private System.Windows.Forms.CheckBox Tasual_Settings_CheckBox_EnterToSave;
		private System.Windows.Forms.CheckBox Tasual_Settings_CheckBox_SaveWindowPos;
	}
}