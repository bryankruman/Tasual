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
			this.CheckBox_GroupTasks = new System.Windows.Forms.CheckBox();
			this.ComboBox_GroupStyle = new System.Windows.Forms.ComboBox();
			this.CheckBox_AlwaysShowCompletedGroup = new System.Windows.Forms.CheckBox();
			this.CheckBox_AlwaysShowOverdueGroup = new System.Windows.Forms.CheckBox();
			this.CheckBox_AlwaysShowTodayGroup = new System.Windows.Forms.CheckBox();
			this.CheckBox_LaunchOnStartup = new System.Windows.Forms.CheckBox();
			this.CheckBox_MinimizeToTray = new System.Windows.Forms.CheckBox();
			this.CheckBox_PromptClear = new System.Windows.Forms.CheckBox();
			this.CheckBox_PromptDelete = new System.Windows.Forms.CheckBox();
			this.GroupBox_Application = new System.Windows.Forms.GroupBox();
			this.CheckBox_EnterToSave = new System.Windows.Forms.CheckBox();
			this.CheckBox_AlwaysOnTop = new System.Windows.Forms.CheckBox();
			this.GroupBox_Display = new System.Windows.Forms.GroupBox();
			this.CheckBox_ShowItemCounts = new System.Windows.Forms.CheckBox();
			this.ListBox_EnabledColumns = new System.Windows.Forms.ListBox();
			this.Label_Columns = new System.Windows.Forms.Label();
			this.Button_Save = new System.Windows.Forms.Button();
			this.Button_Cancel = new System.Windows.Forms.Button();
			this.CheckBox_SaveWindowPos = new System.Windows.Forms.CheckBox();
			this.GroupBox_Application.SuspendLayout();
			this.GroupBox_Display.SuspendLayout();
			this.SuspendLayout();
			// 
			// CheckBox_GroupTasks
			// 
			this.CheckBox_GroupTasks.AutoSize = true;
			this.CheckBox_GroupTasks.Location = new System.Drawing.Point(12, 30);
			this.CheckBox_GroupTasks.Name = "CheckBox_GroupTasks";
			this.CheckBox_GroupTasks.Size = new System.Drawing.Size(83, 17);
			this.CheckBox_GroupTasks.TabIndex = 0;
			this.CheckBox_GroupTasks.Text = "Group tasks";
			this.CheckBox_GroupTasks.UseVisualStyleBackColor = true;
			this.CheckBox_GroupTasks.CheckedChanged += new System.EventHandler(this.CheckBox_GroupTasks_CheckedChanged);
			// 
			// ComboBox_GroupStyle
			// 
			this.ComboBox_GroupStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ComboBox_GroupStyle.FormattingEnabled = true;
			this.ComboBox_GroupStyle.Items.AddRange(new object[] {
            "by category",
            "by due time"});
			this.ComboBox_GroupStyle.Location = new System.Drawing.Point(111, 27);
			this.ComboBox_GroupStyle.Name = "ComboBox_GroupStyle";
			this.ComboBox_GroupStyle.Size = new System.Drawing.Size(157, 21);
			this.ComboBox_GroupStyle.TabIndex = 1;
			this.ComboBox_GroupStyle.SelectedIndexChanged += new System.EventHandler(this.ComboBox_GroupStyle_SelectedIndexChanged);
			// 
			// CheckBox_AlwaysShowCompletedGroup
			// 
			this.CheckBox_AlwaysShowCompletedGroup.AutoSize = true;
			this.CheckBox_AlwaysShowCompletedGroup.Location = new System.Drawing.Point(26, 55);
			this.CheckBox_AlwaysShowCompletedGroup.Name = "CheckBox_AlwaysShowCompletedGroup";
			this.CheckBox_AlwaysShowCompletedGroup.Size = new System.Drawing.Size(220, 17);
			this.CheckBox_AlwaysShowCompletedGroup.TabIndex = 2;
			this.CheckBox_AlwaysShowCompletedGroup.Text = "Split completed tasks into separate group";
			this.CheckBox_AlwaysShowCompletedGroup.UseVisualStyleBackColor = true;
			// 
			// CheckBox_AlwaysShowOverdueGroup
			// 
			this.CheckBox_AlwaysShowOverdueGroup.AutoSize = true;
			this.CheckBox_AlwaysShowOverdueGroup.Location = new System.Drawing.Point(26, 79);
			this.CheckBox_AlwaysShowOverdueGroup.Name = "CheckBox_AlwaysShowOverdueGroup";
			this.CheckBox_AlwaysShowOverdueGroup.Size = new System.Drawing.Size(210, 17);
			this.CheckBox_AlwaysShowOverdueGroup.TabIndex = 3;
			this.CheckBox_AlwaysShowOverdueGroup.Text = "Split overdue tasks into separate group";
			this.CheckBox_AlwaysShowOverdueGroup.UseVisualStyleBackColor = true;
			// 
			// CheckBox_AlwaysShowTodayGroup
			// 
			this.CheckBox_AlwaysShowTodayGroup.AutoSize = true;
			this.CheckBox_AlwaysShowTodayGroup.Location = new System.Drawing.Point(26, 103);
			this.CheckBox_AlwaysShowTodayGroup.Name = "CheckBox_AlwaysShowTodayGroup";
			this.CheckBox_AlwaysShowTodayGroup.Size = new System.Drawing.Size(218, 17);
			this.CheckBox_AlwaysShowTodayGroup.TabIndex = 4;
			this.CheckBox_AlwaysShowTodayGroup.Text = "Split tasks due today into separate group";
			this.CheckBox_AlwaysShowTodayGroup.UseVisualStyleBackColor = true;
			// 
			// CheckBox_LaunchOnStartup
			// 
			this.CheckBox_LaunchOnStartup.AutoSize = true;
			this.CheckBox_LaunchOnStartup.Location = new System.Drawing.Point(12, 23);
			this.CheckBox_LaunchOnStartup.Name = "CheckBox_LaunchOnStartup";
			this.CheckBox_LaunchOnStartup.Size = new System.Drawing.Size(168, 17);
			this.CheckBox_LaunchOnStartup.TabIndex = 5;
			this.CheckBox_LaunchOnStartup.Text = "Start Tasual on system startup";
			this.CheckBox_LaunchOnStartup.UseVisualStyleBackColor = true;
			// 
			// CheckBox_MinimizeToTray
			// 
			this.CheckBox_MinimizeToTray.AutoSize = true;
			this.CheckBox_MinimizeToTray.Location = new System.Drawing.Point(12, 46);
			this.CheckBox_MinimizeToTray.Name = "CheckBox_MinimizeToTray";
			this.CheckBox_MinimizeToTray.Size = new System.Drawing.Size(133, 17);
			this.CheckBox_MinimizeToTray.TabIndex = 6;
			this.CheckBox_MinimizeToTray.Text = "Minimize Tasual to tray";
			this.CheckBox_MinimizeToTray.UseVisualStyleBackColor = true;
			// 
			// CheckBox_PromptClear
			// 
			this.CheckBox_PromptClear.AutoSize = true;
			this.CheckBox_PromptClear.Location = new System.Drawing.Point(12, 123);
			this.CheckBox_PromptClear.Name = "CheckBox_PromptClear";
			this.CheckBox_PromptClear.Size = new System.Drawing.Size(169, 17);
			this.CheckBox_PromptClear.TabIndex = 7;
			this.CheckBox_PromptClear.Text = "Prompt when clearing all tasks";
			this.CheckBox_PromptClear.UseVisualStyleBackColor = true;
			// 
			// CheckBox_PromptDelete
			// 
			this.CheckBox_PromptDelete.AutoSize = true;
			this.CheckBox_PromptDelete.Location = new System.Drawing.Point(12, 146);
			this.CheckBox_PromptDelete.Name = "CheckBox_PromptDelete";
			this.CheckBox_PromptDelete.Size = new System.Drawing.Size(210, 17);
			this.CheckBox_PromptDelete.TabIndex = 8;
			this.CheckBox_PromptDelete.Text = "Prompt when deleting tasks individually";
			this.CheckBox_PromptDelete.UseVisualStyleBackColor = true;
			// 
			// GroupBox_Application
			// 
			this.GroupBox_Application.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GroupBox_Application.Controls.Add(this.CheckBox_SaveWindowPos);
			this.GroupBox_Application.Controls.Add(this.CheckBox_EnterToSave);
			this.GroupBox_Application.Controls.Add(this.CheckBox_AlwaysOnTop);
			this.GroupBox_Application.Controls.Add(this.CheckBox_MinimizeToTray);
			this.GroupBox_Application.Controls.Add(this.CheckBox_PromptDelete);
			this.GroupBox_Application.Controls.Add(this.CheckBox_LaunchOnStartup);
			this.GroupBox_Application.Controls.Add(this.CheckBox_PromptClear);
			this.GroupBox_Application.Location = new System.Drawing.Point(13, 13);
			this.GroupBox_Application.Name = "GroupBox_Application";
			this.GroupBox_Application.Size = new System.Drawing.Size(391, 197);
			this.GroupBox_Application.TabIndex = 9;
			this.GroupBox_Application.TabStop = false;
			this.GroupBox_Application.Text = "Application";
			// 
			// CheckBox_EnterToSave
			// 
			this.CheckBox_EnterToSave.AutoSize = true;
			this.CheckBox_EnterToSave.Location = new System.Drawing.Point(12, 169);
			this.CheckBox_EnterToSave.Name = "CheckBox_EnterToSave";
			this.CheckBox_EnterToSave.Size = new System.Drawing.Size(208, 17);
			this.CheckBox_EnterToSave.TabIndex = 9;
			this.CheckBox_EnterToSave.Text = "Press Enter to save while editing notes";
			this.CheckBox_EnterToSave.UseVisualStyleBackColor = true;
			// 
			// CheckBox_AlwaysOnTop
			// 
			this.CheckBox_AlwaysOnTop.AutoSize = true;
			this.CheckBox_AlwaysOnTop.Location = new System.Drawing.Point(12, 69);
			this.CheckBox_AlwaysOnTop.Name = "CheckBox_AlwaysOnTop";
			this.CheckBox_AlwaysOnTop.Size = new System.Drawing.Size(154, 17);
			this.CheckBox_AlwaysOnTop.TabIndex = 7;
			this.CheckBox_AlwaysOnTop.Text = "Keep Tasual always on top";
			this.CheckBox_AlwaysOnTop.UseVisualStyleBackColor = true;
			// 
			// GroupBox_Display
			// 
			this.GroupBox_Display.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GroupBox_Display.Controls.Add(this.CheckBox_ShowItemCounts);
			this.GroupBox_Display.Controls.Add(this.ListBox_EnabledColumns);
			this.GroupBox_Display.Controls.Add(this.Label_Columns);
			this.GroupBox_Display.Controls.Add(this.CheckBox_GroupTasks);
			this.GroupBox_Display.Controls.Add(this.ComboBox_GroupStyle);
			this.GroupBox_Display.Controls.Add(this.CheckBox_AlwaysShowCompletedGroup);
			this.GroupBox_Display.Controls.Add(this.CheckBox_AlwaysShowOverdueGroup);
			this.GroupBox_Display.Controls.Add(this.CheckBox_AlwaysShowTodayGroup);
			this.GroupBox_Display.Location = new System.Drawing.Point(13, 216);
			this.GroupBox_Display.Name = "GroupBox_Display";
			this.GroupBox_Display.Size = new System.Drawing.Size(391, 160);
			this.GroupBox_Display.TabIndex = 10;
			this.GroupBox_Display.TabStop = false;
			this.GroupBox_Display.Text = "Display";
			// 
			// CheckBox_ShowItemCounts
			// 
			this.CheckBox_ShowItemCounts.AutoSize = true;
			this.CheckBox_ShowItemCounts.Location = new System.Drawing.Point(26, 127);
			this.CheckBox_ShowItemCounts.Name = "CheckBox_ShowItemCounts";
			this.CheckBox_ShowItemCounts.Size = new System.Drawing.Size(211, 17);
			this.CheckBox_ShowItemCounts.TabIndex = 7;
			this.CheckBox_ShowItemCounts.Text = "Show item counts next to group header";
			this.CheckBox_ShowItemCounts.UseVisualStyleBackColor = true;
			// 
			// ListBox_EnabledColumns
			// 
			this.ListBox_EnabledColumns.FormattingEnabled = true;
			this.ListBox_EnabledColumns.Items.AddRange(new object[] {
            "Notes",
            "Category",
            "Due",
            "Time"});
			this.ListBox_EnabledColumns.Location = new System.Drawing.Point(292, 40);
			this.ListBox_EnabledColumns.Name = "ListBox_EnabledColumns";
			this.ListBox_EnabledColumns.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.ListBox_EnabledColumns.Size = new System.Drawing.Size(82, 95);
			this.ListBox_EnabledColumns.TabIndex = 6;
			// 
			// Label_Columns
			// 
			this.Label_Columns.AutoSize = true;
			this.Label_Columns.Location = new System.Drawing.Point(311, 22);
			this.Label_Columns.Name = "Label_Columns";
			this.Label_Columns.Size = new System.Drawing.Size(47, 13);
			this.Label_Columns.TabIndex = 5;
			this.Label_Columns.Text = "Columns";
			// 
			// Button_Save
			// 
			this.Button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Button_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Button_Save.Location = new System.Drawing.Point(248, 382);
			this.Button_Save.Name = "Button_Save";
			this.Button_Save.Size = new System.Drawing.Size(75, 23);
			this.Button_Save.TabIndex = 11;
			this.Button_Save.Text = "Save";
			this.Button_Save.UseVisualStyleBackColor = true;
			this.Button_Save.Click += new System.EventHandler(this.Button_Save_Click);
			// 
			// Button_Cancel
			// 
			this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Button_Cancel.Location = new System.Drawing.Point(329, 382);
			this.Button_Cancel.Name = "Button_Cancel";
			this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.Button_Cancel.TabIndex = 12;
			this.Button_Cancel.Text = "Cancel";
			this.Button_Cancel.UseVisualStyleBackColor = true;
			// 
			// CheckBox_SaveWindowPos
			// 
			this.CheckBox_SaveWindowPos.AutoSize = true;
			this.CheckBox_SaveWindowPos.Location = new System.Drawing.Point(12, 92);
			this.CheckBox_SaveWindowPos.Name = "CheckBox_SaveWindowPos";
			this.CheckBox_SaveWindowPos.Size = new System.Drawing.Size(155, 17);
			this.CheckBox_SaveWindowPos.TabIndex = 10;
			this.CheckBox_SaveWindowPos.Text = "Remember window position";
			this.CheckBox_SaveWindowPos.UseVisualStyleBackColor = true;
			// 
			// Tasual_Settings
			// 
			this.AcceptButton = this.Button_Save;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Button_Cancel;
			this.ClientSize = new System.Drawing.Size(416, 417);
			this.Controls.Add(this.Button_Cancel);
			this.Controls.Add(this.Button_Save);
			this.Controls.Add(this.GroupBox_Display);
			this.Controls.Add(this.GroupBox_Application);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Tasual_Settings";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.FormLoad);
			this.GroupBox_Application.ResumeLayout(false);
			this.GroupBox_Application.PerformLayout();
			this.GroupBox_Display.ResumeLayout(false);
			this.GroupBox_Display.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.CheckBox CheckBox_GroupTasks;
		private System.Windows.Forms.ComboBox ComboBox_GroupStyle;
		private System.Windows.Forms.CheckBox CheckBox_AlwaysShowCompletedGroup;
		private System.Windows.Forms.CheckBox CheckBox_AlwaysShowOverdueGroup;
		private System.Windows.Forms.CheckBox CheckBox_AlwaysShowTodayGroup;
		private System.Windows.Forms.CheckBox CheckBox_LaunchOnStartup;
		private System.Windows.Forms.CheckBox CheckBox_MinimizeToTray;
		private System.Windows.Forms.CheckBox CheckBox_PromptClear;
		private System.Windows.Forms.CheckBox CheckBox_PromptDelete;
		private System.Windows.Forms.GroupBox GroupBox_Application;
		private System.Windows.Forms.CheckBox CheckBox_AlwaysOnTop;
		private System.Windows.Forms.GroupBox GroupBox_Display;
		private System.Windows.Forms.ListBox ListBox_EnabledColumns;
		private System.Windows.Forms.Label Label_Columns;
		private System.Windows.Forms.Button Button_Save;
		private System.Windows.Forms.Button Button_Cancel;
		private System.Windows.Forms.CheckBox CheckBox_ShowItemCounts;
		private System.Windows.Forms.CheckBox CheckBox_EnterToSave;
		private System.Windows.Forms.CheckBox CheckBox_SaveWindowPos;
	}
}