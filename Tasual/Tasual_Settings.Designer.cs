namespace Tasual
{
	partial class Tasual_Settings
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
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.checkBox7 = new System.Windows.Forms.CheckBox();
			this.checkBox8 = new System.Windows.Forms.CheckBox();
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBox9 = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.Tasual_Settings_Button_Accept = new System.Windows.Forms.Button();
			this.Tasual_Settings_Button_Cancel = new System.Windows.Forms.Button();
			this.GroupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(12, 30);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(83, 17);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "Group tasks";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "by category",
            "by due time"});
			this.comboBox1.Location = new System.Drawing.Point(114, 28);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(157, 21);
			this.comboBox1.TabIndex = 1;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(26, 55);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(220, 17);
			this.checkBox2.TabIndex = 2;
			this.checkBox2.Text = "Split completed tasks into separate group";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new System.Drawing.Point(26, 79);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(210, 17);
			this.checkBox3.TabIndex = 3;
			this.checkBox3.Text = "Split overdue tasks into separate group";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// checkBox4
			// 
			this.checkBox4.AutoSize = true;
			this.checkBox4.Location = new System.Drawing.Point(26, 103);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(218, 17);
			this.checkBox4.TabIndex = 4;
			this.checkBox4.Text = "Split tasks due today into separate group";
			this.checkBox4.UseVisualStyleBackColor = true;
			// 
			// checkBox5
			// 
			this.checkBox5.AutoSize = true;
			this.checkBox5.Location = new System.Drawing.Point(12, 23);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(168, 17);
			this.checkBox5.TabIndex = 5;
			this.checkBox5.Text = "Start Tasual on system startup";
			this.checkBox5.UseVisualStyleBackColor = true;
			// 
			// checkBox6
			// 
			this.checkBox6.AutoSize = true;
			this.checkBox6.Location = new System.Drawing.Point(12, 46);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(133, 17);
			this.checkBox6.TabIndex = 6;
			this.checkBox6.Text = "Minimize Tasual to tray";
			this.checkBox6.UseVisualStyleBackColor = true;
			// 
			// checkBox7
			// 
			this.checkBox7.AutoSize = true;
			this.checkBox7.Location = new System.Drawing.Point(12, 100);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.Size = new System.Drawing.Size(169, 17);
			this.checkBox7.TabIndex = 7;
			this.checkBox7.Text = "Prompt when clearing all tasks";
			this.checkBox7.UseVisualStyleBackColor = true;
			// 
			// checkBox8
			// 
			this.checkBox8.AutoSize = true;
			this.checkBox8.Location = new System.Drawing.Point(12, 123);
			this.checkBox8.Name = "checkBox8";
			this.checkBox8.Size = new System.Drawing.Size(210, 17);
			this.checkBox8.TabIndex = 8;
			this.checkBox8.Text = "Prompt when deleting tasks individually";
			this.checkBox8.UseVisualStyleBackColor = true;
			// 
			// GroupBox1
			// 
			this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GroupBox1.Controls.Add(this.checkBox9);
			this.GroupBox1.Controls.Add(this.checkBox6);
			this.GroupBox1.Controls.Add(this.checkBox8);
			this.GroupBox1.Controls.Add(this.checkBox5);
			this.GroupBox1.Controls.Add(this.checkBox7);
			this.GroupBox1.Location = new System.Drawing.Point(13, 13);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(391, 151);
			this.GroupBox1.TabIndex = 9;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Application";
			// 
			// checkBox9
			// 
			this.checkBox9.AutoSize = true;
			this.checkBox9.Location = new System.Drawing.Point(12, 69);
			this.checkBox9.Name = "checkBox9";
			this.checkBox9.Size = new System.Drawing.Size(154, 17);
			this.checkBox9.TabIndex = 7;
			this.checkBox9.Text = "Keep Tasual always on top";
			this.checkBox9.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.listBox1);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.checkBox1);
			this.groupBox2.Controls.Add(this.comboBox1);
			this.groupBox2.Controls.Add(this.checkBox2);
			this.groupBox2.Controls.Add(this.checkBox3);
			this.groupBox2.Controls.Add(this.checkBox4);
			this.groupBox2.Location = new System.Drawing.Point(13, 170);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(391, 145);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Display";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(311, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Columns";
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Items.AddRange(new object[] {
            "Description",
            "Notes",
            "Category",
            "Due",
            "Time"});
			this.listBox1.Location = new System.Drawing.Point(292, 34);
			this.listBox1.Name = "listBox1";
			this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.listBox1.Size = new System.Drawing.Size(82, 95);
			this.listBox1.TabIndex = 6;
			// 
			// Tasual_Settings_Button_Accept
			// 
			this.Tasual_Settings_Button_Accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Tasual_Settings_Button_Accept.Location = new System.Drawing.Point(248, 321);
			this.Tasual_Settings_Button_Accept.Name = "Tasual_Settings_Button_Accept";
			this.Tasual_Settings_Button_Accept.Size = new System.Drawing.Size(75, 23);
			this.Tasual_Settings_Button_Accept.TabIndex = 11;
			this.Tasual_Settings_Button_Accept.Text = "Accept";
			this.Tasual_Settings_Button_Accept.UseVisualStyleBackColor = true;
			// 
			// Tasual_Settings_Button_Cancel
			// 
			this.Tasual_Settings_Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Tasual_Settings_Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Tasual_Settings_Button_Cancel.Location = new System.Drawing.Point(329, 321);
			this.Tasual_Settings_Button_Cancel.Name = "Tasual_Settings_Button_Cancel";
			this.Tasual_Settings_Button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.Tasual_Settings_Button_Cancel.TabIndex = 12;
			this.Tasual_Settings_Button_Cancel.Text = "Cancel";
			this.Tasual_Settings_Button_Cancel.UseVisualStyleBackColor = true;
			// 
			// Tasual_Settings
			// 
			this.AcceptButton = this.Tasual_Settings_Button_Accept;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Tasual_Settings_Button_Cancel;
			this.ClientSize = new System.Drawing.Size(416, 356);
			this.Controls.Add(this.Tasual_Settings_Button_Cancel);
			this.Controls.Add(this.Tasual_Settings_Button_Accept);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.GroupBox1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Tasual_Settings";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.CheckBox checkBox6;
		private System.Windows.Forms.CheckBox checkBox7;
		private System.Windows.Forms.CheckBox checkBox8;
		private System.Windows.Forms.GroupBox GroupBox1;
		private System.Windows.Forms.CheckBox checkBox9;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button Tasual_Settings_Button_Accept;
		private System.Windows.Forms.Button Tasual_Settings_Button_Cancel;
	}
}