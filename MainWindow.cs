using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Data;
using System.Threading;

namespace CSL_Test__1
{
    public partial class MainWindow : Form
    {
        uTorrentHandler utorrent = new uTorrentHandler();
        TorrentBuilder builder = new TorrentBuilder();
        TorrentBuilder tb = new TorrentBuilder();
        OptionsWindow ow = new OptionsWindow();
        DirectoryHandler dh = new DirectoryHandler();
        static System.Timers.Timer timer = new System.Timers.Timer();
        DataGridViewHandler dgvh;
        ArrayList al;

        private Object lockingobject = new Object();

        string[] files;
        string[] torrents;
        string[] zips;

        delegate void ProcessTorrentsInvoker();

        private void PerformClickProcessTorrents()
        {
            ProcessTorrentsButton.PerformClick();
        }
        public MainWindow()
        {
            InitializeComponent();
            TorrentXMLHandler.Initialize(); //Initialize data source (read XML file)

            timer.Interval = (double)SettingsHandler.GetAutoHandleTime();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            if(SettingsHandler.GetAutoHandleBool())
                timer.Start();


            Arrow.Visible = false;
            StatusLabel.Visible = false;
            ArrowText.Visible = false;
            notifyIcon.Visible = false;

            dgvh = new DataGridViewHandler(dataGridView);

            tb.RunWorkerCompleted += new RunWorkerCompletedEventHandler(tb_BuildCompleted);
            tb.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChange);
            dgvh.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChange);
            dgvh.RunWorkerCompleted += new RunWorkerCompletedEventHandler(data_RunWorkerCompleted);
            dh.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChange);
            dh.RunWorkerCompleted += new RunWorkerCompletedEventHandler(dh_RunWorkerCompleted);
        }

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
            if (ProcessTorrentsButton.InvokeRequired)
                this.Invoke(new ProcessTorrentsInvoker(PerformClickProcessTorrents));
            else
                ProcessTorrentsButton.PerformClick();
        }

        private void RefreshData()
        {
            lock (lockingobject)
            {
                dgvh = new DataGridViewHandler(dataGridView);
                dgvh.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChange);
                dgvh.RunWorkerCompleted += new RunWorkerCompletedEventHandler(data_RunWorkerCompleted);

                dataGridViewProgressBar.Visible = false;
                StatusLabel.Visible = false;

                if (timer.Enabled == false)
                    timer.Start();
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
            timer.Close();
            TorrentXMLHandler.Save();
            this.Dispose();
        }

        private void dataGridView_DragDrop(object sender, DragEventArgs e)
        {
            al = new ArrayList();
            files = (string[])e.Data.GetData(DataFormats.FileDrop);
            dataGridViewProgressBar.Visible = true;
            StatusLabel.Visible = true;

            foreach (string file in files)
            {
                string extension = Path.GetExtension(file);
                if (Path.GetExtension(file).Equals(".zip") || Path.GetExtension(file).Equals(".rar"))
                {
                    foreach (string unzipped in DirectoryHandler.UnzipFile(file))
                        al.Add(unzipped);
                }
                else if (Path.GetExtension(file).Equals(".torrent"))
                {
                    al.Add(file);
                }
            }

            lock (lockingobject)
            {
                tb.RunWorkerAsync(al);
            }
        }

        void data_DeleteProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!(e.ProgressPercentage > 100 || e.ProgressPercentage < 0))
                dataGridViewProgressBar.Value = e.ProgressPercentage;

            if ((e.ProgressPercentage % 10) == 0)
                StatusLabel.Text = "Deleting torrents " + 10 * (e.ProgressPercentage / 10) + "%";
            else
                StatusLabel.Text += ".";
        }

        void bw_ProgressChange(object sender, ProgressChangedEventArgs e)
        {
            if (!(e.ProgressPercentage > 100 || e.ProgressPercentage < 0))
                dataGridViewProgressBar.Value = e.ProgressPercentage;

            if ((e.ProgressPercentage % 10) == 0)
            {
                string test = sender.ToString();
                switch (test)
                {
                    case "CSL_Test__1.TorrentBuilder":
                        StatusLabel.Text = "Building " + 10 * (e.ProgressPercentage / 10) + "%";
                        break;
                    case "CSL_Test__1.TorrentXMLHandler":
                        StatusLabel.Text = "Adding torrents " + 10 * (e.ProgressPercentage / 10) + "%";
                        break;
                    case "CSL_Test__1.DirectoryHandler":
                        StatusLabel.Text = "Moving files " + 10 * (e.ProgressPercentage / 10) + "%";
                        break;
                    default:
                        StatusLabel.Text = "Working...";
                        break;
                }
            }
            else
                StatusLabel.Text += ".";
        }

        void tb_BuildCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                builder.Dispose();
                dgvh.RunWorkerAsync((Torrent[])e.Result);
            }
        }

        void data_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dh.RunWorkerAsync((Torrent[])e.Result);
        }

        void dh_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGridViewProgressBar.Visible = false;
            StatusLabel.Visible = false;
            RefreshData();
        }

        void data_DeleteWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RefreshData();
            dgvh.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(data_DeleteWorkerCompleted);
        }

        private void dataGridView_DragEnter(object sender, DragEventArgs e)
        {
            // make sure they're actually dropping files
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                // allow them to continue
                // (without this, the cursor stays a "NO" symbol)
                e.Effect = DragDropEffects.All;
        }

        private void uTorrentSendAllButton_Click(object sender, EventArgs e)
        {
            uTorrentHandler.SendAllTorrents();
            dgvh.dv.Update();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            dgvh.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(data_RunWorkerCompleted);
            dgvh.RunWorkerCompleted += new RunWorkerCompletedEventHandler(data_DeleteWorkerCompleted);
            dgvh.RunWorkerAsync("Delete");
           
            dataGridViewProgressBar.Visible = true;
                /*
                if (dr.Count > 0)
                {
                    foreach (DataGridViewRow r in dr)
                    {
                        dataGridView.Rows.Remove(r);
                    }

                    dgvh.Save();
                }
                else if (dc.Count > 0)
                {
                    foreach (DataGridViewCell c in dc)
                    {
                        if (!(c.ReadOnly))
                        {
                            c.Value = "";
                        }
                    }

                    dgvh.Save();
                }

                dataGridView.Update();
                dgvh.Save();*/
            
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
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

        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void optionsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ow.Show();
        }

        private void ProcessTorrentsButton_Click(object sender, EventArgs e)
        {
            lock (lockingobject)
            {
                al = new ArrayList();
                torrents = DirectoryHandler.GetTorrents();
                zips = DirectoryHandler.GetTorrentZips();

                if (torrents != null || zips != null)
                {
                    if (torrents != null)
                    {
                        foreach (string torrent in torrents)
                            al.Add(torrent);
                    }

                    if (zips != null)
                    {
                        object[] rawFiles = DirectoryHandler.UnzipFiles(zips);
                        string[] ziptorrents = Array.ConvertAll<object, string>(rawFiles, Convert.ToString);
                        foreach (string ziptorrent in ziptorrents)
                            al.Add(ziptorrent);
                    }

                    timer.Stop();
                    dataGridViewProgressBar.Visible = true;
                    StatusLabel.Visible = true;
                    tb.RunWorkerAsync(al);
                }
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            dgvh.Refresh();
        }

        private void uTorrentSendIndividualButton_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Delete))
                DeleteButton_Click(sender, e);
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

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
    }
}
