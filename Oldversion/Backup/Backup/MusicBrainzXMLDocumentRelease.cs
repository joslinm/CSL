using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicBrainzXML
{
    class MusicBrainzXMLDocumentRelease
    {
        public string artist;
        public string artistId;
        public string releaseTitle;
        public string releaseASIN;
        public string releaseID;
        public string releaseType;
        public string ext_score; 

        public string[][] releaseEventList = new string[50][]; //[][0:label;1->10:attributes]
        public string discListCount;
        public string trackListCount;

        public MusicBrainzXMLDocumentRelease()
        {
            for (int a = 0; a < 50; a++)
            releaseEventList[a] = new string[10];
        }
    }
    class MusicBrainzXMLDocumentArtist
    {
        public string name;
        public string sort_name;
        public string birth;
        public string death;
        public string type;
        public string artistId;
        public string ext_score;
    }
}
