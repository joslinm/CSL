using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Collections;
namespace CSL
{
    public partial class MainWindow : Form
    {
        #region Local Variables
        TorrentBuilder tb = new TorrentBuilder();
        OptionsWindow ow = new OptionsWindow();
        DirectoryHandler dh = new DirectoryHandler();
        DataGridViewHandler dgvh;
        List<FileInfo> items;
        TorrentDataHandler data;
        
        //Timing
        DateTime startime;
        DateTime endtime;

        static System.Timers.Timer timer = new System.Timers.Timer();
        static public bool HideSent;

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
            this.ResizeRedraw = true;
            this.MainWindow_Resize(new object(), new EventArgs()); //Force a resize to make things look nice

            this.torrentsTableTableAdapter.Fill(this.dataset.TorrentsTable);
            this.moviesTableTableAdapter.Fill(this.dataset.MoviesTable);
            this.othersTableTableAdapter.Fill(this.dataset.OthersTable);

            //Allow another class to handle the deleting/handling of the datagridviews to keep this class slimmer
            dgvh = new DataGridViewHandler(ref torrentsTableDataGridView, ref torrentsTableBindingSource, 
                ref moviesTableDataGridView, ref moviesTableBindingSource,
                ref othersTableDataGridView, ref othersTableBindingSource);
            data = new TorrentDataHandler(ref dataset, ref torrentsTableTableAdapter);

            timer.Interval = (double)SettingsHandler.GetAutoHandleTime();
            if (timer.Interval < (1000*10))
            {
                SettingsHandler.SetAutoHandleTime(10000);
                timer.Interval = 10000;
            }
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);

            tb.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BuildWorkerCompleted);
            tb.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);

            if (SettingsHandler.GetAutoHandleBool())
                timer.Start();

            HideSent = (HideSentTorrentsCheckBox.Checked) ? true : false;

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

            test();
        }

        #region Timer
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (ProcessTorrentsButton.InvokeRequired)
                this.Invoke(new ProcessTorrentsInvoker(PerformClickProcessTorrents));
            else
                ProcessTorrentsButton.PerformClick();
        }
        static public void UpdateTimer(bool alive, decimal time)
        {
            timer.Enabled = alive;
            timer.Interval = (double)time;
        }

