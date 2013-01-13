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
using System.Collections;

namespace CSL
{
    public class TorrentDataHandler : BackgroundWorker
    {
        public static bool datasetbusy;
        public static dataset data;
        public static BindingSource binding;
        public static DataTable table;
        public static datasetTableAdapters.TorrentsTableTableAdapter musicadapter;
        public datasetTableAdapters.TrackersTableTableAdapter trackeradapter = new datasetTableAdapters.TrackersTableTableAdapter();
        
        public TorrentDataHandler() { }
        public TorrentDataHandler(ref dataset d, ref datasetTableAdapters.TorrentsTableTableAdapter musikadapter)
        {
            data = d;
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
            musicadapter = musikadapter;

            d.Tables.CollectionChanging += new CollectionChangeEventHandler(Tables_CollectionChanging);
            d.Tables.CollectionChanged += new CollectionChangeEventHandler(Tables_CollectionChanged);

            trackeradapter.Fill(data.TrackersTable);
        }

        void Tables_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            datasetbusy = false;
        }
        void Tables_CollectionChanging(object sender, CollectionChangeEventArgs e)
        {
            datasetbusy = true;
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

            dataset.TorrentsTableRow row = data.TorrentsTable.NewTorrentsTableRow();
            string[] information = torrent.GetInformation();

            row.BeginEdit();
            row.File = information[11];
            row.Artist = information[0];
            row.Album = information[1];
            row.Save_Structure = information[13];
            row.Sent = false;
            row.Release_Format = information[2];
            row.Bit_Rate = information[3];
            row.Year = information[4];
            row.Physical_Format = information[5];
            row.Bit_Format = information[6];
            row.File_Path = information[10];
            row.Site_Origin = information[12];
            row.EndEdit();

            while (datasetbusy) System.Threading.Thread.Sleep(100);//sleep while changes are occuring..

            DataRow dr = data.TorrentsTable.Rows.Find(row.ID);
            while (dr != null)
            {
                row.ID++;
                dr = data.TorrentsTable.Rows.Find(row.ID);
            }

            data.TorrentsTable.AddTorrentsTableRow(row);
            musicadapter.Update(row);
        }
        public void AddMovieTorrent(Movie m)
        {
            dataset.MoviesTableRow row = data.MoviesTable.NewMoviesTableRow();
            string[] information = m.GetMovieInformation();

            row.BeginEdit();
            row.File = information[11];
            row.Movie_Title = information[0];
            row.Year = information[1];
            row.Save_Structure = information[13];
            row.Sent = false;
            row.Source_Media = information[2];
            row.Codec_Format = information[3];
            row.File_Format = information[4];
            row.File_Path = information[10];
            row.Site_Origin = information[12];
            row.EndEdit();

            DataRow dr = data.TorrentsTable.Rows.Find(row.ID);
            while (dr != null)
            {
                row.ID++;
                dr = data.TorrentsTable.Rows.Find(row.ID);
            }

            data.MoviesTable.AddMoviesTableRow(row);
        }
        public void AddOtherTorrent(Torrent torrent)
        {
            string[] information = torrent.GetInformation();
            dataset.OthersTableRow row = data.OthersTable.NewOthersTableRow();
            
            row.File = information[11];
            row.File_Path = information[10];
            row.Save_Structure = information[13];
            row.Site_Origin = information[12];
            row.Sent = false;

            data.OthersTable.AddOthersTableRow(row);
        }
        public void AddTracker(string tracker, string name)
        {
            dataset.TrackersTableRow row = data.TrackersTable.NewTrackersTableRow();
            row.BeginEdit();
            row.Tracker = tracker;
            row.Activated = true;
            row.Name = name;
            row.EndEdit();

           /* DataRow dr = data.TrackersTable.Rows.Find(row.id);
            while (dr != null)
            {
                row.id++;
                dr = data.TrackersTable.Rows.Find(row.id);
            }*/
            data.TrackersTable.AddTrackersTableRow(row);
            trackeradapter.Update(row);
        }

        static public DataRowCollection GetTrackers()
        {
            try { return data.TrackersTable.Rows; }
            catch { return null; }

        }
        static public string CheckTracker(string tracker)
        {
            foreach (dataset.TrackersTableRow r in data.TrackersTable.Rows)
            {
                if (r.Tracker.Equals(tracker))
                {
                    if (r.Activated)
                        return "true";
                    else
                        return "false";
                }
            }

            return "na";
        }
        public void UpdateTracker(int id, bool activate)
        {
            dataset.TrackersTableRow r = data.TrackersTable.FindByid(id);

            r.BeginEdit();
            r.Activated = activate;
            r.EndEdit();
           
            trackeradapter.Update(r);
        }
        
            
        static public void RemoveTorrent(string datatable, int index)
        {
            switch (datatable)
            {
                case "music":
                    try
                    {
                        (data.TorrentsTable.Rows.Find(index)).Delete();
                    }
                    catch (Exception e) { DirectoryHandler.LogError(e.Message + "\n" + e.StackTrace); }
                    break;
                case "movies":
                    try
                    {
                        (data.MoviesTable.Rows.Find(index)).Delete();
                    }
                    catch (Exception e) { DirectoryHandler.LogError(e.Message + "\n" + e.StackTrace); }
                    break;
                case "others":
                    try
                    {
                        (data.OthersTable.Rows.Find(index)).Delete();
                    }
                    catch (Exception e) { DirectoryHandler.LogError(e.Message + "\n" + e.StackTrace); }
                    break;
            }
        }
        static public void RemoveAll(string datatable)
        {
            //Can't get table.Clear() to work..
            switch (datatable)
            {
                case "music":
                    foreach (DataRow dr in data.TorrentsTable.Rows)
                        dr.Delete();
                    break;
                case "movies":
                    foreach (DataRow dr in data.MoviesTable.Rows)
                        dr.Delete();
                    break;
                case "others":
                    foreach (DataRow dr in data.OthersTable.Rows)
                        dr.Delete();
                    break;
            }
        }     
    }
}
