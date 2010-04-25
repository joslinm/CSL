using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL_Test__1
{
    class SettingsHandler
    {
        static SettingsFile settings = new SettingsFile();

        public void Save()
        {
            settings.Save();
        }

        #region Set Options
        public void SetTrackTorrents(bool value)
        {
            settings.TrackTorrentFiles = value;
            settings.Save();
        }
        public void SetTrackZips(bool value)
        {
            settings.TrackZipFiles = value;
            settings.Save();
        }
        public void SetRemoveDoubleSpaces(bool value)
        {
            settings.RemoveDoubleSpaces = value;
            settings.Save();
        }
        public void SetAutoHandleBool(bool value)
        {
            settings.AutoCheck = value;
            settings.Save();
        }
        public void SetAutoHandleTime(decimal value)
        {
            settings.AutoCheckTime = value;
            settings.Save();
        }
        public void SetArtistFlip(bool value)
        {
            settings.ArtistFlip = value;
            settings.Save();
        }

        public void SetDisableNotifications(bool value)
        {
            settings.DisableNotifications = value;
            settings.Save();
        }
        
        public void SetDeleteZipFiles(bool value)
        {
            settings.DeleteZipFiles = value;
            settings.Save();
        }
        public void AddDownloadFormat(string value)
        {
            if (!settings.DownloadFormats.Contains(value))
                settings.DownloadFormats += " " + value;

            settings.Save();
        }
        public void RemoveDownloadFormat(string value)
        {
            if (settings.DownloadFormats.Contains(value))
                settings.DownloadFormats = settings.DownloadFormats.Replace(value, "");

            settings.Save();
        }
        public void SetUppercaseAllFolderNames(bool value)
        {
            settings.UppercaseAllFolderNames = value;
            settings.Save();
        }
        public void SetLowercaseAllFolderNames(bool value)
        {
            settings.LowercaseAllFolderNames = value;
            settings.Save();
        }

        public void SetTorrentClient(string value)
        {
            settings.TorrentClient = value;
            settings.Save();
        }
        public void SetTorrentClientFolder(string value)
        {
            settings.TorrentClientFolder = value;
            settings.Save();
        }
        public void SetTorrentSaveFolder(string value)
        {
            settings.TorrentSaveFolder = value;
            settings.Save();
        }
        public void SetDownloadFolder(string value)
        {
            settings.DownloadFolder = value;
            settings.Save();
        }
        public void SetCustomDirectory(string value)
        {
            settings.CustomDirectory = value;
            settings.Save();
        }
        #endregion
        #region Get Options
        public bool GetTrackZips()
        {
            return settings.TrackZipFiles;
        }
        public bool GetTrackTorrents()
        {
            return settings.TrackTorrentFiles;
        }
        public bool GetDoubleSpaceRemoval()
        {
            return settings.RemoveDoubleSpaces;
        }
        public bool GetArtistFlip()
        {
            return settings.ArtistFlip;
        }
        public bool GetDeleteZipFiles()
        {
            return settings.DeleteZipFiles;
        }
        public bool GetDisableNotifications()
        {
            return settings.DisableNotifications;
        }
        public bool GetDownloadFormatExists(string format)
        {
            if (settings.DownloadFormats.Contains(format))
                return true;
            else
                return false;
        }
        public bool GetUppercaseAllFolderNames()
        {
            return settings.UppercaseAllFolderNames;
        }
        public bool GetLowercaseAllFolderNames()
        {
            return settings.LowercaseAllFolderNames;
        }
        public string GetTorrentClient()
        {
            return settings.TorrentClient;
        }
        public string GetTorrentClientFolder()
        {
            return settings.TorrentClientFolder;
        }
        public string GetDownloadDirectory()
        {
            return settings.DownloadFolder;
        }
        public string GetTorrentSaveFolder()
        {
            return settings.TorrentSaveFolder;
        }
        public string GetDownloadFolder()
        {
            return settings.DownloadFolder;
        }
        public string GetCustomDirectory()
        {
            return settings.CustomDirectory;
        }
        public bool GetAutoHandleBool()
        {
            return settings.AutoCheck;
        }
        public decimal GetAutoHandleTime()
        {
            return settings.AutoCheckTime;
        }
        #endregion

    }
}
