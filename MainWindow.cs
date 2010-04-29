using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Data;

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
        public bool restart = false;

        public MainWindow()
        {
            InitializeComponent();
            ow = new OptionsWindow(data);
            dataGridView.DataSource = data.table;
            dataGridView.Columns["Site Origin"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns["Year"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns["Bitrate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns["Release Format"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns["Bit Format"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns["Handled"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns["Error"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns["File Path"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            notifyIcon.Visible = false;
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
                else if (Path.GetExtension(file).Equals(".torrent"))
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
            if (!(e.ProgressPercentage > 100 || e.ProgressPercentage < 0))
            dataGridViewProgressBar.Value = e.ProgressPercentage;
        }

        void tb_DragDropCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                data.AddTorrents((Torrent[])e.Result);
                builder.Dispose();
                dh.MoveProcessedFiles(data);
                dataGridViewProgressBar.Visible = false;
            }
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
                        dataGridView.Rows.Remove(r);
                    }

                    data.Save();
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

                    data.Save();
                }


                dataGridView.Update();
                data.Save();
            }

            if (settings.GetAutoHandleBool())
                ow.StartThread();
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                if (settings.GetMinimizeToTray())
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
            ArrayList al = new ArrayList();
            string[] torrents = dh.GetTorrents();
            string[] zips = dh.GetTorrentZips();

            if (torrents != null || zips != null)
            {
                if (torrents != null)
                {
                    foreach (string torrent in torrents)
                        al.Add(torrent);
                }

                if (zips != null)
                {
                    object[] rawFiles = dh.UnzipFiles(zips);
                    string[] ziptorrents = Array.ConvertAll<object, string>(rawFiles, Convert.ToString);
                    foreach (string ziptorrent in ziptorrents)
                        al.Add(ziptorrent);
                }

                dataGridViewProgressBar.Visible = true;
                tb.RunWorkerCompleted += new RunWorkerCompletedEventHandler(tb_DragDropCompleted);
                tb.ProgressChanged += new ProgressChangedEventHandler(tb_DragDropProgress);
                tb.RunWorkerAsync(al);
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            this.Refresh();
            dataGridView.Refresh();
            dataGridView.ScrollBars = ScrollBars.Both;
        }

        private void uTorrentSendIndividualButton_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rc = dataGridView.SelectedRows;
            DataGridViewSelectedCellCollection cc = dataGridView.SelectedCells;

            if (rc.Count > 0)
            {
                foreach (DataGridViewRow r in rc)
                {
                    if (!(bool)r.Cells["Error"].Value && !(bool)r.Cells["Handled"].Value && !r.Cells["Save Structure"].Equals(DBNull.Value) && !r.Cells["File Path"].Equals(DBNull.Value))
                    {
                        string save = (string)r.Cells["Save Structure"].Value;
                        string path = (string)r.Cells["File Path"].Value;

                        string success = utorrent.SendTorrent(save, path);

                        if (success.Equals("SUCCESS"))
                        {
                            data.table.Columns["Handled"].ReadOnly = false;
                            r.Cells["Handled"].Value = true;
                            data.table.Columns["Handled"].ReadOnly = true;
                        }
                        else
                        {
                            data.table.Columns["Error"].ReadOnly = false;
                            r.Cells["Error"].Value = true;
                            if (!success.Equals("uTorrent.exe does not exist"))
                                r.Cells["Site Origin"].Value = success;
                            data.table.Columns["Error"].ReadOnly = false;
                        }
                    }
                }
                
            }
            if (cc.Count > 0)
            {
                foreach (DataGridViewCell c in cc)
                {
                    if (!(bool)c.OwningRow.Cells["Error"].Value && !(bool)c.OwningRow.Cells["Handled"].Value && !c.OwningRow.Cells["Save Structure"].Value.Equals(DBNull.Value) && !c.OwningRow.Cells["File Path"].Value.Equals(DBNull.Value))
                    {
                        string save = (string)c.OwningRow.Cells["Save Structure"].Value;
                        string path = (string)c.OwningRow.Cells["File Path"].Value;

                        string success = utorrent.SendTorrent(save, path);
                        if (success.Equals("SUCCESS"))
                        {
                            data.table.Columns["Handled"].ReadOnly = false;
                            c.OwningRow.Cells["Handled"].Value = true;
                            data.table.Columns["Handled"].ReadOnly = true;
                        }
                        else
                        {
                            data.table.Columns["Error"].ReadOnly = false;
                            c.OwningRow.Cells["Error"].Value = true;
                            c.OwningRow.Cells["File"].Value = success;
                            data.table.Columns["Error"].ReadOnly = false;
                        }
                    }
                    else
                    {
                        if (c.OwningRow.Cells["Save Structure"].Value.Equals(DBNull.Value))
                        {
                            c.OwningRow.Cells["Site Origin"].Value = "No save structure..";
                            data.table.Columns["Error"].ReadOnly = false;
                            c.OwningRow.Cells["Error"].Value = true;
                            data.table.Columns["Error"].ReadOnly = false;
                        }
                        if (c.OwningRow.Cells["File Path"].Value.Equals(DBNull.Value))
                        {
                            c.OwningRow.Cells["Site Origin"].Value = "No file path..";
                            data.table.Columns["Error"].ReadOnly = false;
                            c.OwningRow.Cells["Error"].Value = true;
                            data.table.Columns["Error"].ReadOnly = false;
                        }
                    }
                }
                
            }
        }

        private void dataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Delete))
                DeleteButton_Click(sender, e);
        }

        private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView.IsCurrentCellDirty)
            {
                if(dataGridView.CurrentCell.OwningColumn.Name.Equals("Save Structure"))
                    dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                else if (!dataGridView.CurrentCell.ReadOnly)
                {
                    dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

                    string[] information = new string[7];
                    DataGridViewCellCollection dc = dataGridView.CurrentCell.OwningRow.Cells;
                    foreach (DataGridViewCell c in dc)
                    {
                        switch (c.OwningColumn.Name)
                        {
                                /* information
                                 * 0: artist
                                 * 1: album
                                 * 2: release format
                                 * 3: bitrate
                                 * 4: year
                                 * 5: physical format
                                 * 6: bit format
                                 * */
                            case "Artist":
                                information[0] = (c.Value.Equals(DBNull.Value))? "" : (string)c.Value;
                                break;
                            case "Album":
                                information[1] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            case "Release Format":
                                information[2] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            case "Bitrate":
                                information[3] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            case "Year":
                                information[4] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            case "Physical Format":
                                information[5] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            case "Bit Format":
                                information[6] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            default:
                                break;
                        }
                    }
                        
                    string newSavePath = tb.RebuildCustomPath(information);
                    dataGridView["Save Structure", dataGridView.CurrentCell.OwningRow.Index].Value = newSavePath;
                }
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
    }
}
