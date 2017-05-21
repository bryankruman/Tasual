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
            this.Tasual_Notes_Accept = new System.Windows.Forms.Button();
            this.Tasual_Notes_WatermarkTextBox = new Tasual.WatermarkTextBox();
            this.SuspendLayout();
            // 
            // Tasual_Notes_Cancel
            // 
            this.Tasual_Notes_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Tasual_Notes_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Tasual_Notes_Cancel.Location = new System.Drawing.Point(274, 226);
            this.Tasual_Notes_Cancel.Name = "Tasual_Notes_Cancel";
            this.Tasual_Notes_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Tasual_Notes_Cancel.TabIndex = 1;
            this.Tasual_Notes_Cancel.Text = "Cancel";
            this.Tasual_Notes_Cancel.UseVisualStyleBackColor = true;
            // 
            // Tasual_Notes_Accept
            // 
            this.Tasual_Notes_Accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Tasual_Notes_Accept.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Tasual_Notes_Accept.Enabled = false;
            this.Tasual_Notes_Accept.Location = new System.Drawing.Point(193, 226);
            this.Tasual_Notes_Accept.Name = "Tasual_Notes_Accept";
            this.Tasual_Notes_Accept.Size = new System.Drawing.Size(75, 23);
            this.Tasual_Notes_Accept.TabIndex = 2;
            this.Tasual_Notes_Accept.Text = "Accept";
            this.Tasual_Notes_Accept.UseVisualStyleBackColor = true;
            // 
            // Tasual_Notes_WatermarkTextBox
            // 
            this.Tasual_Notes_WatermarkTextBox.AcceptsReturn = true;
            this.Tasual_Notes_WatermarkTextBox.AcceptsTab = true;
            this.Tasual_Notes_WatermarkTextBox.AllowDrop = true;
            this.Tasual_Notes_WatermarkTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tasual_Notes_WatermarkTextBox.ForeColor = System.Drawing.Color.Gray;
            this.Tasual_Notes_WatermarkTextBox.Location = new System.Drawing.Point(12, 12);
            this.Tasual_Notes_WatermarkTextBox.Multiline = true;
            this.Tasual_Notes_WatermarkTextBox.Name = "Tasual_Notes_WatermarkTextBox";
            this.Tasual_Notes_WatermarkTextBox.Size = new System.Drawing.Size(337, 208);
            this.Tasual_Notes_WatermarkTextBox.TabIndex = 0;
            this.Tasual_Notes_WatermarkTextBox.WatermarkActive = true;
            this.Tasual_Notes_WatermarkTextBox.WatermarkText = "Type here";
            // 
            // Tasual_Notes
            // 
            this.AcceptButton = this.Tasual_Notes_Accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Tasual_Notes_Cancel;
            this.ClientSize = new System.Drawing.Size(361, 261);
            this.Controls.Add(this.Tasual_Notes_Accept);
            this.Controls.Add(this.Tasual_Notes_Cancel);
            this.Controls.Add(this.Tasual_Notes_WatermarkTextBox);
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
        private System.Windows.Forms.Button Tasual_Notes_Accept;
        private WatermarkTextBox Tasual_Notes_WatermarkTextBox;
    }
}