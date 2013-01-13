using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace CSL_Test__1
{
    public class TorrentXMLHandler : BackgroundWorker
    {
        public static DataSet dataset;
        public static DataTable table;
        private static FileStream xmlStream;
        private Object obj = new Object();
        static string xmlDataName = "torrents.xml";
        static string xmlSchemaName = "torrentschema.xml";
        static string xmlDataPath = Directory.GetCurrentDirectory() + @"\" + xmlDataName;
        static string xmlSchemaPath = Directory.GetCurrentDirectory() + @"\" + xmlSchemaName;
        private BindingSource bs = new BindingSource();

        public TorrentXMLHandler()
        {
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
        }
        static public void Initialize()
        {
            DataColumn column;

            if (File.Exists(xmlSchemaName))
            {
                dataset = new DataSet();
                xmlStream = new FileStream(xmlSchemaName, FileMode.Open);
                dataset.ReadXmlSchema(xmlStream);
                table = dataset.Tables[0];
                xmlStream.Close();
            }
            else
            {
                dataset = new DataSet("TorrentSet");
                table = new DataTable("Torrent");
                
                table.Columns.Add("Site Origin", typeof(string));
                table.Columns.Add("Artist", typeof(string));
                table.Columns.Add("Album", typeof(string));
                table.Columns.Add("Save Structure", typeof(string));
                table.Columns.Add("Year", typeof(string));
                table.Columns.Add("Bitrate", typeof(string));
                table.Columns.Add("Release Format", typeof(string));
                table.Columns.Add("Physical Format", typeof(string));
                table.Columns.Add("Bit Format", typeof(string));

                //Handle Column
                column = new DataColumn();
                column.ReadOnly = true;
                column.DataType = typeof(bool);
                column.ColumnName = "Handled";
                table.Columns.Add(column);
                //Error Column
                column = new DataColumn();
                column.ReadOnly = true;
                column.DataType = typeof(bool);
                column.ColumnName = "Error";
                table.Columns.Add(column);
                //Internally Handled Column
                column = new DataColumn();
                column.ReadOnly = false;
                column.DataType = typeof(bool);
                column.ColumnName = "Processed";
                table.Columns.Add(column);

                table.Columns.Add("File", typeof(string));
                table.Columns.Add("File Path", typeof(string));
                dataset.Tables.Add(table);
                table.PrimaryKey = new DataColumn[] { table.Columns["File"] };

                xmlStream = new FileStream(xmlSchemaName, FileMode.CreateNew);
                dataset.WriteXmlSchema(xmlStream);
                xmlStream.Close();
            }

            if (File.Exists(xmlDataPath))
            {
                xmlStream = new FileStream(xmlDataPath, FileMode.Open);
                try
                {
                    table.ReadXml(xmlStream);
                }
                catch
                {
                    xmlStream.Close();
                    xmlStream = new FileStream(xmlDataPath, FileMode.Create);
                    xmlStream.Close();
                }
            }
        }
        static public void InitializeFresh()
        {
            DataColumn column;

            if (xmlStream != null)
                xmlStream.Close();
            try
            {
                FileInfo fi = new FileInfo(xmlSchemaName);
                fi.Delete();
            }
            catch { }

            dataset = new DataSet("TorrentSet");
            table = new DataTable("Torrent");

            table.Columns.Add("Site Origin", typeof(string));
            table.Columns.Add("Artist", typeof(string));
            table.Columns.Add("Album", typeof(string));
            table.Columns.Add("Save Structure", typeof(string));
            table.Columns.Add("Year", typeof(string));
            table.Columns.Add("Bitrate", typeof(string));
            table.Columns.Add("Release Format", typeof(string));
            table.Columns.Add("Physical Format", typeof(string));
            table.Columns.Add("Bit Format", typeof(string));

            //Handle Column
            column = new DataColumn();
            column.ReadOnly = true;
            column.DataType = typeof(bool);
            column.ColumnName = "Handled";
            table.Columns.Add(column);
            //Error Column
            column = new DataColumn();
            column.ReadOnly = true;
            column.DataType = typeof(bool);
            column.ColumnName = "Error";
            table.Columns.Add(column);
            //Internally Handled Column
            column = new DataColumn();
            column.ReadOnly = false;
            column.DataType = typeof(bool);
            column.ColumnName = "Processed";
            table.Columns.Add(column);

            table.Columns.Add("File", typeof(string));
            table.Columns.Add("File Path", typeof(string));
            dataset.Tables.Add(table);
            table.PrimaryKey = new DataColumn[] { table.Columns["File"] };

            xmlStream = new FileStream(xmlSchemaName, FileMode.CreateNew);
            dataset.WriteXmlSchema(xmlStream);
            xmlStream.Close();
        }

        public void Save()
        {
            //lock (obj)
            //{
                dataset.AcceptChanges();

                if (xmlStream.Name.Equals(xmlDataName))
                    dataset.WriteXml(xmlStream);
                else
                {
                    xmlStream.Close();
                    xmlStream = new FileStream(xmlDataName, FileMode.OpenOrCreate);
                    dataset.WriteXml(xmlStream);
                }

                xmlStream.Close();
            //}
        }
        static public void SaveAndClose()
        {
            dataset.AcceptChanges();

            if (xmlStream.Name.Equals(xmlDataName))
                dataset.WriteXml(xmlStream);
            else
            {
                xmlStream.Close();
                xmlStream = new FileStream(xmlDataName, FileMode.OpenOrCreate);
                dataset.WriteXml(xmlStream);
            }

            xmlStream.Close();
        }
        public void AddTorrent(Torrent torrent)
        {
            #region Information Contents
            /* **Information[0-10] -- Music information**
         * 
         * information[0] --> Artist
         * information[1] --> Album
         * information[2] --> AlbumType
         * information[3] --> bitrate
         * information[4] --> year
         * information[5] --> physical format (CD,DVD,VINYL,WEB)
         * information[6] --> bit format (MP3,FLAC)
         * 
         * **Information[10-20] -- File Information**
         * 
         * information[10] --> path
         * information[11] --> file name
         * information[12] --> birth
         * information[13] --> destination path
         * information[14] --> discard
         * */
            #endregion

            try
            {
                string[] information = torrent.GetInformation();
                DataRow row;
                row = table.NewRow();
                row["File"] = information[11];
                row["Artist"] = information[0];
                row["Album"] = information[1];
                row["Save Structure"] = information[13];
                row["Handled"] = false;
                row["Error"] = (information[14] == "true") ? true : false;
                row["Release Format"] = information[2];
                row["Bitrate"] = information[3];
                row["Year"] = information[4];
                row["Physical Format"] = information[5];
                row["Bit Format"] = information[6];
                row["File Path"] = information[10];
                row["Site Origin"] = (information[14] == "true") ? "Discarded" : information[12];
                row["Processed"] = true;

                string currentfilename = torrent.GetFileName();
                lock (obj)
                {
                    DataRow dr = table.Rows.Find(currentfilename);
                    if (dr != null)
                        table.Rows[table.Rows.IndexOf(dr)].Delete();

                    table.Rows.Add(row);
                }

            }
            catch (Exception e)
            {
                DirectoryHandler.LogError(e.Message + "\n" + e.StackTrace);
            }
        }
        static public DataTable GetProcessedRows()
        {
            DataTable dt = table.Clone();
            DataRow[] drs = table.Select("Processed = true");
            foreach(DataRow dr in drs)
            {
                string filepath = (string)dr["File Path"];
                if(!filepath.Contains("[CSL] --"))
                dt.ImportRow(dr); 
            }
            return dt;
        }          
    }
}
