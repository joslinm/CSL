using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Data;

namespace CSL_Test__1
{
    class uTorrentHandler
    {
        SettingsHandler settings = new SettingsHandler();
        DirectoryHandler dh = new DirectoryHandler();

        public void SendAllTorrents(TorrentXMLHandler xml)
        {
            if (!dh.GetFileExists(settings.GetTorrentClientFolder() + "\\uTorrent.exe"))
            {
                ErrorWindow ew = new ErrorWindow();
                ew.IssueGeneralWarning("Be sure uTorrent folder is correct", "uTorrent.exe does not exist", null);
            }
            else
            {
                xml.table.Columns["Handled"].ReadOnly = false;
                xml.table.Columns["Error"].ReadOnly = false;

                foreach (DataRow row in xml.dataset.Tables[0].Rows)
                {
                    try
                    {
                        if (!(bool)row["Error"] && !(bool)row["Handled"] && !row["File Path"].Equals(DBNull.Value))
                        {
                            if (dh.GetFileExists((string)row["File Path"]))
                            {
                                try
                                {
                                    Process sendTorrentProcess = new Process();
                                    //torrentClient.exe /directory "C:\Save Path" "D:\Some folder\your.torrent"

                                    string fullArgument = "/directory " + "\"" + row["Save Structure"] + "\" "
                                        + "\"" + row["File Path"] + "\"";
                                    sendTorrentProcess.StartInfo.WorkingDirectory = settings.GetTorrentClientFolder();
                                    sendTorrentProcess.StartInfo.Arguments = fullArgument;
                                    sendTorrentProcess.StartInfo.FileName = settings.GetTorrentClient();

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
                xml.table.Columns["Handled"].ReadOnly = true;
                xml.table.Columns["Error"].ReadOnly = true;
            }
        }
        public string SendTorrent(string save, string path)
        {
            if (!dh.GetFileExists(settings.GetTorrentClientFolder() + "\\uTorrent.exe"))
            {
                ErrorWindow ew = new ErrorWindow();
                ew.IssueGeneralWarning("Be sure uTorrent folder is correct", "uTorrent.exe does not exist", null);
                return "uTorrent.exe does not exist";
            }
            else
            {
                try
                {
                    Process sendTorrentProcess = new Process();
                    //torrentClient.exe /directory "C:\Save Path" "D:\Some folder\your.torrent"
                    if (dh.GetFileExists(path))
                    {
                        string fullArgument = "/directory " + "\"" + save + "\" "
                            + "\"" + path + "\"";
                        sendTorrentProcess.StartInfo.WorkingDirectory = settings.GetTorrentClientFolder();
                        sendTorrentProcess.StartInfo.Arguments = fullArgument;
                        sendTorrentProcess.StartInfo.FileName = settings.GetTorrentClient();

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
