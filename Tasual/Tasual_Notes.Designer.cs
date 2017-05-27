namespace Tasual
{
	partial class Tasual_Notes
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
			this.Tasual_Notes_Cancel = new System.Windows.Forms.Button();
			this.Tasual_Notes_Save = new System.Windows.Forms.Button();
			this.Tasual_Notes_TextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// Tasual_Notes_Cancel
			// 
			this.Tasual_Notes_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Tasual_Notes_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Tasual_Notes_Cancel.Location = new System.Drawing.Point(274, 173);
			this.Tasual_Notes_Cancel.Name = "Tasual_Notes_Cancel";
			this.Tasual_Notes_Cancel.Size = new System.Drawing.Size(75, 23);
			this.Tasual_Notes_Cancel.TabIndex = 1;
			this.Tasual_Notes_Cancel.Text = "Cancel";
			this.Tasual_Notes_Cancel.UseVisualStyleBackColor = true;
			// 
			// Tasual_Notes_Save
			// 
			this.Tasual_Notes_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Tasual_Notes_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Tasual_Notes_Save.Location = new System.Drawing.Point(193, 173);
			this.Tasual_Notes_Save.Name = "Tasual_Notes_Save";
			this.Tasual_Notes_Save.Size = new System.Drawing.Size(75, 23);
			this.Tasual_Notes_Save.TabIndex = 2;
			this.Tasual_Notes_Save.Text = "Save";
			this.Tasual_Notes_Save.UseVisualStyleBackColor = true;
			this.Tasual_Notes_Save.Click += new System.EventHandler(this.Tasual_Notes_Save_Click);
			// 
			// Tasual_Notes_TextBox
			// 
			this.Tasual_Notes_TextBox.AcceptsReturn = true;
			this.Tasual_Notes_TextBox.AcceptsTab = true;
			this.Tasual_Notes_TextBox.AllowDrop = true;
			this.Tasual_Notes_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Tasual_Notes_TextBox.ForeColor = System.Drawing.Color.Black;
			this.Tasual_Notes_TextBox.Location = new System.Drawing.Point(12, 12);
			this.Tasual_Notes_TextBox.Multiline = true;
			this.Tasual_Notes_TextBox.Name = "Tasual_Notes_TextBox";
			this.Tasual_Notes_TextBox.Size = new System.Drawing.Size(337, 155);
			this.Tasual_Notes_TextBox.TabIndex = 0;
			this.Tasual_Notes_TextBox.Text = "Write notes here";
			// 
			// Tasual_Notes
			// 
			this.AcceptButton = this.Tasual_Notes_Save;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Tasual_Notes_Cancel;
			this.ClientSize = new System.Drawing.Size(361, 208);
			this.Controls.Add(this.Tasual_Notes_Save);
			this.Controls.Add(this.Tasual_Notes_Cancel);
			this.Controls.Add(this.Tasual_Notes_TextBox);
			this.Name = "Tasual_Notes";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Notes";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button Tasual_Notes_Cancel;
		private System.Windows.Forms.Button Tasual_Notes_Save;
		private System.Windows.Forms.TextBox Tasual_Notes_TextBox;
	}
}