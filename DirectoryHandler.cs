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
    class DirectoryHandler : DataGridViewHandler
    {
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            MoveProcessedFiles((Torrent[])e.Argument);
        }
        public static void DeleteFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            if (File.Exists(fs.Name))
            {
                try
                {
                    File.Delete(fs.Name);
                }
                catch (Exception e)
                { }
            }

            fs.Dispose();
        }
        public static bool DeleteFolder(string folder)
        {
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
                return true;
            }
            else
                return false;
        }
        public static bool DeleteTempFolder()
        {
            if (Directory.Exists(SettingsHandler.GetTorrentSaveFolder() + @"\[CSL]--Temp"))
                Directory.Delete(SettingsHandler.GetTorrentSaveFolder() + @"\[CSL]--Temp", true);

            if (!Directory.Exists(SettingsHandler.GetTorrentSaveFolder() + @"\[CSL]--Temp"))
                return true;
            else
                return false;
        }
        public static void DeleteZipFiles()
        {
            string[] zipFiles = null;

            zipFiles = Directory.GetFiles(SettingsHandler.GetTorrentSaveFolder(), "*.zip", SearchOption.TopDirectoryOnly);

            for (int a = 0; a < zipFiles.Length; a++)
            {
                File.Delete(zipFiles[a]);
            }
        }

        public static string[] GetTorrentZips()
        {
            string[] files;

            try
            {
                files = Directory.GetFiles(SettingsHandler.GetTorrentSaveFolder(), "*.zip", SearchOption.TopDirectoryOnly);
                return (files.Length == 0) ? null : files;
            }
            catch { return null; }
        }
        public static string[] GetTorrents()
        {
            try
            {
                string[] files = Directory.GetFiles(SettingsHandler.GetTorrentSaveFolder(), "*.torrent", SearchOption.TopDirectoryOnly);
                return (files.Length == 0) ? null : files;
            }
            catch { return null; }
        }

        public static string GetFileName(string path, bool extension)
        {
            FileStream fs;
            string filename;

            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);

                if (extension)
                    filename = Path.GetFileName(fs.Name);
                else
                    filename = Path.GetFileNameWithoutExtension(fs.Name);

                fs.Dispose();
                return filename;
            }
            catch
            {
                return null;
            }

        }
        public static string GetDirectoryName(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            string directoryname = Path.GetDirectoryName(fs.Name);
            fs.Dispose();

            return directoryname;
        }
        public static string GetHTMLLookUp(string value)
        {
            ErrorWindow ew = new ErrorWindow();
            StreamReader sr = null;

            string[] ab = new string[2];
            string a;
            string b;
            int c = 0;
            string[] lines = null;
            string replace;

            if (value == null)
                return value;
            try
            {
                sr = new StreamReader(System.Windows.Forms.Application.StartupPath + @"\HTML-Look-Up.txt");

                while (!sr.EndOfStream)
                {
                    ab = sr.ReadLine().Split(' ');
                    a = ab[0];
                    b = ab[1];

                    if (value.Contains(a) || value.Contains(b))
                    {
                        while (value.Contains(a))
                        {
                            replace = sr.ReadLine();
                            value = value.Replace(a, replace);
                        }
                        while (value.Contains(b))
                        {
                            replace = sr.ReadLine();
                            value = value.Replace(b, replace);
                        }
                    }
                    else
                    {
                        sr.ReadLine();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                ew.IssueGeneralWarning("HTML replacing will not occur", "Could not find HTML-Look-Up.txt...", null);
            }
            catch (Exception e)
            {
                ew.IssueGeneralWarning("Error..", lines[c], e.Message);
            }
            finally
            {
                if (sr != null)
                    sr.Dispose();
            }

            return value;
        }
        public static bool GetFileExists(string path)
        {
            FileStream fs = null;
            bool value;

            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                value = true;
            }
            catch (DirectoryNotFoundException)
            {
                value = false;
            }
            catch (FileNotFoundException)
            {
                value = false;
            }
            catch (Exception e)
            {
                value = false;
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }

            return value;
        }
        public static bool MoveFile(string origin, string dest)
        {
            FileStream fs;

            try
            {
                fs = new FileStream(origin, FileMode.Open, FileAccess.Read);
                FileStream ds = null;

                try
                {
                    ds = new FileStream(dest + Path.GetFileName(origin), FileMode.Open, FileAccess.Read); //Try accessing destination file
                    fs.Close(); //Destination file exists if reach here
                    if (!fs.Name.Contains("[CSL] -- Handled Torrents"))
                        File.Delete(origin);
                    return true;
                }
                catch (FileNotFoundException)
                {
                    string o = fs.Name;
                    string d = dest + Path.GetFileName(fs.Name);
                    fs.Close();
                    File.Move(fs.Name, dest + Path.GetFileName(fs.Name));
                    return true;
                }
                catch (Exception) { return false; }
            }
            catch { return false; }
        }

        public static string[] UnzipFile(string zipFile)
        {
            string destination;
            string[] files;
            FastZip fz = new FastZip();

            try
            {
                destination = SettingsHandler.GetTorrentSaveFolder() + @"\[CSL]--Temp\" + DirectoryHandler.GetFileName(zipFile, false);
                Directory.CreateDirectory(destination);
                fz.ExtractZip(zipFile, destination, ".torrent");

                files = Directory.GetFiles(destination, "*.torrent", SearchOption.AllDirectories);
                return files;
            }

            catch (Exception)
            {
                return null;
            }
        }
        public static object[] UnzipFiles(string[] zipFiles)
        {
            ArrayList al = new ArrayList();
            FastZip fz = new FastZip();
            ErrorWindow ew = new ErrorWindow();
            string[] files = null;
            string filename;
            string destination;

            foreach (string zipfile in zipFiles)
            {
                filename = DirectoryHandler.GetFileName(zipfile, false);
                destination = SettingsHandler.GetTorrentSaveFolder() + @"\[CSL]--Temp\" + filename + @"\";

                try
                {
                    fz.ExtractZip(zipfile, destination, ".torrent");
                }
                catch (Exception) { ew.IssueGeneralWarning("No action necessary", "Warning: Could not unzip " + filename, zipfile); }

                try
                {
                    files = Directory.GetFiles(destination,
                        "*.torrent", SearchOption.AllDirectories);
                }
                catch (DirectoryNotFoundException) { ew.IssueGeneralWarning("No action necessary", "Warning: " + filename + "was empty", zipfile); }

                finally
                {
                    foreach (string file in files)
                        al.Add(file);
                }
            }

            return al.ToArray();
        }

        public static string MoveTorrentFile(string file, string where)
        {
            string cslSaveFolder = null;
            string filename;

            filename = DirectoryHandler.GetFileName(file, true);

            switch (where)
            {
                case "handled":
                    cslSaveFolder = SettingsHandler.GetTorrentSaveFolder() + @"\[CSL] -- Handled Torrents\";
                    break;
                case "unhandled":
                    cslSaveFolder = SettingsHandler.GetTorrentSaveFolder() + @"\[CSL] -- Unhandled Torrents\";
                    break;
                default:
                    cslSaveFolder = SettingsHandler.GetTorrentSaveFolder() + '\\' + where + '\\';
                    break;
            }
            if (!Directory.Exists(cslSaveFolder))
                Directory.CreateDirectory(cslSaveFolder);

            bool success = DirectoryHandler.MoveFile(file, cslSaveFolder);

            if (success)
                return cslSaveFolder + filename;
            else
                return null;
        }
        //To be called only from override DoWork
        public void MoveProcessedFiles(Torrent[] torrents) 
        {
            string filepath;
            string[] zipFiles;
            bool skip = false;
            int total = TorrentXMLHandler.table.Rows.Count;
            double progress = 0;
            double count = 0;

            ArrayList al = new ArrayList();

            foreach (Torrent t in torrents)
            {
                DataRow dr = table.Rows.Find(t.GetFileName());
                al.Add(dr);
            }

            //MOVE TORRENT FILES
            foreach (object datarow in al)
            {
                DataRow dr = (DataRow)datarow;
                if (!dr["File Path"].Equals(DBNull.Value))
                {
                    int index = TorrentXMLHandler.table.Rows.IndexOf(dr);
                    filepath = (string)dr["File Path"];

                    if (filepath.Contains("[CSL] -- Unhandled Torrents") && (bool)dr["Error"])
                        skip = true;
                    else if ((bool)dr["Handled"])
                        skip = true;
                    else
                        skip = false;

                    if (!skip)
                    {
                        switch ((bool)dr["Error"])
                        {
                            case true:
                                filepath = MoveTorrentFile((string)dr["File Path"], "unhandled");
                                if (filepath == null)
                                {
                                    TorrentXMLHandler.table.Rows[index]["File Path"] = "Problem moving file..";
                                    TorrentXMLHandler.table.Columns["Error"].ReadOnly = false;
                                    TorrentXMLHandler.table.Rows[index]["Error"] = true;
                                    TorrentXMLHandler.table.Columns["Error"].ReadOnly = true;
                                }
                                else
                                    TorrentXMLHandler.table.Rows[index]["File Path"] = filepath;
                                break;
                            case false:
                                filepath = MoveTorrentFile((string)dr["File Path"], "handled");
                                if (filepath == null)
                                {
                                    TorrentXMLHandler.table.Rows[index]["File Path"] = "Problem moving file..";
                                    TorrentXMLHandler.table.Columns["Error"].ReadOnly = false;
                                    TorrentXMLHandler.table.Rows[index]["Error"] = true;
                                    TorrentXMLHandler.table.Columns["Error"].ReadOnly = true;
                                }
                                else
                                    TorrentXMLHandler.table.Rows[index]["File Path"] = filepath;
                                break;
                            default:
                                filepath = MoveTorrentFile((string)dr["File Path"], "handled");
                                if (filepath == null)
                                {
                                    TorrentXMLHandler.table.Rows[index]["File Path"] = "Problem moving file..";
                                    TorrentXMLHandler.table.Columns["Error"].ReadOnly = false;
                                    TorrentXMLHandler.table.Rows[index]["Error"] = true;
                                    TorrentXMLHandler.table.Columns["Error"].ReadOnly = true;
                                }
                                else
                                    TorrentXMLHandler.table.Rows[index]["File Path"] = filepath;
                                break;
                        }
                    }
                }


                progress = (++count / total) * 100;

                if (progress <= 100 && progress >= 0)
                    ReportProgress((int)progress);
            }
            TorrentXMLHandler.Save();
            //MOVE ZIP FILES
            zipFiles = Directory.GetFiles(SettingsHandler.GetTorrentSaveFolder(), "*.zip", SearchOption.TopDirectoryOnly);

            if (zipFiles != null && zipFiles.Length > 0)
            {
                if (!Directory.Exists(SettingsHandler.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips"))
                    Directory.CreateDirectory(SettingsHandler.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips");

                for (int a = 0; a < zipFiles.Length; a++)
                {
                    if (!File.Exists(SettingsHandler.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips\" + zipFiles[a].Substring(zipFiles[a].LastIndexOf('\\') + 1)))
                        File.Move(zipFiles[a], SettingsHandler.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips\" + zipFiles[a].Substring(zipFiles[a].LastIndexOf('\\') + 1));
                    else
                        File.Delete(zipFiles[a]);
                }
            }

            if (Directory.Exists(SettingsHandler.GetTorrentSaveFolder() + @"\[CSL]--Temp"))
                Directory.Delete(SettingsHandler.GetTorrentSaveFolder() + @"\[CSL]--Temp", true);
        }
    }
}
