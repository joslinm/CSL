using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CSL_Test__1
{
    class DataGridViewHandler : TorrentDataHandler
    {
        public DataGridView dv;
        BindingSource bs;
        private Object obj = new Object();
        delegate void SuspendLayoutInvoker();

        public DataGridViewHandler() { }
        public DataGridViewHandler(ref DataGridView in_dv, ref BindingSource b)
        {
            bs = b;
            dv = in_dv;

            dv.CellBeginEdit += new DataGridViewCellCancelEventHandler(dv_CellBeginEdit);
            dv.CellEndEdit += new DataGridViewCellEventHandler(dv_CellEndEdit);
            dv.CurrentCellDirtyStateChanged += new EventHandler(dv_CurrentCellDirtyStateChanged);
            dv.DataError += new DataGridViewDataErrorEventHandler(dv_DataError);

            //BW
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
        }

        public void SuspendLayout()
        {
            dv.Enabled = false;
            dv.SuspendLayout();
        }
        public void ResumeLayout()
        {
            bs.EndEdit();
            dv.ResumeLayout();
            dv.Enabled = true;
        }
        public void HideSentTorrents()
        {
            bs.SuspendBinding();
            foreach (DataGridViewRow dr in dv.Rows)
                if ((bool)dr.Cells["Sent"].Value)
                    dr.Visible = false;
            bs.ResumeBinding();
        }
        public void ShowSentTorrents()
        {
            bs.SuspendBinding();
            foreach (DataGridViewRow dr in dv.Rows)
                if ((bool)dr.Cells["Sent"].Value)
                    dr.Visible = true;
            bs.ResumeBinding();
        }

        void dv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
        }
        void dv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        { 
        }
        void dv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dv.IsCurrentCellDirty)
            {
                if (dv.CurrentCell.OwningColumn.Name.Equals("Save_Structure"))
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
                            case "Release_Format":
                                information[2] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            case "Bit_Rate":
                                information[3] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            case "Year":
                                information[4] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            case "Physical_Format":
                                information[5] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            case "Bit_Format":
                                information[6] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            default:
                                break;
                        }
                    }

                    string newSavePath =  TorrentBuilder.RebuildCustomPath(information);
                    dv["Save_Structure", dv.CurrentCell.OwningRow.Index].Value = newSavePath;
                }
            }
        }
        void dv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Ignore the data error
        }

        public void DeleteTorrents()
        {
            DataGridViewSelectedRowCollection dr = dv.SelectedRows;
            DataGridViewSelectedCellCollection dc = dv.SelectedCells;

            if (dv.AreAllCellsSelected(false))
                TorrentDataHandler.RemoveAll();
            else if (dr.Count > 0)
            {
                int total = dr.Count;
                double progress = 0;
                double count = 0;

                lock (obj)
                {
                    foreach (DataGridViewRow r in dr)
                    {
                            try
                            {
                                TorrentDataHandler.RemoveTorrent((int)r.Cells["ID"].Value);
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
            int id;

            if (rc.Count > 0)
                foreach (DataGridViewRow r in rc)
                {
                    id = (int)r.Cells["ID"].Value;
                    uTorrentHandler.SendTorrent(id);
                }
            else if (cc.Count > 0)
                foreach (DataGridViewCell c in cc)
                {
                    id = (int)c.OwningRow.Cells["ID"].Value;
                    uTorrentHandler.SendTorrent(id);
                }

            if (MainWindow.HideSent)
                HideSentTorrents();
        }

    }
}
