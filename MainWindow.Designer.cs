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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewProgressBar = new System.Windows.Forms.ProgressBar();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.TorrentDataSet = new System.Data.DataSet();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ProcessTorrentsButton = new System.Windows.Forms.Button();
            this.AutoProcessCheckBox = new System.Windows.Forms.CheckBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.uTorrentSendAllButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TorrentDataSet)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridViewProgressBar);
            this.panel1.Controls.Add(this.dataGridView);
            this.panel1.Location = new System.Drawing.Point(-2, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1111, 215);
            this.panel1.TabIndex = 0;
            // 
            // dataGridViewProgressBar
            // 
            this.dataGridViewProgressBar.Location = new System.Drawing.Point(817, 201);
            this.dataGridViewProgressBar.Name = "dataGridViewProgressBar";
            this.dataGridViewProgressBar.Size = new System.Drawing.Size(294, 14);
            this.dataGridViewProgressBar.TabIndex = 1;
            this.dataGridViewProgressBar.Visible = false;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowDrop = true;
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(1111, 215);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView_DragEnter);
            this.dataGridView.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView_DragDrop);
            // 
            // TorrentDataSet
            // 
            this.TorrentDataSet.DataSetName = "NewDataSet";
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1109, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem1});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // optionsToolStripMenuItem1
            // 
            this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
            this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(125, 22);
            this.optionsToolStripMenuItem1.Text = "Options...";
            // 
            // ProcessTorrentsButton
            // 
            this.ProcessTorrentsButton.Location = new System.Drawing.Point(443, 43);
            this.ProcessTorrentsButton.Name = "ProcessTorrentsButton";
            this.ProcessTorrentsButton.Size = new System.Drawing.Size(191, 52);
            this.ProcessTorrentsButton.TabIndex = 2;
            this.ProcessTorrentsButton.Text = "Process Torrents";
            this.ProcessTorrentsButton.UseVisualStyleBackColor = true;
            // 
            // AutoProcessCheckBox
            // 
            this.AutoProcessCheckBox.AutoSize = true;
            this.AutoProcessCheckBox.Location = new System.Drawing.Point(640, 78);
            this.AutoProcessCheckBox.Name = "AutoProcessCheckBox";
            this.AutoProcessCheckBox.Size = new System.Drawing.Size(131, 17);
            this.AutoProcessCheckBox.TabIndex = 3;
            this.AutoProcessCheckBox.Text = "Auto-Process Torrents";
            this.AutoProcessCheckBox.UseVisualStyleBackColor = true;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(980, 162);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(117, 32);
            this.DeleteButton.TabIndex = 4;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // uTorrentSendAllButton
            // 
            this.uTorrentSendAllButton.Location = new System.Drawing.Point(443, 105);
            this.uTorrentSendAllButton.Name = "uTorrentSendAllButton";
            this.uTorrentSendAllButton.Size = new System.Drawing.Size(191, 52);
            this.uTorrentSendAllButton.TabIndex = 6;
            this.uTorrentSendAllButton.Text = "Send All Torrents";
            this.uTorrentSendAllButton.UseVisualStyleBackColor = true;
            this.uTorrentSendAllButton.Click += new System.EventHandler(this.uTorrentSendAllButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 417);
            this.Controls.Add(this.uTorrentSendAllButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AutoProcessCheckBox);
            this.Controls.Add(this.ProcessTorrentsButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainWindow";
            this.Text = "CSL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TorrentDataSet)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
        private System.Data.DataSet TorrentDataSet;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button ProcessTorrentsButton;
        private System.Windows.Forms.CheckBox AutoProcessCheckBox;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button uTorrentSendAllButton;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ProgressBar dataGridViewProgressBar;

    }
}