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
        TorrentXMLHandler xml = new TorrentXMLHandler();
        SettingsHandler settings = new SettingsHandler();

        public void SendTorrents()
        {
            foreach(DataRow row in xml.dataset.Tables[0].Rows)
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

                    /*sendTorrentProcess.Start();

                    Thread.Sleep(100);
                    sendTorrentProcess.WaitForInputIdle();
                    sendTorrentProcess.Dispose();
                    sendTorrentProcess.Close();*/

                }
                catch (Exception e)
                {
                    Debug.Print(e.ToString());
                }

                row.BeginEdit();
                row["Handled"] = true;
                row.EndEdit();
            }
            xml.dataset.AcceptChanges();
        }

    }
}
