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
            this.Tasual_MenuStrip_Sources = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_ListView = new Tasual.TasualListView();
            this.Tasual_TaskIcons = new System.Windows.Forms.ImageList(this.components);
            this.Tasual_Notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.Tasual_MenuStrip_Notify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_StatusLabel = new System.Windows.Forms.LinkLabel();
            this.Tasual_AboutLabel = new System.Windows.Forms.LinkLabel();
            this.Tasual_MenuStrip_Status = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Tasual_StatusLabel_MenuStrip_Print = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromSaveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_StatusLabel_MenuStrip_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.toClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toSaveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_StatusLabel_MenuStrip_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_Timer_ListViewClick = new System.Windows.Forms.Timer(this.components);
            this.Tasual_MenuStrip_Group = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.noOtherGroupsAvailableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_MenuStrip_Item = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasual_MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tasual_ListView)).BeginInit();
            this.Tasual_MenuStrip_Notify.SuspendLayout();
            this.Tasual_MenuStrip_Status.SuspendLayout();
            this.Tasual_MenuStrip_Group.SuspendLayout();
            this.Tasual_MenuStrip_Item.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tasual_MenuStrip
            // 
            this.Tasual_MenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
            this.Tasual_MenuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tasual_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.Tasual_MenuStrip_Edit,
            this.Tasual_MenuStrip_Settings,
            this.Tasual_MenuStrip_Sources});
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
            // Tasual_MenuStrip_Sources
            // 
            this.Tasual_MenuStrip_Sources.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Tasual_MenuStrip_Sources.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.Tasual_MenuStrip_Sources.Image = global::Tasual.Properties.Resources.Button_Sync;
            this.Tasual_MenuStrip_Sources.Name = "Tasual_MenuStrip_Sources";
            this.Tasual_MenuStrip_Sources.Size = new System.Drawing.Size(76, 20);
            this.Tasual_MenuStrip_Sources.Text = "Sources";
            this.Tasual_MenuStrip_Sources.Click += new System.EventHandler(this.Tasual_MenuStrip_Sources_Click);
            // 
            // Tasual_ListView
            // 
            this.Tasual_ListView.AllowDrop = true;
            this.Tasual_ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tasual_ListView.AutoArrange = false;
            this.Tasual_ListView.BackColor = System.Drawing.Color.White;
            this.Tasual_ListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Tasual_ListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.Tasual_ListView.EmptyListMsg = "All tasks completed. Create some tasks!";
            this.Tasual_ListView.EmptyListMsgFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tasual_ListView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tasual_ListView.FullRowSelect = true;
            this.Tasual_ListView.HideSelection = false;
            this.Tasual_ListView.Location = new System.Drawing.Point(0, 24);
            this.Tasual_ListView.Margin = new System.Windows.Forms.Padding(0);
            this.Tasual_ListView.MultiSelect = false;
            this.Tasual_ListView.Name = "Tasual_ListView";
            this.Tasual_ListView.Size = new System.Drawing.Size(405, 369);
            this.Tasual_ListView.SmallImageList = this.Tasual_TaskIcons;
            this.Tasual_ListView.TabIndex = 0;
            this.Tasual_ListView.UseCompatibleStateImageBehavior = false;
            this.Tasual_ListView.View = System.Windows.Forms.View.Details;
            this.Tasual_ListView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.Tasual_ListView_AfterLabelEdit);
            this.Tasual_ListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.Tasual_ListView_ColumnWidthChanging);
            this.Tasual_ListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.Tasual_ListView_ItemDrag);
            this.Tasual_ListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.Tasual_ListView_DragDrop);
            this.Tasual_ListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.Tasual_ListView_DragEnter);
            this.Tasual_ListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tasual_ListView_MouseDown);
            this.Tasual_ListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Tasual_ListView_MouseUp);
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
            this.Tasual_Notify.ContextMenuStrip = this.Tasual_MenuStrip_Notify;
            this.Tasual_Notify.Icon = ((System.Drawing.Icon)(resources.GetObject("Tasual_Notify.Icon")));
            this.Tasual_Notify.Text = "Tasual";
            this.Tasual_Notify.Visible = true;
            this.Tasual_Notify.Click += new System.EventHandler(this.Tasual_Notify_Click);
            // 
            // Tasual_MenuStrip_Notify
            // 
            this.Tasual_MenuStrip_Notify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.Tasual_MenuStrip_Notify.Name = "Tasual_MenuStrip_Notify";
            this.Tasual_MenuStrip_Notify.Size = new System.Drawing.Size(117, 70);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.showToolStripMenuItem.Text = "Show";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.quitToolStripMenuItem.Text = "Quit";
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
            // Tasual_MenuStrip_Status
            // 
            this.Tasual_MenuStrip_Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tasual_StatusLabel_MenuStrip_Print,
            this.importToolStripMenuItem,
            this.Tasual_StatusLabel_MenuStrip_Export,
            this.Tasual_StatusLabel_MenuStrip_Clear});
            this.Tasual_MenuStrip_Status.Name = "Tasual_Main_Status_MenuStrip";
            this.Tasual_MenuStrip_Status.Size = new System.Drawing.Size(111, 92);
            // 
            // Tasual_StatusLabel_MenuStrip_Print
            // 
            this.Tasual_StatusLabel_MenuStrip_Print.Name = "Tasual_StatusLabel_MenuStrip_Print";
            this.Tasual_StatusLabel_MenuStrip_Print.Size = new System.Drawing.Size(110, 22);
            this.Tasual_StatusLabel_MenuStrip_Print.Text = "Print";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromClipboardToolStripMenuItem,
            this.fromSaveFileToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // fromClipboardToolStripMenuItem
            // 
            this.fromClipboardToolStripMenuItem.Name = "fromClipboardToolStripMenuItem";
            this.fromClipboardToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.fromClipboardToolStripMenuItem.Text = "From clipboard";
            // 
            // fromSaveFileToolStripMenuItem
            // 
            this.fromSaveFileToolStripMenuItem.Name = "fromSaveFileToolStripMenuItem";
            this.fromSaveFileToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.fromSaveFileToolStripMenuItem.Text = "From save file";
            // 
            // Tasual_StatusLabel_MenuStrip_Export
            // 
            this.Tasual_StatusLabel_MenuStrip_Export.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toClipboardToolStripMenuItem,
            this.toSaveFileToolStripMenuItem});
            this.Tasual_StatusLabel_MenuStrip_Export.Name = "Tasual_StatusLabel_MenuStrip_Export";
            this.Tasual_StatusLabel_MenuStrip_Export.Size = new System.Drawing.Size(110, 22);
            this.Tasual_StatusLabel_MenuStrip_Export.Text = "Export";
            // 
            // toClipboardToolStripMenuItem
            // 
            this.toClipboardToolStripMenuItem.Name = "toClipboardToolStripMenuItem";
            this.toClipboardToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.toClipboardToolStripMenuItem.Text = "To clipboard";
            // 
            // toSaveFileToolStripMenuItem
            // 
            this.toSaveFileToolStripMenuItem.Name = "toSaveFileToolStripMenuItem";
            this.toSaveFileToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.toSaveFileToolStripMenuItem.Text = "To save file";
            // 
            // Tasual_StatusLabel_MenuStrip_Clear
            // 
            this.Tasual_StatusLabel_MenuStrip_Clear.Name = "Tasual_StatusLabel_MenuStrip_Clear";
            this.Tasual_StatusLabel_MenuStrip_Clear.Size = new System.Drawing.Size(110, 22);
            this.Tasual_StatusLabel_MenuStrip_Clear.Text = "Clear";
            this.Tasual_StatusLabel_MenuStrip_Clear.Click += new System.EventHandler(this.Tasual_StatusLabel_MenuStrip_Clear_Click);
            // 
            // Tasual_Timer_ListViewClick
            // 
            this.Tasual_Timer_ListViewClick.Interval = 1000;
            this.Tasual_Timer_ListViewClick.Tick += new System.EventHandler(this.Tasual_Timer_ListViewClick_Tick);
            // 
            // Tasual_MenuStrip_Group
            // 
            this.Tasual_MenuStrip_Group.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.toolStripSeparator1,
            this.hideToolStripMenuItem,
            this.toolStripSeparator2,
            this.editToolStripMenuItem,
            this.moveTasksToolStripMenuItem,
            this.deleteToolStripMenuItem1});
            this.Tasual_MenuStrip_Group.Name = "Tasual_MenuStrip_Header";
            this.Tasual_MenuStrip_Group.Size = new System.Drawing.Size(139, 126);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickToolStripMenuItem,
            this.advancedToolStripMenuItem});
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.createToolStripMenuItem.Text = "Create";
            // 
            // quickToolStripMenuItem
            // 
            this.quickToolStripMenuItem.Name = "quickToolStripMenuItem";
            this.quickToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.quickToolStripMenuItem.Text = "Quick";
            // 
            // advancedToolStripMenuItem
            // 
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            this.advancedToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.advancedToolStripMenuItem.Text = "Advanced";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(135, 6);
            this.toolStripSeparator2.Visible = false;
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.editToolStripMenuItem.Text = "Rename";
            // 
            // moveTasksToolStripMenuItem
            // 
            this.moveTasksToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.moveTasksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox2,
            this.noOtherGroupsAvailableToolStripMenuItem});
            this.moveTasksToolStripMenuItem.Name = "moveTasksToolStripMenuItem";
            this.moveTasksToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.moveTasksToolStripMenuItem.Text = "Move Tasks";
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox2.Items.AddRange(new object[] {
            "Move to Foobar",
            "Move to Yinzers",
            "Move to Solar City",
            "Move to Work"});
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(121, 23);
            // 
            // noOtherGroupsAvailableToolStripMenuItem
            // 
            this.noOtherGroupsAvailableToolStripMenuItem.Enabled = false;
            this.noOtherGroupsAvailableToolStripMenuItem.Name = "noOtherGroupsAvailableToolStripMenuItem";
            this.noOtherGroupsAvailableToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.noOtherGroupsAvailableToolStripMenuItem.Text = "(No other groups available)";
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.deleteToolStripMenuItem1.Text = "Delete Tasks";
            // 
            // Tasual_MenuStrip_Item
            // 
            this.Tasual_MenuStrip_Item.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem1,
            this.deleteToolStripMenuItem,
            this.moveToolStripMenuItem,
            this.duplicateToolStripMenuItem});
            this.Tasual_MenuStrip_Item.Name = "Tasual_MenuStrip_Item";
            this.Tasual_MenuStrip_Item.Size = new System.Drawing.Size(125, 92);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.editToolStripMenuItem1.Text = "Edit";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // moveToolStripMenuItem
            // 
            this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            this.moveToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.moveToolStripMenuItem.Text = "Move";
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.duplicateToolStripMenuItem.Text = "Duplicate";
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tasual_Main_FormClosing);
            this.Load += new System.EventHandler(this.Tasual_Main_Load);
            this.Resize += new System.EventHandler(this.Tasual_Main_Resize);
            this.Tasual_MenuStrip.ResumeLayout(false);
            this.Tasual_MenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tasual_ListView)).EndInit();
            this.Tasual_MenuStrip_Notify.ResumeLayout(false);
            this.Tasual_MenuStrip_Status.ResumeLayout(false);
            this.Tasual_MenuStrip_Group.ResumeLayout(false);
            this.Tasual_MenuStrip_Item.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        //private System.Windows.Forms.ListView Tasual_ListView;
        public Tasual.TasualListView Tasual_ListView;
		private System.Windows.Forms.MenuStrip Tasual_MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Settings;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Create_Advanced;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Create_Quick;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Edit;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Settings_Sorting;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Settings_Sorting_Category;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Settings_Sorting_Time;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Settings_LaunchOnStartup;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Settings_MinimizeToTray;
		private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Settings_AlwaysOnTop;
		private System.Windows.Forms.NotifyIcon Tasual_Notify;
		private System.Windows.Forms.LinkLabel Tasual_StatusLabel;
		private System.Windows.Forms.LinkLabel Tasual_AboutLabel;
		private System.Windows.Forms.ContextMenuStrip Tasual_MenuStrip_Status;
		private System.Windows.Forms.ToolStripMenuItem Tasual_StatusLabel_MenuStrip_Print;
		private System.Windows.Forms.ToolStripMenuItem Tasual_StatusLabel_MenuStrip_Export;
		private System.Windows.Forms.ToolStripMenuItem Tasual_StatusLabel_MenuStrip_Clear;
        private System.Windows.Forms.ImageList Tasual_TaskIcons;
        private System.Windows.Forms.Timer Tasual_Timer_ListViewClick;
        private System.Windows.Forms.ToolStripMenuItem Tasual_MenuStrip_Sources;
        private System.Windows.Forms.ContextMenuStrip Tasual_MenuStrip_Group;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem moveTasksToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox2;
        private System.Windows.Forms.ToolStripMenuItem noOtherGroupsAvailableToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip Tasual_MenuStrip_Item;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromSaveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toSaveFileToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip Tasual_MenuStrip_Notify;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
    }
}

