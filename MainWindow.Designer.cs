namespace CSL_Test__1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.dataset = new CSL_Test__1.dataset();
            this.MoviesTab = new System.Windows.Forms.TabPage();
            this.moviesTableDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.moviesTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.torrentsTableTableAdapter = new CSL_Test__1.datasetTableAdapters.TorrentsTableTableAdapter();
            this.tableAdapterManager = new CSL_Test__1.datasetTableAdapters.TableAdapterManager();
            this.moviesTableTableAdapter = new CSL_Test__1.datasetTableAdapters.MoviesTableTableAdapter();
            this.ProcessTimer = new System.Windows.Forms.Label();
            this.MainMenu.SuspendLayout();
            this.TabbedContainer.SuspendLayout();
            this.MusicTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.torrentsTableDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.torrentsTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataset)).BeginInit();
            this.MoviesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moviesTableDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moviesTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1132, 26);
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
            this.DeleteButton.Location = new System.Drawing.Point(1043, 152);
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
            this.dataGridViewProgressBar.Location = new System.Drawing.Point(0, 493);
            this.dataGridViewProgressBar.Name = "dataGridViewProgressBar";
            this.dataGridViewProgressBar.Size = new System.Drawing.Size(1132, 19);
            this.dataGridViewProgressBar.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Location = new System.Drawing.Point(-2, 516);
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
            this.uTorrentSendIndividualButton.Location = new System.Drawing.Point(1003, 189);
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
            this.RefreshButton.Location = new System.Drawing.Point(0, 166);
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
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.ForeColor = System.Drawing.Color.Yellow;
            this.StatusLabel.Location = new System.Drawing.Point(-4, 491);
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
            this.HideSentTorrentsCheckBox.Location = new System.Drawing.Point(69, 168);
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
            this.TabbedContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TabbedContainer.Location = new System.Drawing.Point(0, 204);
            this.TabbedContainer.Name = "TabbedContainer";
            this.TabbedContainer.SelectedIndex = 0;
            this.TabbedContainer.Size = new System.Drawing.Size(1132, 289);
            this.TabbedContainer.TabIndex = 15;
            // 
            // MusicTab
            // 
            this.MusicTab.Controls.Add(this.torrentsTableDataGridView);
            this.MusicTab.Location = new System.Drawing.Point(4, 22);
            this.MusicTab.Name = "MusicTab";
            this.MusicTab.Padding = new System.Windows.Forms.Padding(3);
            this.MusicTab.Size = new System.Drawing.Size(1124, 263);
            this.MusicTab.TabIndex = 0;
            this.MusicTab.Text = "Music";
            this.MusicTab.UseVisualStyleBackColor = true;
            // 
            // torrentsTableDataGridView
            // 
            this.torrentsTableDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightCyan;
            this.torrentsTableDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.torrentsTableDataGridView.AutoGenerateColumns = false;
            this.torrentsTableDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.torrentsTableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.torrentsTableDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sent,
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
            this.torrentsTableDataGridView.Size = new System.Drawing.Size(1118, 257);
            this.torrentsTableDataGridView.TabIndex = 13;
            // 
            // Sent
            // 
            this.Sent.DataPropertyName = "Sent";
            this.Sent.HeaderText = "Sent";
            this.Sent.Name = "Sent";
            // 
            // Artist
            // 
            this.Artist.DataPropertyName = "Artist";
            this.Artist.HeaderText = "Artist";
            this.Artist.Name = "Artist";
            // 
            // Album
            // 
            this.Album.DataPropertyName = "Album";
            this.Album.HeaderText = "Album";
            this.Album.Name = "Album";
            // 
            // Save_Structure
            // 
            this.Save_Structure.DataPropertyName = "Save Structure";
            this.Save_Structure.HeaderText = "Save Structure";
            this.Save_Structure.Name = "Save_Structure";
            // 
            // Year
            // 
            this.Year.DataPropertyName = "Year";
            this.Year.HeaderText = "Year";
            this.Year.Name = "Year";
            // 
            // Bit_Rate
            // 
            this.Bit_Rate.DataPropertyName = "Bit Rate";
            this.Bit_Rate.HeaderText = "Bit Rate";
            this.Bit_Rate.Name = "Bit_Rate";
            // 
            // Bit_Format
            // 
            this.Bit_Format.DataPropertyName = "Bit Format";
            this.Bit_Format.HeaderText = "Bit Format";
            this.Bit_Format.Name = "Bit_Format";
            // 
            // Physical_Format
            // 
            this.Physical_Format.DataPropertyName = "Physical Format";
            this.Physical_Format.HeaderText = "Physical Format";
            this.Physical_Format.Name = "Physical_Format";
            // 
            // Release_Format
            // 
            this.Release_Format.DataPropertyName = "Release Format";
            this.Release_Format.HeaderText = "Release Format";
            this.Release_Format.Name = "Release_Format";
            // 
            // File
            // 
            this.File.DataPropertyName = "File";
            this.File.HeaderText = "File";
            this.File.Name = "File";
            // 
            // File_Path
            // 
            this.File_Path.DataPropertyName = "File Path";
            this.File_Path.HeaderText = "File Path";
            this.File_Path.Name = "File_Path";
            // 
            // Site_Origin
            // 
            this.Site_Origin.DataPropertyName = "Site Origin";
            this.Site_Origin.HeaderText = "Site Origin";
            this.Site_Origin.Name = "Site_Origin";
            // 
            // ID
            // 
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
            this.MoviesTab.Size = new System.Drawing.Size(1124, 275);
            this.MoviesTab.TabIndex = 1;
            this.MoviesTab.Text = "Movies";
            this.MoviesTab.UseVisualStyleBackColor = true;
            // 
            // moviesTableDataGridView
            // 
            this.moviesTableDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightCyan;
            this.moviesTableDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.moviesTableDataGridView.AutoGenerateColumns = false;
            this.moviesTableDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.moviesTableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.moviesTableDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn7});
            this.moviesTableDataGridView.DataSource = this.moviesTableBindingSource;
            this.moviesTableDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moviesTableDataGridView.Location = new System.Drawing.Point(3, 3);
            this.moviesTableDataGridView.Name = "moviesTableDataGridView";
            this.moviesTableDataGridView.Size = new System.Drawing.Size(1118, 269);
            this.moviesTableDataGridView.TabIndex = 0;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "Sent";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Sent";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Movie Title";
            this.dataGridViewTextBoxColumn1.HeaderText = "Movie Title";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Year";
            this.dataGridViewTextBoxColumn2.HeaderText = "Year";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Source Media";
            this.dataGridViewTextBoxColumn3.HeaderText = "Source Media";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Codec Format";
            this.dataGridViewTextBoxColumn4.HeaderText = "Codec Format";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "File Format";
            this.dataGridViewTextBoxColumn5.HeaderText = "File Format";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Save Structure";
            this.dataGridViewTextBoxColumn8.HeaderText = "Save Structure";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Site Origin";
            this.dataGridViewTextBoxColumn6.HeaderText = "Site Origin";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "File Path";
            this.dataGridViewTextBoxColumn9.HeaderText = "File Path";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "File";
            this.dataGridViewTextBoxColumn10.HeaderText = "File";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "ID";
            this.dataGridViewTextBoxColumn7.HeaderText = "ID";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // moviesTableBindingSource
            // 
            this.moviesTableBindingSource.DataMember = "MoviesTable";
            this.moviesTableBindingSource.DataSource = this.dataset;
            // 
            // torrentsTableTableAdapter
            // 
            this.torrentsTableTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.MoviesTableTableAdapter = null;
            this.tableAdapterManager.TorrentsTableTableAdapter = this.torrentsTableTableAdapter;
            this.tableAdapterManager.UpdateOrder = CSL_Test__1.datasetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // moviesTableTableAdapter
            // 
            this.moviesTableTableAdapter.ClearBeforeFill = true;
            // 
            // ProcessTimer
            // 
            this.ProcessTimer.AutoSize = true;
            this.ProcessTimer.Location = new System.Drawing.Point(674, 104);
            this.ProcessTimer.Name = "ProcessTimer";
            this.ProcessTimer.Size = new System.Drawing.Size(53, 13);
            this.ProcessTimer.TabIndex = 16;
            this.ProcessTimer.Text = "0.00 secs";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1132, 512);
            this.Controls.Add(this.StatusLabel);
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
            this.Controls.Add(this.dataGridViewProgressBar);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.MinimumSize = new System.Drawing.Size(1088, 457);
            this.Name = "MainWindow";
            this.Text = "CSL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
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
        private System.Windows.Forms.DataGridViewCheckBoxColumn Sent;
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
        private System.Windows.Forms.DataGridView moviesTableDataGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.Label ProcessTimer;

    }
}