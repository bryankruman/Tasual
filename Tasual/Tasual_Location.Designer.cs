namespace Tasual
{
	partial class Tasual_Location
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
			this.Tasual_Location_TextBox = new System.Windows.Forms.TextBox();
			this.Tasual_Location_Button_GoogleMaps = new System.Windows.Forms.Button();
			this.Tasual_Location_Button_Save = new System.Windows.Forms.Button();
			this.Tasual_Location_Button_Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Tasual_Location_TextBox
			// 
			this.Tasual_Location_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Tasual_Location_TextBox.Location = new System.Drawing.Point(13, 13);
			this.Tasual_Location_TextBox.Name = "Tasual_Location_TextBox";
			this.Tasual_Location_TextBox.Size = new System.Drawing.Size(353, 20);
			this.Tasual_Location_TextBox.TabIndex = 0;
			this.Tasual_Location_TextBox.TextChanged += new System.EventHandler(this.Tasual_Location_TextBox_TextChanged);
			// 
			// Tasual_Location_Button_GoogleMaps
			// 
			this.Tasual_Location_Button_GoogleMaps.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Tasual_Location_Button_GoogleMaps.Enabled = false;
			this.Tasual_Location_Button_GoogleMaps.Location = new System.Drawing.Point(13, 39);
			this.Tasual_Location_Button_GoogleMaps.Name = "Tasual_Location_Button_GoogleMaps";
			this.Tasual_Location_Button_GoogleMaps.Size = new System.Drawing.Size(99, 23);
			this.Tasual_Location_Button_GoogleMaps.TabIndex = 1;
			this.Tasual_Location_Button_GoogleMaps.Text = "Google Maps";
			this.Tasual_Location_Button_GoogleMaps.UseVisualStyleBackColor = true;
			// 
			// Tasual_Location_Button_Save
			// 
			this.Tasual_Location_Button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Tasual_Location_Button_Save.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Tasual_Location_Button_Save.Location = new System.Drawing.Point(210, 39);
			this.Tasual_Location_Button_Save.Name = "Tasual_Location_Button_Save";
			this.Tasual_Location_Button_Save.Size = new System.Drawing.Size(75, 23);
			this.Tasual_Location_Button_Save.TabIndex = 2;
			this.Tasual_Location_Button_Save.Text = "Save";
			this.Tasual_Location_Button_Save.UseVisualStyleBackColor = true;
			this.Tasual_Location_Button_Save.Click += new System.EventHandler(this.Tasual_Location_Button_Save_Click);
			// 
			// Tasual_Location_Button_Cancel
			// 
			this.Tasual_Location_Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Tasual_Location_Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Tasual_Location_Button_Cancel.Location = new System.Drawing.Point(291, 39);
			this.Tasual_Location_Button_Cancel.Name = "Tasual_Location_Button_Cancel";
			this.Tasual_Location_Button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.Tasual_Location_Button_Cancel.TabIndex = 3;
			this.Tasual_Location_Button_Cancel.Text = "Cancel";
			this.Tasual_Location_Button_Cancel.UseVisualStyleBackColor = true;
			// 
			// Tasual_Location
			// 
			this.AcceptButton = this.Tasual_Location_Button_Save;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Tasual_Location_Button_Cancel;
			this.ClientSize = new System.Drawing.Size(378, 73);
			this.Controls.Add(this.Tasual_Location_Button_Cancel);
			this.Controls.Add(this.Tasual_Location_Button_Save);
			this.Controls.Add(this.Tasual_Location_Button_GoogleMaps);
			this.Controls.Add(this.Tasual_Location_TextBox);
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

		private System.Windows.Forms.TextBox Tasual_Location_TextBox;
		private System.Windows.Forms.Button Tasual_Location_Button_GoogleMaps;
		private System.Windows.Forms.Button Tasual_Location_Button_Save;
		private System.Windows.Forms.Button Tasual_Location_Button_Cancel;
	}
}