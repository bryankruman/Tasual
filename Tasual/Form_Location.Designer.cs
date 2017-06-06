namespace Tasual
{
	partial class Form_Location
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
			this.TextBox = new System.Windows.Forms.TextBox();
			this.Button_GoogleMaps = new System.Windows.Forms.Button();
			this.Button_Save = new System.Windows.Forms.Button();
			this.Button_Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TextBox
			// 
			this.TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextBox.Location = new System.Drawing.Point(12, 12);
			this.TextBox.Name = "TextBox";
			this.TextBox.Size = new System.Drawing.Size(354, 20);
			this.TextBox.TabIndex = 0;
			this.TextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
			// 
			// Button_GoogleMaps
			// 
			this.Button_GoogleMaps.Enabled = false;
			this.Button_GoogleMaps.Location = new System.Drawing.Point(12, 38);
			this.Button_GoogleMaps.Name = "Button_GoogleMaps";
			this.Button_GoogleMaps.Size = new System.Drawing.Size(99, 23);
			this.Button_GoogleMaps.TabIndex = 1;
			this.Button_GoogleMaps.Text = "Google Maps";
			this.Button_GoogleMaps.UseVisualStyleBackColor = true;
			this.Button_GoogleMaps.Click += new System.EventHandler(this.Button_GoogleMaps_Click);
			// 
			// Button_Save
			// 
			this.Button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Button_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Button_Save.Location = new System.Drawing.Point(210, 38);
			this.Button_Save.Name = "Button_Save";
			this.Button_Save.Size = new System.Drawing.Size(75, 23);
			this.Button_Save.TabIndex = 2;
			this.Button_Save.Text = "Save";
			this.Button_Save.UseVisualStyleBackColor = true;
			this.Button_Save.Click += new System.EventHandler(this.Button_Save_Click);
			// 
			// Button_Cancel
			// 
			this.Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Button_Cancel.Location = new System.Drawing.Point(291, 38);
			this.Button_Cancel.Name = "Button_Cancel";
			this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.Button_Cancel.TabIndex = 3;
			this.Button_Cancel.Text = "Cancel";
			this.Button_Cancel.UseVisualStyleBackColor = true;
			// 
			// Tasual_Location
			// 
			this.AcceptButton = this.Button_Save;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Button_Cancel;
			this.ClientSize = new System.Drawing.Size(378, 73);
			this.Controls.Add(this.Button_Cancel);
			this.Controls.Add(this.Button_Save);
			this.Controls.Add(this.Button_GoogleMaps);
			this.Controls.Add(this.TextBox);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Tasual_Location";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Location";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TextBox;
		private System.Windows.Forms.Button Button_GoogleMaps;
		private System.Windows.Forms.Button Button_Save;
		private System.Windows.Forms.Button Button_Cancel;
	}
}