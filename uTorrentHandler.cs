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

        public static void SendAllTorrents()
        {
            try
            {
                ErrorWindow ew = new ErrorWindow();

                if (!DirectoryHandler.GetFileExists(SettingsHandler.GetTorrentClientFolder() + "\\uTorrent.exe"))
                {
                    ew.IssueGeneralWarning("Be sure uTorrent folder is correct", "uTorrent.exe does not exist", null);
                }
                else
                {
                    table.Columns["Handled"].ReadOnly = false;
                    table.Columns["Error"].ReadOnly = false;

                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        try
                        {
                            if (!(bool)row["Error"] && !(bool)row["Handled"] && !row["File Path"].Equals(DBNull.Value))
                            {
                                if (DirectoryHandler.GetFileExists((string)row["File Path"]))
                                {
                                    try
                                    {
                                        Process sendTorrentProcess = new Process();
                                        //torrentClient.exe /directory "C:\Save Path" "D:\Some folder\your.torrent"

                                        string fullArgument = "/directory " + "\"" + row["Save Structure"] + "\" "
                                            + "\"" + row["File Path"] + "\"";
                                        sendTorrentProcess.StartInfo.WorkingDirectory = SettingsHandler.GetTorrentClientFolder();
                                        sendTorrentProcess.StartInfo.Arguments = fullArgument;
                                        sendTorrentProcess.StartInfo.FileName = SettingsHandler.GetTorrentClient();

                                        sendTorrentProcess.Start();

                                        Thread.Sleep(100);
                                        sendTorrentProcess.Dispose();
                                        sendTorrentProcess.Close();

                                        row.BeginEdit();
                                        row["Handled"] = true;
                                        row.EndEdit();

                                    }
                                    catch (Exception e)
                                    {
                                        Debug.Print(e.ToString());
                                    }
                                }
                                else
                                {
                                    row.BeginEdit();
                                    row["Error"] = true;
                                    row["Birth"] = "File does not exist";
                                    row.EndEdit();
                                }
                            }
                            else if (row["File Path"].Equals(DBNull.Value))
                            {
                                row.BeginEdit();
                                row["Error"] = true;
                                row["Site Origin"] = "No file path";
                                row.EndEdit();
                            }
                        }
                        catch (Exception e)
                        {
                            row.BeginEdit();
                            row["Error"] = true;
                            row["Birth"] = e.Message;
                            row.EndEdit();
                        }
                    }
                    TorrentXMLHandler.table.Columns["Handled"].ReadOnly = true;
                    TorrentXMLHandler.table.Columns["Error"].ReadOnly = true;
                    Save();
                }
            }
            catch (Exception e)
            {
                ErrorWindow ew = new ErrorWindow();
                ew.IssueGeneralWarning("Fatal Error", "Please report", e.Message + "\n" + e.StackTrace);
            }
        }

        protected override void OnDoWork(System.ComponentModel.DoWorkEventArgs e)
        {
            SendAllTorrents();
        }

        public static string SendTorrent(string save, string path)
        {
            ErrorWindow ew = new ErrorWindow();

            if (!DirectoryHandler.GetFileExists(SettingsHandler.GetTorrentClientFolder() + "\\uTorrent.exe"))
            {
                ew.IssueGeneralWarning("Be sure uTorrent folder is correct", "uTorrent.exe does not exist", null);
                return "uTorrent.exe does not exist";
            }
            else
            {
                try
                {
                    Process sendTorrentProcess = new Process();
                    //torrentClient.exe /directory "C:\Save Path" "D:\Some folder\your.torrent"
                    if (DirectoryHandler.GetFileExists(path))
                    {
                        string fullArgument = "/directory " + "\"" + save + "\" "
                            + "\"" + path + "\"";
                        sendTorrentProcess.StartInfo.WorkingDirectory = SettingsHandler.GetTorrentClientFolder();
                        sendTorrentProcess.StartInfo.Arguments = fullArgument;
                        sendTorrentProcess.StartInfo.FileName = SettingsHandler.GetTorrentClient();

                        sendTorrentProcess.Start();

                        Thread.Sleep(100);
                        sendTorrentProcess.Dispose();
                        sendTorrentProcess.Close();
                    }
                    else
                    {
                        return "File does not exist";
                    }
                }
                catch (Exception e)
                {
                    if (path == null || path == "")
                    {
                        return "Empty file path";
                    }
                    else
                    {
                        return e.Message;
                    }
                }

                return "SUCCESS";
            }
        }
    }
}
