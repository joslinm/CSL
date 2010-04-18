using System;
using System.Threading;

namespace CSL_Test__1
{
    class TorrentWatch
    {
        SettingsHandler settings = new SettingsHandler();
        TorrentBuilder builder = new TorrentBuilder();
        DirectoryHandler directory = new DirectoryHandler();
        TorrentXMLHandler xml;
        string saveFolder = null;

        public TorrentWatch(TorrentXMLHandler data)
        {
            xml = data;
            saveFolder = settings.GetTorrentSaveFolder();
        }

        public void Watch()
        {
            Torrent[] torrents;
            string[] torrentFiles;
            string[] zipFiles;
            bool autocheck = settings.GetAutoHandleBool();
            decimal sleep = settings.GetAutoHandleTime();

            while (autocheck)
            {
                torrentFiles = directory.GetTorrents();
                zipFiles = directory.GetTorrentZips();

                if (torrentFiles != null)
                {
                    torrents = builder.Build(torrentFiles);
                    builder.Dispose();
                    xml.AddTorrents(torrents);
                    directory.MoveProcessedFiles(xml);
                }
                if (zipFiles != null)
                {
                    try
                    {
                        object[] rawFiles = directory.UnzipFiles(zipFiles);
                        string[] files = Array.ConvertAll<object, string>(rawFiles, Convert.ToString);
                        if (files != null)
                        {
                            torrents = builder.Build(files);
                            builder.Dispose();
                            xml.AddTorrents(torrents);
                            directory.MoveProcessedFiles(xml);
                            directory.MoveProcessedZipFiles();
                        }
                    }
                    catch (Exception e)
                    { }
                }

                int s = Decimal.ToInt32(sleep);
                Thread.Sleep(s * 1000);
            }
        }
                    
    }
}
