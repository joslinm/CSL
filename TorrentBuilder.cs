using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using MusicBrainzXML;

namespace CSL_Test__1
{
    class TorrentBuilder
    {
        SettingsHandler settings = new SettingsHandler();
        string[] information = new string[20];

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

        public Torrent[] Build(string[] files)
        {
            Torrent[] torrent = new Torrent[files.Length];
            information[14] = null;

            for (int a = 0; a < files.Length; a++)
            {
                string birth = GetTorrentBirth(files[a]);

                torrent[a] = ProcessTorrent(files[a], birth);
                if (information[14] != "true") 
                    VerifyTorrent(torrent[a]);

                //Clear out information for this run to avoid misinformation on the next run
                for (int b = 0; b < information.Length; b++)
                    information[b] = null;
            }

            return torrent;
        }
        public Torrent ProcessTorrent(string file, string birth)
        {
            /*Directory Switches:
           ***********************************
           %a - Artist Name    %l = Live
           %s = Soundtrack     %c = Compilation
           %e = EP             %r = Remix
           %v = Interview      %n = Single
           %x = Bootleg        %u = Unknown
           %y - Year
           %t - Album Name     %i = Artist's First Initial
           %b = Bitrate        %p = Physical Format (CD/Vinyl)
           %d = Bitrate Format (MP3/FLAC)  
           %z = All release formats (live,EP,comp,etc)
           ***********************************/

            string directoryName = null;

            char[] directoryArray = settings.GetCustomDirectory().ToCharArray();

            if (!settings.GetDownloadDirectory().EndsWith("\\"))
            {
                settings.SetDownloadFolder(settings.GetDownloadDirectory() + "\\");
            }

            for (int a = 0; a < directoryArray.Length; a++)
            {
                if (directoryArray[a] == ('%'))
                {
                    if (information[14] == "true")
                        goto ReturnTorrent;

                    switch (directoryArray[a + 1])
                    {

                        case ('a'):
                            {
                                string artist = ExtractArtist(birth, file);
                                directoryName += artist;
                                information[0] = artist;
                                a++;
                            } break;
                        case ('y'):
                            {
                                string year = ExtractYear(birth, file);
                                directoryName += year;
                                information[4] = year;
                                a++;
                            } break;
                        case ('b'):
                            {
                                string bitrate = ExtractBitrate(birth, file);
                                directoryName += bitrate;
                                information[3] = bitrate;
                                a++;
                            } break;
                        case ('d'):
                            {
                                string bitformat = ExtractBitformat(birth, file);
                                directoryName += bitformat;
                                information[6] = bitformat;
                                a++;
                            } break;
                        case ('p'):
                            {
                                string pformat = ExtractPhysicalFormat(birth, file);
                                directoryName += pformat;
                                information[5] = pformat;
                                a++;
                            } break;
                        case ('t'):
                            {
                                string album = ExtractAlbum(birth, file);
                                directoryName += album;
                                information[1] = album;
                                a++;
                            } break;
                        case ('z'):
                            {
                                string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];
                                directoryName += format;
                                information[2] = format;
                                a++;
                            } break;
                        case ('l'):
                            {
                                string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];
                                information[2] = format;
                                if (format == "Live")
                                {
                                    directoryName += "Live Album";
                                    information[2] = format;
                                }
                                a++;
                            } break;
                        case ('c'):
                            {
                                string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                if (format == "Compilation")
                                {
                                    directoryName += "Compilation";
                                    information[2] = format;
                                }
                                a++;
                            } break;
                        case ('e'):
                            {
                                string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                if (format == "EP")
                                {
                                    directoryName += "EP";
                                    information[2] = format;
                                }
                                a++;
                            } break;
                        case ('r'):
                            {
                                string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                if (format == "Remix")
                                {
                                    directoryName += "Remix";
                                    information[2] = format;
                                }
                                a++;
                            } break;
                        case ('v'):
                            {
                                string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                if (format == "Interview")
                                {
                                    directoryName += "Interview";
                                    information[2] = format;
                                }
                                a++;
                            } break;
                        case ('n'):
                            {
                                string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                if (format == "Single")
                                {
                                    directoryName += "Single";
                                    information[2] = format;
                                }
                                a++;
                            } break;
                        case ('x'):
                            {
                                string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                if (format == "Bootleg")
                                {
                                    directoryName += "Bootleg";
                                    information[2] = format;
                                }
                                a++;
                            } break;
                        case ('s'):
                            {
                                string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                if (format == "Soundtrack")
                                {
                                    directoryName += "Soundtrack";
                                    information[2] = format;
                                }
                                a++;
                            } break;
                        case ('u'):
                            {
                                string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                if (format == "Unknown")
                                {
                                    directoryName += "Unknown";
                                    information[2] = format;
                                }
                                a++;
                            } break;
                        case ('f'):
                            {
                                string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                if (format == "Album")
                                {
                                    directoryName += "Album";
                                    information[2] = format;
                                }
                                a++;
                            } break;
                        case ('i'):
                            {
                                directoryName += (information[0] == null) ? ExtractArtist(birth, file)[0] : information[0][0];
                                a++;
                            } break;
                    }
                }
                else
                {
                    if (information[14] == "true")
                        goto ReturnTorrent;

                    directoryName += directoryArray[a];
                }
            }

