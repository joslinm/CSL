using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL
{
    class SettingsHandler
    {
        static SettingsFile settings = new SettingsFile();

        public SettingsHandler()
        {
        }
        public static void Save()
        {
            settings.Save();
        }
        #region Set Options
        public static void SetVariousArtistsDownloadDirectory(string value)
        {
            settings.VariousArtistsDownloadDirectory = value;
            settings.Save();
        }
        public static void SetVariousArtistsDownloadSwitches(string value)
        {
            settings.VariousArtistsDownloadSwitches = value;
            settings.Save();
        }
        public static void SetOtherDownloadDirectory(string value)
        {
            settings.OtherDownloadDirectory = value;
            settings.Save();
        }
        public static void SetMovieDownloadDirectory(string value)
        {
            settings.MovieDownloadDirectory = value;
            settings.Save();
        }
        public static void SetMovieCustomDirectory(string value)
        {
            settings.MovieCustomDirectory = value;
            settings.Save();
        }
        public static void SetCurrentVersion(string value)
        {
            settings.CurrentVersion = value;
            settings.Save();
        }
        public static void SetDeleteTheFolderNames(bool value)
        {
            settings.DeleteTheFolderNames = value;
            settings.Save();
        }
        public static void SetSkipReleaseFormatCheck(bool value)
        {
            settings.SkipReleaseFormatCheck = value;
            settings.Save();
        }
        public static void SetReleaseFormatName(string format, string name)
        {
            for (int a = 0; a < settings.ReleaseFormatNames.Count; a++)
            {
                if (settings.ReleaseFormatNames[a].Contains(format + ":"))
                {
                    int index = settings.ReleaseFormatNames[a].IndexOf(':');
                    string substring = settings.ReleaseFormatNames[a].Substring(index);
                    settings.ReleaseFormatNames[a] = settings.ReleaseFormatNames[a].Replace(substring, ":" + name);
                }
            }

            settings.Save();
        }
        public static void SetBitrateName(string bitrate, string name)
        {
            for (int a = 0; a < settings.BitrateNames.Count; a++)
            {
                if (settings.BitrateNames[a].Contains(bitrate + ":"))
                {
                    int index = settings.BitrateNames[a].IndexOf(':');
                    string substring = settings.BitrateNames[a].Substring(index);
                    settings.BitrateNames[a] = settings.BitrateNames[a].Replace(substring, ":" + name);
                }
            }

            settings.Save();
        }
        public static void SetTrackTorrents(bool value)
        {
            settings.TrackTorrentFiles = value;
            settings.Save();
        }
        public static void SetTrackZips(bool value)
        {
            settings.TrackZipFiles = value;
            settings.Save();
        }
        public static void SetRemoveDoubleSpaces(bool value)
        {
            settings.RemoveDoubleSpaces = value;
            settings.Save();
        }
        public static void SetAutoHandleBool(bool value)
        {
            settings.AutoCheck = value;
            settings.Save();
        }
        public static void SetAutoHandleTime(decimal value)
        {
            settings.AutoCheckTime = value;
            settings.Save();
        }
        public static void SetArtistFlip(bool value)
        {
            settings.ArtistFlip = value;
            settings.Save();
        }

        public static void SetDisableNotifications(bool value)
        {
            settings.DisableNotifications = value;
            settings.Save();
        }

        public static void SetRawHandleTime(decimal time)
        {
            settings.RawHandleTime = time;
            settings.Save();
        }
        
        public static void SetDeleteZipFiles(bool value)
        {
            settings.DeleteZipFiles = value;
            settings.Save();
        }
        public static void AddDownloadFormat(string value)
        {
            if (!settings.DownloadFormats.Contains(value))
                settings.DownloadFormats += " " + value;

            settings.Save();
        }
        public static void RemoveDownloadFormat(string value)
        {
            if (settings.DownloadFormats.Contains(value))
                settings.DownloadFormats = settings.DownloadFormats.Replace(value, "");

            settings.Save();
        }
        public static void SetUppercaseAllFolderNames(bool value)
        {
            settings.UppercaseAllFolderNames = value;
            settings.Save();
        }
        public static void SetLowercaseAllFolderNames(bool value)
        {
            settings.LowercaseAllFolderNames = value;
            settings.Save();
        }

        public static void SetTorrentClient(string value)
        {
            settings.TorrentClient = value;
            settings.Save();
        }
        public static void SetTorrentClientFolder(string value)
        {
            settings.TorrentClientFolder = value;
            settings.Save();
        }
        public static void SetTorrentSaveFolder(string value)
        {
            settings.TorrentSaveFolder = value;
            settings.Save();
        }
        public static void SetDownloadFolder(string value)
        {
            settings.DownloadFolder = value;
            settings.Save();
        }
        public static void SetCustomDirectory(string value)
        {
            settings.CustomDirectory = value;
            settings.Save();
        }
        public static void SetMinimizeToTray(bool value)
        {
            settings.MinimizeToTray = value;
            settings.Save();
        }
        public static void SetHandleLoneTsAsAlbums(bool value)
        {
            settings.ProcessLoneTsAlbums = value;
            settings.Save();
        }
        public static void SetTimeFormat(string value)
        {
            settings.TimeFormat = value;
            settings.Save();
        }
        #endregion
        #region Get Options
        public static string GetVariousArtistsDownloadDirectory()
        {
            return settings.VariousArtistsDownloadDirectory;
        }
        public static string GetVariousArtistsDownloadSwitches()
        {
            return settings.VariousArtistsDownloadSwitches;
        }
        public static string GetOtherDownloadDirectory()
        {
            return settings.OtherDownloadDirectory;
        }
        public static string GetMovieDownloadDirectory()
        {
            return settings.MovieDownloadDirectory;
        }
        public static string GetMovieCustomDirectory()
        {
            return settings.MovieCustomDirectory;
        }
        public static string GetCurrentVersion()
        {
            return settings.CurrentVersion;
        }
        public static string GetTimeFormat()
        {
            return settings.TimeFormat;
        }
        public static decimal GetRawHandleTime()
        {
            return settings.RawHandleTime;
        }
        public static bool GetDeleteThe()
        {
            return settings.DeleteTheFolderNames;
        }
        public static bool GetTrackZips()
        {
            return settings.TrackZipFiles;
        }
        public static bool GetTrackTorrents()
        {
            return settings.TrackTorrentFiles;
        }
        public static bool GetHandleLoneTAsAlbum()
        {
            return settings.ProcessLoneTsAlbums;
        }
        public static string GetReleaseFormat(string format)
        {
            for (int a = 0; a < settings.ReleaseFormatNames.Count; a++)
            {
                if (settings.ReleaseFormatNames[a].Contains(format + ":"))
                {
                    int index = settings.ReleaseFormatNames[a].IndexOf(':');
                    string substring = settings.ReleaseFormatNames[a].Substring(++index);
                    return substring;
                }
            }

            return format;
        }
        public static string GetBitrate(string bitrate)
        {
            for (int a = 0; a < settings.BitrateNames.Count; a++)
            {
                if (settings.BitrateNames[a].Contains(bitrate + ":"))
                {
                    int index = settings.BitrateNames[a].IndexOf(':');
                    string substring = settings.BitrateNames[a].Substring(++index);
                    return substring;
                }
            }

            return bitrate;
        }

        public static bool GetDoubleSpaceRemoval()
        {
            return settings.RemoveDoubleSpaces;
        }
        public static bool GetArtistFlip()
        {
            return settings.ArtistFlip;
        }
        public static bool GetDeleteZipFiles()
        {
            return settings.DeleteZipFiles;
        }
        public static bool GetDisableNotifications()
        {
            return settings.DisableNotifications;
        }
        public static bool GetMinimizeToTray()
        {
            return settings.MinimizeToTray;
        }
        public static bool GetDownloadFormatExists(string format)
        {
            if (settings.DownloadFormats.Contains(format))
                return true;
            else
                return false;
        }
        public static bool GetUppercaseAllFolderNames()
        {
            return settings.UppercaseAllFolderNames;
        }
        public static bool GetLowercaseAllFolderNames()
        {
            return settings.LowercaseAllFolderNames;
        }
        public static string GetTorrentClient()
        {
            return settings.TorrentClient;
        }
        public static string GetTorrentClientFolder()
        {
            return settings.TorrentClientFolder;
        }
        public static string GetDownloadDirectory()
        {
            return settings.DownloadFolder;
        }
        public static string GetTorrentSaveFolder()
        {
            return settings.TorrentSaveFolder;
        }
        public static string GetDownloadFolder()
        {
            return settings.DownloadFolder;
        }
        public static string GetCustomDirectory()
        {
            return settings.CustomDirectory;
        }
        public static bool GetAutoHandleBool()
        {
            return settings.AutoCheck;
        }
        public static decimal GetAutoHandleTime()
        {
            return settings.AutoCheckTime;
        }
        public static bool GetSkipReleaseCheck()
        {
            return settings.SkipReleaseFormatCheck;
        }
        #endregion

    }
}
