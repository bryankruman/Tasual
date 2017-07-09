namespace Tasual
{
	partial class Form_Main
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
			this.MenuStrip = new System.Windows.Forms.MenuStrip();
			this.MenuStrip_Create = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Create_Quick = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Create_Advanced = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Edit_Quick = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Edit_Advanced = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Settings = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Sources = new System.Windows.Forms.ToolStripMenuItem();
			this.ListView = new BrightIdeasSoftware.ObjectListView();
			this.TaskIcons = new System.Windows.Forms.ImageList(this.components);
			this.Notify = new System.Windows.Forms.NotifyIcon(this.components);
			this.MenuStrip_Notify = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MenuStrip_Notify_Show = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Notify_Settings = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Notify_Quit = new System.Windows.Forms.ToolStripMenuItem();
			this.StatusLabel = new System.Windows.Forms.LinkLabel();
			this.AboutLabel = new System.Windows.Forms.LinkLabel();
			this.MenuStrip_Status = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MenuStrip_Status_Print = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Status_Import = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Status_Import_Clipboard = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Status_Import_SaveFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Status_Export = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Status_Export_Clipboard = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Status_Export_SaveFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Status_Clear = new System.Windows.Forms.ToolStripMenuItem();
			this.Timer_ListViewClick = new System.Windows.Forms.Timer(this.components);
			this.MenuStrip_Group = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MenuStrip_Group_Create = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Group_Create_Quick = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Group_Create_Advanced = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Group_Show = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Group_Hide = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Group_Rename = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Group_MoveTasks = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Group_Move_Blank = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Group_Delete = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Item = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MenuStrip_Item_Create = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Item_Create_Quick = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Item_Create_Advanced = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Item_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Item_Edit_Quick = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Item_Edit_Advanced = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Item_Duplicate = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Item_Move = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Item_Move_Blank = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Item_Delete = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MenuStrip_Icon_Notes = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_Notes_EditNotes = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_Notes_Clipboard = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_Notes_Remove = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_AddNotes = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_Link = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_Link_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_Link_Clipboard = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_Link_Follow = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_Link_Remove = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_AddLink = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_Location = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_Location_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_Location_Clipboard = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_Location_Maps = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_Location_Remove = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Icon_AddLocation = new System.Windows.Forms.ToolStripMenuItem();
			this.Timer_CheckUpdate = new System.Windows.Forms.Timer(this.components);
			this.MenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ListView)).BeginInit();
			this.MenuStrip_Notify.SuspendLayout();
			this.MenuStrip_Status.SuspendLayout();
			this.MenuStrip_Group.SuspendLayout();
			this.MenuStrip_Item.SuspendLayout();
			this.MenuStrip_Icon.SuspendLayout();
			this.SuspendLayout();
			// 
			// MenuStrip
			// 
			this.MenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
			this.MenuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Create,
            this.MenuStrip_Edit,
            this.MenuStrip_Settings,
            this.MenuStrip_Sources});
			this.MenuStrip.Location = new System.Drawing.Point(0, 0);
			this.MenuStrip.Name = "MenuStrip";
			this.MenuStrip.Size = new System.Drawing.Size(513, 24);
			this.MenuStrip.TabIndex = 1;
			this.MenuStrip.Text = "menuStrip1";
			// 
			// MenuStrip_Create
			// 
			this.MenuStrip_Create.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Create_Quick,
            this.MenuStrip_Create_Advanced});
			this.MenuStrip_Create.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.MenuStrip_Create.Image = global::Tasual.Properties.Resources.Add;
			this.MenuStrip_Create.Name = "MenuStrip_Create";
			this.MenuStrip_Create.Size = new System.Drawing.Size(69, 20);
			this.MenuStrip_Create.Text = "Create";
			// 
			// MenuStrip_Create_Quick
			// 
			this.MenuStrip_Create_Quick.Name = "MenuStrip_Create_Quick";
			this.MenuStrip_Create_Quick.Size = new System.Drawing.Size(127, 22);
			this.MenuStrip_Create_Quick.Text = "Quick";
			this.MenuStrip_Create_Quick.Click += new System.EventHandler(this.MenuStrip_Create_Quick_Click);
			// 
			// MenuStrip_Create_Advanced
			// 
			this.MenuStrip_Create_Advanced.Name = "MenuStrip_Create_Advanced";
			this.MenuStrip_Create_Advanced.Size = new System.Drawing.Size(127, 22);
			this.MenuStrip_Create_Advanced.Text = "Advanced";
			this.MenuStrip_Create_Advanced.Click += new System.EventHandler(this.MenuStrip_Create_Advanced_Click);
			// 
			// MenuStrip_Edit
			// 
			this.MenuStrip_Edit.BackColor = System.Drawing.Color.Transparent;
			this.MenuStrip_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Edit_Quick,
            this.MenuStrip_Edit_Advanced});
			this.MenuStrip_Edit.Enabled = false;
			this.MenuStrip_Edit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.MenuStrip_Edit.Image = global::Tasual.Properties.Resources.File_List;
			this.MenuStrip_Edit.Name = "MenuStrip_Edit";
			this.MenuStrip_Edit.Size = new System.Drawing.Size(55, 20);
			this.MenuStrip_Edit.Text = "Edit";
			// 
			// MenuStrip_Edit_Quick
			// 
			this.MenuStrip_Edit_Quick.Name = "MenuStrip_Edit_Quick";
			this.MenuStrip_Edit_Quick.Size = new System.Drawing.Size(127, 22);
			this.MenuStrip_Edit_Quick.Text = "Quick";
			this.MenuStrip_Edit_Quick.Click += new System.EventHandler(this.MenuStrip_Edit_Quick_Click);
			// 
			// MenuStrip_Edit_Advanced
			// 
			this.MenuStrip_Edit_Advanced.Name = "MenuStrip_Edit_Advanced";
			this.MenuStrip_Edit_Advanced.Size = new System.Drawing.Size(127, 22);
			this.MenuStrip_Edit_Advanced.Text = "Advanced";
			this.MenuStrip_Edit_Advanced.Click += new System.EventHandler(this.MenuStrip_Edit_Advanced_Click);
			// 
			// MenuStrip_Settings
			// 
			this.MenuStrip_Settings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.MenuStrip_Settings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.MenuStrip_Settings.Image = global::Tasual.Properties.Resources.Gear;
			this.MenuStrip_Settings.Name = "MenuStrip_Settings";
			this.MenuStrip_Settings.Size = new System.Drawing.Size(77, 20);
			this.MenuStrip_Settings.Text = "Settings";
			this.MenuStrip_Settings.Click += new System.EventHandler(this.MenuStrip_Settings_Click);
			// 
			// MenuStrip_Sources
			// 
			this.MenuStrip_Sources.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.MenuStrip_Sources.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.MenuStrip_Sources.Image = global::Tasual.Properties.Resources.Button_Sync;
			this.MenuStrip_Sources.Name = "MenuStrip_Sources";
			this.MenuStrip_Sources.Size = new System.Drawing.Size(76, 20);
			this.MenuStrip_Sources.Text = "Sources";
			this.MenuStrip_Sources.Visible = false;
			this.MenuStrip_Sources.Click += new System.EventHandler(this.MenuStrip_Sources_Click);
			// 
			// ListView
			// 
			this.ListView.AllowDrop = true;
			this.ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ListView.AutoArrange = false;
			this.ListView.BackColor = System.Drawing.Color.White;
			this.ListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ListView.CellEditUseWholeCell = false;
			this.ListView.Cursor = System.Windows.Forms.Cursors.Default;
			this.ListView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ListView.FullRowSelect = true;
			this.ListView.HeaderMaximumHeight = 24;
			this.ListView.HideSelection = false;
			this.ListView.Location = new System.Drawing.Point(0, 24);
			this.ListView.Margin = new System.Windows.Forms.Padding(0);
			this.ListView.MultiSelect = false;
			this.ListView.Name = "ListView";
			this.ListView.Size = new System.Drawing.Size(513, 471);
			this.ListView.SmallImageList = this.TaskIcons;
			this.ListView.TabIndex = 0;
			this.ListView.UseCompatibleStateImageBehavior = false;
			this.ListView.View = System.Windows.Forms.View.Details;
			this.ListView.AboutToCreateGroups += new System.EventHandler<BrightIdeasSoftware.CreateGroupsEventArgs>(this.ListView_AboutToCreateGroups);
			this.ListView.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.ListView_CellEditFinished);
			this.ListView.SelectedIndexChanged += new System.EventHandler(this.ListView_SelectedIndexChanged);
			this.ListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListView_KeyDown);
			this.ListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseDown);
			this.ListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseUp);
			// 
			// TaskIcons
			// 
			this.TaskIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TaskIcons.ImageStream")));
			this.TaskIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.TaskIcons.Images.SetKeyName(0, "attach-checked.png");
			this.TaskIcons.Images.SetKeyName(1, "location.png");
			this.TaskIcons.Images.SetKeyName(2, "link.png");
			this.TaskIcons.Images.SetKeyName(3, "note.png");
			this.TaskIcons.Images.SetKeyName(4, "location-checked.png");
			this.TaskIcons.Images.SetKeyName(5, "link-checked.png");
			this.TaskIcons.Images.SetKeyName(6, "note-checked.png");
			// 
			// Notify
			// 
			this.Notify.ContextMenuStrip = this.MenuStrip_Notify;
			this.Notify.Text = "Tasual";
			this.Notify.Visible = true;
			this.Notify.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Notify_MouseClick);
			// 
			// MenuStrip_Notify
			// 
			this.MenuStrip_Notify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Notify_Show,
            this.MenuStrip_Notify_Settings,
            this.MenuStrip_Notify_Quit});
			this.MenuStrip_Notify.Name = "MenuStrip_Notify";
			this.MenuStrip_Notify.Size = new System.Drawing.Size(117, 70);
			// 
			// MenuStrip_Notify_Show
			// 
			this.MenuStrip_Notify_Show.Name = "MenuStrip_Notify_Show";
			this.MenuStrip_Notify_Show.Size = new System.Drawing.Size(116, 22);
			this.MenuStrip_Notify_Show.Text = "Show";
			this.MenuStrip_Notify_Show.Click += new System.EventHandler(this.MenuStrip_Notify_Show_Click);
			// 
			// MenuStrip_Notify_Settings
			// 
			this.MenuStrip_Notify_Settings.Name = "MenuStrip_Notify_Settings";
			this.MenuStrip_Notify_Settings.Size = new System.Drawing.Size(116, 22);
			this.MenuStrip_Notify_Settings.Text = "Settings";
			this.MenuStrip_Notify_Settings.Click += new System.EventHandler(this.MenuStrip_Notify_Settings_Click);
			// 
			// MenuStrip_Notify_Quit
			// 
			this.MenuStrip_Notify_Quit.Name = "MenuStrip_Notify_Quit";
			this.MenuStrip_Notify_Quit.Size = new System.Drawing.Size(116, 22);
			this.MenuStrip_Notify_Quit.Text = "Quit";
			this.MenuStrip_Notify_Quit.Click += new System.EventHandler(this.MenuStrip_Notify_Quit_Click);
			// 
			// StatusLabel
			// 
			this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.StatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.StatusLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.StatusLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.StatusLabel.Location = new System.Drawing.Point(0, 495);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
			this.StatusLabel.Size = new System.Drawing.Size(444, 21);
			this.StatusLabel.TabIndex = 2;
			this.StatusLabel.TabStop = true;
			this.StatusLabel.Text = "All tasks complete";
			this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.StatusLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.StatusLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.StatusLabel_LinkClicked);
			// 
			// AboutLabel
			// 
			this.AboutLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.AboutLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.AboutLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.AboutLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.AboutLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.AboutLabel.Location = new System.Drawing.Point(450, 495);
			this.AboutLabel.Name = "AboutLabel";
			this.AboutLabel.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.AboutLabel.Size = new System.Drawing.Size(51, 21);
			this.AboutLabel.TabIndex = 3;
			this.AboutLabel.TabStop = true;
			this.AboutLabel.Text = "About";
			this.AboutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.AboutLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
			this.AboutLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AboutLabel_LinkClicked);
			// 
			// MenuStrip_Status
			// 
			this.MenuStrip_Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Status_Print,
            this.MenuStrip_Status_Import,
            this.MenuStrip_Status_Export,
            this.MenuStrip_Status_Clear});
			this.MenuStrip_Status.Name = "Main_Status_MenuStrip";
			this.MenuStrip_Status.Size = new System.Drawing.Size(111, 92);
			// 
			// MenuStrip_Status_Print
			// 
			this.MenuStrip_Status_Print.Enabled = false;
			this.MenuStrip_Status_Print.Name = "MenuStrip_Status_Print";
			this.MenuStrip_Status_Print.Size = new System.Drawing.Size(110, 22);
			this.MenuStrip_Status_Print.Text = "Print";
			// 
			// MenuStrip_Status_Import
			// 
			this.MenuStrip_Status_Import.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Status_Import_Clipboard,
            this.MenuStrip_Status_Import_SaveFile});
			this.MenuStrip_Status_Import.Enabled = false;
			this.MenuStrip_Status_Import.Name = "MenuStrip_Status_Import";
			this.MenuStrip_Status_Import.Size = new System.Drawing.Size(110, 22);
			this.MenuStrip_Status_Import.Text = "Import";
			// 
			// MenuStrip_Status_Import_Clipboard
			// 
			this.MenuStrip_Status_Import_Clipboard.Name = "MenuStrip_Status_Import_Clipboard";
			this.MenuStrip_Status_Import_Clipboard.Size = new System.Drawing.Size(155, 22);
			this.MenuStrip_Status_Import_Clipboard.Text = "From clipboard";
			// 
			// MenuStrip_Status_Import_SaveFile
			// 
			this.MenuStrip_Status_Import_SaveFile.Name = "MenuStrip_Status_Import_SaveFile";
			this.MenuStrip_Status_Import_SaveFile.Size = new System.Drawing.Size(155, 22);
			this.MenuStrip_Status_Import_SaveFile.Text = "From save file";
			// 
			// MenuStrip_Status_Export
			// 
			this.MenuStrip_Status_Export.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Status_Export_Clipboard,
            this.MenuStrip_Status_Export_SaveFile});
			this.MenuStrip_Status_Export.Enabled = false;
			this.MenuStrip_Status_Export.Name = "MenuStrip_Status_Export";
			this.MenuStrip_Status_Export.Size = new System.Drawing.Size(110, 22);
			this.MenuStrip_Status_Export.Text = "Export";
			// 
			// MenuStrip_Status_Export_Clipboard
			// 
			this.MenuStrip_Status_Export_Clipboard.Name = "MenuStrip_Status_Export_Clipboard";
			this.MenuStrip_Status_Export_Clipboard.Size = new System.Drawing.Size(141, 22);
			this.MenuStrip_Status_Export_Clipboard.Text = "To clipboard";
			// 
			// MenuStrip_Status_Export_SaveFile
			// 
			this.MenuStrip_Status_Export_SaveFile.Name = "MenuStrip_Status_Export_SaveFile";
			this.MenuStrip_Status_Export_SaveFile.Size = new System.Drawing.Size(141, 22);
			this.MenuStrip_Status_Export_SaveFile.Text = "To save file";
			// 
			// MenuStrip_Status_Clear
			// 
			this.MenuStrip_Status_Clear.Name = "MenuStrip_Status_Clear";
			this.MenuStrip_Status_Clear.Size = new System.Drawing.Size(110, 22);
			this.MenuStrip_Status_Clear.Text = "Clear";
			this.MenuStrip_Status_Clear.Click += new System.EventHandler(this.MenuStrip_Status_Clear_Click);
			// 
			// Timer_ListViewClick
			// 
			this.Timer_ListViewClick.Interval = 1000;
			this.Timer_ListViewClick.Tick += new System.EventHandler(this.Timer_ListViewClick_Tick);
			// 
			// MenuStrip_Group
			// 
			this.MenuStrip_Group.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Group_Create,
            this.MenuStrip_Group_Show,
            this.MenuStrip_Group_Hide,
            this.MenuStrip_Group_Rename,
            this.MenuStrip_Group_MoveTasks,
            this.MenuStrip_Group_Delete});
			this.MenuStrip_Group.Name = "MenuStrip_Header";
			this.MenuStrip_Group.Size = new System.Drawing.Size(140, 136);
			this.MenuStrip_Group.Opening += new System.ComponentModel.CancelEventHandler(this.MenuStrip_Group_Opening);
			// 
			// MenuStrip_Group_Create
			// 
			this.MenuStrip_Group_Create.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Group_Create_Quick,
            this.MenuStrip_Group_Create_Advanced});
			this.MenuStrip_Group_Create.Name = "MenuStrip_Group_Create";
			this.MenuStrip_Group_Create.Size = new System.Drawing.Size(139, 22);
			this.MenuStrip_Group_Create.Text = "Create Task";
			// 
			// MenuStrip_Group_Create_Quick
			// 
			this.MenuStrip_Group_Create_Quick.Name = "MenuStrip_Group_Create_Quick";
			this.MenuStrip_Group_Create_Quick.Size = new System.Drawing.Size(127, 22);
			this.MenuStrip_Group_Create_Quick.Text = "Quick";
			this.MenuStrip_Group_Create_Quick.Click += new System.EventHandler(this.MenuStrip_Group_Create_Quick_Click);
			// 
			// MenuStrip_Group_Create_Advanced
			// 
			this.MenuStrip_Group_Create_Advanced.Name = "MenuStrip_Group_Create_Advanced";
			this.MenuStrip_Group_Create_Advanced.Size = new System.Drawing.Size(127, 22);
			this.MenuStrip_Group_Create_Advanced.Text = "Advanced";
			this.MenuStrip_Group_Create_Advanced.Click += new System.EventHandler(this.MenuStrip_Group_Create_Advanced_Click);
			// 
			// MenuStrip_Group_Show
			// 
			this.MenuStrip_Group_Show.Name = "MenuStrip_Group_Show";
			this.MenuStrip_Group_Show.Size = new System.Drawing.Size(139, 22);
			this.MenuStrip_Group_Show.Text = "Show";
			this.MenuStrip_Group_Show.Click += new System.EventHandler(this.MenuStrip_Group_Show_Click);
			// 
			// MenuStrip_Group_Hide
			// 
			this.MenuStrip_Group_Hide.Name = "MenuStrip_Group_Hide";
			this.MenuStrip_Group_Hide.Size = new System.Drawing.Size(139, 22);
			this.MenuStrip_Group_Hide.Text = "Hide";
			this.MenuStrip_Group_Hide.Click += new System.EventHandler(this.MenuStrip_Group_Hide_Click);
			// 
			// MenuStrip_Group_Rename
			// 
			this.MenuStrip_Group_Rename.Enabled = false;
			this.MenuStrip_Group_Rename.Name = "MenuStrip_Group_Rename";
			this.MenuStrip_Group_Rename.Size = new System.Drawing.Size(139, 22);
			this.MenuStrip_Group_Rename.Text = "Rename";
			// 
			// MenuStrip_Group_MoveTasks
			// 
			this.MenuStrip_Group_MoveTasks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.MenuStrip_Group_MoveTasks.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Group_Move_Blank});
			this.MenuStrip_Group_MoveTasks.Name = "MenuStrip_Group_MoveTasks";
			this.MenuStrip_Group_MoveTasks.Size = new System.Drawing.Size(139, 22);
			this.MenuStrip_Group_MoveTasks.Text = "Move Tasks";
			this.MenuStrip_Group_MoveTasks.DropDownOpening += new System.EventHandler(this.MenuStrip_Group_MoveTasks_DropDownOpening);
			// 
			// MenuStrip_Group_Move_Blank
			// 
			this.MenuStrip_Group_Move_Blank.Name = "MenuStrip_Group_Move_Blank";
			this.MenuStrip_Group_Move_Blank.Size = new System.Drawing.Size(130, 22);
			this.MenuStrip_Group_Move_Blank.Text = "Blank Item";
			this.MenuStrip_Group_Move_Blank.Visible = false;
			// 
			// MenuStrip_Group_Delete
			// 
			this.MenuStrip_Group_Delete.Name = "MenuStrip_Group_Delete";
			this.MenuStrip_Group_Delete.Size = new System.Drawing.Size(139, 22);
			this.MenuStrip_Group_Delete.Text = "Delete Tasks";
			this.MenuStrip_Group_Delete.Click += new System.EventHandler(this.MenuStrip_Group_Delete_Click);
			// 
			// MenuStrip_Item
			// 
			this.MenuStrip_Item.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Item_Create,
            this.MenuStrip_Item_Edit,
            this.MenuStrip_Item_Duplicate,
            this.MenuStrip_Item_Move,
            this.MenuStrip_Item_Delete});
			this.MenuStrip_Item.Name = "MenuStrip_Item";
			this.MenuStrip_Item.Size = new System.Drawing.Size(125, 114);
			// 
			// MenuStrip_Item_Create
			// 
			this.MenuStrip_Item_Create.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Item_Create_Quick,
            this.MenuStrip_Item_Create_Advanced});
			this.MenuStrip_Item_Create.Name = "MenuStrip_Item_Create";
			this.MenuStrip_Item_Create.Size = new System.Drawing.Size(124, 22);
			this.MenuStrip_Item_Create.Text = "Create";
			// 
			// MenuStrip_Item_Create_Quick
			// 
			this.MenuStrip_Item_Create_Quick.Name = "MenuStrip_Item_Create_Quick";
			this.MenuStrip_Item_Create_Quick.Size = new System.Drawing.Size(127, 22);
			this.MenuStrip_Item_Create_Quick.Text = "Quick";
			this.MenuStrip_Item_Create_Quick.Click += new System.EventHandler(this.MenuStrip_Item_Create_Quick_Click);
			// 
			// MenuStrip_Item_Create_Advanced
			// 
			this.MenuStrip_Item_Create_Advanced.Name = "MenuStrip_Item_Create_Advanced";
			this.MenuStrip_Item_Create_Advanced.Size = new System.Drawing.Size(127, 22);
			this.MenuStrip_Item_Create_Advanced.Text = "Advanced";
			this.MenuStrip_Item_Create_Advanced.Click += new System.EventHandler(this.MenuStrip_Item_Create_Advanced_Click);
			// 
			// MenuStrip_Item_Edit
			// 
			this.MenuStrip_Item_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Item_Edit_Quick,
            this.MenuStrip_Item_Edit_Advanced});
			this.MenuStrip_Item_Edit.Name = "MenuStrip_Item_Edit";
			this.MenuStrip_Item_Edit.Size = new System.Drawing.Size(124, 22);
			this.MenuStrip_Item_Edit.Text = "Edit";
			// 
			// MenuStrip_Item_Edit_Quick
			// 
			this.MenuStrip_Item_Edit_Quick.Name = "MenuStrip_Item_Edit_Quick";
			this.MenuStrip_Item_Edit_Quick.Size = new System.Drawing.Size(127, 22);
			this.MenuStrip_Item_Edit_Quick.Text = "Quick";
			this.MenuStrip_Item_Edit_Quick.Click += new System.EventHandler(this.MenuStrip_Item_Edit_Quick_Click);
			// 
			// MenuStrip_Item_Edit_Advanced
			// 
			this.MenuStrip_Item_Edit_Advanced.Name = "MenuStrip_Item_Edit_Advanced";
			this.MenuStrip_Item_Edit_Advanced.Size = new System.Drawing.Size(127, 22);
			this.MenuStrip_Item_Edit_Advanced.Text = "Advanced";
			this.MenuStrip_Item_Edit_Advanced.Click += new System.EventHandler(this.MenuStrip_Item_Edit_Advanced_Click);
			// 
			// MenuStrip_Item_Duplicate
			// 
			this.MenuStrip_Item_Duplicate.Name = "MenuStrip_Item_Duplicate";
			this.MenuStrip_Item_Duplicate.Size = new System.Drawing.Size(124, 22);
			this.MenuStrip_Item_Duplicate.Text = "Duplicate";
			this.MenuStrip_Item_Duplicate.Click += new System.EventHandler(this.MenuStrip_Item_Duplicate_Click);
			// 
			// MenuStrip_Item_Move
			// 
			this.MenuStrip_Item_Move.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Item_Move_Blank});
			this.MenuStrip_Item_Move.Name = "MenuStrip_Item_Move";
			this.MenuStrip_Item_Move.Size = new System.Drawing.Size(124, 22);
			this.MenuStrip_Item_Move.Text = "Move";
			this.MenuStrip_Item_Move.DropDownOpening += new System.EventHandler(this.MenuStrip_Item_Move_DropDownOpening);
			// 
			// MenuStrip_Item_Move_Blank
			// 
			this.MenuStrip_Item_Move_Blank.Name = "MenuStrip_Item_Move_Blank";
			this.MenuStrip_Item_Move_Blank.Size = new System.Drawing.Size(130, 22);
			this.MenuStrip_Item_Move_Blank.Text = "Blank Item";
			this.MenuStrip_Item_Move_Blank.Visible = false;
			// 
			// MenuStrip_Item_Delete
			// 
			this.MenuStrip_Item_Delete.Name = "MenuStrip_Item_Delete";
			this.MenuStrip_Item_Delete.Size = new System.Drawing.Size(124, 22);
			this.MenuStrip_Item_Delete.Text = "Delete";
			this.MenuStrip_Item_Delete.Click += new System.EventHandler(this.MenuStrip_Item_Delete_Click);
			// 
			// MenuStrip_Icon
			// 
			this.MenuStrip_Icon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Icon_Notes,
            this.MenuStrip_Icon_AddNotes,
            this.MenuStrip_Icon_Link,
            this.MenuStrip_Icon_AddLink,
            this.MenuStrip_Icon_Location,
            this.MenuStrip_Icon_AddLocation});
			this.MenuStrip_Icon.Name = "MenuStrip_Icon";
			this.MenuStrip_Icon.Size = new System.Drawing.Size(143, 136);
			this.MenuStrip_Icon.Opening += new System.ComponentModel.CancelEventHandler(this.MenuStrip_Icon_Opening);
			// 
			// MenuStrip_Icon_Notes
			// 
			this.MenuStrip_Icon_Notes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Icon_Notes_EditNotes,
            this.MenuStrip_Icon_Notes_Clipboard,
            this.MenuStrip_Icon_Notes_Remove});
			this.MenuStrip_Icon_Notes.Name = "MenuStrip_Icon_Notes";
			this.MenuStrip_Icon_Notes.Size = new System.Drawing.Size(142, 22);
			this.MenuStrip_Icon_Notes.Text = "Notes";
			// 
			// MenuStrip_Icon_Notes_EditNotes
			// 
			this.MenuStrip_Icon_Notes_EditNotes.Name = "MenuStrip_Icon_Notes_EditNotes";
			this.MenuStrip_Icon_Notes_EditNotes.Size = new System.Drawing.Size(169, 22);
			this.MenuStrip_Icon_Notes_EditNotes.Text = "Edit notes";
			this.MenuStrip_Icon_Notes_EditNotes.Click += new System.EventHandler(this.MenuStrip_Icon_Notes_Edit_Click);
			// 
			// MenuStrip_Icon_Notes_Clipboard
			// 
			this.MenuStrip_Icon_Notes_Clipboard.Name = "MenuStrip_Icon_Notes_Clipboard";
			this.MenuStrip_Icon_Notes_Clipboard.Size = new System.Drawing.Size(169, 22);
			this.MenuStrip_Icon_Notes_Clipboard.Text = "Copy to clipboard";
			this.MenuStrip_Icon_Notes_Clipboard.Click += new System.EventHandler(this.MenuStrip_Icon_Notes_Clipboard_Click);
			// 
			// MenuStrip_Icon_Notes_Remove
			// 
			this.MenuStrip_Icon_Notes_Remove.Name = "MenuStrip_Icon_Notes_Remove";
			this.MenuStrip_Icon_Notes_Remove.Size = new System.Drawing.Size(169, 22);
			this.MenuStrip_Icon_Notes_Remove.Text = "Remove notes";
			this.MenuStrip_Icon_Notes_Remove.Click += new System.EventHandler(this.MenuStrip_Icon_Notes_Remove_Click);
			// 
			// MenuStrip_Icon_AddNotes
			// 
			this.MenuStrip_Icon_AddNotes.Name = "MenuStrip_Icon_AddNotes";
			this.MenuStrip_Icon_AddNotes.Size = new System.Drawing.Size(142, 22);
			this.MenuStrip_Icon_AddNotes.Text = "Add notes";
			this.MenuStrip_Icon_AddNotes.Click += new System.EventHandler(this.MenuStrip_Icon_AddNotes_Click);
			// 
			// MenuStrip_Icon_Link
			// 
			this.MenuStrip_Icon_Link.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Icon_Link_Edit,
            this.MenuStrip_Icon_Link_Clipboard,
            this.MenuStrip_Icon_Link_Follow,
            this.MenuStrip_Icon_Link_Remove});
			this.MenuStrip_Icon_Link.Name = "MenuStrip_Icon_Link";
			this.MenuStrip_Icon_Link.Size = new System.Drawing.Size(142, 22);
			this.MenuStrip_Icon_Link.Text = "Link";
			// 
			// MenuStrip_Icon_Link_Edit
			// 
			this.MenuStrip_Icon_Link_Edit.Name = "MenuStrip_Icon_Link_Edit";
			this.MenuStrip_Icon_Link_Edit.Size = new System.Drawing.Size(169, 22);
			this.MenuStrip_Icon_Link_Edit.Text = "Edit link";
			this.MenuStrip_Icon_Link_Edit.Click += new System.EventHandler(this.MenuStrip_Icon_Link_Edit_Click);
			// 
			// MenuStrip_Icon_Link_Clipboard
			// 
			this.MenuStrip_Icon_Link_Clipboard.Name = "MenuStrip_Icon_Link_Clipboard";
			this.MenuStrip_Icon_Link_Clipboard.Size = new System.Drawing.Size(169, 22);
			this.MenuStrip_Icon_Link_Clipboard.Text = "Copy to clipboard";
			this.MenuStrip_Icon_Link_Clipboard.Click += new System.EventHandler(this.MenuStrip_Icon_Link_Clipboard_Click);
			// 
			// MenuStrip_Icon_Link_Follow
			// 
			this.MenuStrip_Icon_Link_Follow.Name = "MenuStrip_Icon_Link_Follow";
			this.MenuStrip_Icon_Link_Follow.Size = new System.Drawing.Size(169, 22);
			this.MenuStrip_Icon_Link_Follow.Text = "Follow link";
			this.MenuStrip_Icon_Link_Follow.Click += new System.EventHandler(this.MenuStrip_Icon_Link_Follow_Click);
			// 
			// MenuStrip_Icon_Link_Remove
			// 
			this.MenuStrip_Icon_Link_Remove.Name = "MenuStrip_Icon_Link_Remove";
			this.MenuStrip_Icon_Link_Remove.Size = new System.Drawing.Size(169, 22);
			this.MenuStrip_Icon_Link_Remove.Text = "Remove link";
			this.MenuStrip_Icon_Link_Remove.Click += new System.EventHandler(this.MenuStrip_Icon_Link_Remove_Click);
			// 
			// MenuStrip_Icon_AddLink
			// 
			this.MenuStrip_Icon_AddLink.Name = "MenuStrip_Icon_AddLink";
			this.MenuStrip_Icon_AddLink.Size = new System.Drawing.Size(142, 22);
			this.MenuStrip_Icon_AddLink.Text = "Add link";
			this.MenuStrip_Icon_AddLink.Click += new System.EventHandler(this.MenuStrip_Icon_AddLink_Click);
			// 
			// MenuStrip_Icon_Location
			// 
			this.MenuStrip_Icon_Location.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Icon_Location_Edit,
            this.MenuStrip_Icon_Location_Clipboard,
            this.MenuStrip_Icon_Location_Maps,
            this.MenuStrip_Icon_Location_Remove});
			this.MenuStrip_Icon_Location.Name = "MenuStrip_Icon_Location";
			this.MenuStrip_Icon_Location.Size = new System.Drawing.Size(142, 22);
			this.MenuStrip_Icon_Location.Text = "Location";
			// 
			// MenuStrip_Icon_Location_Edit
			// 
			this.MenuStrip_Icon_Location_Edit.Name = "MenuStrip_Icon_Location_Edit";
			this.MenuStrip_Icon_Location_Edit.Size = new System.Drawing.Size(176, 22);
			this.MenuStrip_Icon_Location_Edit.Text = "Edit location";
			this.MenuStrip_Icon_Location_Edit.Click += new System.EventHandler(this.MenuStrip_Icon_Location_Edit_Click);
			// 
			// MenuStrip_Icon_Location_Clipboard
			// 
			this.MenuStrip_Icon_Location_Clipboard.Name = "MenuStrip_Icon_Location_Clipboard";
			this.MenuStrip_Icon_Location_Clipboard.Size = new System.Drawing.Size(176, 22);
			this.MenuStrip_Icon_Location_Clipboard.Text = "Copy to clipboard";
			this.MenuStrip_Icon_Location_Clipboard.Click += new System.EventHandler(this.MenuStrip_Icon_Location_Clipboard_Click);
			// 
			// MenuStrip_Icon_Location_Maps
			// 
			this.MenuStrip_Icon_Location_Maps.Name = "MenuStrip_Icon_Location_Maps";
			this.MenuStrip_Icon_Location_Maps.Size = new System.Drawing.Size(176, 22);
			this.MenuStrip_Icon_Location_Maps.Text = "Open Google Maps";
			this.MenuStrip_Icon_Location_Maps.Click += new System.EventHandler(this.MenuStrip_Icon_Location_Maps_Click);
			// 
			// MenuStrip_Icon_Location_Remove
			// 
			this.MenuStrip_Icon_Location_Remove.Name = "MenuStrip_Icon_Location_Remove";
			this.MenuStrip_Icon_Location_Remove.Size = new System.Drawing.Size(176, 22);
			this.MenuStrip_Icon_Location_Remove.Text = "Remove location";
			this.MenuStrip_Icon_Location_Remove.Click += new System.EventHandler(this.MenuStrip_Icon_Location_Remove_Click);
			// 
			// MenuStrip_Icon_AddLocation
			// 
			this.MenuStrip_Icon_AddLocation.Name = "MenuStrip_Icon_AddLocation";
			this.MenuStrip_Icon_AddLocation.Size = new System.Drawing.Size(142, 22);
			this.MenuStrip_Icon_AddLocation.Text = "Add location";
			this.MenuStrip_Icon_AddLocation.Click += new System.EventHandler(this.MenuStrip_Icon_AddLocation_Click);
			// 
			// Timer_CheckUpdate
			// 
			this.Timer_CheckUpdate.Enabled = true;
			this.Timer_CheckUpdate.Interval = 60000;
			this.Timer_CheckUpdate.Tick += new System.EventHandler(this.Timer_CheckUpdate_Tick);
			// 
			// Form_Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(232)))), ((int)(((byte)(246)))));
			this.ClientSize = new System.Drawing.Size(513, 516);
			this.Controls.Add(this.AboutLabel);
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.ListView);
			this.Controls.Add(this.MenuStrip);
			this.ForeColor = System.Drawing.Color.Black;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.MenuStrip;
			this.MinimumSize = new System.Drawing.Size(300, 200);
			this.Name = "Form_Main";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Tasual";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.Resize += new System.EventHandler(this.Main_Resize);
			this.MenuStrip.ResumeLayout(false);
			this.MenuStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ListView)).EndInit();
			this.MenuStrip_Notify.ResumeLayout(false);
			this.MenuStrip_Status.ResumeLayout(false);
			this.MenuStrip_Group.ResumeLayout(false);
			this.MenuStrip_Item.ResumeLayout(false);
			this.MenuStrip_Icon.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		/// <summary>
		/// Public ListView object.
		/// </summary>
		public BrightIdeasSoftware.ObjectListView ListView;
		private System.Windows.Forms.MenuStrip MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Create;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Settings;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Create_Advanced;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Create_Quick;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Edit;
		private System.Windows.Forms.NotifyIcon Notify;
		private System.Windows.Forms.LinkLabel StatusLabel;
		private System.Windows.Forms.LinkLabel AboutLabel;
		private System.Windows.Forms.ContextMenuStrip MenuStrip_Status;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Status_Print;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Status_Export;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Status_Clear;
		private System.Windows.Forms.ImageList TaskIcons;
		private System.Windows.Forms.Timer Timer_ListViewClick;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Sources;
		private System.Windows.Forms.ContextMenuStrip MenuStrip_Group;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Group_Create;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Group_Create_Quick;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Group_Create_Advanced;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Group_Rename;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Group_Delete;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Group_Hide;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Group_MoveTasks;
		private System.Windows.Forms.ContextMenuStrip MenuStrip_Item;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Item_Edit;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Item_Delete;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Item_Move;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Item_Duplicate;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Status_Import;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Status_Import_Clipboard;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Status_Import_SaveFile;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Status_Export_Clipboard;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Status_Export_SaveFile;
		private System.Windows.Forms.ContextMenuStrip MenuStrip_Notify;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Notify_Show;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Notify_Settings;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Notify_Quit;
		private System.Windows.Forms.ContextMenuStrip MenuStrip_Icon;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_Notes;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_Notes_EditNotes;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_Notes_Clipboard;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_Notes_Remove;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_Link;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_Link_Edit;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_Link_Clipboard;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_Link_Follow;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_Link_Remove;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_Location;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_Location_Edit;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_Location_Clipboard;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_Location_Maps;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_Location_Remove;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_AddNotes;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_AddLink;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Icon_AddLocation;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Group_Show;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Edit_Quick;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Edit_Advanced;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Item_Edit_Quick;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Item_Edit_Advanced;
		private System.Windows.Forms.Timer Timer_CheckUpdate;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Group_Move_Blank;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Item_Create;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Item_Create_Quick;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Item_Create_Advanced;
		private System.Windows.Forms.ToolStripMenuItem MenuStrip_Item_Move_Blank;
	}
}

