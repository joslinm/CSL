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

        public void DeleteFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            if (File.Exists(fs.Name))
                File.Delete(fs.Name);
            
            fs.Dispose();
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
            if (Directory.Exists(settings.GetTorrentSaveFolder() + @"\[CSL]--Temp"))
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
        public string GetFileName(string path, bool extension)
        {
            string filename;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            if (extension)
                filename = Path.GetFileName(fs.Name);
            else
                filename = Path.GetFileNameWithoutExtension(fs.Name);

            fs.Dispose();
            return filename;
        }
        public string GetDirectoryName(string path)
        {
            string directoryname;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            directoryname = Path.GetDirectoryName(fs.Name);

            fs.Dispose();
            return directoryname;
        }
        public bool GetFileExists(string path)
        {
            bool value;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            value = File.Exists(fs.Name);

            fs.Dispose();
            return value;
        }

        public string[] UnzipFile(string zipFile)
        {
            string[] files;

            try
            {
                string destination = settings.GetTorrentSaveFolder() + @"\[CSL]--Temp\" + this.GetFileName(zipFile, false);
                Directory.CreateDirectory(destination);
                fz.ExtractZip(zipFile,destination,".torrent");

                files = Directory.GetFiles(destination,"*.torrent",SearchOption.AllDirectories);
                return files;
            }

            catch(Exception e)
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
                string destination = settings.GetTorrentSaveFolder() + @"\[CSL]--Temp\" + this.GetFileName(zipfile, false) + @"\";

                try
                {
                    fz.ExtractZip(zipfile, destination, ".torrent");
                }
                catch (Exception e) { }

                try
                {
                    files = Directory.GetFiles(destination,
                        "*.torrent", SearchOption.AllDirectories);
                }
                catch (DirectoryNotFoundException e)
                {
                    //This most likely means that the zip file had nothing in it
                    //Flagging a warning here would be wise
                }
                finally
                {
                    foreach (string file in files)
                        al.Add(file);
                }
            }
            return al.ToArray(); //return all files of all zips
        }

        public string MoveTorrentFile(string file, string where)
        {
            string cslSaveFolder = null;
            string fileName = null;

            fileName = this.GetFileName(file, true);

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

            if (File.Exists(cslSaveFolder + fileName))
            {
                if (!file.Equals(cslSaveFolder + fileName))
                    File.Delete(file); //If pulling in from a handled directory, don't delete
            }
            else
            {
                try
                {
                    File.Move(file, cslSaveFolder + fileName);
                }
                catch(Exception eee)
                {
                    GC.Collect();
                    try
                    {
                        File.Move(file, cslSaveFolder + fileName);
                    }
                    catch(Exception e)
                    {
                        ErrorWindow ew = new ErrorWindow();
                        ew.IssueFileMoveWarning(file, true);
                    }
                }
            }
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
                File.Move(zipFiles[a], settings.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips\" + zipFiles[a].Substring(zipFiles[a].LastIndexOf('\\') + 1));
            }
        }
        public void MoveProcessedFiles(TorrentXMLHandler data)
        {
            string filepath;
            int index;
            bool skip = false;

            foreach (DataRow datarow in data.table.Rows)
            {
                if (!datarow["File Path"].Equals(DBNull.Value))
                {
                    filepath = (string)datarow["File Path"];

                    index = data.table.Rows.IndexOf(datarow);

                    if (filepath.Contains("[CSL] -- Unhandled Torrents") && !(bool)datarow["Error"])
                        skip = false;
                    else if (!filepath.Contains("[CSL] -- Handled Torrents") && !(bool)datarow["Error"] && !(bool)datarow["Handled"])
                        skip = false;
                    else
                        skip = true;

                    if (!skip)
                    {
                        switch ((bool)datarow["Error"])
                        {
                            case true:
                                data.table.Rows[index]["File Path"] = MoveTorrentFile((string)datarow["File Path"], "unhandled");
                                break;
                            case false:
                                data.table.Rows[index]["File Path"] = MoveTorrentFile((string)datarow["File Path"], "handled");
                                break;
                            default:
                                data.table.Rows[index]["File Path"] = MoveTorrentFile((string)datarow["File Path"], "unhandled");
                                break;
                        }
                    }
                }
            }

            
            data.table.AcceptChanges();
            data.Save();

            string[] zipFiles = Directory.GetFiles(settings.GetTorrentSaveFolder(), "*.zip", SearchOption.TopDirectoryOnly);

            if (zipFiles != null || zipFiles.Length > 0)
            {
                if (!Directory.Exists(settings.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips"))
                    Directory.CreateDirectory(settings.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips");

                for (int a = 0; a < zipFiles.Length; a++)
                {
                    File.Move(zipFiles[a], settings.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips\" + zipFiles[a].Substring(zipFiles[a].LastIndexOf('\\') + 1));
                }
            }

            if (Directory.Exists(settings.GetTorrentSaveFolder() + @"\[CSL]--Temp"))
                Directory.Delete(settings.GetTorrentSaveFolder() + @"\[CSL]--Temp", true);
        }

    }
}
