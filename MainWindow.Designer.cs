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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.TorrentDataSet = new System.Data.DataSet();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ProcessTorrentsButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.uTorrentSendAllButton = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewProgressBar = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.uTorrentSendIndividualButton = new System.Windows.Forms.Button();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TorrentDataSet)).BeginInit();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // TorrentDataSet
            // 
            this.TorrentDataSet.DataSetName = "NewDataSet";
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.Color.Gainsboro;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1072, 26);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.BackColor = System.Drawing.Color.SteelBlue;
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem1,
            this.optionsToolStripMenuItem2});
            this.optionsToolStripMenuItem.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(60, 22);
            this.optionsToolStripMenuItem.Text = "Main";
            // 
            // optionsToolStripMenuItem1
            // 
            this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
            this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(206, 22);
            this.optionsToolStripMenuItem1.Text = "View Stats...";
            this.optionsToolStripMenuItem1.Click += new System.EventHandler(this.optionsToolStripMenuItem1_Click);
            // 
            // optionsToolStripMenuItem2
            // 
            this.optionsToolStripMenuItem2.Name = "optionsToolStripMenuItem2";
            this.optionsToolStripMenuItem2.Size = new System.Drawing.Size(206, 22);
            this.optionsToolStripMenuItem2.Text = "Options...";
            this.optionsToolStripMenuItem2.Click += new System.EventHandler(this.optionsToolStripMenuItem2_Click);
            // 
            // ProcessTorrentsButton
            // 
            this.ProcessTorrentsButton.BackColor = System.Drawing.Color.SteelBlue;
            this.ProcessTorrentsButton.Font = new System.Drawing.Font("Franklin Gothic Medium", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessTorrentsButton.ForeColor = System.Drawing.Color.Snow;
            this.ProcessTorrentsButton.Location = new System.Drawing.Point(406, 40);
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
            this.DeleteButton.Location = new System.Drawing.Point(983, 113);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(77, 31);
            this.DeleteButton.TabIndex = 4;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // uTorrentSendAllButton
            // 
            this.uTorrentSendAllButton.BackColor = System.Drawing.Color.Snow;
            this.uTorrentSendAllButton.Font = new System.Drawing.Font("Franklin Gothic Medium", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uTorrentSendAllButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.uTorrentSendAllButton.Location = new System.Drawing.Point(443, 125);
            this.uTorrentSendAllButton.Name = "uTorrentSendAllButton";
            this.uTorrentSendAllButton.Size = new System.Drawing.Size(191, 52);
            this.uTorrentSendAllButton.TabIndex = 6;
            this.uTorrentSendAllButton.Text = "Send All";
            this.uTorrentSendAllButton.UseVisualStyleBackColor = false;
            this.uTorrentSendAllButton.Click += new System.EventHandler(this.uTorrentSendAllButton_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowDrop = true;
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridView.Location = new System.Drawing.Point(0, 187);
            this.dataGridView.Name = "dataGridView";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView.Size = new System.Drawing.Size(1077, 233);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView_DragEnter);
            this.dataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_CurrentCellDirtyStateChanged);
            this.dataGridView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyUp);
            this.dataGridView.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView_DragDrop);
            // 
            // dataGridViewProgressBar
            // 
            this.dataGridViewProgressBar.Location = new System.Drawing.Point(0, 387);
            this.dataGridViewProgressBar.Name = "dataGridViewProgressBar";
            this.dataGridViewProgressBar.Size = new System.Drawing.Size(607, 33);
            this.dataGridViewProgressBar.TabIndex = 1;
            this.dataGridViewProgressBar.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Location = new System.Drawing.Point(-2, 430);
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
            this.uTorrentSendIndividualButton.Location = new System.Drawing.Point(943, 150);
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
            this.RefreshButton.Location = new System.Drawing.Point(0, 151);
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
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1072, 419);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.uTorrentSendIndividualButton);
            this.Controls.Add(this.dataGridViewProgressBar);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.uTorrentSendAllButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.ProcessTorrentsButton);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainWindow";
            this.Text = "CSL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.TorrentDataSet)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
        private System.Data.DataSet TorrentDataSet;
        private System.Windows.Forms.Button ProcessTorrentsButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button uTorrentSendAllButton;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ProgressBar dataGridViewProgressBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button uTorrentSendIndividualButton;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;

    }
}