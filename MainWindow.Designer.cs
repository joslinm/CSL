namespace CSL
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ProcessTorrentsButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.uTorrentSendAllButton = new System.Windows.Forms.Button();
            this.dataGridViewProgressBar = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uTorrentSendIndividualButton = new System.Windows.Forms.Button();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ArrowText = new System.Windows.Forms.Label();
            this.Arrow = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.HideSentTorrentsCheckBox = new System.Windows.Forms.CheckBox();
            this.TabbedContainer = new System.Windows.Forms.TabControl();
            this.MusicTab = new System.Windows.Forms.TabPage();
            this.torrentsTableDataGridView = new System.Windows.Forms.DataGridView();
            this.Sent = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MusicOpenWithClient = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Artist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Album = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Save_Structure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bit_Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bit_Format = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Physical_Format = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Release_Format = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.File = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.File_Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Site_Origin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.torrentsTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataset = new CSL.dataset();
            this.MoviesTab = new System.Windows.Forms.TabPage();
            this.moviesTableDataGridView = new System.Windows.Forms.DataGridView();
            this.MovieSent = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MoviesOpenWithClient = new System.Windows.Forms.DataGridViewButtonColumn();
            this.MovieTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MovieYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceMedia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodecFormat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileFormat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MovieSaveStructure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MovieSiteOrigin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MovieFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MovieFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MovieID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.moviesTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.OthersTab = new System.Windows.Forms.TabPage();
            this.othersTableDataGridView = new System.Windows.Forms.DataGridView();
            this.OthersSent = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OpenWithClient = new System.Windows.Forms.DataGridViewButtonColumn();
            this.OthersSiteOrigin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OthersFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OthersSaveStructure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OthersFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OthersID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.othersTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ProcessTimer = new System.Windows.Forms.Label();
            this.torrentsTableTableAdapter = new CSL.datasetTableAdapters.TorrentsTableTableAdapter();
            this.tableAdapterManager = new CSL.datasetTableAdapters.TableAdapterManager();
            this.moviesTableTableAdapter = new CSL.datasetTableAdapters.MoviesTableTableAdapter();
            this.othersTableTableAdapter = new CSL.datasetTableAdapters.OthersTableTableAdapter();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.MainMenu.SuspendLayout();
            this.TabbedContainer.SuspendLayout();
            this.MusicTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.torrentsTableDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.torrentsTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataset)).BeginInit();
            this.MoviesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moviesTableDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moviesTableBindingSource)).BeginInit();
            this.OthersTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.othersTableDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.othersTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1158, 26);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.BackColor = System.Drawing.Color.SteelBlue;
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem2,
            this.optionsToolStripMenuItem1});
            this.optionsToolStripMenuItem.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(60, 22);
            this.optionsToolStripMenuItem.Text = "Main";
            // 
            // optionsToolStripMenuItem2
            // 
            this.optionsToolStripMenuItem2.Name = "optionsToolStripMenuItem2";
            this.optionsToolStripMenuItem2.Size = new System.Drawing.Size(206, 22);
            this.optionsToolStripMenuItem2.Text = "Options...";
            this.optionsToolStripMenuItem2.Click += new System.EventHandler(this.optionsToolStripMenuItem2_Click);
            // 
            // optionsToolStripMenuItem1
            // 
            this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
            this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.optionsToolStripMenuItem1.Text = "View Stats...";
            this.optionsToolStripMenuItem1.Click += new System.EventHandler(this.optionsToolStripMenuItem1_Click);
            // 
            // ProcessTorrentsButton
            // 
            this.ProcessTorrentsButton.BackColor = System.Drawing.Color.SteelBlue;
            this.ProcessTorrentsButton.Font = new System.Drawing.Font("Franklin Gothic Medium", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessTorrentsButton.ForeColor = System.Drawing.Color.Snow;
            this.ProcessTorrentsButton.Location = new System.Drawing.Point(405, 40);
            this.ProcessTorrentsButton.Name = "ProcessTorrentsButton";
            this.ProcessTorrentsButton.Size = new System.Drawing.Size(263, 79);
            this.ProcessTorrentsButton.TabIndex = 2;
            this.ProcessTorrentsButton.Text = "Process Torrents";
            this.ProcessTorrentsButton.UseVisualStyleBackColor = false;
            this.ProcessTorrentsButton.Click += new System.EventHandler(this.ProcessTorrentsButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DeleteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.DeleteButton.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteButton.ForeColor = System.Drawing.Color.Maroon;
            this.DeleteButton.Location = new System.Drawing.Point(1069, 145);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(77, 31);
            this.DeleteButton.TabIndex = 4;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            this.DeleteButton.MouseEnter += new System.EventHandler(this.DeleteButton_MouseEnter);
            this.DeleteButton.MouseLeave += new System.EventHandler(this.DeleteButton_MouseLeave);
            // 
            // uTorrentSendAllButton
            // 
            this.uTorrentSendAllButton.BackColor = System.Drawing.Color.Snow;
            this.uTorrentSendAllButton.Font = new System.Drawing.Font("Franklin Gothic Medium", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uTorrentSendAllButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.uTorrentSendAllButton.Location = new System.Drawing.Point(441, 125);
            this.uTorrentSendAllButton.Name = "uTorrentSendAllButton";
            this.uTorrentSendAllButton.Size = new System.Drawing.Size(191, 52);
            this.uTorrentSendAllButton.TabIndex = 6;
            this.uTorrentSendAllButton.Text = "Send All";
            this.uTorrentSendAllButton.UseVisualStyleBackColor = false;
            this.uTorrentSendAllButton.Click += new System.EventHandler(this.uTorrentSendAllButton_Click);
            // 
            // dataGridViewProgressBar
            // 
            this.dataGridViewProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewProgressBar.Location = new System.Drawing.Point(0, 527);
            this.dataGridViewProgressBar.Name = "dataGridViewProgressBar";
            this.dataGridViewProgressBar.Size = new System.Drawing.Size(1158, 19);
            this.dataGridViewProgressBar.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Location = new System.Drawing.Point(-2, 554);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0, 0);
            this.panel1.TabIndex = 0;
            // 
            // uTorrentSendIndividualButton
            // 
            this.uTorrentSendIndividualButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.uTorrentSendIndividualButton.BackColor = System.Drawing.Color.Snow;
            this.uTorrentSendIndividualButton.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uTorrentSendIndividualButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.uTorrentSendIndividualButton.Location = new System.Drawing.Point(1029, 182);
            this.uTorrentSendIndividualButton.Name = "uTorrentSendIndividualButton";
            this.uTorrentSendIndividualButton.Size = new System.Drawing.Size(117, 31);
            this.uTorrentSendIndividualButton.TabIndex = 7;
            this.uTorrentSendIndividualButton.Text = "Send";
            this.uTorrentSendIndividualButton.UseVisualStyleBackColor = false;
            this.uTorrentSendIndividualButton.Click += new System.EventHandler(this.uTorrentSendIndividualButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(39, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 108);
            this.label1.TabIndex = 8;
            this.label1.Text = "C S L";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RefreshButton.BackColor = System.Drawing.Color.Snow;
            this.RefreshButton.Font = new System.Drawing.Font("Franklin Gothic Medium", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.RefreshButton.Location = new System.Drawing.Point(0, 185);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(67, 31);
            this.RefreshButton.TabIndex = 10;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = false;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "CSL";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // ArrowText
            // 
            this.ArrowText.AutoSize = true;
            this.ArrowText.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ArrowText.ForeColor = System.Drawing.Color.DarkRed;
            this.ArrowText.Location = new System.Drawing.Point(53, 161);
            this.ArrowText.Name = "ArrowText";
            this.ArrowText.Size = new System.Drawing.Size(236, 24);
            this.ArrowText.TabIndex = 11;
            this.ArrowText.Text = "Click here to delete rows";
            // 
            // Arrow
            // 
            this.Arrow.AutoSize = true;
            this.Arrow.Font = new System.Drawing.Font("Franklin Gothic Medium", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Arrow.ForeColor = System.Drawing.Color.DarkRed;
            this.Arrow.Location = new System.Drawing.Point(-5, 104);
            this.Arrow.Name = "Arrow";
            this.Arrow.Size = new System.Drawing.Size(68, 81);
            this.Arrow.TabIndex = 12;
            this.Arrow.Text = "↓";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.StatusLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.ForeColor = System.Drawing.Color.Yellow;
            this.StatusLabel.Location = new System.Drawing.Point(0, 507);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(62, 20);
            this.StatusLabel.TabIndex = 13;
            this.StatusLabel.Text = "Status";
            // 
            // HideSentTorrentsCheckBox
            // 
            this.HideSentTorrentsCheckBox.AutoSize = true;
            this.HideSentTorrentsCheckBox.Checked = true;
            this.HideSentTorrentsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HideSentTorrentsCheckBox.Location = new System.Drawing.Point(73, 200);
            this.HideSentTorrentsCheckBox.Name = "HideSentTorrentsCheckBox";
            this.HideSentTorrentsCheckBox.Size = new System.Drawing.Size(109, 17);
            this.HideSentTorrentsCheckBox.TabIndex = 14;
            this.HideSentTorrentsCheckBox.Text = "Hide sent torrents";
            this.HideSentTorrentsCheckBox.UseVisualStyleBackColor = true;
            this.HideSentTorrentsCheckBox.CheckedChanged += new System.EventHandler(this.HideSentTorrentsCheckBox_CheckedChanged);
            // 
            // TabbedContainer
            // 
            this.TabbedContainer.Controls.Add(this.MusicTab);
            this.TabbedContainer.Controls.Add(this.MoviesTab);
            this.TabbedContainer.Controls.Add(this.OthersTab);
            this.TabbedContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TabbedContainer.Location = new System.Drawing.Point(0, 218);
            this.TabbedContainer.Name = "TabbedContainer";
            this.TabbedContainer.SelectedIndex = 0;
            this.TabbedContainer.Size = new System.Drawing.Size(1158, 289);
            this.TabbedContainer.TabIndex = 15;
            // 
            // MusicTab
            // 
            this.MusicTab.Controls.Add(this.torrentsTableDataGridView);
            this.MusicTab.Location = new System.Drawing.Point(4, 22);
            this.MusicTab.Name = "MusicTab";
            this.MusicTab.Padding = new System.Windows.Forms.Padding(3);
            this.MusicTab.Size = new System.Drawing.Size(1150, 263);
            this.MusicTab.TabIndex = 0;
            this.MusicTab.Text = "Music";
            this.MusicTab.UseVisualStyleBackColor = true;
            // 
            // torrentsTableDataGridView
            // 
            this.torrentsTableDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.LightCyan;
            this.torrentsTableDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.torrentsTableDataGridView.AutoGenerateColumns = false;
            this.torrentsTableDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.torrentsTableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.torrentsTableDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sent,
            this.MusicOpenWithClient,
            this.Artist,
            this.Album,
            this.Save_Structure,
            this.Year,
            this.Bit_Rate,
            this.Bit_Format,
            this.Physical_Format,
            this.Release_Format,
            this.File,
            this.File_Path,
            this.Site_Origin,
            this.ID});
            this.torrentsTableDataGridView.DataSource = this.torrentsTableBindingSource;
            this.torrentsTableDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.torrentsTableDataGridView.Location = new System.Drawing.Point(3, 3);
            this.torrentsTableDataGridView.Name = "torrentsTableDataGridView";
            this.torrentsTableDataGridView.Size = new System.Drawing.Size(1144, 257);
            this.torrentsTableDataGridView.TabIndex = 13;
            // 
            // Sent
            // 
            this.Sent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Sent.DataPropertyName = "Sent";
            this.Sent.HeaderText = "Sent";
            this.Sent.Name = "Sent";
            this.Sent.Width = 35;
            // 
            // MusicOpenWithClient
            // 
            this.MusicOpenWithClient.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.NullValue = "Open with µTorrent";
            this.MusicOpenWithClient.DefaultCellStyle = dataGridViewCellStyle14;
            this.MusicOpenWithClient.HeaderText = "Open File";
            this.MusicOpenWithClient.Name = "MusicOpenWithClient";
            this.MusicOpenWithClient.Width = 52;
            // 
            // Artist
            // 
            this.Artist.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Artist.DataPropertyName = "Artist";
            this.Artist.HeaderText = "Artist";
            this.Artist.Name = "Artist";
            // 
            // Album
            // 
            this.Album.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Album.DataPropertyName = "Album";
            this.Album.HeaderText = "Album";
            this.Album.Name = "Album";
            // 
            // Save_Structure
            // 
            this.Save_Structure.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Save_Structure.DataPropertyName = "Save Structure";
            this.Save_Structure.HeaderText = "Save Structure";
            this.Save_Structure.MinimumWidth = 10;
            this.Save_Structure.Name = "Save_Structure";
            this.Save_Structure.Width = 200;
            // 
            // Year
            // 
            this.Year.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Year.DataPropertyName = "Year";
            this.Year.HeaderText = "Year";
            this.Year.Name = "Year";
            this.Year.Width = 54;
            // 
            // Bit_Rate
            // 
            this.Bit_Rate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Bit_Rate.DataPropertyName = "Bit Rate";
            this.Bit_Rate.HeaderText = "Bit Rate";
            this.Bit_Rate.Name = "Bit_Rate";
            this.Bit_Rate.Width = 65;
            // 
            // Bit_Format
            // 
            this.Bit_Format.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Bit_Format.DataPropertyName = "Bit Format";
            this.Bit_Format.HeaderText = "Bit Format";
            this.Bit_Format.Name = "Bit_Format";
            this.Bit_Format.Width = 73;
            // 
            // Physical_Format
            // 
            this.Physical_Format.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Physical_Format.DataPropertyName = "Physical Format";
            this.Physical_Format.HeaderText = "Physical Format";
            this.Physical_Format.Name = "Physical_Format";
            this.Physical_Format.Width = 97;
            // 
            // Release_Format
            // 
            this.Release_Format.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Release_Format.DataPropertyName = "Release Format";
            this.Release_Format.HeaderText = "Release Format";
            this.Release_Format.Name = "Release_Format";
            this.Release_Format.Width = 97;
            // 
            // File
            // 
            this.File.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.File.DataPropertyName = "File";
            this.File.HeaderText = "File";
            this.File.MinimumWidth = 75;
            this.File.Name = "File";
            // 
            // File_Path
            // 
            this.File_Path.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.File_Path.DataPropertyName = "File Path";
            this.File_Path.HeaderText = "File Path";
            this.File_Path.MinimumWidth = 106;
            this.File_Path.Name = "File_Path";
            // 
            // Site_Origin
            // 
            this.Site_Origin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Site_Origin.DataPropertyName = "Site Origin";
            this.Site_Origin.HeaderText = "Site Origin";
            this.Site_Origin.Name = "Site_Origin";
            this.Site_Origin.Width = 74;
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            // 
            // torrentsTableBindingSource
            // 
            this.torrentsTableBindingSource.DataMember = "TorrentsTable";
            this.torrentsTableBindingSource.DataSource = this.dataset;
            // 
            // dataset
            // 
            this.dataset.DataSetName = "dataset";
            this.dataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // MoviesTab
            // 
            this.MoviesTab.AutoScroll = true;
            this.MoviesTab.Controls.Add(this.moviesTableDataGridView);
            this.MoviesTab.Location = new System.Drawing.Point(4, 22);
            this.MoviesTab.Name = "MoviesTab";
            this.MoviesTab.Padding = new System.Windows.Forms.Padding(3);
            this.MoviesTab.Size = new System.Drawing.Size(1150, 263);
            this.MoviesTab.TabIndex = 1;
            this.MoviesTab.Text = "Movies";
            this.MoviesTab.UseVisualStyleBackColor = true;
            // 
            // moviesTableDataGridView
            // 
            this.moviesTableDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.LightCyan;
            this.moviesTableDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle15;
            this.moviesTableDataGridView.AutoGenerateColumns = false;
            this.moviesTableDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.moviesTableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.moviesTableDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MovieSent,
            this.MoviesOpenWithClient,
            this.MovieTitle,
            this.MovieYear,
            this.SourceMedia,
            this.CodecFormat,
            this.FileFormat,
            this.MovieSaveStructure,
            this.MovieSiteOrigin,
            this.MovieFilePath,
            this.MovieFile,
            this.MovieID});
            this.moviesTableDataGridView.DataSource = this.moviesTableBindingSource;
            this.moviesTableDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moviesTableDataGridView.Location = new System.Drawing.Point(3, 3);
            this.moviesTableDataGridView.Name = "moviesTableDataGridView";
            this.moviesTableDataGridView.Size = new System.Drawing.Size(1144, 257);
            this.moviesTableDataGridView.TabIndex = 0;
            // 
            // MovieSent
            // 
            this.MovieSent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MovieSent.DataPropertyName = "Sent";
            this.MovieSent.HeaderText = "Sent";
            this.MovieSent.Name = "MovieSent";
            this.MovieSent.Width = 35;
            // 
            // MoviesOpenWithClient
            // 
            this.MoviesOpenWithClient.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.NullValue = "Open with µTorrent";
            this.MoviesOpenWithClient.DefaultCellStyle = dataGridViewCellStyle16;
            this.MoviesOpenWithClient.HeaderText = "Open File";
            this.MoviesOpenWithClient.Name = "MoviesOpenWithClient";
            this.MoviesOpenWithClient.Width = 52;
            // 
            // MovieTitle
            // 
            this.MovieTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MovieTitle.DataPropertyName = "Movie Title";
            this.MovieTitle.HeaderText = "Movie Title";
            this.MovieTitle.Name = "MovieTitle";
            // 
            // MovieYear
            // 
            this.MovieYear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MovieYear.DataPropertyName = "Year";
            this.MovieYear.HeaderText = "Year";
            this.MovieYear.Name = "MovieYear";
            this.MovieYear.Width = 54;
            // 
            // SourceMedia
            // 
            this.SourceMedia.DataPropertyName = "Source Media";
            this.SourceMedia.HeaderText = "Source Media";
            this.SourceMedia.Name = "SourceMedia";
            // 
            // CodecFormat
            // 
            this.CodecFormat.DataPropertyName = "Codec Format";
            this.CodecFormat.HeaderText = "Codec Format";
            this.CodecFormat.Name = "CodecFormat";
            // 
            // FileFormat
            // 
            this.FileFormat.DataPropertyName = "File Format";
            this.FileFormat.HeaderText = "File Format";
            this.FileFormat.Name = "FileFormat";
            // 
            // MovieSaveStructure
            // 
            this.MovieSaveStructure.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MovieSaveStructure.DataPropertyName = "Save Structure";
            this.MovieSaveStructure.HeaderText = "Save Structure";
            this.MovieSaveStructure.Name = "MovieSaveStructure";
            // 
            // MovieSiteOrigin
            // 
            this.MovieSiteOrigin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MovieSiteOrigin.DataPropertyName = "Site Origin";
            this.MovieSiteOrigin.HeaderText = "Site Origin";
            this.MovieSiteOrigin.Name = "MovieSiteOrigin";
            this.MovieSiteOrigin.Width = 74;
            // 
            // MovieFilePath
            // 
            this.MovieFilePath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MovieFilePath.DataPropertyName = "File Path";
            this.MovieFilePath.HeaderText = "File Path";
            this.MovieFilePath.Name = "MovieFilePath";
            // 
            // MovieFile
            // 
            this.MovieFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MovieFile.DataPropertyName = "File";
            this.MovieFile.HeaderText = "File";
            this.MovieFile.Name = "MovieFile";
            // 
            // MovieID
            // 
            this.MovieID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MovieID.DataPropertyName = "ID";
            this.MovieID.HeaderText = "ID";
            this.MovieID.Name = "MovieID";
            // 
            // moviesTableBindingSource
            // 
            this.moviesTableBindingSource.DataMember = "MoviesTable";
            this.moviesTableBindingSource.DataSource = this.dataset;
            // 
            // OthersTab
            // 
            this.OthersTab.AutoScroll = true;
            this.OthersTab.Controls.Add(this.othersTableDataGridView);
            this.OthersTab.Location = new System.Drawing.Point(4, 22);
            this.OthersTab.Name = "OthersTab";
            this.OthersTab.Size = new System.Drawing.Size(1150, 263);
            this.OthersTab.TabIndex = 2;
            this.OthersTab.Text = "Other";
            this.OthersTab.UseVisualStyleBackColor = true;
            // 
            // othersTableDataGridView
            // 
            this.othersTableDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.LightCyan;
            this.othersTableDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            this.othersTableDataGridView.AutoGenerateColumns = false;
            this.othersTableDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.othersTableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.othersTableDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OthersSent,
            this.OpenWithClient,
            this.OthersSiteOrigin,
            this.OthersFile,
            this.OthersSaveStructure,
            this.OthersFilePath,
            this.OthersID});
            this.othersTableDataGridView.DataSource = this.othersTableBindingSource;
            this.othersTableDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.othersTableDataGridView.Location = new System.Drawing.Point(0, 0);
            this.othersTableDataGridView.Name = "othersTableDataGridView";
            this.othersTableDataGridView.Size = new System.Drawing.Size(1150, 263);
            this.othersTableDataGridView.TabIndex = 0;
            // 
            // OthersSent
            // 
            this.OthersSent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.OthersSent.DataPropertyName = "Sent";
            this.OthersSent.HeaderText = "Sent";
            this.OthersSent.Name = "OthersSent";
            this.OthersSent.Width = 35;
            // 
            // OpenWithClient
            // 
            this.OpenWithClient.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.OpenWithClient.DataPropertyName = "OpenWithClient";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.NullValue = "Open with µTorrent";
            this.OpenWithClient.DefaultCellStyle = dataGridViewCellStyle18;
            this.OpenWithClient.HeaderText = "Open File";
            this.OpenWithClient.Name = "OpenWithClient";
            this.OpenWithClient.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OpenWithClient.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.OpenWithClient.Width = 77;
            // 
            // OthersSiteOrigin
            // 
            this.OthersSiteOrigin.DataPropertyName = "Site Origin";
            this.OthersSiteOrigin.HeaderText = "Site Origin";
            this.OthersSiteOrigin.Name = "OthersSiteOrigin";
            // 
            // OthersFile
            // 
            this.OthersFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OthersFile.DataPropertyName = "File";
            this.OthersFile.HeaderText = "File";
            this.OthersFile.Name = "OthersFile";
            // 
            // OthersSaveStructure
            // 
            this.OthersSaveStructure.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OthersSaveStructure.DataPropertyName = "Save Structure";
            this.OthersSaveStructure.HeaderText = "Save Structure";
            this.OthersSaveStructure.Name = "OthersSaveStructure";
            // 
            // OthersFilePath
            // 
            this.OthersFilePath.DataPropertyName = "File Path";
            this.OthersFilePath.HeaderText = "File Path";
            this.OthersFilePath.Name = "OthersFilePath";
            // 
            // OthersID
            // 
            this.OthersID.DataPropertyName = "ID";
            this.OthersID.HeaderText = "ID";
            this.OthersID.Name = "OthersID";
            // 
            // othersTableBindingSource
            // 
            this.othersTableBindingSource.DataMember = "OthersTable";
            this.othersTableBindingSource.DataSource = this.dataset;
            // 
            // ProcessTimer
            // 
            this.ProcessTimer.AutoSize = true;
            this.ProcessTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessTimer.Location = new System.Drawing.Point(674, 110);
            this.ProcessTimer.Name = "ProcessTimer";
            this.ProcessTimer.Size = new System.Drawing.Size(37, 9);
            this.ProcessTimer.TabIndex = 16;
            this.ProcessTimer.Text = "0.00 secs";
            // 
            // torrentsTableTableAdapter
            // 
            this.torrentsTableTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.MoviesTableTableAdapter = null;
            this.tableAdapterManager.OthersTableTableAdapter = null;
            this.tableAdapterManager.TorrentsTableTableAdapter = this.torrentsTableTableAdapter;
            this.tableAdapterManager.UpdateOrder = CSL.datasetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.UserStatsTableTableAdapter = null;
            // 
            // moviesTableTableAdapter
            // 
            this.moviesTableTableAdapter.ClearBeforeFill = true;
            // 
            // othersTableTableAdapter
            // 
            this.othersTableTableAdapter.ClearBeforeFill = true;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewButtonColumn1.DataPropertyName = "OpenWithClient";
            this.dataGridViewButtonColumn1.HeaderText = "Open w/ µTorrent";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.ReadOnly = true;
            this.dataGridViewButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewButtonColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewButtonColumn1.Text = "Open w/ µTorrent";
            this.dataGridViewButtonColumn1.ToolTipText = "Open w/ µTorrent";
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1158, 546);
            this.Controls.Add(this.ProcessTimer);
            this.Controls.Add(this.ArrowText);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.uTorrentSendIndividualButton);
            this.Controls.Add(this.uTorrentSendAllButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.ProcessTorrentsButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Arrow);
            this.Controls.Add(this.HideSentTorrentsCheckBox);
            this.Controls.Add(this.TabbedContainer);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.dataGridViewProgressBar);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.MinimumSize = new System.Drawing.Size(1088, 457);
            this.Name = "MainWindow";
            this.Text = "CSL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragEnter);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.TabbedContainer.ResumeLayout(false);
            this.MusicTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.torrentsTableDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.torrentsTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataset)).EndInit();
            this.MoviesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.moviesTableDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moviesTableBindingSource)).EndInit();
            this.OthersTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.othersTableDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.othersTableBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
        private System.Windows.Forms.Button ProcessTorrentsButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button uTorrentSendAllButton;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ProgressBar dataGridViewProgressBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button uTorrentSendIndividualButton;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label ArrowText;
        private System.Windows.Forms.Label Arrow;
        private System.Windows.Forms.Label StatusLabel;
        private dataset dataset;
        private System.Windows.Forms.BindingSource torrentsTableBindingSource;
        private datasetTableAdapters.TorrentsTableTableAdapter torrentsTableTableAdapter;
        private datasetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.CheckBox HideSentTorrentsCheckBox;
        private System.Windows.Forms.TabControl TabbedContainer;
        private System.Windows.Forms.TabPage MusicTab;
        private System.Windows.Forms.TabPage MoviesTab;
        private System.Windows.Forms.BindingSource moviesTableBindingSource;
        private datasetTableAdapters.MoviesTableTableAdapter moviesTableTableAdapter;
        private System.Windows.Forms.DataGridView torrentsTableDataGridView;
        private System.Windows.Forms.DataGridView moviesTableDataGridView;
        private System.Windows.Forms.Label ProcessTimer;
        private System.Windows.Forms.TabPage OthersTab;
        private System.Windows.Forms.BindingSource othersTableBindingSource;
        private datasetTableAdapters.OthersTableTableAdapter othersTableTableAdapter;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridView othersTableDataGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Sent;
        private System.Windows.Forms.DataGridViewButtonColumn MusicOpenWithClient;
        private System.Windows.Forms.DataGridViewTextBoxColumn Artist;
        private System.Windows.Forms.DataGridViewTextBoxColumn Album;
        private System.Windows.Forms.DataGridViewTextBoxColumn Save_Structure;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bit_Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bit_Format;
        private System.Windows.Forms.DataGridViewTextBoxColumn Physical_Format;
        private System.Windows.Forms.DataGridViewTextBoxColumn Release_Format;
        private System.Windows.Forms.DataGridViewTextBoxColumn File;
        private System.Windows.Forms.DataGridViewTextBoxColumn File_Path;
        private System.Windows.Forms.DataGridViewTextBoxColumn Site_Origin;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MovieSent;
        private System.Windows.Forms.DataGridViewButtonColumn MoviesOpenWithClient;
        private System.Windows.Forms.DataGridViewTextBoxColumn MovieTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn MovieYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceMedia;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodecFormat;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileFormat;
        private System.Windows.Forms.DataGridViewTextBoxColumn MovieSaveStructure;
        private System.Windows.Forms.DataGridViewTextBoxColumn MovieSiteOrigin;
        private System.Windows.Forms.DataGridViewTextBoxColumn MovieFilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn MovieFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn MovieID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn OthersSent;
        private System.Windows.Forms.DataGridViewButtonColumn OpenWithClient;
        private System.Windows.Forms.DataGridViewTextBoxColumn OthersSiteOrigin;
        private System.Windows.Forms.DataGridViewTextBoxColumn OthersFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn OthersSaveStructure;
        private System.Windows.Forms.DataGridViewTextBoxColumn OthersFilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn OthersID;

    }
}