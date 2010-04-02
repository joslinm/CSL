using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace CSL_Test__1
{
    class uTorrentHandler
    {
        TorrentXMLHandler xml = new TorrentXMLHandler();
        SettingsHandler settings = new SettingsHandler();

        public void SendTorrents()
        {
            System.Data.DataRowCollection rows = xml.dataset.Tables[0].Rows;

            for (int a = 0; a < rows.Count; a++)
            {
                try
                {
                    Process sendTorrentProcess = new Process();
                    //torrentClient.exe /directory "C:\Save Path" "D:\Some folder\your.torrent"

                    string fullArgument = "//directory " + "\"" + settings.GetDownloadDirectory() + "\" "
                        + "\"" + rows[a]["File"] + "\"";
                    sendTorrentProcess.StartInfo.WorkingDirectory = settings.GetTorrentClientFolder();
                    sendTorrentProcess.StartInfo.Arguments = fullArgument;
                    sendTorrentProcess.StartInfo.FileName = settings.GetTorrentClient();

                    sendTorrentProcess.Start();

                    Thread.Sleep(100);
                    sendTorrentProcess.WaitForInputIdle();
                    sendTorrentProcess.Dispose();
                    sendTorrentProcess.Close();
                }
                catch (Exception e)
                {
                    Debug.Print(e.ToString());
                }
            }
        }

    }
}
