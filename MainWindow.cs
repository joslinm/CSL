using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Data;
using System.Threading;
using System.Collections.Generic;

namespace CSL_Test__1
{
    public partial class MainWindow : Form
    {

        #region Local Variables
        TorrentBuilder tb = new TorrentBuilder();
        OptionsWindow ow = new OptionsWindow();
        BWHandler bw;

        static System.Timers.Timer timer = new System.Timers.Timer();
        static System.Timers.Timer internaltimer = new System.Timers.Timer();

        DataGridViewHandler dgvh;
        List<FileInfo> items;

        string[] files;
        string[] torrents;
        string[] zips;

        delegate void ProcessTorrentsInvoker();
        delegate void RefreshInvoker();

        private Object lockingobject = new Object(); 
        #endregion

        public MainWindow()
        {
            
            InitializeComponent();
            /*
            //TorrentXMLHandler.Initialize(); //Initialize data source (read XML file)
            dgvh = new DataGridViewHandler(dataGridView);
            bw = new BWHandler(dataGridViewProgressBar, StatusLabel);

            internaltimer.Interval = 2;
            internaltimer.Elapsed += new System.Timers.ElapsedEventHandler(internaltimer_Elapsed);

            timer.Interval = (double)SettingsHandler.GetAutoHandleTime();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            if (SettingsHandler.GetAutoHandleBool())
                timer.Start();

            Arrow.Visible = false;
            StatusLabel.Visible = false;
            ArrowText.Visible = false;
            notifyIcon.Visible = false;

            try
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    System.Deployment.Application.ApplicationDeployment ad =
                    System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                    string version = ad.CurrentVersion.ToString();

                    if (SettingsHandler.GetCurrentVersion().CompareTo(version) < 0)
                    {
                        UpdatedInformationWindow uw = new UpdatedInformationWindow();
                        uw.ShowDialog();
                        SettingsHandler.SetCurrentVersion(version);
                    }
                }
            }
            catch (Exception) { }
            */
            //test();
            CSLDataSet.CSLDataTableRow row = cSLDataSet.CSLDataTable.NewCSLDataTableRow();

                row.File_ = "file";
                row.Artist = "artist11";
                row.Album = "album";
                row.Save_Structure = "save";
                row.Sent = false;
                row.Error = true;
                row.Release_Format = "release";
                row.Bit_Rate = "bitrate..";
                row.Year = "year";
                row.Physical_Format = "format";
                row.Bit_Format = "bitformat";
                row.File_Path = "File!!path";
                row.Site_Origin = "what";
                cSLDataSet.CSLDataTable.Rows.Add(row);


                cSLDataSet.AcceptChanges();
                cSLDataTableTableAdapter.Fill(cSLDataSet.CSLDataTable);
                
                cSLDataTableTableAdapter.Update(cSLDataSet);
                
            dataGridView.Refresh();
            dataGridView.Update();
        }
        private void test()
        {
            TorrentXMLHandler data = new TorrentXMLHandler();
            string[] information = new string[20];
            for (int a = 0; a < 20; a++)
            {
                byte[] buffer = new byte[10];
                Random r = new Random();
                r.NextBytes(buffer);
                information[a] = (buffer[r.Next(0, 9)].ToString());
            }
            data.AddTorrent(new Torrent(information));
        }
        #region General
        /*GENERAL*/
        private void RefreshData()
        {
            Thread.Sleep(100);

            dataGridViewProgressBar.Visible = false;
            StatusLabel.Visible = false;
            ProcessTorrentsButton.Enabled = true;
            RefreshButton.Enabled = true;
            DeleteButton.Enabled = true;

            dgvh.ResumeLayout();

            if (timer.Enabled == false && SettingsHandler.GetAutoHandleBool())
                timer.Start();

            try
            {
                dgvh = new DataGridViewHandler(dataGridView);
            }
            catch { }
        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
            timer.Close();
            //TorrentXMLHandler.SaveAndClose();
            this.Dispose();
        }
        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                if (SettingsHandler.GetMinimizeToTray())
                {
                    Hide();
                    notifyIcon.Visible = true;
                }
            }
            //DATAGRIDVIEW
            dataGridView.Location = new System.Drawing.Point(0, 187);
            dataGridView.Size = new System.Drawing.Size(this.Width - 15, this.Height - 225);
            dataGridView.ScrollBars = ScrollBars.Both;
            //DELETE BUTTON
            DeleteButton.Location = new System.Drawing.Point(this.Width - 105, 113);
            //uTORRENT SEND BUTTON
            uTorrentSendIndividualButton.Location = new System.Drawing.Point(this.Width - 145, 150);
            //REFRESH BUTTON
            RefreshButton.Location = new System.Drawing.Point(0, 151);
        }
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
        #endregion
        #region Buttons
        /*<BUTTONS>*/

        //Click
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (!dgvh.IsBusy)
            {

                dgvh.ProgressChanged += new ProgressChangedEventHandler(DeleteWorkerProgressChange);
                dgvh.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DeleteWorkerCompleted);

                try { dgvh.SuspendLayout(); }
                catch { }
                dataGridViewProgressBar.Visible = true;
                StatusLabel.Visible = true;
                dgvh.RunWorkerAsync("Delete");
            }
        }
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            dgvh.Refresh();
        }
        private void ProcessTorrentsButton_Click(object sender, EventArgs e)
        {
            items = new List<FileInfo>();
            torrents = DirectoryHandler.GetTorrents();
            zips = DirectoryHandler.GetTorrentZips();

            if (torrents != null || zips != null)
            {
                if (dgvh.dv != null)
                    dgvh.SuspendLayout();

                ProcessTorrentsButton.Enabled = false;
                RefreshButton.Enabled = false;
                DeleteButton.Enabled = false;
                internaltimer.Start(); //Periodically check to see if BW threads finished
                timer.Stop();

                if (torrents != null)
                {
                    foreach (string torrent in torrents)
                    {
                        try
                        {
                            items.Add(new FileInfo(torrent));
                        }
                        catch (Exception ee)
                        {
                            DirectoryHandler.LogError(ee.Message + "\n" + ee.StackTrace);
                        }
                    }
                }

                if (zips != null)
                {
                    bw.Unzip(zips);
                }
                bw.Process(items);
            }
            else
            {
                ProcessTorrentsButton.Enabled = true;
            }
        }
        private void uTorrentSendIndividualButton_Click(object sender, EventArgs e)
        {
            dgvh.SendIndividualTorrent();
        }
        private void uTorrentSendAllButton_Click(object sender, EventArgs e)
        {
            //uTorrentHandler.SendAllTorrents();
            dgvh.dv.Update();
        }
        private void PerformClickProcessTorrents()
        {
            ProcessTorrentsButton.PerformClick();
        }
        private void PerformClickRefreshButton()
        {
            RefreshButton.PerformClick();
        }
        private void ShowProcessTorrentsButton()
        {
            ProcessTorrentsButton.Visible = true;
        }
        //Hover
        private void DeleteButton_MouseEnter(object sender, EventArgs e)
        {
            RefreshButton.Visible = false;
            Arrow.Visible = true;
            ArrowText.Visible = true;
        }
        private void DeleteButton_MouseLeave(object sender, EventArgs e)
        {
            RefreshButton.Visible = true;
            ArrowText.Visible = false;
            Arrow.Visible = false;
        }
        #endregion

        #region Tool Strip
        /*TOOL STRIP*/
        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
        private void optionsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ow.Show();
        }
        #endregion
        #region DataGridView
        /*DataGridView*/
        private void dataGridView_DragDrop(object sender, DragEventArgs e)
        {
            items = new List<FileInfo>();
            files = (string[])e.Data.GetData(DataFormats.FileDrop);
            dataGridViewProgressBar.Visible = true;
            StatusLabel.Visible = true;

            foreach (string file in files)
            {
                string extension = Path.GetExtension(file);
                if (Path.GetExtension(file).Equals(".zip") || Path.GetExtension(file).Equals(".rar"))
                {
                    foreach (string unzipped in DirectoryHandler.UnzipFile(file))
                        items.Add(new FileInfo(unzipped));
                }
                else if (Path.GetExtension(file).Equals(".torrent"))
                {
                    items.Add(new FileInfo(file));
                }
            }

            tb.RunWorkerAsync(items);
        }
        private void dataGridView_DragEnter(object sender, DragEventArgs e)
        {
            // make sure they're actually dropping files
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                // allow them to continue
                // (without this, the cursor stays a "NO" symbol)
                e.Effect = DragDropEffects.All;
        }
        private void dataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Delete))
                DeleteButton_Click(sender, e);
        }
        #endregion
        #region Timer
        /*TIMER*/
        public static void UpdateTimer(bool active, decimal time)
        {
            if (active)
            {
                if (timer == null)
                    timer = new System.Timers.Timer();

                timer.Interval = (double)time;
                if (!timer.Enabled)
                    timer.Start();
            }
            else
            {
                timer.Stop();
                timer.Close();
            }
        }
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (ProcessTorrentsButton.InvokeRequired && !BWHandler.Busy)
                this.Invoke(new ProcessTorrentsInvoker(PerformClickProcessTorrents));
            else if (!BWHandler.Busy)
                ProcessTorrentsButton.PerformClick();
        }
        void internaltimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!BWHandler.Busy)
            {
                if (this.InvokeRequired)
                    this.Invoke(new RefreshInvoker(RefreshData));
                else
                    RefreshData();

                if (ProcessTorrentsButton.InvokeRequired)
                    this.Invoke(new ProcessTorrentsInvoker(ShowProcessTorrentsButton));
                else
                    ProcessTorrentsButton.Visible = true;

                internaltimer.Stop();
            }
        }
        #endregion
        #region BackgroundWorker
        void DeleteWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvh.ResumeLayout();
            dataGridViewProgressBar.Visible = false;
            StatusLabel.Visible = false;
            RefreshButton.Enabled = true;
        }
        void DeleteWorkerProgressChange(object sender, ProgressChangedEventArgs e)
        {
            int progress = 0;

            if (!(e.ProgressPercentage > 100 || e.ProgressPercentage < 0))
                progress = e.ProgressPercentage;

            if (progress % 10 == 0)
                StatusLabel.Text = "Deleting " + 10 * (progress / 10) + "%";
            else
                StatusLabel.Text += ".";
            dataGridViewProgressBar.Value = progress;
        }
        #endregion

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cSLDataSet.CSLDataTable' table. You can move, or remove it, as needed.
            this.cSLDataTableTableAdapter.Fill(this.cSLDataSet.CSLDataTable);
        }
    }
}
