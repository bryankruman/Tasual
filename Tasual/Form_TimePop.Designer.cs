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
			this.Calendar = new System.Windows.Forms.MonthCalendar();
			this.Panel = new System.Windows.Forms.Panel();
			this.Button_Cancel = new System.Windows.Forms.Button();
			this.Button_Save = new System.Windows.Forms.Button();
			this.Label_CantEdit = new System.Windows.Forms.Label();
			this.LinkLabel = new System.Windows.Forms.LinkLabel();
			this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.RadioButton_Specific = new System.Windows.Forms.RadioButton();
			this.RadioButton_AllDay = new System.Windows.Forms.RadioButton();
			this.CheckBox = new System.Windows.Forms.CheckBox();
			this.Panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// Calendar
			// 
			this.Calendar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Calendar.Enabled = false;
			this.Calendar.Location = new System.Drawing.Point(5, 0);
			this.Calendar.Name = "Calendar";
			this.Calendar.TabIndex = 0;
			this.Calendar.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			// 
			// Panel
			// 
			this.Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
			this.Panel.Controls.Add(this.Button_Cancel);
			this.Panel.Controls.Add(this.Button_Save);
			this.Panel.Controls.Add(this.Label_CantEdit);
			this.Panel.Controls.Add(this.LinkLabel);
			this.Panel.Controls.Add(this.DateTimePicker);
			this.Panel.Controls.Add(this.RadioButton_Specific);
			this.Panel.Controls.Add(this.Calendar);
			this.Panel.Controls.Add(this.RadioButton_AllDay);
			this.Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.Panel.Location = new System.Drawing.Point(0, 26);
			this.Panel.Name = "Panel";
			this.Panel.Size = new System.Drawing.Size(237, 250);
			this.Panel.TabIndex = 2;
			// 
			// Button_Cancel
			// 
			this.Button_Cancel.Location = new System.Drawing.Point(122, 218);
			this.Button_Cancel.Name = "Button_Cancel";
			this.Button_Cancel.Size = new System.Drawing.Size(103, 23);
			this.Button_Cancel.TabIndex = 16;
			this.Button_Cancel.Text = "Cancel";
			this.Button_Cancel.UseVisualStyleBackColor = true;
			this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
			// 
			// Button_Save
			// 
			this.Button_Save.Location = new System.Drawing.Point(12, 218);
			this.Button_Save.Name = "Button_Save";
			this.Button_Save.Size = new System.Drawing.Size(103, 23);
			this.Button_Save.TabIndex = 15;
			this.Button_Save.Text = "Save";
			this.Button_Save.UseVisualStyleBackColor = true;
			this.Button_Save.Click += new System.EventHandler(this.Button_Save_Click);
			// 
			// Label_CantEdit
			// 
			this.Label_CantEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.Label_CantEdit.ForeColor = System.Drawing.Color.Silver;
			this.Label_CantEdit.Location = new System.Drawing.Point(51, 70);
			this.Label_CantEdit.Name = "Label_CantEdit";
			this.Label_CantEdit.Size = new System.Drawing.Size(137, 32);
			this.Label_CantEdit.TabIndex = 14;
			this.Label_CantEdit.Text = "Cannot edit date/time from here, use advanced edit";
			this.Label_CantEdit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LinkLabel
			// 
			this.LinkLabel.LinkColor = System.Drawing.Color.Black;
			this.LinkLabel.Location = new System.Drawing.Point(0, 192);
			this.LinkLabel.Name = "LinkLabel";
			this.LinkLabel.Padding = new System.Windows.Forms.Padding(2, 0, 0, 6);
			this.LinkLabel.Size = new System.Drawing.Size(237, 23);
			this.LinkLabel.TabIndex = 13;
			this.LinkLabel.TabStop = true;
			this.LinkLabel.Text = "Use advanced edit to change other attributes";
			this.LinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.LinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
			// 
			// DateTimePicker
			// 
			this.DateTimePicker.CustomFormat = "h:mm tt";
			this.DateTimePicker.Enabled = false;
			this.DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.DateTimePicker.Location = new System.Drawing.Point(123, 167);
			this.DateTimePicker.Name = "DateTimePicker";
			this.DateTimePicker.ShowUpDown = true;
			this.DateTimePicker.Size = new System.Drawing.Size(73, 20);
			this.DateTimePicker.TabIndex = 12;
			// 
			// RadioButton_Specific
			// 
			this.RadioButton_Specific.AutoSize = true;
			this.RadioButton_Specific.Location = new System.Drawing.Point(105, 170);
			this.RadioButton_Specific.Name = "RadioButton_Specific";
			this.RadioButton_Specific.Size = new System.Drawing.Size(14, 13);
			this.RadioButton_Specific.TabIndex = 1;
			this.RadioButton_Specific.TabStop = true;
			this.RadioButton_Specific.UseVisualStyleBackColor = true;
			this.RadioButton_Specific.CheckedChanged += new System.EventHandler(this.RadioButton_Specific_CheckedChanged);
			// 
			// RadioButton_AllDay
			// 
			this.RadioButton_AllDay.AutoSize = true;
			this.RadioButton_AllDay.ForeColor = System.Drawing.Color.Black;
			this.RadioButton_AllDay.Location = new System.Drawing.Point(40, 168);
			this.RadioButton_AllDay.Name = "RadioButton_AllDay";
			this.RadioButton_AllDay.Size = new System.Drawing.Size(56, 17);
			this.RadioButton_AllDay.TabIndex = 0;
			this.RadioButton_AllDay.TabStop = true;
			this.RadioButton_AllDay.Text = "All day";
			this.RadioButton_AllDay.UseVisualStyleBackColor = true;
			this.RadioButton_AllDay.CheckedChanged += new System.EventHandler(this.RadioButton_AllDay_CheckedChanged);
			// 
			// CheckBox
			// 
			this.CheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.CheckBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.CheckBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CheckBox.ForeColor = System.Drawing.Color.White;
			this.CheckBox.Location = new System.Drawing.Point(0, 0);
			this.CheckBox.Name = "CheckBox";
			this.CheckBox.Padding = new System.Windows.Forms.Padding(10, 4, 22, 0);
			this.CheckBox.Size = new System.Drawing.Size(237, 23);
			this.CheckBox.TabIndex = 1;
			this.CheckBox.Text = "Scheduled";
			this.CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.CheckBox.UseVisualStyleBackColor = false;
			this.CheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
			// 
			// Tasual_TimePop
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.ClientSize = new System.Drawing.Size(237, 276);
			this.ControlBox = false;
			this.Controls.Add(this.Panel);
			this.Controls.Add(this.CheckBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Tasual_TimePop";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Tasual_CalendarPopout";
			this.Deactivate += new System.EventHandler(this.Tasual_CalendarPopout_Deactivate);
			this.Panel.ResumeLayout(false);
			this.Panel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.MonthCalendar Calendar;
		private System.Windows.Forms.Panel Panel;
		private System.Windows.Forms.RadioButton RadioButton_Specific;
		private System.Windows.Forms.RadioButton RadioButton_AllDay;
		private System.Windows.Forms.DateTimePicker DateTimePicker;
		private System.Windows.Forms.CheckBox CheckBox;
		private System.Windows.Forms.LinkLabel LinkLabel;
		private System.Windows.Forms.Label Label_CantEdit;
		private System.Windows.Forms.Button Button_Cancel;
		private System.Windows.Forms.Button Button_Save;
	}
}