#endregion
        #region Testing
        private void test()
        {
            /*
                UTorrentWebClient web = new Cleverscape.UTorrentClient.WebClient.UTorrentWebClient();
                Cleverscape.UTorrentClient.WebClient.Torrent torrent = new Cleverscape.UTorrentClient.WebClient.Torrent();
                torrent;
            Cleverscape.UTorrentClient.WebClient.*/
        }
        #endregion

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
                dgvh = new DataGridViewHandler();
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
            TabbedContainer.Location = new System.Drawing.Point(0, 187);
            TabbedContainer.Size = new System.Drawing.Size(this.Width - 15, this.Height - 240);
           
            //DELETE BUTTON
            DeleteButton.Location = new System.Drawing.Point(this.Width - 105, 113);
            //uTORRENT SEND BUTTON
            uTorrentSendIndividualButton.Location = new System.Drawing.Point(this.Width - 145, 150);
            //REFRESH BUTTON
            RefreshButton.Location = new System.Drawing.Point(0, 151);
            HideSentTorrentsCheckBox.Location = new System.Drawing.Point(RefreshButton.Width + 5, 155);
            //PROGRESS BAR
            dataGridViewProgressBar.Location = new System.Drawing.Point(0, this.Height - 80);
            dataGridViewProgressBar.Width = this.Width;
            //STATUS LABEL
            StatusLabel.Location = new System.Drawing.Point(0, this.Height - 100);
        }
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
        private void HideSentTorrentsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (HideSentTorrentsCheckBox.Checked)
            {
                HideSent = true;
                dgvh.HideSentTorrents();
            }
            else
            {
                HideSent = false;
                dgvh.ShowSentTorrents();
            }
        }
        #endregion
        #region Buttons
        /*<BUTTONS>*/

        //Click
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            dgvh.DeleteTorrents();
            torrentsTableTableAdapter.Update(dataset.TorrentsTable);
            torrentsTableBindingSource.EndEdit();
            moviesTableTableAdapter.Update(dataset.MoviesTable);
            moviesTableBindingSource.EndEdit();
            othersTableTableAdapter.Update(dataset.OthersTable);
            othersTableBindingSource.EndEdit();
            tableAdapterManager.UpdateAll(dataset);
        }
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            torrentsTableDataGridView.Refresh();
            moviesTableDataGridView.Refresh();
            othersTableDataGridView.Refresh();
        }
        private void ProcessTorrentsButton_Click(object sender, EventArgs e)
        {
            items = new List<FileInfo>();
            torrents = DirectoryHandler.GetTorrents();
            zips = DirectoryHandler.GetTorrentZips();

            if (torrents != null || zips != null)
            {
                //Prevent anything complicated from happening..
                startime = DateTime.Now;

                dgvh.SuspendLayout();
                ProcessTorrentsButton.Enabled = false;
                RefreshButton.Enabled = false;
                DeleteButton.Enabled = false;
                StatusLabel.Visible = true;
                dataGridViewProgressBar.Visible = true;
                timer.Stop();

                if (torrents != null)
                    foreach (string torrent in torrents)
                        items.Add(new FileInfo(torrent));
                if (zips != null)
                    items.AddRange(dh.UnzipFiles(zips));

                tb.RunWorkerAsync(items);
            }
        }
        private void uTorrentSendIndividualButton_Click(object sender, EventArgs e)
        {
            switch (TabbedContainer.SelectedIndex)
            {
                case 0:
                    dgvh.SendIndividualTorrent("music");
                    torrentsTableTableAdapter.Update(dataset.TorrentsTable);
                    torrentsTableBindingSource.EndEdit();
                    break;
                case 1:

                    dgvh.SendIndividualTorrent("movies");
                    moviesTableTableAdapter.Update(dataset.MoviesTable);
                    moviesTableBindingSource.EndEdit();
                    break;
                case 2:
                    dgvh.SendIndividualTorrent("others");
                    othersTableTableAdapter.Update(dataset.OthersTable);
                    othersTableBindingSource.EndEdit();
                    break;
            }
            
            tableAdapterManager.UpdateAll(dataset);

        }
        private void uTorrentSendAllButton_Click(object sender, EventArgs e)
        {
            torrentsTableDataGridView.SelectAll();
            moviesTableDataGridView.SelectAll();
            othersTableDataGridView.SelectAll();

            dgvh.SendIndividualTorrent("music");
            dgvh.SendIndividualTorrent("movies");
            dgvh.SendIndividualTorrent("others");

            torrentsTableTableAdapter.Update(dataset.TorrentsTable);
            torrentsTableBindingSource.EndEdit();
            moviesTableTableAdapter.Update(dataset.MoviesTable);
            moviesTableBindingSource.EndEdit();
            othersTableTableAdapter.Update(dataset.OthersTable);
            othersTableBindingSource.EndEdit();
            tableAdapterManager.UpdateAll(dataset);
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
        private void UpdateAdapter()
        {
            torrentsTableTableAdapter.Update(dataset.TorrentsTable);
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

        #region BackgroundWorker
        void BuildWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DirectoryHandler.DeleteTempFolder();
            DirectoryHandler.DeleteZipFiles();

            ProcessTorrentsButton.Enabled = true;
            RefreshButton.Enabled = true;
            DeleteButton.Enabled = true;

            dataGridViewProgressBar.Visible = false;
            StatusLabel.Visible = false;
            if (SettingsHandler.GetAutoHandleBool())
                timer.Start();

            torrentsTableTableAdapter.Update(dataset.TorrentsTable);
            torrentsTableBindingSource.EndEdit();
            moviesTableTableAdapter.Update(dataset.MoviesTable);
            moviesTableBindingSource.EndEdit();
            othersTableTableAdapter.Update(dataset.OthersTable);
            othersTableBindingSource.EndEdit();
            tableAdapterManager.UpdateAll(dataset);

            endtime = DateTime.Now;
            TimeSpan duration = endtime - startime;
            ProcessTimer.Text = duration.Seconds + " secs";

            dgvh.ResumeLayout();
        }
        void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string precedence = "TorrentBuilder";
            int progress = 0;

            if (!(e.ProgressPercentage > 100 || e.ProgressPercentage < 0))
                progress = e.ProgressPercentage;

            if ((e.ProgressPercentage % 10) == 0)
            {
                string test = sender.ToString();
                switch (test)
                {
                    case "CSL.TorrentBuilder":
                        StatusLabel.Text = "Building " + 10 * (e.ProgressPercentage / 10) + "%";
                        if (precedence.Equals("TorrentBuilder"))
                        {
                            if (!StatusLabel.Text.Contains("Building"))
                                StatusLabel.Text = "Building";
                            if (progress == 100)
                            {
                                StatusLabel.Text = "Finalizing...";
                                precedence = "DirectoryHandler";
                                dataGridViewProgressBar.Value = 100;
                            }
                            else
                            {
                                dataGridViewProgressBar.Value = progress;
                            }
                        }
                        break;
                    case "CSL.DirectoryHandler":
                        StatusLabel.Text = "Moving files " + 10 * (e.ProgressPercentage / 10) + "%";
                        if (precedence.Equals("DirectoryHandler"))
                        {
                            if (!StatusLabel.Text.Contains("Moving files"))
                                StatusLabel.Text = "Moving files";
                            if (progress == 100)
                            {
                                precedence = "TorrentBuilder";
                                dataGridViewProgressBar.Value = 100;
                            }
                            else
                            {
                                dataGridViewProgressBar.Value = progress;
                            }
                        }
                        break;
                    default:
                        StatusLabel.Text = "Working...";
                        break;
                }
            }
            else
                dataGridViewProgressBar.Value = progress;
        }
        #endregion
    }
}
