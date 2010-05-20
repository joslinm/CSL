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
using System.Data.SqlClient;

namespace CSL_Test__1
{
    public class TorrentXMLHandler : BackgroundWorker
    {
        CSLDataSetTableAdapters.CSLDataTableTableAdapter adapter = new CSLDataSetTableAdapters.CSLDataTableTableAdapter();
        CSLDataSet dataset = new CSLDataSet();
        public static DataTable table = new DataTable();
        public TorrentXMLHandler()
        {
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
        }
        /*
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
            */
        /*
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
        }*/
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

            CSLDataSet.CSLDataTableRow row = dataset.CSLDataTable.NewCSLDataTableRow();
            string[] information = torrent.GetInformation();

            try
            {
                row.File_ = "file";
                row.Artist = "artist11";
                row.Album = "album";
                row.Save_Structure = information[13];
                row.Sent = false;
                row.Error = false;
                row.Release_Format = information[2];
                row.Bit_Rate = information[3];
                row.Year = information[4];
                row.Physical_Format = information[5];
                row.Bit_Format = information[6];
                row.File_Path = information[10];
                row.Site_Origin = "what";

                string currentfilename = torrent.GetFileName();

                DataRow dr = dataset.CSLDataTable.Rows.Find(currentfilename);
                if (dr != null)
                    dataset.CSLDataTable.Rows[table.Rows.IndexOf(dr)].Delete();

                dataset.CSLDataTable.AddCSLDataTableRow(row);
                adapter.Update(dataset);
                dataset.AcceptChanges();
            }
            catch (Exception e)
            {
                DirectoryHandler.LogError(e.Message + "\n" + e.StackTrace);
            }
        }
        /*
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
        }*/          
    }
}
