using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL
{
    public class Movie : Torrent
    {
        /*Main music information*/
        public string movie = null;
        //public string year = null; inherited
        public string source = null;
        public string codec = null;
        public string fileformat = null;


        /*Information[] will hold all values that the user wishes to use for 
         * directory structure output. It is split up as follows:
         * **Information[0-10] -- Music information**
         * 
         * information[0] --> movie
         * information[1] --> year
         * information[2] --> source
         * information[3] --> codec
         * information[4] --> fileformat
         * 
         * **Information[10-20] -- File Information**
         * 
         * information[10] --> path
         * information[11] --> file name
         * information[12] --> birth
         * information[13] --> destination path
         * information[14] --> discard
         * */
        public Movie(string[] information)
        {
            if (information.Length >= 13)
            {
                movie = information[0];
                year = information[1];
                source = information[2];
                codec = information[3];
                fileformat = information[4];

                /*Torrent file information*/
                path = information[10];
                fileName = information[11];
                birth = information[12];
                destPath = information[13];
            }
        }
        public string[] GetMovieInformation()
        {
            string[] information = new string[20];

            information[0] = movie;
            information[1] = year;
            information[2] = source;
            information[3] = codec;
            information[4] = fileformat;

            information[10] =  path;
            information[11] =  fileName;
            information[12] =  birth;
            information[13] = destPath;
            return information;
        }
    }
}
