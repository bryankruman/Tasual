namespace Tasual
{
	partial class Tasual_Popout_Calendar
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
			this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
			this.panel1 = new System.Windows.Forms.Panel();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.Tasual_Create_DateTimePicker_StartTime = new System.Windows.Forms.DateTimePicker();
			this.Tasual_Popout_CheckBox_Scheduled = new System.Windows.Forms.CheckBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// monthCalendar1
			// 
			this.monthCalendar1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.monthCalendar1.Enabled = false;
			this.monthCalendar1.Location = new System.Drawing.Point(5, 0);
			this.monthCalendar1.Name = "monthCalendar1";
			this.monthCalendar1.TabIndex = 0;
			this.monthCalendar1.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.panel1.Controls.Add(this.linkLabel1);
			this.panel1.Controls.Add(this.Tasual_Create_DateTimePicker_StartTime);
			this.panel1.Controls.Add(this.radioButton2);
			this.panel1.Controls.Add(this.monthCalendar1);
			this.panel1.Controls.Add(this.radioButton1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 26);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(237, 213);
			this.panel1.TabIndex = 2;
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.ForeColor = System.Drawing.Color.White;
			this.radioButton1.Location = new System.Drawing.Point(40, 168);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(56, 17);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "All day";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(105, 170);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(14, 13);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.TabStop = true;
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// Tasual_Create_DateTimePicker_StartTime
			// 
			this.Tasual_Create_DateTimePicker_StartTime.CustomFormat = "h:mm tt";
			this.Tasual_Create_DateTimePicker_StartTime.Enabled = false;
			this.Tasual_Create_DateTimePicker_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.Tasual_Create_DateTimePicker_StartTime.Location = new System.Drawing.Point(123, 167);
			this.Tasual_Create_DateTimePicker_StartTime.Name = "Tasual_Create_DateTimePicker_StartTime";
			this.Tasual_Create_DateTimePicker_StartTime.ShowUpDown = true;
			this.Tasual_Create_DateTimePicker_StartTime.Size = new System.Drawing.Size(73, 20);
			this.Tasual_Create_DateTimePicker_StartTime.TabIndex = 12;
			// 
			// Tasual_Popout_CheckBox_Scheduled
			// 
			this.Tasual_Popout_CheckBox_Scheduled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.Tasual_Popout_CheckBox_Scheduled.Dock = System.Windows.Forms.DockStyle.Top;
			this.Tasual_Popout_CheckBox_Scheduled.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Tasual_Popout_CheckBox_Scheduled.ForeColor = System.Drawing.Color.White;
			this.Tasual_Popout_CheckBox_Scheduled.Location = new System.Drawing.Point(0, 0);
			this.Tasual_Popout_CheckBox_Scheduled.Name = "Tasual_Popout_CheckBox_Scheduled";
			this.Tasual_Popout_CheckBox_Scheduled.Padding = new System.Windows.Forms.Padding(10, 4, 22, 0);
			this.Tasual_Popout_CheckBox_Scheduled.Size = new System.Drawing.Size(237, 23);
			this.Tasual_Popout_CheckBox_Scheduled.TabIndex = 1;
			this.Tasual_Popout_CheckBox_Scheduled.Text = "Scheduled";
			this.Tasual_Popout_CheckBox_Scheduled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.Tasual_Popout_CheckBox_Scheduled.UseVisualStyleBackColor = false;
			// 
			// linkLabel1
			// 
			this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
			this.linkLabel1.Location = new System.Drawing.Point(0, 187);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Padding = new System.Windows.Forms.Padding(2, 0, 0, 6);
			this.linkLabel1.Size = new System.Drawing.Size(237, 26);
			this.linkLabel1.TabIndex = 13;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Use advanced edit to change other attributes";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Tasual_Popout_Calendar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.ClientSize = new System.Drawing.Size(237, 239);
			this.ControlBox = false;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.Tasual_Popout_CheckBox_Scheduled);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Tasual_Popout_Calendar";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Tasual_CalendarPopout";
			this.Deactivate += new System.EventHandler(this.Tasual_CalendarPopout_Deactivate);
			this.Leave += new System.EventHandler(this.Tasual_CalendarPopout_Leave);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.MonthCalendar monthCalendar1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.DateTimePicker Tasual_Create_DateTimePicker_StartTime;
		private System.Windows.Forms.CheckBox Tasual_Popout_CheckBox_Scheduled;
		private System.Windows.Forms.LinkLabel linkLabel1;
	}
}