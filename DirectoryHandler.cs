using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using ICSharpCode.SharpZipLib.Zip;
using System.Collections;

namespace CSL_Test__1
{
    class DirectoryHandler
    {
        SettingsHandler settings = new SettingsHandler();
        FastZip fz = new FastZip();

        public bool DeleteFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
                return true;
            }
            else
                return false;
        }
        public bool DeleteFolder(string folder)
        {
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
                return true;
            }
            else
                return false;
        }
        public bool DeleteTempFolder()
        {
            Directory.Delete(settings.GetTorrentSaveFolder() + @"\[CSL]--Temp", true);

            if (!Directory.Exists(settings.GetTorrentSaveFolder() + @"\[CSL]--Temp"))
                return true;
            else
                return false;
        }
        public void DeleteZipFiles()
        {
            string[] zipFiles = Directory.GetFiles(settings.GetTorrentSaveFolder(), "*.zip", SearchOption.TopDirectoryOnly);

            for (int a = 0; a < zipFiles.Length; a++)
            {
                File.Delete(zipFiles[a]);
            }
        }

        public string[] GetTorrentZips()
        {
            string[] files = Directory.GetFiles(settings.GetTorrentSaveFolder(), "*.zip", SearchOption.TopDirectoryOnly);
            return (files.Length == 0) ? null : files;
        }
        public string[] GetTorrents()
        {
            string[] files = Directory.GetFiles(settings.GetTorrentSaveFolder(), "*.torrent", SearchOption.TopDirectoryOnly);
            return (files.Length == 0) ? null : files;
        }
        public string[] UnzipFiles(string[] zipFiles)
        {
            ArrayList al = new ArrayList(); //not sure of proper array size, so using arraylist
            string[] files = null;

            for (int a = 0; a < zipFiles.Length; a++)
            {
                try
                {
                    fz.ExtractZip(zipFiles[a], settings.GetTorrentSaveFolder() + @"\[CSL]--Temp\" + Path.GetFileNameWithoutExtension(zipFiles[a]), ".torrent");

                    files = Directory.GetFiles(settings.GetTorrentSaveFolder() + @"\[CSL]--Temp\"
                        + Path.GetFileNameWithoutExtension(zipFiles[a]), 
                        ".torrent", SearchOption.AllDirectories);

                    for (int b = 0; b < files.Length; b++)
                        al.Add(files[b]);
                }

                catch
                {}
            }

            return (string[])al.ToArray(); //return all files of all zips
        }

        public string MoveTorrentFile(string file, string where)
        {
            string cslSaveFolder = null;
            string fileName = null;
            fileName = Path.GetFileName(file);

            switch (where)
            {
                case "handled":
                    cslSaveFolder = settings.GetTorrentSaveFolder() + @"\[CSL] -- Handled Torrents\";
                    break;
                case "unhandled":
                    cslSaveFolder = settings.GetTorrentSaveFolder() + @"\[CSL] -- Unhandled Torrents\";
                    break;
                default:
                    cslSaveFolder = settings.GetTorrentSaveFolder() + '\\' + where + '\\';
                    break;
            }
                    

            if (!Directory.Exists(cslSaveFolder))
                Directory.CreateDirectory(cslSaveFolder);

            if (!File.Exists(cslSaveFolder + fileName))
            {
                try
                {
                    File.Move(file, cslSaveFolder + fileName);
                }

                catch { }
            }
            else
                File.Delete(file);

            if (File.Exists(cslSaveFolder + fileName))
                return cslSaveFolder + fileName;
            else
                return null;     
        }
        public void MoveProcessedZipFiles()
        {
            string[] zipFiles = Directory.GetFiles(settings.GetTorrentSaveFolder(), "*.zip", SearchOption.TopDirectoryOnly);

            if (!Directory.Exists(settings.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips"))
                Directory.CreateDirectory(settings.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips");

            for (int a = 0; a < zipFiles.Length; a++)
            {
                try
                {
                    File.Move(zipFiles[a], settings.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips\" + zipFiles[a].Substring(zipFiles[a].LastIndexOf('\\') + 1));
                }
                catch
                {
                    File.Delete(zipFiles[a]);
                }
            }
        }

    }
}
