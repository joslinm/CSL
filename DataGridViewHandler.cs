using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace CSL
{
    class DataGridViewHandler : TorrentDataHandler
    {
        //To Find & Active uTorrent Window:
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public DataGridView dv;
        public DataGridView movie_dv;
        public DataGridView other_dv;

        BindingSource bs;
        BindingSource movie_bs;
        BindingSource other_bs;

        delegate void SuspendLayoutInvoker();

        public DataGridViewHandler() { }
        public DataGridViewHandler(ref DataGridView in_dv, ref BindingSource b, ref DataGridView moviedv, ref BindingSource moviebs,
            ref DataGridView otherdv, ref BindingSource otherbs)
        {
            bs = b;
            dv = in_dv;
            movie_dv = moviedv;
            movie_bs = moviebs;
            other_dv = otherdv;
            other_bs = otherbs;

            dv.CellBeginEdit += new DataGridViewCellCancelEventHandler(dv_CellBeginEdit);
            dv.CellEndEdit += new DataGridViewCellEventHandler(dv_CellEndEdit);
            dv.CurrentCellDirtyStateChanged += new EventHandler(dv_CurrentCellDirtyStateChanged);
            dv.DataError += new DataGridViewDataErrorEventHandler(dv_DataError);

            movie_dv.CurrentCellDirtyStateChanged += new EventHandler(movie_dv_CurrentCellDirtyStateChanged);

            //Cell Clicks
            other_dv.CellClick += new DataGridViewCellEventHandler(other_dv_CellClick);
            movie_dv.CellClick += new DataGridViewCellEventHandler(movie_dv_CellClick);
            dv.CellClick += new DataGridViewCellEventHandler(dv_CellClick);
            //BW
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
        }

        void dv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dv.Columns[e.ColumnIndex].Name.Equals("MusicOpenWithClient"))
                {
                    uTorrentHandler.SendTorrentWithoutSavePath("music", (int)dv.Rows[e.RowIndex].Cells["ID"].Value);

                    IntPtr utorrentHandle = FindWindow("µTorrent4823Df041B09", "µTorrent 2.0.2");
                    SetForegroundWindow(utorrentHandle);

                    Thread.Sleep(300);
                    SendKeys.Send("^v");
                }
            }
        }
        void movie_dv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (movie_dv.Columns[e.ColumnIndex].Name.Equals("MoviesOpenWithClient"))
                {
                    uTorrentHandler.SendTorrentWithoutSavePath("movies", (int)movie_dv.Rows[e.RowIndex].Cells["MovieID"].Value);

                    IntPtr utorrentHandle = FindWindow("µTorrent4823Df041B09", "µTorrent 2.0.2");
                    SetForegroundWindow(utorrentHandle);
                    Thread.Sleep(300);
                    SendKeys.Send("^v");
                }
            }
        }
        void other_dv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (other_dv.Columns[e.ColumnIndex].Name.Equals("OpenWithClient"))
                {
                    uTorrentHandler.SendTorrentWithoutSavePath("others", (int)other_dv.Rows[e.RowIndex].Cells["OthersID"].Value);

                    IntPtr utorrentHandle = FindWindow("µTorrent4823Df041B09", "µTorrent 2.0.2");
                    SetForegroundWindow(utorrentHandle);
                    Thread.Sleep(300);
                    SendKeys.Send("^v");
                }
            }
        }

        public void SuspendLayout()
        {
            dv.Enabled = false;
            movie_dv.Enabled = false;
            other_dv.Enabled = false;
            movie_dv.SuspendLayout();
            dv.SuspendLayout();
            other_dv.SuspendLayout();
        }
        public void ResumeLayout()
        {
            bs.EndEdit();
            movie_bs.EndEdit();
            other_bs.EndEdit();

            dv.ResumeLayout();
            movie_dv.ResumeLayout();
            other_dv.ResumeLayout();

            dv.Enabled = true;
            movie_dv.Enabled = true;
            other_dv.Enabled = true;
        }
        public void HideSentTorrents()
        {
            bs.SuspendBinding();
            movie_bs.SuspendBinding();
            other_bs.SuspendBinding();

            foreach (DataGridViewRow dr in dv.Rows)
                if ((bool)dr.Cells["Sent"].Value)
                    dr.Visible = false;
            foreach(DataGridViewRow dr in movie_dv.Rows)
                if ((bool)dr.Cells["MovieSent"].Value)
                    dr.Visible = false;
            foreach(DataGridViewRow dr in other_dv.Rows)
                if ((bool)dr.Cells["OthersSent"].Value)
                    dr.Visible = false;

            bs.ResumeBinding();
            movie_bs.ResumeBinding();
            other_bs.ResumeBinding();
        }
        public void ShowSentTorrents()
        {
            bs.SuspendBinding();
            movie_bs.SuspendBinding();
            other_bs.SuspendBinding();

            foreach (DataGridViewRow dr in dv.Rows)
                if ((bool)dr.Cells["Sent"].Value)
                    dr.Visible = true;
            foreach (DataGridViewRow dr in movie_dv.Rows)
                if ((bool)dr.Cells["MovieSent"].Value)
                    dr.Visible = true;
            foreach (DataGridViewRow dr in other_dv.Rows)
                if ((bool)dr.Cells["OthersSent"].Value)
                    dr.Visible = true;

            bs.ResumeBinding();
            movie_bs.ResumeBinding();
            other_bs.ResumeBinding();
        }

        #region Event Handlers
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

                    string newSavePath = TorrentBuilder.RebuildCustomPath(information);
                    dv["Save_Structure", dv.CurrentCell.OwningRow.Index].Value = newSavePath;
                }
            }
        }
        void dv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Ignore the data error
        }
        void movie_dv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dv.IsCurrentCellDirty)
            {
                if (dv.CurrentCell.OwningColumn.Name.Equals("MovieSaveStructure"))
                    dv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                else if (!dv.CurrentCell.ReadOnly)
                {
                    dv.CommitEdit(DataGridViewDataErrorContexts.Commit);

                    string[] information = new string[5];
                    DataGridViewCellCollection dc = dv.CurrentCell.OwningRow.Cells;
                    foreach (DataGridViewCell c in dc)
                    {
                        switch (c.OwningColumn.Name)
                        {
                            /* information
                             * 0: Movie Title
                             * 1: Year
                             * 2: Source Media
                             * 3: Codec Format
                             * 4: File Format
                             * */
                            case "MovieTitle":
                                information[0] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            case "MovieYear":
                                information[1] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            case "SourceMedia":
                                information[2] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            case "CodecFormat":
                                information[3] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            case "FileFormat":
                                information[4] = (c.Value.Equals(DBNull.Value)) ? "" : (string)c.Value;
                                break;
                            default:
                                break;
                        }
                    }

                    string newSavePath = TorrentBuilder.RebuildMovieCustomPath(information);
                    dv["MovieSaveStructure", dv.CurrentCell.OwningRow.Index].Value = newSavePath;
                }
            }
        }
        #endregion

        public void DeleteTorrents()
        {
            SuspendLayout();
            DataGridViewSelectedRowCollection dr = dv.SelectedRows;
            DataGridViewSelectedCellCollection dc = dv.SelectedCells;
            DataGridViewSelectedRowCollection mdr = movie_dv.SelectedRows;
            DataGridViewSelectedCellCollection mdc = movie_dv.SelectedCells;
            DataGridViewSelectedRowCollection odr = other_dv.SelectedRows;
            DataGridViewSelectedCellCollection odc = other_dv.SelectedCells;

            if (dv.AreAllCellsSelected(false))
                TorrentDataHandler.RemoveAll("music");
            if (movie_dv.AreAllCellsSelected(false))
                TorrentDataHandler.RemoveAll("movies");
            if (other_dv.AreAllCellsSelected(false))
                TorrentDataHandler.RemoveAll("others");
            
            if (dr.Count > 0)
            {
                foreach (DataGridViewRow r in dr)
                {
                    try
                    {
                        TorrentDataHandler.RemoveTorrent("music", (int)r.Cells["ID"].Value);
                    }
                    catch (Exception rowremoveerror) { DirectoryHandler.LogError(rowremoveerror.Message + "\n" + rowremoveerror.StackTrace); }
                }
            }
            else if (dc.Count > 0)
            {
                foreach (DataGridViewCell c in dc)
                {
                    if (!(c.ReadOnly))
                    {
                        Type t = c.ValueType;

                        if (t.Equals(typeof(String)))
                            c.Value = "";
                    }
                }

                //Save();
            }
            if (mdr.Count > 0)
            {
                foreach (DataGridViewRow r in mdr)
                {
                    try
                    {
                        TorrentDataHandler.RemoveTorrent("movies", (int)r.Cells["MovieID"].Value);
                    }
                    catch (Exception rowremoveerror) { DirectoryHandler.LogError(rowremoveerror.Message + "\n" + rowremoveerror.StackTrace); }
                }
            }
            else if (mdc.Count > 0)
            {
                foreach (DataGridViewCell c in mdc)
                {
                    if (!(c.ReadOnly))
                    {
                        Type t = c.ValueType;

                        if(t.Equals(typeof(String)))
                        c.Value = "";
                    }
                }

                //Save();
            }
            if (odr.Count > 0)
            {
                foreach (DataGridViewRow r in odr)
                {
                    try
                    {
                        TorrentDataHandler.RemoveTorrent("others", (int)r.Cells["OthersID"].Value);
                    }
                    catch (Exception rowremoveerror) { DirectoryHandler.LogError(rowremoveerror.Message + "\n" + rowremoveerror.StackTrace); }
                }
            }
            else if (odc.Count > 0)
            {
                foreach (DataGridViewCell c in odc)
                {
                    if (!(c.ReadOnly))
                    {
                        Type t = c.ValueType;

                        if (t.Equals(typeof(String)))
                            c.Value = "";
                    }
                }

                //Save();
            }

            ResumeLayout();
           
        }
        public void SendIndividualTorrent(string datatable)
        {
            DataGridViewSelectedRowCollection rc;
            DataGridViewSelectedCellCollection cc;
            int id;
            switch (datatable)
            {
                case "music":
                    rc = dv.SelectedRows;
                    cc = dv.SelectedCells;

                    if (rc.Count > 0)
                        foreach (DataGridViewRow r in rc)
                        {
                            id = (int)r.Cells["ID"].Value;
                            uTorrentHandler.SendTorrent("music", id);
                        }
                    else if (cc.Count > 0)
                        foreach (DataGridViewCell c in cc)
                        {
                            id = (int)c.OwningRow.Cells["ID"].Value;
                            uTorrentHandler.SendTorrent("music", id);
                        }

                    if (MainWindow.HideSent)
                        HideSentTorrents();
                    break;
                case "movies":
                    rc = movie_dv.SelectedRows;
                    cc = movie_dv.SelectedCells;

                    if (rc.Count > 0)
                        foreach (DataGridViewRow r in rc)
                        {
                            id = (int)r.Cells["MovieID"].Value;
                            uTorrentHandler.SendTorrent("movies", id);
                        }
                    else if (cc.Count > 0)
                        foreach (DataGridViewCell c in cc)
                        {
                            id = (int)c.OwningRow.Cells["MovieID"].Value;
                            uTorrentHandler.SendTorrent("movies", id);
                        }

                    if (MainWindow.HideSent)
                        HideSentTorrents();
                    break;
                case "others":
                    rc = other_dv.SelectedRows;
                    cc = other_dv.SelectedCells;

                    if (rc.Count > 0)
                        foreach (DataGridViewRow r in rc)
                        {
                            id = (int)r.Cells["OthersID"].Value;
                            uTorrentHandler.SendTorrent("others", id);
                        }
                    else if (cc.Count > 0)
                        foreach (DataGridViewCell c in cc)
                        {
                            id = (int)c.OwningRow.Cells["OthersID"].Value;
                            uTorrentHandler.SendTorrent("others", id);
                        }

                    if (MainWindow.HideSent)
                        HideSentTorrents();
                    break;
            }
        }

        protected override void OnDoWork(System.ComponentModel.DoWorkEventArgs e)
        {
            //DeleteTorrents();
        }
    }
}
