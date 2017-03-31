namespace Tasual
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
            System.Windows.Forms.ListViewGroup listViewGroup10 = new System.Windows.Forms.ListViewGroup("Work", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup11 = new System.Windows.Forms.ListViewGroup("Personal", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup12 = new System.Windows.Forms.ListViewGroup("Misc", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "Fuck Bitches, Get Money",
            "Fly motherfucker"}, -1);
            System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem(new string[] {
            "Boat to the island"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Transparent, null);
            System.Windows.Forms.ListViewItem listViewItem21 = new System.Windows.Forms.ListViewItem(new string[] {
            "Drive to the mall",
            "Sunday, Apr 12th - 9pm"}, -1);
            System.Windows.Forms.ListViewItem listViewItem22 = new System.Windows.Forms.ListViewItem(new string[] {
            "Jump to the sky",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem23 = new System.Windows.Forms.ListViewItem(new string[] {
            "Run up the hill",
            "",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem24 = new System.Windows.Forms.ListViewItem(new string[] {
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
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_ListView = new System.Windows.Forms.ListView();
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Tasual_Notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.Tasual_StatusLabel = new System.Windows.Forms.Label();
            this.Tasual_Main_MenuStrip.SuspendLayout();
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
            this.settingsToolStripMenuItem,
            this.toolStripMenuItem1});
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
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(92, 20);
            this.toolStripMenuItem1.Text = "All tasks done";
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.printToolStripMenuItem.Text = "Print";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportToolStripMenuItem.Text = "Export";
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
            listViewGroup10.Header = "Work";
            listViewGroup10.Name = "Work";
            listViewGroup11.Header = "Personal";
            listViewGroup11.Name = "Personal";
            listViewGroup12.Header = "Misc";
            listViewGroup12.Name = "Misc";
            this.Tasual_ListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup10,
            listViewGroup11,
            listViewGroup12});
            this.Tasual_ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewItem19.StateImageIndex = 0;
            listViewItem20.Group = listViewGroup12;
            listViewItem20.StateImageIndex = 0;
            listViewItem21.Group = listViewGroup10;
            listViewItem21.StateImageIndex = 0;
            listViewItem22.Group = listViewGroup11;
            listViewItem22.StateImageIndex = 0;
            listViewItem23.Group = listViewGroup12;
            listViewItem23.StateImageIndex = 0;
            listViewItem24.Group = listViewGroup11;
            listViewItem24.StateImageIndex = 0;
            this.Tasual_ListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem19,
            listViewItem20,
            listViewItem21,
            listViewItem22,
            listViewItem23,
            listViewItem24});
            this.Tasual_ListView.LabelEdit = true;
            this.Tasual_ListView.Location = new System.Drawing.Point(0, 24);
            this.Tasual_ListView.Margin = new System.Windows.Forms.Padding(0);
            this.Tasual_ListView.MultiSelect = false;
            this.Tasual_ListView.Name = "Tasual_ListView";
            this.Tasual_ListView.Size = new System.Drawing.Size(405, 532);
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
            // Tasual_StatusLabel
            // 
            this.Tasual_StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tasual_StatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tasual_StatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.Tasual_StatusLabel.Location = new System.Drawing.Point(-3, 556);
            this.Tasual_StatusLabel.Name = "Tasual_StatusLabel";
            this.Tasual_StatusLabel.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.Tasual_StatusLabel.Size = new System.Drawing.Size(408, 22);
            this.Tasual_StatusLabel.TabIndex = 2;
            this.Tasual_StatusLabel.Text = "All tasks done";
            this.Tasual_StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Tasual_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(405, 577);
            this.Controls.Add(this.Tasual_StatusLabel);
            this.Controls.Add(this.Tasual_ListView);
            this.Controls.Add(this.Tasual_Main_MenuStrip);
            this.ForeColor = System.Drawing.Color.Black;
            this.MainMenuStrip = this.Tasual_Main_MenuStrip;
            this.Name = "Tasual_Main";
            this.Text = "Tasual";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Tasual_Main_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tasual_Main_MouseDown);
            this.Resize += new System.EventHandler(this.Tasual_Main_Resize);
            this.Tasual_Main_MenuStrip.ResumeLayout(false);
            this.Tasual_Main_MenuStrip.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.ToolStripMenuItem sortingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortByCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortByTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launchOnStartupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizeToTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keepOnTToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon Tasual_Notify;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.Label Tasual_StatusLabel;
    }
}

