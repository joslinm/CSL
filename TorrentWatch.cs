using System;
using System.Threading;

namespace CSL_Test__1
{
    class TorrentWatch
    {
        SettingsHandler settings = new SettingsHandler();
        TorrentBuilder builder = new TorrentBuilder();
        DirectoryHandler dh = new DirectoryHandler();
        TorrentXMLHandler xml;
        string saveFolder = null;

        public TorrentWatch(TorrentXMLHandler data)
        {
            xml = data;
            saveFolder = settings.GetTorrentSaveFolder();
            builder.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(builder_RunWorkerCompleted);
        }

        void builder_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                xml.AddTorrents((Torrent[])e.Result);
                builder.Dispose();
                dh.MoveProcessedFiles(xml);
                xml.Save();
            }
        }

        public void Watch()
        {
            string[] torrentFiles;
            string[] zipFiles;
            bool autocheck = settings.GetAutoHandleBool();

            while (autocheck)
            {
                decimal sleep = settings.GetAutoHandleTime();
                int s = Decimal.ToInt32(sleep);

                torrentFiles = dh.GetTorrents();
                zipFiles = dh.GetTorrentZips();

                if (torrentFiles != null)
                {
                    builder.RunWorkerAsync(torrentFiles);
                    while (builder.IsBusy)
                        Thread.Sleep(500);
                }
                if (zipFiles != null)
                {
                    object[] rawFiles = dh.UnzipFiles(zipFiles);
                    torrentFiles = Array.ConvertAll<object, string>(rawFiles, Convert.ToString);

                    builder.RunWorkerAsync(torrentFiles);

                    while (builder.IsBusy)
                        Thread.Sleep(500);
                }

                Thread.Sleep(s * 1000);
            }
        }
                    
    }
}
