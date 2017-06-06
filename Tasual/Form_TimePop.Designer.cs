namespace Tasual
{
	partial class Form_TimePop
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
			this.Tasual_TimePop_Calendar = new System.Windows.Forms.MonthCalendar();
			this.Tasual_TimePop_Panel = new System.Windows.Forms.Panel();
			this.Tasual_TimePop_Button_Cancel = new System.Windows.Forms.Button();
			this.Tasual_TimePop_Button_Save = new System.Windows.Forms.Button();
			this.Tasual_TimePop_Label_CantEdit = new System.Windows.Forms.Label();
			this.Tasual_TimePop_LinkLabel = new System.Windows.Forms.LinkLabel();
			this.Tasual_TimePop_DateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.Tasual_TimePop_RadioButton_Specific = new System.Windows.Forms.RadioButton();
			this.Tasual_TimePop_RadioButton_AllDay = new System.Windows.Forms.RadioButton();
			this.Tasual_TimePop_CheckBox = new System.Windows.Forms.CheckBox();
			this.Tasual_TimePop_Panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// Tasual_TimePop_Calendar
			// 
			this.Tasual_TimePop_Calendar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Tasual_TimePop_Calendar.Enabled = false;
			this.Tasual_TimePop_Calendar.Location = new System.Drawing.Point(5, 0);
			this.Tasual_TimePop_Calendar.Name = "Tasual_TimePop_Calendar";
			this.Tasual_TimePop_Calendar.TabIndex = 0;
			this.Tasual_TimePop_Calendar.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			// 
			// Tasual_TimePop_Panel
			// 
			this.Tasual_TimePop_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
			this.Tasual_TimePop_Panel.Controls.Add(this.Tasual_TimePop_Button_Cancel);
			this.Tasual_TimePop_Panel.Controls.Add(this.Tasual_TimePop_Button_Save);
			this.Tasual_TimePop_Panel.Controls.Add(this.Tasual_TimePop_Label_CantEdit);
			this.Tasual_TimePop_Panel.Controls.Add(this.Tasual_TimePop_LinkLabel);
			this.Tasual_TimePop_Panel.Controls.Add(this.Tasual_TimePop_DateTimePicker);
			this.Tasual_TimePop_Panel.Controls.Add(this.Tasual_TimePop_RadioButton_Specific);
			this.Tasual_TimePop_Panel.Controls.Add(this.Tasual_TimePop_Calendar);
			this.Tasual_TimePop_Panel.Controls.Add(this.Tasual_TimePop_RadioButton_AllDay);
			this.Tasual_TimePop_Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.Tasual_TimePop_Panel.Location = new System.Drawing.Point(0, 26);
			this.Tasual_TimePop_Panel.Name = "Tasual_TimePop_Panel";
			this.Tasual_TimePop_Panel.Size = new System.Drawing.Size(237, 250);
			this.Tasual_TimePop_Panel.TabIndex = 2;
			// 
			// Tasual_TimePop_Button_Cancel
			// 
			this.Tasual_TimePop_Button_Cancel.Location = new System.Drawing.Point(122, 218);
			this.Tasual_TimePop_Button_Cancel.Name = "Tasual_TimePop_Button_Cancel";
			this.Tasual_TimePop_Button_Cancel.Size = new System.Drawing.Size(103, 23);
			this.Tasual_TimePop_Button_Cancel.TabIndex = 16;
			this.Tasual_TimePop_Button_Cancel.Text = "Cancel";
			this.Tasual_TimePop_Button_Cancel.UseVisualStyleBackColor = true;
			this.Tasual_TimePop_Button_Cancel.Click += new System.EventHandler(this.Tasual_TimePop_Button_Cancel_Click);
			// 
			// Tasual_TimePop_Button_Save
			// 
			this.Tasual_TimePop_Button_Save.Location = new System.Drawing.Point(12, 218);
			this.Tasual_TimePop_Button_Save.Name = "Tasual_TimePop_Button_Save";
			this.Tasual_TimePop_Button_Save.Size = new System.Drawing.Size(103, 23);
			this.Tasual_TimePop_Button_Save.TabIndex = 15;
			this.Tasual_TimePop_Button_Save.Text = "Save";
			this.Tasual_TimePop_Button_Save.UseVisualStyleBackColor = true;
			this.Tasual_TimePop_Button_Save.Click += new System.EventHandler(this.Tasual_TimePop_Button_Save_Click);
			// 
			// Tasual_TimePop_Label_CantEdit
			// 
			this.Tasual_TimePop_Label_CantEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.Tasual_TimePop_Label_CantEdit.ForeColor = System.Drawing.Color.Silver;
			this.Tasual_TimePop_Label_CantEdit.Location = new System.Drawing.Point(51, 70);
			this.Tasual_TimePop_Label_CantEdit.Name = "Tasual_TimePop_Label_CantEdit";
			this.Tasual_TimePop_Label_CantEdit.Size = new System.Drawing.Size(137, 32);
			this.Tasual_TimePop_Label_CantEdit.TabIndex = 14;
			this.Tasual_TimePop_Label_CantEdit.Text = "Cannot edit date/time from here, use advanced edit";
			this.Tasual_TimePop_Label_CantEdit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Tasual_TimePop_LinkLabel
			// 
			this.Tasual_TimePop_LinkLabel.LinkColor = System.Drawing.Color.Black;
			this.Tasual_TimePop_LinkLabel.Location = new System.Drawing.Point(0, 192);
			this.Tasual_TimePop_LinkLabel.Name = "Tasual_TimePop_LinkLabel";
			this.Tasual_TimePop_LinkLabel.Padding = new System.Windows.Forms.Padding(2, 0, 0, 6);
			this.Tasual_TimePop_LinkLabel.Size = new System.Drawing.Size(237, 23);
			this.Tasual_TimePop_LinkLabel.TabIndex = 13;
			this.Tasual_TimePop_LinkLabel.TabStop = true;
			this.Tasual_TimePop_LinkLabel.Text = "Use advanced edit to change other attributes";
			this.Tasual_TimePop_LinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Tasual_TimePop_LinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Tasual_TimePop_LinkLabel_LinkClicked);
			// 
			// Tasual_TimePop_DateTimePicker
			// 
			this.Tasual_TimePop_DateTimePicker.CustomFormat = "h:mm tt";
			this.Tasual_TimePop_DateTimePicker.Enabled = false;
			this.Tasual_TimePop_DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.Tasual_TimePop_DateTimePicker.Location = new System.Drawing.Point(123, 167);
			this.Tasual_TimePop_DateTimePicker.Name = "Tasual_TimePop_DateTimePicker";
			this.Tasual_TimePop_DateTimePicker.ShowUpDown = true;
			this.Tasual_TimePop_DateTimePicker.Size = new System.Drawing.Size(73, 20);
			this.Tasual_TimePop_DateTimePicker.TabIndex = 12;
			// 
			// Tasual_TimePop_RadioButton_Specific
			// 
			this.Tasual_TimePop_RadioButton_Specific.AutoSize = true;
			this.Tasual_TimePop_RadioButton_Specific.Location = new System.Drawing.Point(105, 170);
			this.Tasual_TimePop_RadioButton_Specific.Name = "Tasual_TimePop_RadioButton_Specific";
			this.Tasual_TimePop_RadioButton_Specific.Size = new System.Drawing.Size(14, 13);
			this.Tasual_TimePop_RadioButton_Specific.TabIndex = 1;
			this.Tasual_TimePop_RadioButton_Specific.TabStop = true;
			this.Tasual_TimePop_RadioButton_Specific.UseVisualStyleBackColor = true;
			this.Tasual_TimePop_RadioButton_Specific.CheckedChanged += new System.EventHandler(this.Tasual_TimePop_RadioButton_Specific_CheckedChanged);
			// 
			// Tasual_TimePop_RadioButton_AllDay
			// 
			this.Tasual_TimePop_RadioButton_AllDay.AutoSize = true;
			this.Tasual_TimePop_RadioButton_AllDay.ForeColor = System.Drawing.Color.Black;
			this.Tasual_TimePop_RadioButton_AllDay.Location = new System.Drawing.Point(40, 168);
			this.Tasual_TimePop_RadioButton_AllDay.Name = "Tasual_TimePop_RadioButton_AllDay";
			this.Tasual_TimePop_RadioButton_AllDay.Size = new System.Drawing.Size(56, 17);
			this.Tasual_TimePop_RadioButton_AllDay.TabIndex = 0;
			this.Tasual_TimePop_RadioButton_AllDay.TabStop = true;
			this.Tasual_TimePop_RadioButton_AllDay.Text = "All day";
			this.Tasual_TimePop_RadioButton_AllDay.UseVisualStyleBackColor = true;
			this.Tasual_TimePop_RadioButton_AllDay.CheckedChanged += new System.EventHandler(this.Tasual_TimePop_RadioButton_AllDay_CheckedChanged);
			// 
			// Tasual_TimePop_CheckBox
			// 
			this.Tasual_TimePop_CheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.Tasual_TimePop_CheckBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.Tasual_TimePop_CheckBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Tasual_TimePop_CheckBox.ForeColor = System.Drawing.Color.White;
			this.Tasual_TimePop_CheckBox.Location = new System.Drawing.Point(0, 0);
			this.Tasual_TimePop_CheckBox.Name = "Tasual_TimePop_CheckBox";
			this.Tasual_TimePop_CheckBox.Padding = new System.Windows.Forms.Padding(10, 4, 22, 0);
			this.Tasual_TimePop_CheckBox.Size = new System.Drawing.Size(237, 23);
			this.Tasual_TimePop_CheckBox.TabIndex = 1;
			this.Tasual_TimePop_CheckBox.Text = "Scheduled";
			this.Tasual_TimePop_CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Tasual_TimePop_CheckBox.UseVisualStyleBackColor = false;
			this.Tasual_TimePop_CheckBox.CheckedChanged += new System.EventHandler(this.Tasual_TimePop_CheckBox_CheckedChanged);
			// 
			// Tasual_TimePop
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.ClientSize = new System.Drawing.Size(237, 276);
			this.ControlBox = false;
			this.Controls.Add(this.Tasual_TimePop_Panel);
			this.Controls.Add(this.Tasual_TimePop_CheckBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Tasual_TimePop";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Tasual_CalendarPopout";
			this.Deactivate += new System.EventHandler(this.Tasual_CalendarPopout_Deactivate);
			this.Tasual_TimePop_Panel.ResumeLayout(false);
			this.Tasual_TimePop_Panel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.MonthCalendar Tasual_TimePop_Calendar;
		private System.Windows.Forms.Panel Tasual_TimePop_Panel;
		private System.Windows.Forms.RadioButton Tasual_TimePop_RadioButton_Specific;
		private System.Windows.Forms.RadioButton Tasual_TimePop_RadioButton_AllDay;
		private System.Windows.Forms.DateTimePicker Tasual_TimePop_DateTimePicker;
		private System.Windows.Forms.CheckBox Tasual_TimePop_CheckBox;
		private System.Windows.Forms.LinkLabel Tasual_TimePop_LinkLabel;
		private System.Windows.Forms.Label Tasual_TimePop_Label_CantEdit;
		private System.Windows.Forms.Button Tasual_TimePop_Button_Cancel;
		private System.Windows.Forms.Button Tasual_TimePop_Button_Save;
	}
}