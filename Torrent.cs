using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL_Test__1
{
    class Torrent
    {
        /*Main music information*/
        string artist = null;
        string album = null;
        string albumType = null;
        string bitrate = null;
        string year = null;
        string physicalFormat = null;
        string bitFormat = null;
        
        /*Torrent file information*/
        string path;
        string fileName;
        string birth; //Waffles | What.CD
        string destPath;
        bool discard;


        /*Information[] will hold all values that the user wishes to use for 
         * directory structure output. It is split up as follows:
         * **Information[0-10] -- Music information**
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
        public Torrent(string[] information)
        {
            if (information.Length >= 13)
            {
                artist = information[0];
                album = information[1];
                albumType = information[2];
                bitrate = information[3];
                year = information[4];
                physicalFormat = information[5];
                bitFormat = information[6];

                /*Torrent file information*/
                path = information[10];
                fileName = information[11];
                birth = information[12];
                destPath = information[13];
                discard = (information[14] == "true") ? true : false;
            }
        }
        public string[] GetInformation()
        {
            string[] information = new string[20];

            information[0] = artist;
            information[1] =  album ;
            information[2] =  albumType ;
            information[3] =  bitrate ;
            information[4] =  year ;
            information[5] =  physicalFormat ;
            information[6] =  bitFormat ;

            information[10] =  path;
            information[11] =  fileName;
            information[12] =  birth;
            information[13] = destPath;
            information[14] = (discard == true) ? "true" : "false";
            return information;
        }

        #region Get Methods
        public string GetArtist()
        {
            return this.artist;
        }
        public string GetAlbum ()
        {
            return this.album;
        }
        public string GetYear()
        {
            return this.year;
        }
        public string GetAlbumType()
        {
            return this.albumType;
        }
        public string GetBitrate()
        {
            return this.bitrate;
        }
        public string GetBitFormat()
        {
            return this.bitFormat;
        }
        public string GetPhysicalFormat()
        {
            return this.physicalFormat;
        }
        public string GetPath()
        {
            return this.path;
        }
        public string GetFileName()
        {
            return this.fileName;
        }
        public string GetBirth()
        {
            return this.birth;
        }
        public string GetDestinationPath()
        {
            return this.destPath;
        }
        public bool GetDiscard()
        {
            return discard;
        }
#endregion
        #region Set Methods
        public void SetArtist(string artist)
        {
            this.artist = artist;
        }
        public void SetAlbum(string album)
        {
            this.album = album;
        }
        public void SetYear(string year)
        {
            this.year = year;
        }
        public void SetAlbumType(string albumType)
        {
            this.albumType = albumType;
        }
        public void SetBitrate(string bitrate)
        {
            this.bitrate = bitrate;
        }
        public void SetBitFormat(string bitformat)
        {
            this.bitFormat = bitformat;
        }
        public void SetPhysicalFormat(string physicalformat)
        {
            this.physicalFormat = physicalformat;
        }
        public void SetDestinationPath(string destpath)
        {
            this.destPath = destpath;
        }
        public void SetDiscard(bool value)
        {
            this.discard = value;
        }
        #endregion

    }
}
