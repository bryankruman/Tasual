namespace Tasual
{
	partial class Tasual_Confirm_Clear
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.Tasual_Confirm_Clear_Button_Confirm = new System.Windows.Forms.Button();
			this.Tasual_Confirm_Clear_Button_Cancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.Tasual_Confirm_Clear_Button_Confirm);
			this.panel1.Controls.Add(this.Tasual_Confirm_Clear_Button_Cancel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 109);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(328, 30);
			this.panel1.TabIndex = 3;
			// 
			// Tasual_Confirm_Clear_Button_Confirm
			// 
			this.Tasual_Confirm_Clear_Button_Confirm.Location = new System.Drawing.Point(166, 3);
			this.Tasual_Confirm_Clear_Button_Confirm.Name = "Tasual_Confirm_Clear_Button_Confirm";
			this.Tasual_Confirm_Clear_Button_Confirm.Size = new System.Drawing.Size(75, 23);
			this.Tasual_Confirm_Clear_Button_Confirm.TabIndex = 1;
			this.Tasual_Confirm_Clear_Button_Confirm.Text = "Confirm";
			this.Tasual_Confirm_Clear_Button_Confirm.UseVisualStyleBackColor = true;
			this.Tasual_Confirm_Clear_Button_Confirm.Click += new System.EventHandler(this.Tasual_Confirm_Clear_Button_Confirm_Click);
			// 
			// Tasual_Confirm_Clear_Button_Cancel
			// 
			this.Tasual_Confirm_Clear_Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Tasual_Confirm_Clear_Button_Cancel.Location = new System.Drawing.Point(247, 3);
			this.Tasual_Confirm_Clear_Button_Cancel.Name = "Tasual_Confirm_Clear_Button_Cancel";
			this.Tasual_Confirm_Clear_Button_Cancel.Size = new System.Drawing.Size(75, 23);
			this.Tasual_Confirm_Clear_Button_Cancel.TabIndex = 0;
			this.Tasual_Confirm_Clear_Button_Cancel.Text = "Cancel";
			this.Tasual_Confirm_Clear_Button_Cancel.Click += new System.EventHandler(this.Tasual_Confirm_Clear_Button_Cancel_Click);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.White;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(328, 106);
			this.label1.TabIndex = 2;
			this.label1.Text = "Are you sure you want to clear all tasks?";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(334, 142);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// Tasual_Confirm_Clear
			// 
			this.AcceptButton = this.Tasual_Confirm_Clear_Button_Cancel;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Tasual_Confirm_Clear_Button_Cancel;
			this.ClientSize = new System.Drawing.Size(334, 142);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Tasual_Confirm_Clear";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Tasual";
			this.panel1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button Tasual_Confirm_Clear_Button_Confirm;
		private System.Windows.Forms.Button Tasual_Confirm_Clear_Button_Cancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}