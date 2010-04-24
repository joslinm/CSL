using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace CSL_Test__1
{
    public class TorrentXMLHandler
    {
        static string xmlDataName = "torrents.xml";
        static string xmlSchemaName = "torrentschema.xml";
        static string xmlDataPath = Directory.GetCurrentDirectory() + @"\" + xmlDataName;
        static string xmlSchemaPath = Directory.GetCurrentDirectory() + @"\" + xmlSchemaName;
        static FileStream xmlStream;

        public DataSet dataset;
        public DataTable table;

        public TorrentXMLHandler()
        {
            if (dataset == null && table == null)
                Initialize();
            table.RowDeleting += new DataRowChangeEventHandler(table_RowDeleting);
            table.RowDeleted += new DataRowChangeEventHandler(table_RowDeleted);
        }

        void table_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            
        }

        void table_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
        }
        public void Initialize()
        {
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
                DataColumn column;

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
        public void Save()
        {
            dataset.AcceptChanges();

            if (xmlStream != null)
                xmlStream.Close();

            xmlStream = new FileStream(xmlDataName, FileMode.OpenOrCreate);
            dataset.WriteXml(xmlStream);
            xmlStream.Close();
        }
        public void AddTorrents(Torrent[] torrents)
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

            DataRow row;
            string[] information;
            string filename;
            string currentfilename;

            foreach (Torrent torrent in torrents)
            {
                information = torrent.GetInformation();

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
                row["Site Origin"] = information[12];

                bool duplicate = false;
                currentfilename = torrent.GetFileName();
                DataRow dr = table.Rows.Find(currentfilename);
                if (dr != null)
                {
                    table.Rows[table.Rows.IndexOf(dr)].Delete();
                }
                table.Rows.Add(row);
            }

            table.AcceptChanges();

        }
    }
}
