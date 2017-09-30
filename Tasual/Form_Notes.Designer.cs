namespace Tasual
{
	partial class Form_Notes
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
			this.Cancel = new System.Windows.Forms.Button();
			this.Save = new System.Windows.Forms.Button();
			this.TextBox = new System.Windows.Forms.TextBox();
			this.CheckBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// Cancel
			// 
			this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(274, 173);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(75, 23);
			this.Cancel.TabIndex = 1;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			// 
			// Save
			// 
			this.Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Save.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Save.Location = new System.Drawing.Point(193, 173);
			this.Save.Name = "Save";
			this.Save.Size = new System.Drawing.Size(75, 23);
			this.Save.TabIndex = 2;
			this.Save.Text = "Save";
			this.Save.UseVisualStyleBackColor = true;
			this.Save.Click += new System.EventHandler(this.Save_Click);
			// 
			// TextBox
			// 
			this.TextBox.AcceptsTab = true;
			this.TextBox.AllowDrop = true;
			this.TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextBox.ForeColor = System.Drawing.Color.Black;
			this.TextBox.Location = new System.Drawing.Point(12, 12);
			this.TextBox.Multiline = true;
			this.TextBox.Name = "TextBox";
			this.TextBox.Size = new System.Drawing.Size(337, 155);
			this.TextBox.TabIndex = 0;
			this.TextBox.Text = "Write notes here";
			// 
			// CheckBox
			// 
			this.CheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CheckBox.AutoSize = true;
			this.CheckBox.Location = new System.Drawing.Point(12, 177);
			this.CheckBox.Name = "CheckBox";
			this.CheckBox.Size = new System.Drawing.Size(117, 17);
			this.CheckBox.TabIndex = 3;
			this.CheckBox.Text = "Press enter to save";
			this.CheckBox.UseVisualStyleBackColor = true;
			this.CheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
			// 
			// Form_Notes
			// 
			this.AcceptButton = this.Save;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Cancel;
			this.ClientSize = new System.Drawing.Size(361, 208);
			this.Controls.Add(this.CheckBox);
			this.Controls.Add(this.Save);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.TextBox);
			this.Name = "Form_Notes";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Notes";
			this.Load += new System.EventHandler(this.FormLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.Button Save;
		private System.Windows.Forms.TextBox TextBox;
		private System.Windows.Forms.CheckBox CheckBox;
	}
}