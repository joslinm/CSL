using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace CSL_Test__1  
{
    class BWHandler
    {
        static List<FileInfo> unzipped = new List<FileInfo>();
        static List<FileInfo> torrents = new List<FileInfo>();
        public static bool Busy = true;
        private object locker = new object();
        DirectoryHandler dh = new DirectoryHandler();
        TorrentBuilder tb = new TorrentBuilder();
        uTorrentHandler utorrent = new uTorrentHandler();
        System.Windows.Forms.ProgressBar dataGridViewProgressBar;
        System.Windows.Forms.Label StatusLabel;

        public BWHandler(System.Windows.Forms.ProgressBar pb, System.Windows.Forms.Label sl)
        {
            dataGridViewProgressBar = pb;
            StatusLabel = sl;

            tb.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BuildWorkerCompleted);
            tb.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            dh.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            dh.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DirectoryWorkerCompleted);
        }
        public void Process(List<FileInfo> items)
        {
            torrents = items;
            Busy = true;

            lock (locker)
            {
                dataGridViewProgressBar.Visible = true;
                StatusLabel.Visible = true;
            }

            if(!dh.IsBusy && items != null)
            {
                dh = new DirectoryHandler();
                dh.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DirectoryWorkerCompleted);
                tb.RunWorkerAsync(torrents);
                dh.RunWorkerAsync();
            }
        }
        public void Unzip(string[] files)
        {
            Busy = true;

            dh.RunWorkerAsync(files);
        }

        #region BackgroundWorker
        /*BACKGROUNDWORKER COMPLETED*/
        void BuildWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (dh.IsBusy)
                dh.CancelAsync(); //Notify DH that building has completed && to do one final move
        }
        void DirectoryWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null && e.Result.GetType().Equals(typeof(List<FileInfo>)))
            {
                unzipped = (List<FileInfo>)e.Result;
                torrents.AddRange(unzipped);
                Process(torrents);
            }
            else
            {
                StatusLabel.Text = "Status";

                Busy = false;
                dataGridViewProgressBar.Visible = false;
                StatusLabel.Visible = false;
                DirectoryHandler.MoveZipFiles();
            }

            return;
        }
        /*BACKGROUNDWORKER PROGRESS CHANGE*/
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
                    case "CSL_Test__1.TorrentBuilder":
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
                    case "CSL_Test__1.DirectoryHandler":
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
                StatusLabel.Text += ".";
        }
        #endregion
    }
}
