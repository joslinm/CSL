using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace CSL_Test__1
{
    class TorrentWatch
    {
        SettingsHandler settings = new SettingsHandler();
        TorrentBuilder builder = new TorrentBuilder();
        TorrentXMLHandler xml = new TorrentXMLHandler();
        DirectoryHandler directory = new DirectoryHandler();
        string saveFolder = null;

        public TorrentWatch()
        {
            saveFolder = settings.GetTorrentSaveFolder();
        }

        public void Start()
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
                    xml.AddTorrents(torrents);
                }
                if (zipFiles != null)
                {
                    torrents = builder.Build(directory.UnzipFiles(zipFiles));
                    xml.AddTorrents(torrents);
                }

                if (autocheck)
                    Thread.Sleep((int)sleep * 1000);
            }
        }
                    
    }
}
