using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;
using System.Threading;

namespace CSL_Test__1
{
    public partial class MainWindow : Form
    {
        TorrentXMLHandler data = null;
        uTorrentHandler utorrent = new uTorrentHandler();
        TorrentBuilder builder = new TorrentBuilder();
        Torrent[] torrents;
        SettingsHandler settings = new SettingsHandler();
        public MainWindow()
        {
            data = new TorrentXMLHandler();
            InitializeComponent();
            dataGridView.DataSource = data.table;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            data.dataset.AcceptChanges();
            data.SaveAndClose();
        }

        private void dataGridView_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoDragDrop);
            bw.RunWorkerAsync(files);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_DragDropProgress);
        }

        void bw_DragDropProgress(object sender, ProgressChangedEventArgs e)
        {
            dataGridViewProgressBar.Value = e.ProgressPercentage;
        }

        void bw_DoDragDrop(object sender, DoWorkEventArgs e)
        {
            torrents = builder.Build((string[])e.Argument);
            data.AddTorrents(torrents);
        }

        private void dataGridView_DragEnter(object sender, DragEventArgs e)
        {
            // make sure they're actually dropping files (not text or anything else)
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                // allow them to continue
                // (without this, the cursor stays a "NO" symbol
                e.Effect = DragDropEffects.All;
        }

        private void uTorrentSendAllButton_Click(object sender, EventArgs e)
        {
            utorrent.SendTorrents(data);
            dataGridView.Update();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection dr = dataGridView.SelectedRows;
            if (dr.Count > 0)
            {
                foreach (DataGridViewRow r in dr)
                {
                    data.table.Rows[r.Index].Delete();
                }
            }
            else
            {
                DataGridViewSelectedCellCollection dc = dataGridView.SelectedCells;
                foreach (DataGridViewCell c in dc)
                    c.Value = "";
            }
            data.Save();
        }
    }
}
