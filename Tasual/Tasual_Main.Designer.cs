﻿namespace Tasual
{
	partial class Tasual_Main
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Work", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Personal", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Misc", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "Fuck Bitches, Get Money",
            "Fly motherfucker"}, "Add.png");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "Boat to the island"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Transparent, null);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "Drive to the mall",
            "Sunday, Apr 12th - 9pm"}, -1);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "Jump to the sky",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
            "Run up the hill",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
            "Teleport to the store",
            "Sunday, Apr 13th - 8pm"}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tasual_Main));
            this.Tasual_MenuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_MenuStrip_Create_Quick = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_MenuStrip_Create_Advanced = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_MenuStrip_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_MenuStrip_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_MenuStrip_Settings_Sorting = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_MenuStrip_Settings_Sorting_Category = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_MenuStrip_Settings_Sorting_Time = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_MenuStrip_Settings_AlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_MenuStrip_Settings_MinimizeToTray = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_MenuStrip_Settings_LaunchOnStartup = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_ListView = new System.Windows.Forms.ListView();
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Tasual_TaskIcons = new System.Windows.Forms.ImageList(this.components);
            this.Tasual_Notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.Tasual_StatusLabel = new System.Windows.Forms.LinkLabel();
            this.Tasual_AboutLabel = new System.Windows.Forms.LinkLabel();
            this.Tasual_StatusLabel_MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Tasual_StatusLabel_MenuStrip_Print = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_StatusLabel_MenuStrip_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_StatusLabel_MenuStrip_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_Timer_ListViewClick = new System.Windows.Forms.Timer(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.Tasual_MenuStrip.SuspendLayout();
            this.Tasual_StatusLabel_MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // Tasual_MenuStrip
            // 
            this.Tasual_MenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
            this.Tasual_MenuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tasual_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.Tasual_MenuStrip_Edit,
            this.Tasual_MenuStrip_Settings});
            this.Tasual_MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.Tasual_MenuStrip.Name = "Tasual_MenuStrip";
            this.Tasual_MenuStrip.Size = new System.Drawing.Size(405, 24);
            this.Tasual_MenuStrip.TabIndex = 1;
            this.Tasual_MenuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tasual_MenuStrip_Create_Quick,
            this.Tasual_MenuStrip_Create_Advanced});
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.toolStripMenuItem2.Image = global::Tasual.Properties.Resources.Add;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(69, 20);
            this.toolStripMenuItem2.Text = "Create";
            // 
            // Tasual_MenuStrip_Create_Quick
            // 
            this.Tasual_MenuStrip_Create_Quick.Name = "Tasual_MenuStrip_Create_Quick";
            this.Tasual_MenuStrip_Create_Quick.Size = new System.Drawing.Size(127, 22);
            this.Tasual_MenuStrip_Create_Quick.Text = "Quick";
            this.Tasual_MenuStrip_Create_Quick.Click += new System.EventHandler(this.Tasual_MenuStrip_Create_Quick_Click);
            // 
            // Tasual_MenuStrip_Create_Advanced
            // 
            this.Tasual_MenuStrip_Create_Advanced.Name = "Tasual_MenuStrip_Create_Advanced";
            this.Tasual_MenuStrip_Create_Advanced.Size = new System.Drawing.Size(127, 22);
            this.Tasual_MenuStrip_Create_Advanced.Text = "Advanced";
            this.Tasual_MenuStrip_Create_Advanced.Click += new System.EventHandler(this.Tasual_MenuStrip_Create_Advanced_Click);
            // 
            // Tasual_MenuStrip_Edit
            // 
            this.Tasual_MenuStrip_Edit.BackColor = System.Drawing.Color.Transparent;
            this.Tasual_MenuStrip_Edit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.Tasual_MenuStrip_Edit.Image = global::Tasual.Properties.Resources.File_List;
            this.Tasual_MenuStrip_Edit.Name = "Tasual_MenuStrip_Edit";
            this.Tasual_MenuStrip_Edit.Size = new System.Drawing.Size(55, 20);
            this.Tasual_MenuStrip_Edit.Text = "Edit";
            this.Tasual_MenuStrip_Edit.Click += new System.EventHandler(this.Tasual_MenuStrip_Edit_Click);
            // 
            // Tasual_MenuStrip_Settings
            // 
            this.Tasual_MenuStrip_Settings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Tasual_MenuStrip_Settings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tasual_MenuStrip_Settings_Sorting,
            this.Tasual_MenuStrip_Settings_AlwaysOnTop,
            this.Tasual_MenuStrip_Settings_MinimizeToTray,
            this.Tasual_MenuStrip_Settings_LaunchOnStartup});
            this.Tasual_MenuStrip_Settings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.Tasual_MenuStrip_Settings.Image = global::Tasual.Properties.Resources.Gear;
            this.Tasual_MenuStrip_Settings.Name = "Tasual_MenuStrip_Settings";
            this.Tasual_MenuStrip_Settings.Size = new System.Drawing.Size(77, 20);
            this.Tasual_MenuStrip_Settings.Text = "Settings";
            // 
            // Tasual_MenuStrip_Settings_Sorting
            // 
            this.Tasual_MenuStrip_Settings_Sorting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tasual_MenuStrip_Settings_Sorting_Category,
            this.Tasual_MenuStrip_Settings_Sorting_Time});
            this.Tasual_MenuStrip_Settings_Sorting.Name = "Tasual_MenuStrip_Settings_Sorting";
            this.Tasual_MenuStrip_Settings_Sorting.Size = new System.Drawing.Size(170, 22);
            this.Tasual_MenuStrip_Settings_Sorting.Text = "Sorting";
            // 
            // Tasual_MenuStrip_Settings_Sorting_Category
            // 
            this.Tasual_MenuStrip_Settings_Sorting_Category.Checked = true;
            this.Tasual_MenuStrip_Settings_Sorting_Category.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Tasual_MenuStrip_Settings_Sorting_Category.Name = "Tasual_MenuStrip_Settings_Sorting_Category";
            this.Tasual_MenuStrip_Settings_Sorting_Category.Size = new System.Drawing.Size(162, 22);
            this.Tasual_MenuStrip_Settings_Sorting_Category.Text = "Sort by Category";
            // 
            // Tasual_MenuStrip_Settings_Sorting_Time
            // 
            this.Tasual_MenuStrip_Settings_Sorting_Time.Name = "Tasual_MenuStrip_Settings_Sorting_Time";
            this.Tasual_MenuStrip_Settings_Sorting_Time.Size = new System.Drawing.Size(162, 22);
            this.Tasual_MenuStrip_Settings_Sorting_Time.Text = "Sort by Time";
            // 
            // Tasual_MenuStrip_Settings_AlwaysOnTop
            // 
            this.Tasual_MenuStrip_Settings_AlwaysOnTop.Name = "Tasual_MenuStrip_Settings_AlwaysOnTop";
            this.Tasual_MenuStrip_Settings_AlwaysOnTop.Size = new System.Drawing.Size(170, 22);
            this.Tasual_MenuStrip_Settings_AlwaysOnTop.Text = "Always on top";
            this.Tasual_MenuStrip_Settings_AlwaysOnTop.Click += new System.EventHandler(this.Tasual_MenuStrip_Settings_AlwaysOnTop_Click);
            // 
            // Tasual_MenuStrip_Settings_MinimizeToTray
            // 
            this.Tasual_MenuStrip_Settings_MinimizeToTray.Name = "Tasual_MenuStrip_Settings_MinimizeToTray";
            this.Tasual_MenuStrip_Settings_MinimizeToTray.Size = new System.Drawing.Size(170, 22);
            this.Tasual_MenuStrip_Settings_MinimizeToTray.Text = "Minimize to tray";
            // 
            // Tasual_MenuStrip_Settings_LaunchOnStartup
            // 
            this.Tasual_MenuStrip_Settings_LaunchOnStartup.Name = "Tasual_MenuStrip_Settings_LaunchOnStartup";
            this.Tasual_MenuStrip_Settings_LaunchOnStartup.Size = new System.Drawing.Size(170, 22);
            this.Tasual_MenuStrip_Settings_LaunchOnStartup.Text = "Launch on startup";
            // 
            // Tasual_ListView
            // 
            this.Tasual_ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tasual_ListView.BackColor = System.Drawing.Color.White;
            this.Tasual_ListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tasual_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Description,
            this.Time});
            this.Tasual_ListView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tasual_ListView.FullRowSelect = true;
            listViewGroup4.Header = "Work";
            listViewGroup4.Name = "Work";
            listViewGroup5.Header = "Personal";
            listViewGroup5.Name = "Personal";
            listViewGroup6.Header = "Misc";
            listViewGroup6.Name = "Misc";
            this.Tasual_ListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup4,
            listViewGroup5,
            listViewGroup6});
            this.Tasual_ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewItem7.StateImageIndex = 0;
            listViewItem8.StateImageIndex = 0;
            listViewItem9.StateImageIndex = 0;
            listViewItem10.StateImageIndex = 0;
            listViewItem11.StateImageIndex = 0;
            listViewItem12.StateImageIndex = 0;
            this.Tasual_ListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12});
            this.Tasual_ListView.Location = new System.Drawing.Point(0, 24);
            this.Tasual_ListView.Margin = new System.Windows.Forms.Padding(0);
            this.Tasual_ListView.MultiSelect = false;
            this.Tasual_ListView.Name = "Tasual_ListView";
            this.Tasual_ListView.Size = new System.Drawing.Size(405, 369);
            this.Tasual_ListView.SmallImageList = this.Tasual_TaskIcons;
            this.Tasual_ListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.Tasual_ListView.TabIndex = 0;
            this.Tasual_ListView.UseCompatibleStateImageBehavior = false;
            this.Tasual_ListView.View = System.Windows.Forms.View.Details;
            this.Tasual_ListView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.Tasual_ListView_AfterLabelEdit);
            this.Tasual_ListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.Tasual_ListView_ColumnWidthChanging);
            this.Tasual_ListView.SelectedIndexChanged += new System.EventHandler(this.Tasual_ListView_SelectedIndexChanged);
            this.Tasual_ListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tasual_ListView_MouseDown);
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 272;
            // 
            // Time
            // 
            this.Time.Text = "Time";
            this.Time.Width = 133;
            // 
            // Tasual_TaskIcons
            // 
            this.Tasual_TaskIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Tasual_TaskIcons.ImageStream")));
            this.Tasual_TaskIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.Tasual_TaskIcons.Images.SetKeyName(0, "checked-s.png");
            this.Tasual_TaskIcons.Images.SetKeyName(1, "unchecked-s.png");
            // 
            // Tasual_Notify
            // 
            this.Tasual_Notify.Text = "Tasual_Notify";
            this.Tasual_Notify.Visible = true;
            // 
            // Tasual_StatusLabel
            // 
            this.Tasual_StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tasual_StatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tasual_StatusLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.Tasual_StatusLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.Tasual_StatusLabel.Location = new System.Drawing.Point(0, 393);
            this.Tasual_StatusLabel.Name = "Tasual_StatusLabel";
            this.Tasual_StatusLabel.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.Tasual_StatusLabel.Size = new System.Drawing.Size(336, 21);
            this.Tasual_StatusLabel.TabIndex = 2;
            this.Tasual_StatusLabel.TabStop = true;
            this.Tasual_StatusLabel.Text = "All tasks complete";
            this.Tasual_StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Tasual_StatusLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.Tasual_StatusLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Tasual_StatusLabel_LinkClicked);
            // 
            // Tasual_AboutLabel
            // 
            this.Tasual_AboutLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Tasual_AboutLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tasual_AboutLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.Tasual_AboutLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.Tasual_AboutLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.Tasual_AboutLabel.Location = new System.Drawing.Point(342, 393);
            this.Tasual_AboutLabel.Name = "Tasual_AboutLabel";
            this.Tasual_AboutLabel.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Tasual_AboutLabel.Size = new System.Drawing.Size(51, 21);
            this.Tasual_AboutLabel.TabIndex = 3;
            this.Tasual_AboutLabel.TabStop = true;
            this.Tasual_AboutLabel.Text = "About";
            this.Tasual_AboutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Tasual_AboutLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.Tasual_AboutLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Tasual_AboutLabel_LinkClicked);
            // 
            // Tasual_StatusLabel_MenuStrip
            // 
            this.Tasual_StatusLabel_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tasual_StatusLabel_MenuStrip_Print,
            this.Tasual_StatusLabel_MenuStrip_Export,
            this.Tasual_StatusLabel_MenuStrip_Clear});
            this.Tasual_StatusLabel_MenuStrip.Name = "Tasual_Main_Status_MenuStrip";
            this.Tasual_StatusLabel_MenuStrip.Size = new System.Drawing.Size(153, 92);
            // 
            // Tasual_StatusLabel_MenuStrip_Print
            // 
            this.Tasual_StatusLabel_MenuStrip_Print.Name = "Tasual_StatusLabel_MenuStrip_Print";
            this.Tasual_StatusLabel_MenuStrip_Print.Size = new System.Drawing.Size(152, 22);
            this.Tasual_StatusLabel_MenuStrip_Print.Text = "Print";
            // 
            // Tasual_StatusLabel_MenuStrip_Export
            // 
            this.Tasual_StatusLabel_MenuStrip_Export.Name = "Tasual_StatusLabel_MenuStrip_Export";
            this.Tasual_StatusLabel_MenuStrip_Export.Size = new System.Drawing.Size(152, 22);
            this.Tasual_StatusLabel_MenuStrip_Export.Text = "Export";
            // 
            // Tasual_StatusLabel_MenuStrip_Clear
            // 
            this.Tasual_StatusLabel_MenuStrip_Clear.Name = "Tasual_StatusLabel_MenuStrip_Clear";
            this.Tasual_StatusLabel_MenuStrip_Clear.Size = new System.Drawing.Size(152, 22);
            this.Tasual_StatusLabel_MenuStrip_Clear.Text = "Clear";
            this.Tasual_StatusLabel_MenuStrip_Clear.Click += new System.EventHandler(this.Tasual_StatusLabel_MenuStrip_Clear_Click);
            // 
            // Tasual_Timer_ListViewClick
            // 
            this.Tasual_Timer_ListViewClick.Interval = 1000;
            this.Tasual_Timer_ListViewClick.Tick += new System.EventHandler(this.Tasual_Timer_ListViewClick_Tick);
            // 
            // Tasual_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(405, 414);
            this.Controls.Add(this.Tasual_AboutLabel);
            this.Controls.Add(this.Tasual_StatusLabel);
            this.Controls.Add(this.Tasual_ListView);
            this.Controls.Add(this.Tasual_MenuStrip);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.Tasual_MenuStrip;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "Tasual_Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Tasual";
            this.Load += new System.EventHandler(this.Tasual_Main_Load);
            this.Resize += new System.EventHandler(this.Tasual_Main_Resize);
            this.Tasual_MenuStrip.ResumeLayout(false);
            this.Tasual_MenuStrip.PerformLayout();
            this.Tasual_StatusLabel_MenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView Tasual_ListView;
		private System.Windows.Forms.MenuStrip Tasual_MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Settings;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Create_Advanced;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Create_Quick;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Edit;
		private System.Windows.Forms.ColumnHeader Description;
		private System.Windows.Forms.ColumnHeader Time;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Settings_Sorting;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Settings_Sorting_Category;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Settings_Sorting_Time;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Settings_LaunchOnStartup;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Settings_MinimizeToTray;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Settings_AlwaysOnTop;
		private System.Windows.Forms.NotifyIcon Tasual_Notify;
		private System.Windows.Forms.LinkLabel Tasual_StatusLabel;
		private System.Windows.Forms.LinkLabel Tasual_AboutLabel;
		private System.Windows.Forms.ContextMenuStrip Tasual_StatusLabel_MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem Tasual_StatusLabel_MenuStrip_Print;
		private System.Windows.Forms.ToolStripMenuItem Tasual_StatusLabel_MenuStrip_Export;
		private System.Windows.Forms.ToolStripMenuItem Tasual_StatusLabel_MenuStrip_Clear;
        private System.Windows.Forms.ImageList Tasual_TaskIcons;
        private System.Windows.Forms.Timer Tasual_Timer_ListViewClick;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}

