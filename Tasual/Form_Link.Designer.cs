namespace Tasual
{
	partial class Form_Link
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
			this.Button_Cancel = new System.Windows.Forms.Button();
			this.Button_Save = new System.Windows.Forms.Button();
			this.Button_Follow = new System.Windows.Forms.Button();
			this.TextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// Button_Cancel
			// 
			this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Button_Cancel.Location = new System.Drawing.Point(291, 38);
			this.Button_Cancel.Name = "Button_Cancel";
			this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.Button_Cancel.TabIndex = 0;
			this.Button_Cancel.Text = "Cancel";
			this.Button_Cancel.UseVisualStyleBackColor = true;
			// 
			// Button_Save
			// 
			this.Button_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Button_Save.Location = new System.Drawing.Point(210, 38);
			this.Button_Save.Name = "Button_Save";
			this.Button_Save.Size = new System.Drawing.Size(75, 23);
			this.Button_Save.TabIndex = 1;
			this.Button_Save.Text = "Save";
			this.Button_Save.UseVisualStyleBackColor = true;
			this.Button_Save.Click += new System.EventHandler(this.Button_Save_Click);
			// 
			// Button_Follow
			// 
			this.Button_Follow.Enabled = false;
			this.Button_Follow.Location = new System.Drawing.Point(12, 38);
			this.Button_Follow.Name = "Button_Follow";
			this.Button_Follow.Size = new System.Drawing.Size(92, 23);
			this.Button_Follow.TabIndex = 2;
			this.Button_Follow.Text = "Follow Link";
			this.Button_Follow.UseVisualStyleBackColor = true;
			this.Button_Follow.Click += new System.EventHandler(this.Button_Follow_Click);
			// 
			// TextBox
			// 
			this.TextBox.Location = new System.Drawing.Point(12, 12);
			this.TextBox.Name = "TextBox";
			this.TextBox.Size = new System.Drawing.Size(354, 20);
			this.TextBox.TabIndex = 0;
			this.TextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
			// 
			// Tasual_Link
			// 
			this.AcceptButton = this.Button_Save;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Button_Cancel;
			this.ClientSize = new System.Drawing.Size(378, 73);
			this.Controls.Add(this.TextBox);
			this.Controls.Add(this.Button_Follow);
			this.Controls.Add(this.Button_Save);
			this.Controls.Add(this.Button_Cancel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Tasual_Link";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Link";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button Button_Cancel;
		private System.Windows.Forms.Button Button_Save;
		private System.Windows.Forms.Button Button_Follow;
		private System.Windows.Forms.TextBox TextBox;
	}
}