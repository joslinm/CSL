﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Data;
using System.Windows.Forms;

namespace CSL
{
    class uTorrentHandler : DataGridViewHandler
    {
        public static void SendTorrent(string datatable, int index)
        {
            string path = null;
            string save = null;
            DataRow dr = null;

            switch (datatable)
            {
                case "others":
                    try { dr = TorrentDataHandler.data.OthersTable.Rows.Find(index); }
                    catch { return;  }
                    break;
                case "movies":
                    try { dr = TorrentDataHandler.data.MoviesTable.Rows.Find(index); }
                    catch { return; }
                    break;
                case "music":
                    try { dr = TorrentDataHandler.data.TorrentsTable.Rows.Find(index); }
                    catch { return; }
                    break;
                default:
                    return;
            }
            
            try
            {
                if (dr != null)
                    dr.BeginEdit();
                else
                    return;
                path = (dr["File Path"].Equals(DBNull.Value)) ? null : (string)dr["File Path"];
                save = (dr["Save Structure"].Equals(DBNull.Value)) ? null : (string)dr["Save Structure"];

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
                    ErrorWindow ew = new ErrorWindow();
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
        public static void SendTorrentWithoutSavePath(string table, int index)
        {
            string filepath = null;
            DataRow dr = null;
            switch (table)
            {
                case "others":
                    try { dr = TorrentDataHandler.data.OthersTable.Rows.Find(index); }
                    catch { return; }
                    break;
                case "movies":
                    try { dr = TorrentDataHandler.data.MoviesTable.Rows.Find(index); }
                    catch { return;  }
                    break;
                case "music":
                    try { dr = TorrentDataHandler.data.TorrentsTable.Rows.Find(index); }
                    catch { return;  }
                    break;
                default:
                    return;
            }

            try
            {
                Clipboard.SetData(DataFormats.StringFormat, dr["Save Structure"]);

                if (dr != null)
                    dr.BeginEdit();
                else
                    return;
                filepath = (dr["File Path"].Equals(DBNull.Value)) ? null : (string)dr["File Path"];

                //Preliminary error checking
                if (filepath == null || !DirectoryHandler.GetFileExists(filepath))
                {
                    dr.SetColumnError(TorrentDataHandler.data.TorrentsTable.Columns["File Path"], "File does not exist");
                    dr.EndEdit();
                    return;
                }
                if (!DirectoryHandler.GetFileExists(SettingsHandler.GetTorrentClientFolder() + "\\uTorrent.exe"))
                {
                    ErrorWindow ew = new ErrorWindow();
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
                sendTorrentProcess.StartInfo.FileName = filepath;
                sendTorrentProcess.Start();
                sendTorrentProcess.Close();
                /*
                //torrentClient.exe /directory "C:\Save Path" "D:\Some folder\your.torrent"

                string fullArgument = "/directory " + "\"" + filepath + "\"";
                sendTorrentProcess.StartInfo.WorkingDirectory = SettingsHandler.GetTorrentClientFolder();
                sendTorrentProcess.StartInfo.Arguments = fullArgument;
                sendTorrentProcess.StartInfo.FileName = SettingsHandler.GetTorrentClient();

                sendTorrentProcess.Start();
                sendTorrentProcess.Dispose();
                sendTorrentProcess.Close();
                 * */
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