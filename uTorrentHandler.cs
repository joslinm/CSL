using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Data;

namespace CSL_Test__1
{
    class uTorrentHandler : DataGridViewHandler
    {
        ErrorWindow ew = new ErrorWindow();
        private Object locker = new Object();

        public static void SendTorrent(int index)
        {
            ErrorWindow ew = new ErrorWindow();
            string path = null;
            string save = null;
            DataRow dr = null;
     
            try
            {
                dr = TorrentDataHandler.data.TorrentsTable.Rows.Find(index);
                if (dr != null)
                    dr.BeginEdit();
                else
                    return;
                path = (dr["File Path"].Equals(DBNull.Value))? null : (string)dr["File Path"];
                save = (dr["Save Structure"].Equals(DBNull.Value))? null : (string)dr["Save Structure"];

                //Preliminary error checking
                if (path == null || !DirectoryHandler.GetFileExists(path))
                {
                    dr.SetColumnError(TorrentDataHandler.data.TorrentsTable.Columns["File Path"], "File does not exist");
                    dr.EndEdit();
                    return;
                }
                if (save == null || save == "")
                {
                    dr.SetColumnError(TorrentDataHandler.data.TorrentsTable.Columns["Save Structure"], "Empty save structure");
                    dr.EndEdit();
                    return;
                }
                if (!DirectoryHandler.GetFileExists(SettingsHandler.GetTorrentClientFolder() + "\\uTorrent.exe"))
                {
                    ew.IssueGeneralWarning("Be sure uTorrent folder is correct", "uTorrent.exe does not exist", null);
                    dr.EndEdit();
                    return;
                }
                if ((bool)dr["Sent"])
                {
                    dr.EndEdit();
                    return;
                }
            }
            catch (Exception e)
            {
                if (dr != null)
                    dr.RowError = ("General exception: " + e.Message);
                dr.EndEdit();
                return;
            }

            try
            {
                Process sendTorrentProcess = new Process();
                //torrentClient.exe /directory "C:\Save Path" "D:\Some folder\your.torrent"

                string fullArgument = "/directory " + "\"" + save + "\" "
                    + "\"" + path + "\"";
                sendTorrentProcess.StartInfo.WorkingDirectory = SettingsHandler.GetTorrentClientFolder();
                sendTorrentProcess.StartInfo.Arguments = fullArgument;
                sendTorrentProcess.StartInfo.FileName = SettingsHandler.GetTorrentClient();

                sendTorrentProcess.Start();
                sendTorrentProcess.Dispose();
                sendTorrentProcess.Close();
                dr["Sent"] = true;
                if (dr.HasErrors)
                    dr.ClearErrors();

                dr.EndEdit();
            }
            catch (Exception e)
            {
                if (dr != null)
                    dr.RowError = ("General exception: " + e.Message);
                dr.EndEdit();
                return;
            }
        }
    }
}
