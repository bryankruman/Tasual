namespace Tasual
{
	partial class Tasual_Link
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
			this.Tasual_Link_Button_Cancel = new System.Windows.Forms.Button();
			this.Tasual_Link_Button_Save = new System.Windows.Forms.Button();
			this.Tasual_Link_Button_Follow = new System.Windows.Forms.Button();
			this.Tasual_Link_TextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// Tasual_Link_Button_Cancel
			// 
			this.Tasual_Link_Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Tasual_Link_Button_Cancel.Location = new System.Drawing.Point(291, 38);
			this.Tasual_Link_Button_Cancel.Name = "Tasual_Link_Button_Cancel";
			this.Tasual_Link_Button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.Tasual_Link_Button_Cancel.TabIndex = 0;
			this.Tasual_Link_Button_Cancel.Text = "Cancel";
			this.Tasual_Link_Button_Cancel.UseVisualStyleBackColor = true;
			// 
			// Tasual_Link_Button_Save
			// 
			this.Tasual_Link_Button_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Tasual_Link_Button_Save.Location = new System.Drawing.Point(210, 38);
			this.Tasual_Link_Button_Save.Name = "Tasual_Link_Button_Save";
			this.Tasual_Link_Button_Save.Size = new System.Drawing.Size(75, 23);
			this.Tasual_Link_Button_Save.TabIndex = 1;
			this.Tasual_Link_Button_Save.Text = "Save";
			this.Tasual_Link_Button_Save.UseVisualStyleBackColor = true;
			this.Tasual_Link_Button_Save.Click += new System.EventHandler(this.Tasual_Link_Button_Save_Click);
			// 
			// Tasual_Link_Button_Follow
			// 
			this.Tasual_Link_Button_Follow.Location = new System.Drawing.Point(12, 38);
			this.Tasual_Link_Button_Follow.Name = "Tasual_Link_Button_Follow";
			this.Tasual_Link_Button_Follow.Size = new System.Drawing.Size(92, 23);
			this.Tasual_Link_Button_Follow.TabIndex = 2;
			this.Tasual_Link_Button_Follow.Text = "Follow Link";
			this.Tasual_Link_Button_Follow.UseVisualStyleBackColor = true;
			this.Tasual_Link_Button_Follow.Click += new System.EventHandler(this.Tasual_Link_Button_Follow_Click);
			// 
			// Tasual_Link_TextBox
			// 
			this.Tasual_Link_TextBox.Location = new System.Drawing.Point(12, 12);
			this.Tasual_Link_TextBox.Name = "Tasual_Link_TextBox";
			this.Tasual_Link_TextBox.Size = new System.Drawing.Size(354, 20);
			this.Tasual_Link_TextBox.TabIndex = 3;
			this.Tasual_Link_TextBox.TextChanged += new System.EventHandler(this.Tasual_Link_TextBox_TextChanged);
			// 
			// Tasual_Link
			// 
			this.AcceptButton = this.Tasual_Link_Button_Save;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Tasual_Link_Button_Cancel;
			this.ClientSize = new System.Drawing.Size(378, 73);
			this.Controls.Add(this.Tasual_Link_TextBox);
			this.Controls.Add(this.Tasual_Link_Button_Follow);
			this.Controls.Add(this.Tasual_Link_Button_Save);
			this.Controls.Add(this.Tasual_Link_Button_Cancel);
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

		private System.Windows.Forms.Button Tasual_Link_Button_Cancel;
		private System.Windows.Forms.Button Tasual_Link_Button_Save;
		private System.Windows.Forms.Button Tasual_Link_Button_Follow;
		private System.Windows.Forms.TextBox Tasual_Link_TextBox;
	}
}