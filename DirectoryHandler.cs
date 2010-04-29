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
                return (files.Length == 0) ? null : files;
            }
            catch { return null; }
        }
        public string[] GetTorrents()
        {
            try
            {
                string[] files = Directory.GetFiles(settings.GetTorrentSaveFolder(), "*.torrent", SearchOption.TopDirectoryOnly);
                return (files.Length == 0) ? null : files;
            }
            catch { return null; }
        }
        public string GetFileName(string path, bool extension)
        {
            try
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
            catch
            {
                return null;
            }

        }
        public string GetDirectoryName(string path)
        {
            string directoryname;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            directoryname = Path.GetDirectoryName(fs.Name);

            fs.Dispose();
            return directoryname;
        }
        public string GetHTMLLookUp(string value)
        {
            FileStream fs = null;
            if (value == null)
                return value;
            string[] ab = new string[2];
            int c = 0;
            string[] lines = null;
            try
            {
                StreamReader sr = new StreamReader(System.Windows.Forms.Application.StartupPath + @"\HTML-Look-Up.txt");
               
                while(!sr.EndOfStream)
                {
                    ab = sr.ReadLine().Split(' ');
                    string a = ab[0];
                    string b = ab[1];

                    if (value.Contains(a) || value.Contains(b))
                    {
                        while (value.Contains(a))
                        {
                            string replace = sr.ReadLine();
                            value = value.Replace(a, replace);
                        }
                        while (value.Contains(b))
                        {
                            string replace = sr.ReadLine();
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
                if (fs != null)
                    fs.Dispose();
            }
            
            return value;
        }
        public bool GetFileExists(string path)
        {
            bool value;
            FileStream fs = null;

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
                if(fs != null)
                fs.Dispose();
            }

            return value;
        }
        public bool MoveFile(string origin, string dest)
        {
            try
            {
                FileStream fs = new FileStream(origin, FileMode.Open, FileAccess.Read);
                FileStream ds = null;
                try
                {
                    ds = new FileStream(dest + Path.GetFileName(origin), FileMode.Open, FileAccess.Read);
                    fs.Close();
                    File.Delete(origin);
                    return true;
                }
                catch(Exception e)
                {
                    string o = fs.Name;
                    string d = dest + Path.GetFileName(fs.Name);
                    fs.Close();
                    File.Move(fs.Name, dest + Path.GetFileName(fs.Name));
                    return true;
                }
            }
            catch { return false; }
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

            bool success = this.MoveFile(file, cslSaveFolder);

            if (success)
                return cslSaveFolder + fileName;
            else
                return null;     
        }
        public void MoveProcessedFiles(TorrentXMLHandler data)
        {
            string filepath;
            bool skip = false;
            int count = data.table.Rows.Count;
            
            foreach (DataRow datarow in data.table.Rows)
            {
                if (!datarow["File Path"].Equals(DBNull.Value))
                {
                    int index = data.table.Rows.IndexOf(datarow);

                    filepath = (string)datarow["File Path"];

                    if (filepath.Contains("[CSL] -- Unhandled Torrents") && (bool)datarow["Error"])
                        skip = true;
                    else if ((bool)datarow["Handled"])
                        skip = true;
                    else
                        skip = false;

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
            data.Save();

            string[] zipFiles = Directory.GetFiles(settings.GetTorrentSaveFolder(), "*.zip", SearchOption.TopDirectoryOnly);

            if (zipFiles != null && zipFiles.Length > 0)
            {
                if (!Directory.Exists(settings.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips"))
                    Directory.CreateDirectory(settings.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips");

                for (int a = 0; a < zipFiles.Length; a++)
                {
                    if(!File.Exists(settings.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips\" + zipFiles[a].Substring(zipFiles[a].LastIndexOf('\\') + 1)))
                    File.Move(zipFiles[a], settings.GetTorrentSaveFolder() + @"\[CSL] -- Processed Zips\" + zipFiles[a].Substring(zipFiles[a].LastIndexOf('\\') + 1));
                    else
                    File.Delete(zipFiles[a]);
                }
            }

            if (Directory.Exists(settings.GetTorrentSaveFolder() + @"\[CSL]--Temp"))
                Directory.Delete(settings.GetTorrentSaveFolder() + @"\[CSL]--Temp", true);
        }

    }
}
