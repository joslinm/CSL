using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace CSL_Test__1
{
    public partial class MainWindow : Form
    {
        TorrentXMLHandler data = new TorrentXMLHandler();
        uTorrentHandler utorrent = new uTorrentHandler();
        TorrentBuilder builder = new TorrentBuilder();
        SettingsHandler settings = new SettingsHandler();
        TorrentBuilder tb = new TorrentBuilder();
        DirectoryHandler dh = new DirectoryHandler();
        OptionsWindow ow;

        public MainWindow()
        {
            InitializeComponent();
            ow = new OptionsWindow(data);
            dataGridView.DataSource = data.table;
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            data.Save();
            ow.StopTorrentWatch();
        }

        private void dataGridView_DragDrop(object sender, DragEventArgs e)
        {
            ArrayList al = new ArrayList();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                string extension = Path.GetExtension(file);
                if (Path.GetExtension(file).Equals(".zip") || Path.GetExtension(file).Equals(".rar"))
                {
                    foreach (string unzipped in dh.UnzipFile(file))
                        al.Add(unzipped);
                }
                else
                {
                    al.Add(file);
                }
            }

            dataGridViewProgressBar.Visible = true;
            tb.RunWorkerCompleted += new RunWorkerCompletedEventHandler(tb_DragDropCompleted);
            tb.ProgressChanged += new ProgressChangedEventHandler(tb_DragDropProgress);
            tb.RunWorkerAsync(al);
        }

        void tb_DragDropProgress(object sender, ProgressChangedEventArgs e)
        {
            dataGridViewProgressBar.Value = e.ProgressPercentage;
        }

        void tb_DragDropCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            data.AddTorrents((Torrent[])e.Result);
            builder.Dispose();
            dataGridViewProgressBar.Visible = false;
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
            utorrent.SendAllTorrents(data);
            dataGridView.Update();
            dh.MoveProcessedFiles(data);
            dh.DeleteTempFolder();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (settings.GetAutoHandleBool())
                ow.StopThread();

            lock (this)
            {
                DataGridViewSelectedRowCollection dr = dataGridView.SelectedRows;
                DataGridViewSelectedCellCollection dc = dataGridView.SelectedCells;
                if (dr.Count > 0)
                {
                    foreach (DataGridViewRow r in dr)
                    {
                        try
                        {
                            data.table.Rows[r.Index].Delete();
                        }
                        catch { }
                    }
                }
                else if(dc.Count > 0)
                {
                    foreach (DataGridViewCell c in dc)
                    {
                        if (!(c.ReadOnly))
                        {
                            c.Value = "";
                        }
                    }
                }
                data.Save();

            }

            if (settings.GetAutoHandleBool())
                ow.StartThread();
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
          
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
            ArrayList al = new ArrayList();
            string[] torrents = dh.GetTorrents();
            string[] zips = dh.GetTorrentZips();

            if (torrents != null)
            {
                foreach (string torrent in torrents)
                    al.Add(torrent);
            }

            if (zips != null)
            {
                string[] ziptorrents = (string[])dh.UnzipFiles(zips);
                foreach (string ziptorrent in ziptorrents)
                    al.Add(ziptorrent);
            }

            dataGridViewProgressBar.Visible = true;
            tb.RunWorkerCompleted += new RunWorkerCompletedEventHandler(tb_DragDropCompleted);
            tb.ProgressChanged += new ProgressChangedEventHandler(tb_DragDropProgress);
            tb.RunWorkerAsync(al);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            dataGridView.Refresh();
            this.Refresh();
        }

        private void uTorrentSendIndividualButton_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rc = dataGridView.SelectedRows;
            DataGridViewSelectedCellCollection cc = dataGridView.SelectedCells;

            if (rc.Count > 0)
            {
                foreach (DataGridViewRow r in rc)
                    utorrent.SendTorrent(data, r.Index);
                
            }
            if (cc.Count > 0)
            {
                foreach (DataGridViewCell c in cc)
                    utorrent.SendTorrent(data, c.RowIndex);
                
            }
        }

        private void dataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Delete))
                DeleteButton_Click(sender, e);
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView.EndEdit();
            data.table.Rows[e.RowIndex][e.ColumnIndex] = dataGridView[e.ColumnIndex, e.RowIndex].Value;
        }
    }
}
