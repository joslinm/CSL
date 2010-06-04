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

namespace CSL
{
    class DirectoryHandler : BackgroundWorker
    {
        static FileInfo ErrorLog = new FileInfo(Directory.GetCurrentDirectory() + "\\" + "errorlog.txt");
        private object locker = new object();

        public DirectoryHandler()
        {
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
        }
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            Type t = null;
            if (e.Argument != null)
            {
                t = e.Argument.GetType();

                if (t.Equals(typeof(String[])))
                {
                    List<FileInfo> list = UnzipFiles((string[])e.Argument);
                    e.Result = list;
                }
            }
        }
        public static void LogError(string error)
        {
            /*
            FileStream fs = null;

            if (!ErrorLog.Exists)
                ErrorLog.Create();
            try
            {
                fs = ErrorLog.OpenWrite();
                List<byte> bytes = new List<byte>();
                foreach (char l in error.ToCharArray())
                    bytes.Add(Convert.ToByte(l));

                fs.Write(bytes.ToArray(), 0, bytes.Count);
            }
            catch { }
            finally { if(fs != null)fs.Close(); }*/
        }

        public static bool DeleteFile(FileInfo file)
        {
            try
            {
                file.Delete();
            }
            catch (IOException ioe)
            {
                LogError(ioe.Message + "\n" + ioe.StackTrace);
            }
            catch (System.Security.SecurityException soe)
            {
                LogError(soe.Message + "\n" + soe.StackTrace);
            }
            catch (UnauthorizedAccessException ue)
            {
                LogError(ue.Message + "\n" + ue.StackTrace);
            }

            if (file.Exists)
                return false; //Didn't delete successfully
            else
                return true; //Deleted
        }
        public static bool DeleteFolder(DirectoryInfo folder)
        {
            try
            {
                folder.Delete(true);
            }
            catch (IOException ioe)
            {
                LogError(ioe.Message + "\n" + ioe.StackTrace);
            }
            catch (System.Security.SecurityException soe)
            {
                LogError(soe.Message + "\n" + soe.StackTrace);
            }

            if (folder.Exists)
                return false; //Didn't delete successfully
            else
                return true; //Deleted
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

        public static bool GetFileExists(string file)
        {
            FileInfo fi = new FileInfo(file);
            if (fi.Exists)
                return true;
            else
                return false;
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
        public static FileInfo[] GetFileInfos(string[] files)
        {
            List<FileInfo> infos = new List<FileInfo>();
            foreach (string file in files)
                infos.Add(new FileInfo(file));

            return infos.ToArray<FileInfo>();
        }

        public static string GetHTMLLookUp(string value)
        {
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
                ErrorWindow ew = new ErrorWindow();
                ew.IssueGeneralWarning("HTML replacing will not occur", "Could not find HTML-Look-Up.txt...", null);
            }
            catch (Exception e)
            {
                ErrorWindow ew = new ErrorWindow();
                ew.IssueGeneralWarning("Error..", lines[c], e.Message);
            }
            finally
            {
                if (sr != null)
                    sr.Dispose();
            }

            return value;
        }
        public static bool MoveFile(FileInfo origin, DirectoryInfo dest)
        {
            if (File.Exists(dest.Name + origin.Name))
            {
                if (!origin.DirectoryName.Equals(dest.Name))
                    DirectoryHandler.DeleteFile(origin);
                return true;
            }
            else
            {
                try
                {
                    origin.MoveTo(dest.FullName + "\\" + origin.Name);
                }
                catch (Exception e)
                {
                    LogError(e.Message + "\n" + e.StackTrace);
                }
            }

            if (File.Exists(dest.Name + origin.Name))
                return true;
            else
                return false;
        }
        public static string[] UnzipFile(string zipFile)
        {
            string destination;
            string[] files;
            FastZip fz = new FastZip();
            FileInfo fi = new FileInfo(zipFile);

            if (fi.Exists)
            {
                try
                {
                    destination = SettingsHandler.GetTorrentSaveFolder() + @"\[CSL]--Temp\" + fi.Name;
                    Directory.CreateDirectory(destination);
                    fz.ExtractZip(zipFile, destination, ".torrent");

                    files = Directory.GetFiles(destination, "*.torrent", SearchOption.AllDirectories);

                    try
                    {
                        fi.MoveTo(SettingsHandler.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips\" + fi.Name);
                    }
                    catch(Exception e) { }
                    return files;
                }

                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        //To be called only from DoWork
        public List<FileInfo> UnzipFiles(string[] zipFiles)
        {
            List<FileInfo> items = new List<FileInfo>();
            List<FileInfo> fileinfos = new List<FileInfo>();
            foreach (string zipfile in zipFiles)
            {
                try
                {
                    fileinfos.Add(new FileInfo(zipfile));
                }
                catch (Exception e)
                {
                    LogError(e.Message + "\n" + e.StackTrace);
                }
            }

            FastZip fz = new FastZip();
            ErrorWindow ew = new ErrorWindow();

            string[] files = null;
            string filename;
            DirectoryInfo destination;
            int total = zipFiles.Length;
            double progress = 0;
            double count = 0;
            foreach (FileInfo zipfile in fileinfos)
            {
                filename = zipfile.Name;
                filename = filename.Replace(zipfile.Extension, "");
                string dest = SettingsHandler.GetTorrentSaveFolder() + @"\[CSL]--Temp\" + filename + @"\";
                destination = new DirectoryInfo(dest);
                if (!destination.Exists)
                {
                    try
                    {
                        destination.Create();
                    }
                    catch (Exception e)
                    {
                        LogError(e.Message + "\n" + e.StackTrace);
                    }
                }

                try
                {
                    fz.ExtractZip(zipfile.FullName, destination.FullName, ".torrent");
                }
                catch (Exception) { ew.IssueGeneralWarning("No action necessary", "Warning: Could not unzip " + filename, zipfile.Name); }

                try
                {
                    files = Directory.GetFiles(destination.FullName,
                        "*.torrent", SearchOption.AllDirectories);
                }
                catch (DirectoryNotFoundException) { ew.IssueGeneralWarning("No action necessary", "Warning: " + filename + "was empty", zipfile.Name); }

                finally
                {
                    foreach (string file in files)
                    {
                        try
                        {
                            items.Add(new FileInfo(file));
                        }
                        catch (Exception e)
                        {
                            LogError(e.Message + "\n" + e.StackTrace);
                        }
                    }
                }


                try
                {
                    zipfile.MoveTo(SettingsHandler.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips\" + zipfile.Name);
                }
                catch { }
                progress = (++count / total) * 100;

                if (progress <= 100 && progress >= 0)
                    ReportProgress((int)progress);
            }

            return items;
        }

        public static void MoveZipFiles()
        {
            string[] zipFiles;

            //MOVE ZIP FILES
            zipFiles = Directory.GetFiles(SettingsHandler.GetTorrentSaveFolder(), "*.zip", SearchOption.TopDirectoryOnly);
            List<FileInfo> zips = new List<FileInfo>();
            foreach (string zipfile in zipFiles)
                zips.Add(new FileInfo(zipfile));

            if (zips != null && zips.Count > 0)
            {
                DirectoryInfo zipsavefolder = new DirectoryInfo(SettingsHandler.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips");
                if (!zipsavefolder.Exists)
                    zipsavefolder.Create();

                foreach (FileInfo zip in zips)
                {
                    if (!zip.DirectoryName.Equals(zipsavefolder.FullName))
                    {
                        try
                        {
                            zip.MoveTo(zipsavefolder.FullName + "\\" + zip.Name);
                        }
                        catch { zip.Delete(); }
                    }
                }
            }

            //DELETE TEMP FOLDER
            DirectoryHandler.DeleteTempFolder();
        }
        public static string MoveTorrentFile(Torrent t)
        {
            FileInfo fi;
            DirectoryInfo cslSaveFolder = null;
            string filename;
            string file = t.GetPath();
            string where = (t.GetDiscard()) ? "unhandled" : "handled";

            try
            {
                fi = new FileInfo(file);
                filename = fi.Name;
                switch (where)
                {
                    case "handled":
                        cslSaveFolder = new DirectoryInfo(SettingsHandler.GetTorrentSaveFolder() + @"\[CSL] -- Handled Torrents\");
                        break;
                    case "unhandled":
                        cslSaveFolder = new DirectoryInfo(SettingsHandler.GetTorrentSaveFolder() + @"\[CSL] -- Unhandled Torrents\");
                        break;
                    default:
                        cslSaveFolder = new DirectoryInfo(SettingsHandler.GetTorrentSaveFolder() + '\\' + where + '\\');
                        break;
                }

                if (!cslSaveFolder.Exists)
                    cslSaveFolder.Create();

                if (File.Exists(cslSaveFolder.FullName + fi.Name))
                {
                    fi.Delete();
                    return cslSaveFolder.FullName + fi.Name;
                }
                else if (fi != null)
                {
                    fi.MoveTo(cslSaveFolder.FullName + "\\" + fi.Name);
                    return fi.FullName;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                LogError(e.Message + "\n" + e.StackTrace);
                return null;
            }
        }

        #region Dead Methods (for reference)
        /****DEAD
        //To be called only from DoWork
        public void MoveProcessedFiles()
        {
            string filepath;
            bool skip = false;
            int total = TorrentDataHandler.table.Rows.Count;
            double progress = 0;
            double count = 0;
            List<DataRow> items = new List<DataRow>();

            while (true)
            {
                DataTable copytable= null;
                if (this.CancellationPending)
                {
                    //lock (locker) { copytable = TorrentXMLHandler.GetProcessedRows(); }
                }
                //GET A COPY OF ALL NEWLY ADDED TORRENTS 
                //and work with the copied table to allow TorrentXMLHandler to properly add/remove
                else
                {
                    lock (locker) { copytable = TorrentDataHandler.table.GetChanges(DataRowState.Added); }
                }
                
                if (copytable != null && copytable.Rows.Count > 0)
                {
                    foreach (DataRow dr in copytable.Rows)
                    {
                        if (dr["Processed"] != DBNull.Value && (bool)dr["Processed"])
                        {
                            if (!dr["File Path"].Equals(DBNull.Value))
                            {
                                int index = copytable.Rows.IndexOf(dr);
                                filepath = (string)dr["File Path"];

                                if (filepath.Contains("[CSL] -- Unhandled Torrents") && (bool)dr["Error"])
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
                                                copytable.Rows[index]["File Path"] = "Problem moving file..";
                                                copytable.Columns["Error"].ReadOnly = false;
                                                copytable.Rows[index]["Error"] = true;
                                                copytable.Columns["Error"].ReadOnly = true;
                                            }
                                            else
                                                copytable.Rows[index]["File Path"] = filepath;
                                            break;
                                        case false:
                                            try
                                            {
                                                filepath = MoveTorrentFile((string)dr["File Path"], "Sent");
                                                if (filepath == null)
                                                {
                                                    copytable.Rows[index]["File Path"] = "Problem moving file..";
                                                    copytable.Columns["Error"].ReadOnly = false;
                                                    copytable.Rows[index]["Error"] = true;
                                                    copytable.Columns["Error"].ReadOnly = true;
                                                }
                                                else
                                                    copytable.Rows[index]["File Path"] = filepath;
                                            }
                                            catch (Exception e) { LogError(e.Message + "\n" + e.StackTrace); }
                                            break;
                                        default:
                                            filepath = MoveTorrentFile((string)dr["File Path"], "Sent");
                                            if (filepath == null)
                                            {
                                                copytable.Rows[index]["File Path"] = "Problem moving file..";
                                                copytable.Columns["Error"].ReadOnly = false;
                                                copytable.Rows[index]["Error"] = true;
                                                copytable.Columns["Error"].ReadOnly = true;
                                            }
                                            else
                                                copytable.Rows[index]["File Path"] = filepath;
                                            break;
                                    }
                                }
                            }
                            progress = (++count / total) * 100;

                            if (progress <= 100 && progress >= 0)
                                ReportProgress((int)progress);
                        }
                    }//END FOREACH
                    //Load the copytable back into TorrentXMLHandler
                    lock (locker)
                    {
                        TorrentDataHandler.table.Columns["Error"].ReadOnly = false;

                        foreach (DataRow dr in copytable.Rows)
                        {
                            DataRow find = TorrentDataHandler.table.Rows.Find(dr["File"]);
                            if (find != null)
                            {
                                int index = TorrentDataHandler.table.Rows.IndexOf(find);
                                try
                                {
                                    TorrentDataHandler.table.Rows[index]["File Path"] = dr["File Path"];
                                    TorrentDataHandler.table.Rows[index]["Error"] = dr["Error"];
                                }
                                catch (Exception e) { DirectoryHandler.LogError(e.Message + "\n" + e.StackTrace); }
                            }
                        }

                        TorrentDataHandler.table.Columns["Error"].ReadOnly = true;
                        try
                        {
                            TorrentDataHandler.table.AcceptChanges();
                        }
                        catch (Exception e) { DirectoryHandler.LogError(e.Message + "\n" + e.StackTrace); }
                    }
                }
                if (this.CancellationPending)
                {
                    //copytable = TorrentXMLHandler.GetProcessedRows();
                    if (copytable.Rows.Count == 0)
                        break; //Break out of while
                }
                else
                    Thread.Sleep(150); 
            }
         * */
        ////
        #endregion
    }
}
            

