using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CSL_Test__1
{
    class DataGridViewHandler : TorrentXMLHandler
    {
        public DataGridView dv;
        BindingSource bs;
        private Object obj = new Object();
        delegate void SuspendLayoutInvoker();

        public DataGridViewHandler() { }
        public DataGridViewHandler(DataGridView in_dv)
        {
            bs = new BindingSource();
            dv = in_dv;
            bs.DataSource = table;
            dv.DataSource = bs;

            dv.DataError += new DataGridViewDataErrorEventHandler(dv_DataError);
            dv.CurrentCellDirtyStateChanged += new EventHandler(dv_CurrentCellDirtyStateChanged);
            dv.CellBeginEdit += new DataGridViewCellCancelEventHandler(dv_CellBeginEdit);
            dv.CellEndEdit += new DataGridViewCellEventHandler(dv_CellEndEdit);
            dv.Columns["Site Origin"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dv.Columns["Year"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dv.Columns["Bitrate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dv.Columns["Release Format"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dv.Columns["Bit Format"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dv.Columns["Handled"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dv.Columns["Error"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dv.Columns["File Path"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            if (dv.Columns["Processed"]  == null)
                //TorrentXMLHandler.InitializeFresh();
            dv.Columns["Processed"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        public void SuspendLayout()
        {
            dv.SuspendLayout();
            dv.Enabled = false;
        }
        public void ResumeLayout()
        {
            dv.ResumeLayout();
            dv.Enabled = true;
        }

        void dv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            MainWindow.UpdateTimer(false, SettingsHandler.GetAutoHandleTime());
        }
        void dv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(SettingsHandler.GetAutoHandleBool())
                MainWindow.UpdateTimer(true, SettingsHandler.GetAutoHandleTime());
        }
        void dv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dv.IsCurrentCellDirty)
            {
                if (dv.CurrentCell.OwningColumn.Name.Equals("Save Structure"))
                    dv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                else if (!dv.CurrentCell.ReadOnly)
                {
                    dv.CommitEdit(DataGridViewDataErrorContexts.Commit);

                    string[] information = new string[7];
                    DataGridViewCellCollection dc = dv.CurrentCell.OwningRow.Cells;
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
                                information[0] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
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

                    string newSavePath =  TorrentBuilder.RebuildCustomPath(information);
                    dv["Save Structure", dv.CurrentCell.OwningRow.Index].Value = newSavePath;
                }
            }
        }
        void dv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Ignore the data error
        }

        public void Refresh()
        {
            dv.Refresh();
        }
        public void DeleteTorrents()
        {
            DataGridViewSelectedRowCollection dr = dv.SelectedRows;
            DataGridViewSelectedCellCollection dc = dv.SelectedCells;

            if (dr.Count > 0)
            {
                int total = dr.Count;
                double progress = 0;
                double count = 0;

                lock (obj)
                {
                    foreach (DataGridViewRow r in dr)
                    {
                        if (r.Index >= 0 && r.Index <= dr.Count)
                        {
                            try
                            {
                                dv.Rows.Remove(r);
                            }
                            catch (Exception rowremoveerror)
                            {
                                string message = rowremoveerror.Message;
                            }
                        }

                        progress = (++count / total) * 100;
                        if (progress <= 100 && progress >= 0)
                            this.ReportProgress((int)progress);
                    }
                }

                //Save();
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

                //Save();
            }
        }
        public void SendIndividualTorrent()
        {
            DataGridViewSelectedRowCollection rc = dv.SelectedRows;
            DataGridViewSelectedCellCollection cc = dv.SelectedCells;

            if (rc.Count > 0)
            {
                foreach (DataGridViewRow r in rc)
                {
                    if (!(bool)r.Cells["Error"].Value && !(bool)r.Cells["Handled"].Value && !r.Cells["Save Structure"].Equals(DBNull.Value) && !r.Cells["File Path"].Equals(DBNull.Value))
                    {
                        string save = (string)r.Cells["Save Structure"].Value;
                        string path = (string)r.Cells["File Path"].Value;

                        //string success = uTorrentHandler.SendTorrent(save, path);
                        string success = "nothing";
                        if (success.Equals("SUCCESS"))
                        {
                            TorrentXMLHandler.table.Columns["Handled"].ReadOnly = false;
                            r.Cells["Handled"].Value = true;
                            TorrentXMLHandler.table.Columns["Handled"].ReadOnly = true;
                        }
                        else
                        {
                            TorrentXMLHandler.table.Columns["Error"].ReadOnly = false;
                            r.Cells["Error"].Value = true;
                            if (!success.Equals("uTorrent.exe does not exist"))
                                r.Cells["Site Origin"].Value = success;
                            TorrentXMLHandler.table.Columns["Error"].ReadOnly = false;
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

                        string success = "nothing";
                        //string success = uTorrentHandler.SendTorrent(save, path);
                        if (success.Equals("SUCCESS"))
                        {
                            TorrentXMLHandler.table.Columns["Handled"].ReadOnly = false;
                            c.OwningRow.Cells["Handled"].Value = true;
                            TorrentXMLHandler.table.Columns["Handled"].ReadOnly = true;
                        }
                        else
                        {
                            TorrentXMLHandler.table.Columns["Error"].ReadOnly = false;
                            c.OwningRow.Cells["Error"].Value = true;
                            c.OwningRow.Cells["File"].Value = success;
                            TorrentXMLHandler.table.Columns["Error"].ReadOnly = false;
                        }
                    }
                    else
                    {
                        if (c.OwningRow.Cells["Save Structure"].Value.Equals(DBNull.Value))
                        {
                            c.OwningRow.Cells["Site Origin"].Value = "No save structure..";
                            TorrentXMLHandler.table.Columns["Error"].ReadOnly = false;
                            c.OwningRow.Cells["Error"].Value = true;
                            TorrentXMLHandler.table.Columns["Error"].ReadOnly = false;
                        }
                        if (c.OwningRow.Cells["File Path"].Value.Equals(DBNull.Value))
                        {
                            c.OwningRow.Cells["Site Origin"].Value = "No file path..";
                            TorrentXMLHandler.table.Columns["Error"].ReadOnly = false;
                            c.OwningRow.Cells["Error"].Value = true;
                            TorrentXMLHandler.table.Columns["Error"].ReadOnly = false;
                        }
                    }
                }

            }
        }

        protected override void OnDoWork(System.ComponentModel.DoWorkEventArgs e)
        {
            Type type = e.Argument.GetType();

            if (type.Equals(typeof(String)))
            {
                //dv.Invoke(new SuspendLayoutInvoker(SuspendLayout));
                DeleteTorrents();
                //dv.Invoke(new SuspendLayoutInvoker(ResumeLayout));
                return;
            }
        }

    }
}