            information[13] = settings.GetDownloadDirectory().Trim() + directoryName.Trim();

            if (settings.GetUppercaseAllFolderNames())
                information[13] = information[13].ToUpper();
            else if (settings.GetLowercaseAllFolderNames())
                information[13] = information[13].ToLower();

            information[10] = file;
            information[11] = Path.GetFileName(file);
            information[12] = birth;
            ReturnTorrent:
            return new Torrent(information);
        }
        public void VerifyTorrent(Torrent torrent)
        {
            string[] information = torrent.GetInformation();
            for (int a = 0; a < information.Length; a++)
            {
                /*Switch statement soley to improve readability*/
                switch (a)
                {
                    case 0: //Artist
                        {
                            if (information[a] == null)
                                goto default;

                            MusicBrainzXMLDocumentCreator createXML = new MusicBrainzXMLDocumentCreator("http://musicbrainz.org/ws/1/artist/?type=xml&name=" + information[0]);
                            MusicBrainzXMLDocumentArtist[] artists = createXML.ProcessArtist();

                            /*if (!artists[0].name.Equals(information[0]) && !artists[1].name.Equals(information[0]))
                            {
                                string trimmedArtist = information[0].Trim();

                                if (!artists[0].name.Equals(trimmedArtist) && !artists[1].name.Equals(trimmedArtist))
                                {
                                    information[0] = IssueWarning("Artist is not perfect match", information[10]);
                                }
                            }*/
                        }
                        break;
                    case 1: //Album
                        {
                            if (information[a] == null)
                                goto default;

                            MusicBrainzXMLDocumentCreator createXML = new MusicBrainzXMLDocumentCreator("http://musicbrainz.org/ws/1/release/?type=xml&title=" + information[1]);
                            MusicBrainzXMLDocumentRelease[] releases = createXML.ProcessRelease();

                            /*if (!releases[0].releaseTitle.Equals(information[1]) && releases[0].ext_score.Equals("100"))
                            {
                                information[1] = IssueWarning("Album is not perfect match", information[10]);
                            }*/
                        }
                        break;
                    case 2: //Release Type
                        {
                            if (information[a] == null)
                                goto default;

                            /*MusicBrainzXMLDocumentCreator createXML = new MusicBrainzXMLDocumentCreator("http://musicbrainz.org/ws/1/artist/?type=xml&name=" + information[0]);
                            MusicBrainzXMLDocumentRelease[] releases = createXML.ProcessRelease();

                            if (!releases[0].releaseType.Equals(information[2]) && releases[0].ext_score.Equals("100"))
                            {
                                information[2] = IssueWarning("Release type is not perfect match", information[10]);
                            }
                            else if (!releases[1].releaseType.Equals(information[2]))
                            {
                                information[2] = IssueWarning("Release type is not perfect match", information[10]);
                            }*/
                        }
                        break;
                    case 3:
                        {
                            if (information[a] == null)
                                goto default;

                            Match match = (Regex.Match(information[3], "(([0-2][0-9][0-9]|V0|V1|V2|APS|APX|V8)+[(]?(VBR)?[)]?)|Lossless"));
                            if (!match.Success)
                            {
                                information[3] = IssueWarning("Bitrate is not perfect match", information[10]);
                            }
                        }
                        break;
                    case 4:
                        {
                            if (information[a] == null)
                                goto default;

                            if (int.Parse(information[4]) < 1900 || int.Parse(information[4]) > DateTime.Today.Year + 1)
                            {
                                information[4] = IssueWarning("Year is unrealistic", information[10]);
                            }

                        }
                        break;
                    case 5:
                        {
                            /*Musicbrainz doesn't have physical formats as far as I'm aware, and
                             * since my means of getting the physical format are String.Contains(...)
                             * it's pointless to do another check.
                             * */
                        }
                        break;
                    case 6:
                        {
                            /*Bitrates are defined and verified by the birth of the torrent. There's 
                             nothing I can do to check if this is correct*/
                        }
                        break;
                    case 13:
                        {
                            if (information[13].EndsWith("EP EP"))
                            {
                                information[13] = information[13].Remove(information[13].Length - 2, 2);
                                information[13] = information[13].Trim();
                            }

                            while (information[13].Contains("  "))
                            {
                                information[13] = information[13].Replace("  ", " ");
                            }

                            try
                            {
                                Match match = Regex.Match(information[13], "[<]|[>]|[/]|[|]|[?]|[*]");
                            
                            if (match.Success)
                            {
                                information[13] = IssueError("Illegal characters", information[13]);
                            }
                            }
                            catch (Exception e)
                            { }

                            if ((settings.GetTorrentSaveFolder() + @"\[CSL] -- Handled Torrents\" + information[11]).Length >= 255)
                            {
                                IssueError("Torrent save location is greater than 255", information[10]);
                            }

                        }
                        break;
                    default:
                        {
                            //Check to see if null values are supposed to be null
                            string switches = settings.GetCustomDirectory().ToLower();

                            switch (a)
                            {
                                case 0:
                                    if (switches.Contains("%a"))
                                    {
                                        information[0] = IssueError("Can't parse artist", information[10]);
                                    }
                                    break;
                                case 1:
                                    if (switches.Contains("%t"))
                                    {
                                        information[1] = IssueError("Can't parse album", information[10]);
                                    }
                                    break;
                                case 2:
                                    Match match = Regex.Match(switches, "%l|%s|%c|%e|%r|%v|%n|%x|%u|%v|%z");
                                    if (match.Success)
                                    {
                                        information[2] = IssueError("Can't parse release format", information[10]);
                                    }
                                    break;
                                case 3:
                                    if (switches.Contains("%b"))
                                    {
                                        information[2] = IssueError("Can't parse bitrate", information[10]);
                                    }
                                    break;
                                case 4:
                                    if (switches.Contains("%y"))
                                    {
                                        information[2] = IssueError("Can't parse year", information[10]);
                                    }
                                    break;
                                case 5:
                                    if (switches.Contains("%p"))
                                    {
                                        information[2] = IssueError("Can't parse physical format", information[10]);
                                    }
                                    break;
                                case 6:
                                    if (switches.Contains("%d"))
                                    {
                                        information[2] = IssueError("Can't parse bit format", information[10]);
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        public string GetTorrentBirth(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string fileContents = sr.ReadToEnd();

            if (fileContents.Contains("waffles"))
                return "waffles";
            else if (fileContents.Contains("what"))
                return "what";
            else
                fileContents = sr.ReadLine();


            return IssueError("Can't parse birth", file);
        }
        public string IssueError(string error, string file)
        {
            string returnString = null;

            switch (error)
            {
                case "Can't parse artist":
                    {
                        ErrorWindow ew = new ErrorWindow();
                        returnString =  ew.IssueArtistError(file);
                    }
                    break;
                case "Can't parse album":
                    {
                        ErrorWindow ew = new ErrorWindow();
                        returnString =  ew.IssueAlbumError(file);
                    }
                    break;
                case "Can't parse year":
                    {
                        ErrorWindow ew = new ErrorWindow();
                        returnString =  ew.IssueYearError(file);
                    }
                    break;
                case "Can't parse release format":
                    {
                        ErrorWindow ew = new ErrorWindow();
                        returnString =  ew.IssueReleaseFormatError(file);
                    }
                    break;
                case "Can't parse bitrate":
                    {
                        ErrorWindow ew = new ErrorWindow();
                        returnString =  ew.IssueBitrateError(file);
                    }
                    break;
                case "Can't parse physical format":
                    {
                        ErrorWindow ew = new ErrorWindow();
                        returnString =  ew.IssuePhysicalFormatError(file);
                    }
                    break;
                case "Can't parse bit format":
                    {
                        ErrorWindow ew = new ErrorWindow();
                        returnString =  ew.IssueBitformatError(file);
                    }
                    break;
                case "Can't parse birth":
                    {
                        ErrorWindow ew = new ErrorWindow();
                        returnString = ew.IssueBirthError(file);
                    }
                    break;
                case "Illegal characters":
                    {
                        ErrorWindow ew = new ErrorWindow();
                        returnString = ew.IssueIllegalCharactersError(file, information[13]);
                        Match match = Regex.Match(returnString, "<|>|:|/|[|]|[?]|*");

                        while (match.Success)
                        {
                            returnString = ew.IssueIllegalCharactersError(file, information[10]);
                            match = Regex.Match(returnString, "<|>|:|/|[|]|[?]|*");
                        }
                    }
                    break;
                default:
                    DiscardTorrent(file);
                    break;
            }

            if (returnString.Equals("discard torrent") || returnString.Equals(null))
            {
                DiscardTorrent(file);
                return null;
            }
            else
                return returnString;
        }
        public string IssueWarning(string warning, string file)
        {
            string returnString = null;
            switch (warning)
            {
                case "Artist is not a perfect match":
                    {
                        ErrorWindow ew = new ErrorWindow();
                        returnString = ew.IssueArtistWarning(file, information[0]);
                    }
                    break;
                case "Album is not a perfect match":
                    {
                        ErrorWindow ew = new ErrorWindow();
                        returnString = ew.IssueAlbumWarning(file, information[1]);
                    }
                    break;
                case "Year is unrealistic":
                    {
                        ErrorWindow ew = new ErrorWindow();
                        returnString = ew.IssueYearWarning(file, information[4]);
                    }
                    break;
            }

            if (returnString.Equals("discard torrent") || returnString.Equals(null))
            {
                DiscardTorrent(file);
                return null;
            }
            else
                return returnString;
        }
        public void DiscardTorrent(string file)
        {
            information[14] = "true"; //discard torrent
        }

        public string ExtractArtist(string birth, string file)
        {
            file = Path.GetFileName(file);
            switch (birth)
            {
                case "waffles":
                    {
                        /*Look for a space on either side of the dash*/
                        int startingPosition = 0;
                        int dashes = file.Split('-').Length;
                        for (int a = 0; a <= dashes; a++)
                        {
                            int dash = file.IndexOf('-', startingPosition);

                            if (file[dash - 1].Equals(' ') && file[dash + 1].Equals(' '))
                            {
                                return file.Substring(0, dash - 1); //return the artist
                            }

                            startingPosition = dash + 1;
                        }
                        /*If that fails...*/
                        return IssueError("Can't parse artist", file);
                    }
                case "what":
                    {
                        bool zipped = file.Contains("[CSL]--Temp");

                        switch (zipped) //Zipped or not zipped.. switch statement improves readability
                        {
                            case (true):
                                {
                                    file = Path.GetDirectoryName(file);
                                    //...\[CSL]--Temp\artist\[physicalformat]\[files]
                                    int firstSlash = file.IndexOf("[CSL]--Temp") - 1;
                                    int secondSlash = file.IndexOf(@"\", firstSlash + 1);
                                    int thirdSlash = file.IndexOf(@"\", secondSlash + 1);

                                    return file.Substring(secondSlash, thirdSlash - secondSlash);
                                }
                            case (false):
                                {
                                    /*Look for a space on either side of the dash*/
                                    int startingPosition = 0;
                                    int dashes = file.Split('-').Length;
                                    for (int a = 0; a <= dashes; a++)
                                    {
                                        int dash = file.IndexOf('-', startingPosition);

                                        if (file[dash - 1].Equals(' ') && file[dash + 1].Equals(' '))
                                        {
                                            if (file[dash + 2].Equals('2') || (file[dash + 2].Equals('1')))
                                                break;
                                            else
                                            {
                                                string artist = file.Substring(0, dash - 1);
                                                return artist;
                                            }
                                        }

                                        startingPosition = dash + 1;
                                    }
                                    /*If that fails..*/
                                    return IssueError("Can't parse artist", file);
                                }
                                
                            default:
                                return IssueError("Can't parse artist", file);
                        }
                    }
                default:
                    return IssueError("Can't parse artist", file);
            }
        }
        public string ExtractAlbum(string birth, string file)
        {
            if (information[1] != null) //AlbumFormat may be called first which may grab the album name
                return information[1];

            switch (birth)
            {
                case "waffles":
                    {
                        /*Look for a space on either side of the dash*/
                        int startingPosition = 0;
                        int dashes = file.Split('-').Length;
                        for (int a = 0; a <= dashes; a++)
                        {
                            int dash = file.IndexOf('-', startingPosition);

                            if (file[dash - 1].Equals(' ') && file[dash + 1].Equals(' '))
                            {
                                return file.Substring(dash + 2, (file.IndexOf('[') - (dash + 2))); //return the album
                            }

                            startingPosition = dash + 1;
                        }
                        /*If that fails...*/
                        return IssueError("Can't parse album", file);
                    }
                    
                case "what":
                    {
                        /*Look for a space on either side of the dash*/
                        int startingPosition = 0;
                        int dashes = file.Split('-').Length;
                        for (int a = 0; a <= dashes; a++)
                        {
                            int dash = file.IndexOf('-', startingPosition);

                            if (file[dash - 1].Equals(' ') && file[dash + 1].Equals(' '))
                            {
                                if (file[dash + 2].Equals('2') || (file[dash + 2].Equals('1')))
                                    break;
                                else
                                    return file.Substring(dash + 2, (file.IndexOf('-', dash + 1) - (dash + 2))); //return the album
                            }

                            startingPosition = dash + 1;
                        }
                        /*If that fails...*/
                        return IssueError("Can't parse album", file);
                    }
                    
                default:
                    return IssueError("Can't parse album", file);
            }
        }
        /*Year can be extracted rather easily by looking for a ####, though live
         * albums must be taken into account as they usually have a date on them.
         * Consequently, we'll do do a check that there is either 
         *  a.) a parenthesis '(' immediately following the what.cd year 
         *  b.) a bracket '[' immediately precedes the waffles.fm year
         *  */
        public string ExtractYear(string birth, string file)
        {
            switch (birth)
            {
                case "waffles":
                    {
                        file = Path.GetFileName(file);
                        Match match = Regex.Match(file, "[1-2]+[0-9]+[0-9]+[0-9]");
                        while (match.Success)
                        {
                            if (file[file.IndexOf(match.Value) - 1] == '[')
                                return match.Value;
                            else
                                match = match.NextMatch();
                        }
                        return IssueError("Can't parse year", file);
                    }
                case "what":
                    {
                        Match match = Regex.Match(Path.GetFileName(file), "[0-9]+[0-9]+[0-9]+[0-9]");
                        while (match.Success)
                        {
                            if (file[file.IndexOf(match.Value) + 5] == '(' | file[file.IndexOf(match.Value) + 6] == '(')
                                return match.Value;
                            else
                                match = match.NextMatch();
                        }

                        return IssueError("Can't parse year", file);
                    }
                default:
                    return IssueError("Can't parse year", file);
            }
        }
        public string ExtractBitrate(string birth, string file)
        {
            switch (birth)
            {
                case "waffles":
                    {
                        if (file.Contains("(VBR)"))
                        {
                            if (file.Contains("V0"))
                                return "V0(VBR)";
                            else if (file.Contains("V2"))
                                return "V2(VBR)";
                            else if (file.Contains("V4"))
                                return "V4(VBR)";
                            else if (file.Contains("V6"))
                                return "V6(VBR)";
                            else if (file.Contains("V8"))
                                return "V8(VBR)";
                            else if (file.Contains("V10"))
                                return "V10(VBR)";
                            else
                                return IssueError("Can't parse bitrate", file);
                        }
                        else if (file.Contains("Lossless]"))
                            return "Lossless";
                        else if (file.Contains("192]"))
                            return "192";
                        else if (file.Contains("256]"))
                            return "256";
                        else if (file.Contains("320]"))
                            return "320";
                        else
                            return IssueError("Can't parse bitrate", file);
                    }
                case "what":
                    {
                        if (file.Contains("(VBR)"))
                        {
                            if (file.Contains("V0"))
                                return "V0(VBR)";
                            else if (file.Contains("V1"))
                                return "V1(VBR)";
                            else if (file.Contains("V2"))
                                return "V2(VBR)";
                            else if (file.Contains("V4"))
                                return "V4(VBR)";
                            else if (file.Contains("V6"))
                                return "V6(VBR)";
                            else if (file.Contains("V8"))
                                return "V8(VBR)";
                            else if (file.Contains("V10"))
                                return "V10(VBR)";
                            else if (file.Contains("APX(VBR)"))
                                return "APX(VBR)";
                            else if (file.Contains("APS(VBR)"))
                                return "APS(VBR)";
                            else if (file.Contains("q8.x(VBR)"))
                                return "q8.x(VBR)";
                            else
                                return IssueError("Can't parse bitrate", file);
                        }
                        else if (file.Contains("24bit Lossless"))
                            return "24bit Lossless";
                        else if (file.Contains("Lossless)"))
                            return "Lossless";
                        else if (file.Contains("192)"))
                            return "192";
                        else if (file.Contains("256)"))
                            return "256";
                        else if (file.Contains("320)"))
                            return "320";
                        else
                            return IssueError("Can't parse bitrate", file);
                    }
                default:
                    return IssueError("Can't parse bitrate", file);
            }
        }
        public string ExtractBitformat(string birth, string file)
        {
            switch (birth)
            {
                case "waffles":
                    {
                        file = Path.GetFileName(file);

                        int parenthesisIndex = file.IndexOf("[20") - 1;
                        if (parenthesisIndex < 0)
                            parenthesisIndex = file.IndexOf("[19") - 1;
                        if (parenthesisIndex < 0)
                            return null;
                        string innerString = file.Substring(parenthesisIndex);

                        if (innerString.Contains("MP3"))
                            return "MP3";
                        else if (innerString.Contains("AAC"))
                            return "AAC";
                        else if (innerString.Contains("FLAC"))
                            return "FLAC";
                        else if (innerString.Contains("AC3"))
                            return "AC3";
                        else if (innerString.Contains("DTS"))
                            return "DTS";
                        else if (innerString.Contains("Ogg"))
                            return "Ogg";
                        else
                            return IssueError("Can't parse bit format", file);
                    }
                case "what":
                    {
                        file = Path.GetFileName(file);
                        if (file.Contains("MP3"))
                            return "MP3";
                        else if (file.Contains("AAC"))
                            return "AAC";
                        else if (file.Contains("FLAC"))
                            return "FLAC";
                        else if (file.Contains("AC3"))
                            return "AC3";
                        else if (file.Contains("DTS"))
                            return "DTS";
                        else if (file.Contains("Ogg"))
                            return "Ogg";
                        else
                            return IssueError("Can't parse bit format", file);
                    }
                default:
                    return null;
            }
        }
        public string ExtractPhysicalFormat(string birth, string file)
        {
            switch (birth)
            {
                case "waffles":
                    {
                        file = Path.GetFileName(file);

                        if (file.Contains("-CD"))
                            return "CD";
                        else if (file.Contains("-Vinyl"))
                            return "Vinyl";
                        else if (file.Contains("-Cassette"))
                            return "Cassette";
                        else if (file.Contains("-Other"))
                            return "Other/Unknown";
                        else
                            return IssueError("Can't parse physical format", file);
                    }
                case "what":
                    {
                        bool zipped = file.Contains("[CSL]--Temp");

                        switch (zipped) //Zipped or not zipped.. switch statement improves readability
                        {
                            case (true):
                                {
                                    file = Path.GetDirectoryName(file);
                                    //...\[CSL]--Temp\artist\[physicalformat]\[files]
                                    int firstSlash = file.IndexOf("[CSL]--Temp") - 1;
                                    int secondSlash = file.IndexOf(@"\", firstSlash + 1);
                                    int thirdSlash = file.IndexOf(@"\", secondSlash + 1);
                                    int fourthSlash = file.IndexOf(@"\", thirdSlash + 1);

                                    return file.Substring(secondSlash, thirdSlash - secondSlash);
                                }
                                
                            case (false):
                                {
                                    file = Path.GetFileName(file);

                                    if (file.Contains("(CD"))
                                        return "CD";
                                    else if (file.Contains("(Vinyl"))
                                        return "Vinyl";
                                    else if (file.Contains("(Cassette"))
                                        return "Cassette";
                                    else if (file.Contains("(DVD"))
                                        return "DVD";
                                    else if (file.Contains("(WEB"))
                                        return "WEB";
                                    else if (file.Contains("(SACD"))
                                        return "SACD";
                                    else if (file.Contains("(Soundboard"))
                                        return "Soundboard";
                                    else if (file.Contains("(DAT"))
                                        return "DAT";
                                    else
                                        return IssueError("Can't parse physical format", file);
                                }
                                
                            default:
                                return IssueError("Can't parse physical format", file); 
                        }
                    }
                default:
                    return IssueError("Can't parse physical format", file);
            }
        }
        public string ExtractAlbumFormat(string birth, string file, bool zipped)
        {
            switch (zipped)
            {
                case (true):
                    {
                        if (file.Contains("\\Album\\"))
                            return "Album";
                        else if (file.Contains("\\Bootleg\\"))
                            return "Bootleg";
                        else if (file.Contains("\\Live album\\"))
                            return "Live Album";
                        else if (file.Contains("\\Mixtape\\"))
                            return "Mixtape";
                        else if (file.Contains("\\EP\\"))
                            return "EP";
                        else if (file.Contains("\\Compilation\\"))
                            return "Compilation";
                        else if (file.Contains("\\Interview\\"))
                            return "Interview";
                        else if (file.Contains("\\Remix\\"))
                            return "Remix";
                        else if (file.Contains("\\Single\\"))
                            return "Single";
                        else if (file.Contains("\\Unknown\\"))
                            return "Unknown";
                        else if (file.Contains("\\Soundtrack\\"))
                            return "Soundtrack";
                        else
                            return IssueError("Can't parse release format", file);
                    }
                case (false):
                    {
                        string album = (information[1] == null)? ExtractAlbum(birth, file) : information[1];

                        MusicBrainzXML.MusicBrainzXMLDocumentCreator mb = new MusicBrainzXMLDocumentCreator("http://musicbrainz.org/ws/1/release/?type=xml&title=" + album);
                        MusicBrainzXMLDocumentRelease[] results = mb.ProcessRelease();
                        if (results[0].ext_score.Equals("100")) //Look into making this a variable amount, or keep it concrete at 100
                            return results[0].releaseType;
                        else
                        {
                            return IssueError("Can't parse release format", file);
                        }
                    }
                default:
                    return IssueError("Can't parse release format", file);
            }
        }
    }
}
