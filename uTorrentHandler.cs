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

        public void SendAllTorrents(TorrentXMLHandler xml)
        {
            xml.table.Columns["Handled"].ReadOnly = false;
            
            foreach(DataRow row in xml.dataset.Tables[0].Rows)
            {
                if (!(bool)row["Error"] && !(bool)row["Handled"])
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
            }
            xml.table.Columns["Handled"].ReadOnly = true;
        }
        public bool SendTorrent(string save, string path)
        {
            try
            {
                Process sendTorrentProcess = new Process();
                //torrentClient.exe /directory "C:\Save Path" "D:\Some folder\your.torrent"

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
            catch (Exception e)
            {
                Debug.Print(e.ToString());
                return false;
            }

            return true;
        }
    }
}
