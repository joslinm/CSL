using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Data;
using ICSharpCode.SharpZipLib.Zip;
using System.Collections;
using System.Threading;
using System.ComponentModel;

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
            try
            {
                string[] files = Directory.GetFiles(settings.GetTorrentSaveFolder(), "*.zip", SearchOption.TopDirectoryOnly);
                GC.Collect();
                return (files.Length == 0) ? null : files;
            }
            catch { return null; }
        }
        public string[] GetTorrents()
        {
            try
            {
                string[] files = Directory.GetFiles(settings.GetTorrentSaveFolder(), "*.torrent", SearchOption.TopDirectoryOnly);
                GC.Collect();
                return (files.Length == 0) ? null : files;
            }
            catch { return null; }
        }

        public string[] UnzipFile(string zipFile)
        {
            string[] files;

            try
            {
                string destination = settings.GetTorrentSaveFolder() + @"\[CSL]--Temp\" + Path.GetFileNameWithoutExtension(zipFile);
                Directory.CreateDirectory(destination);
                fz.ExtractZip(zipFile,destination,"*.torrent");

                files = Directory.GetFiles(destination,"*.torrent",SearchOption.AllDirectories);
                return files;
            }

            catch
            {
                return files = null;
            }
        }
        public object[] UnzipFiles(string[] zipFiles)
        {
            ArrayList al = new ArrayList(); //not sure of proper array size, so using arraylist
            string[] files = null;

            foreach (string zipfile in zipFiles)
            {
                string destination = settings.GetTorrentSaveFolder() + @"\[CSL]--Temp\" + Path.GetFileNameWithoutExtension(zipfile) + @"\";

                fz.ExtractZip(zipfile, destination, ".torrent");

                files = Directory.GetFiles(destination,
                    "*.torrent", SearchOption.AllDirectories);

                foreach (string file in files)
                    al.Add(file);
            }
            return al.ToArray(); //return all files of all zips
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
                int counter = 10;
                while (!File.Exists(cslSaveFolder + fileName) && counter > 0)
                {
                    try
                    {
                        File.Move(file, cslSaveFolder + fileName);
                    }
                    catch
                    {
                        Thread.Sleep(100);
                    }
                    counter--;
                }
                if (!File.Exists(cslSaveFolder + fileName))
                {
                    bool torrenthandled = (where.Equals("handled")) ? true : false;
                    ErrorWindow ew = new ErrorWindow();
                    ew.IssueFileMoveWarning(file, torrenthandled);
                }
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
                catch (Exception e)
                {
                }
            }
        }
        public void MoveProcessedFiles(TorrentXMLHandler data)
        {
            string filepath;

            foreach (DataRow datarow in data.table.Rows)
            {
                filepath = (string)datarow["File Path"];
                if (Path.GetDirectoryName(filepath).Equals(settings.GetTorrentSaveFolder()))
                {
                    switch ((bool)datarow["Error"])
                    {
                        case true:
                            datarow["File Path"] = MoveTorrentFile((string)datarow["File Path"], "unhandled");
                            break;
                        case false:
                            datarow["File Path"] = MoveTorrentFile((string)datarow["File Path"], "handled");
                            break;
                        default:
                            datarow["File Path"] = MoveTorrentFile((string)datarow["File Path"], "unhandled");
                            break;
                    }
                }
            }
        }

    }
}
