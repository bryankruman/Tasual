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
            "Fly motherfucker"}, -1);
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
            this.Tasual_Main_MenuStrip = new System.Windows.Forms.MenuStrip();
            this.Button_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.taskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortByCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortByTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keepOnTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeToTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchOnStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_ListView = new System.Windows.Forms.ListView();
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Tasual_Notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.Tasual_Main_Status_MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_Main_MenuStrip.SuspendLayout();
            this.Tasual_Main_Status_MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tasual_Main_MenuStrip
            // 
            this.Tasual_Main_MenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
            this.Tasual_Main_MenuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tasual_Main_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Button_Exit,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.settingsToolStripMenuItem});
            this.Tasual_Main_MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.Tasual_Main_MenuStrip.Name = "Tasual_Main_MenuStrip";
            this.Tasual_Main_MenuStrip.Size = new System.Drawing.Size(405, 24);
            this.Tasual_Main_MenuStrip.TabIndex = 1;
            this.Tasual_Main_MenuStrip.Text = "menuStrip1";
            this.Tasual_Main_MenuStrip.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tasual_Main_MouseDown);
            // 
            // Button_Exit
            // 
            this.Button_Exit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Button_Exit.BackColor = System.Drawing.Color.Transparent;
            this.Button_Exit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.Button_Exit.Image = global::Tasual.Properties.Resources.Error_Symbol;
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(53, 20);
            this.Button_Exit.Text = "Exit";
            this.Button_Exit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button_Exit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.Button_Exit.Visible = false;
            this.Button_Exit.Click += new System.EventHandler(this.Button_Exit_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taskToolStripMenuItem,
            this.categoryToolStripMenuItem});
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.toolStripMenuItem2.Image = global::Tasual.Properties.Resources.Add;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(69, 20);
            this.toolStripMenuItem2.Text = "Create";
            // 
            // taskToolStripMenuItem
            // 
            this.taskToolStripMenuItem.Name = "taskToolStripMenuItem";
            this.taskToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.taskToolStripMenuItem.Text = "Task";
            this.taskToolStripMenuItem.Click += new System.EventHandler(this.taskToolStripMenuItem_Click);
            // 
            // categoryToolStripMenuItem
            // 
            this.categoryToolStripMenuItem.Name = "categoryToolStripMenuItem";
            this.categoryToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.categoryToolStripMenuItem.Text = "Category";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.BackColor = System.Drawing.Color.Transparent;
            this.toolStripMenuItem3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.toolStripMenuItem3.Image = global::Tasual.Properties.Resources.File_List;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(55, 20);
            this.toolStripMenuItem3.Text = "Edit";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortingToolStripMenuItem,
            this.keepOnTToolStripMenuItem,
            this.minimizeToTrayToolStripMenuItem,
            this.launchOnStartupToolStripMenuItem});
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.settingsToolStripMenuItem.Image = global::Tasual.Properties.Resources.Gear;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // sortingToolStripMenuItem
            // 
            this.sortingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortByCategoryToolStripMenuItem,
            this.sortByTimeToolStripMenuItem});
            this.sortingToolStripMenuItem.Name = "sortingToolStripMenuItem";
            this.sortingToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.sortingToolStripMenuItem.Text = "Sorting";
            // 
            // sortByCategoryToolStripMenuItem
            // 
            this.sortByCategoryToolStripMenuItem.Checked = true;
            this.sortByCategoryToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sortByCategoryToolStripMenuItem.Name = "sortByCategoryToolStripMenuItem";
            this.sortByCategoryToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.sortByCategoryToolStripMenuItem.Text = "Sort by Category";
            // 
            // sortByTimeToolStripMenuItem
            // 
            this.sortByTimeToolStripMenuItem.Name = "sortByTimeToolStripMenuItem";
            this.sortByTimeToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.sortByTimeToolStripMenuItem.Text = "Sort by Time";
            // 
            // keepOnTToolStripMenuItem
            // 
            this.keepOnTToolStripMenuItem.Name = "keepOnTToolStripMenuItem";
            this.keepOnTToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.keepOnTToolStripMenuItem.Text = "Always on top";
            this.keepOnTToolStripMenuItem.Click += new System.EventHandler(this.keepOnTToolStripMenuItem_Click);
            // 
            // minimizeToTrayToolStripMenuItem
            // 
            this.minimizeToTrayToolStripMenuItem.Name = "minimizeToTrayToolStripMenuItem";
            this.minimizeToTrayToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.minimizeToTrayToolStripMenuItem.Text = "Minimize to tray";
            // 
            // launchOnStartupToolStripMenuItem
            // 
            this.launchOnStartupToolStripMenuItem.Name = "launchOnStartupToolStripMenuItem";
            this.launchOnStartupToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.launchOnStartupToolStripMenuItem.Text = "Launch on startup";
            // 
            // Tasual_ListView
            // 
            this.Tasual_ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tasual_ListView.BackColor = System.Drawing.Color.White;
            this.Tasual_ListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tasual_ListView.CheckBoxes = true;
            this.Tasual_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Description,
            this.Time});
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
            listViewItem8.Group = listViewGroup6;
            listViewItem8.StateImageIndex = 0;
            listViewItem9.Group = listViewGroup4;
            listViewItem9.StateImageIndex = 0;
            listViewItem10.Group = listViewGroup5;
            listViewItem10.StateImageIndex = 0;
            listViewItem11.Group = listViewGroup6;
            listViewItem11.StateImageIndex = 0;
            listViewItem12.Group = listViewGroup5;
            listViewItem12.StateImageIndex = 0;
            this.Tasual_ListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12});
            this.Tasual_ListView.LabelEdit = true;
            this.Tasual_ListView.Location = new System.Drawing.Point(0, 24);
            this.Tasual_ListView.Margin = new System.Windows.Forms.Padding(0);
            this.Tasual_ListView.MultiSelect = false;
            this.Tasual_ListView.Name = "Tasual_ListView";
            this.Tasual_ListView.Size = new System.Drawing.Size(405, 369);
            this.Tasual_ListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.Tasual_ListView.TabIndex = 0;
            this.Tasual_ListView.UseCompatibleStateImageBehavior = false;
            this.Tasual_ListView.View = System.Windows.Forms.View.Details;
            this.Tasual_ListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.Tasual_ListView_ColumnWidthChanging);
            this.Tasual_ListView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
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
            // Tasual_Notify
            // 
            this.Tasual_Notify.Text = "Tasual_Notify";
            this.Tasual_Notify.Visible = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.linkLabel1.Location = new System.Drawing.Point(0, 393);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.linkLabel1.Size = new System.Drawing.Size(336, 21);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "All tasks complete";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel2.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.linkLabel2.Location = new System.Drawing.Point(342, 393);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel2.Size = new System.Drawing.Size(51, 21);
            this.linkLabel2.TabIndex = 3;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "About";
            this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel2.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            // 
            // Tasual_Main_Status_MenuStrip
            // 
            this.Tasual_Main_Status_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.Tasual_Main_Status_MenuStrip.Name = "Tasual_Main_Status_MenuStrip";
            this.Tasual_Main_Status_MenuStrip.Size = new System.Drawing.Size(108, 70);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.printToolStripMenuItem.Text = "Print";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            // 
            // Tasual_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(405, 414);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.Tasual_ListView);
            this.Controls.Add(this.Tasual_Main_MenuStrip);
            this.ForeColor = System.Drawing.Color.Black;
            this.MainMenuStrip = this.Tasual_Main_MenuStrip;
            this.Name = "Tasual_Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Tasual";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tasual_Main_MouseDown);
            this.Resize += new System.EventHandler(this.Tasual_Main_Resize);
            this.Tasual_Main_MenuStrip.ResumeLayout(false);
            this.Tasual_Main_MenuStrip.PerformLayout();
            this.Tasual_Main_Status_MenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView Tasual_ListView;
        private System.Windows.Forms.MenuStrip Tasual_Main_MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem Button_Exit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem categoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.ToolStripMenuItem sortingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortByCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortByTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launchOnStartupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizeToTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keepOnTToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon Tasual_Notify;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.ContextMenuStrip Tasual_Main_Status_MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
    }
}

