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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.torrentsTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataset = new CSL_Test__1.dataset();
            this.torrentsTableTableAdapter = new CSL_Test__1.datasetTableAdapters.TorrentsTableTableAdapter();
            this.tableAdapterManager = new CSL_Test__1.datasetTableAdapters.TableAdapterManager();
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
            this.HideSentTorrentsCheckBox = new System.Windows.Forms.CheckBox();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.torrentsTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.torrentsTableDataGridView)).BeginInit();
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
            this.DeleteButton.Location = new System.Drawing.Point(1043, 117);
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
            this.dataGridViewProgressBar.Location = new System.Drawing.Point(0, 440);
            this.dataGridViewProgressBar.Name = "dataGridViewProgressBar";
            this.dataGridViewProgressBar.Size = new System.Drawing.Size(1132, 40);
            this.dataGridViewProgressBar.TabIndex = 1;
            this.dataGridViewProgressBar.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Location = new System.Drawing.Point(-2, 491);
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
            this.uTorrentSendIndividualButton.Location = new System.Drawing.Point(1003, 154);
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
            this.RefreshButton.Location = new System.Drawing.Point(0, 154);
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
            this.StatusLabel.Location = new System.Drawing.Point(1, 417);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(62, 20);
            this.StatusLabel.TabIndex = 13;
            this.StatusLabel.Text = "Status";
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
            // torrentsTableTableAdapter
            // 
            this.torrentsTableTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.TorrentsTableTableAdapter = this.torrentsTableTableAdapter;
            this.tableAdapterManager.UpdateOrder = CSL_Test__1.datasetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // torrentsTableDataGridView
            // 
            this.torrentsTableDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightCyan;
            this.torrentsTableDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
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
            this.torrentsTableDataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.torrentsTableDataGridView.Location = new System.Drawing.Point(0, 191);
            this.torrentsTableDataGridView.Name = "torrentsTableDataGridView";
            this.torrentsTableDataGridView.Size = new System.Drawing.Size(1132, 289);
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
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1132, 480);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.ArrowText);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.uTorrentSendIndividualButton);
            this.Controls.Add(this.dataGridViewProgressBar);
            this.Controls.Add(this.uTorrentSendAllButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.ProcessTorrentsButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Arrow);
            this.Controls.Add(this.torrentsTableDataGridView);
            this.Controls.Add(this.HideSentTorrentsCheckBox);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.MinimumSize = new System.Drawing.Size(1088, 457);
            this.Name = "MainWindow";
            this.Text = "CSL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.torrentsTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.torrentsTableDataGridView)).EndInit();
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
        private System.Windows.Forms.CheckBox HideSentTorrentsCheckBox;

    }
}