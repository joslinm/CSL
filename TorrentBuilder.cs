using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Collections;
using System.Data;
using MusicBrainzXML;
using System.Windows.Forms;

namespace CSL_Test__1
{
    class TorrentBuilder : BackgroundWorker
    {
        #region Release Types
        public const string Album = "Album";
        public const string EP = "EP";
        public const string Single = "Single";
        public const string Live = "Live";
        public const string Remix = "Remix";
        public const string Compilation = "Compilation";
        public const string Soundtrack = "Soundtrack";
        public const string Bootleg = "Bootleg";
        public const string Interview = "Interview";
        public const string Mixtape = "Mixtape";
        public const string Unknown = "Unknown";
        public string[] ReleaseTypes = new string[] { Album, EP, Single, Live, Remix, Compilation, Soundtrack, Bootleg, Interview, Mixtape, Unknown };
        #endregion

        SettingsHandler SettingsHandler = new SettingsHandler();
        DirectoryHandler DirectoryHandler = new DirectoryHandler();
        ErrorWindow ew = new ErrorWindow();

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

        public TorrentBuilder()
        {
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
        }

        public Torrent[] BuildFromArrayList(ArrayList files)
        {
            ArrayList torrents = new ArrayList();
            Torrent torrent;
            information[14] = null;
            int filescount = files.Count;
            double progress = 0;
            double count = 0;

            for (int a = 0; a < filescount; a++)
            {
                string birth = GetTorrentBirth((string)files[a]);
                torrent = ProcessTorrent((string)files[a], birth);

                if (information[14] != "true")
                {
                    if (SettingsHandler.GetDownloadFormatExists(information[2]))
                    {
                        torrent = VerifyTorrent(torrent);
                        torrents.Add(torrent);
                    }
                }

                //Clear out information for this run to avoid misinformation on the next run
                for (int b = 0; b < information.Length; b++)
                    information[b] = null;

                progress = (++count / filescount) * 100;

                if (progress <= 100 && progress >= 0)
                    this.ReportProgress((int)progress);

            }
            
            
            ew.ClearApplyToAll();
            object[] raw = torrents.ToArray();
            Torrent[] tarray = Array.ConvertAll(raw, new Converter<object, Torrent>(Converter));
            return tarray;
        }
        public static Torrent Converter(object raw)
        {
            return (Torrent)raw;
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
           %m = Mixtape
           %f = Album
           %y = Year
           %t = Album Name     %i = Artist's First Initial
           %b = Bitrate        %p = Physical Format (CD/Vinyl)
           %d = Bitrate Format (MP3/FLAC)  
           %z = All release formats (live,EP,comp,etc)
           ***********************************/

            try
            {
                string directoryName = null;

                char[] directoryArray = SettingsHandler.GetCustomDirectory().ToCharArray();

                if (!SettingsHandler.GetDownloadDirectory().EndsWith("\\"))
                {
                    SettingsHandler.SetDownloadFolder(SettingsHandler.GetDownloadDirectory() + "\\");
                }

                if (!SkipReleaseFormatCheck())
                {
                    if (SettingsHandler.GetHandleLoneTAsAlbum())
                    {
                        if (file.Contains("[CSL]--Temp"))
                            information[2] = ExtractAlbumFormat(birth, file, true);
                        else
                            information[2] = Album;
                    }
                    else
                        information[2] = ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp"));

                    if (!SettingsHandler.GetDownloadFormatExists(information[2]))
                        goto ReturnTorrent;
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
                                    artist = DirectoryHandler.GetHTMLLookUp(artist);
                                    information[0] = artist;

                                    if (SettingsHandler.GetArtistFlip() && information != null)
                                    {
                                        if (information[0].StartsWith("The") || information[0].StartsWith("A "))
                                        {
                                            string[] modifiedArtist = information[0].Split(' ');
                                            artist = "";
                                            for (int b = 1; b < modifiedArtist.Length; b++)
                                            {
                                                artist += modifiedArtist[b];
                                                if (!((b + 1) == modifiedArtist.Length))
                                                {
                                                    artist += " ";
                                                }
                                            }
                                            artist += ", " + modifiedArtist[0];
                                            information[0] = artist;
                                        }
                                    }
                                    else if (SettingsHandler.GetDeleteThe())
                                    {
                                        if (information[0].StartsWith("The"))
                                        {
                                            string[] modifiedArtist = information[0].Split(' ');
                                            artist = "";
                                            for (int b = 1; b < modifiedArtist.Length; b++)
                                            {
                                                artist += modifiedArtist[b];
                                                if (!((b + 1) == modifiedArtist.Length))
                                                {
                                                    artist += " ";
                                                }
                                            }
                                            information[0] = artist;
                                        }
                                    }
                                    directoryName += information[0];
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
                                    album = DirectoryHandler.GetHTMLLookUp(album);
                                    directoryName += album;
                                    information[1] = album;
                                    a++;
                                } break;
                            case ('z'):
                                {
                                    string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];
                                    directoryName += SettingsHandler.GetReleaseFormat(format);
                                    information[2] = format;
                                    a++;
                                } break;
                            case ('l'):
                                {
                                    string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                    if (format == Live)
                                    {
                                        directoryName += SettingsHandler.GetReleaseFormat(Live);
                                        information[2] = format;
                                    }
                                    a++;
                                } break;
                            case ('c'):
                                {
                                    string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                    if (format == Compilation)
                                    {
                                        directoryName += SettingsHandler.GetReleaseFormat(Compilation);
                                        information[2] = format;
                                    }
                                    a++;
                                } break;
                            case ('e'):
                                {
                                    string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                    if (format == EP)
                                    {
                                        directoryName += SettingsHandler.GetReleaseFormat(EP);
                                        information[2] = format;
                                    }
                                    a++;
                                } break;
                            case ('r'):
                                {
                                    string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                    if (format == Remix)
                                    {
                                        directoryName += SettingsHandler.GetReleaseFormat(Remix);
                                        information[2] = format;
                                    }
                                    a++;
                                } break;
                            case ('v'):
                                {
                                    string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                    if (format == Interview)
                                    {
                                        directoryName += SettingsHandler.GetReleaseFormat(Interview);
                                        information[2] = format;
                                    }
                                    a++;
                                } break;
                            case ('n'):
                                {
                                    string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                    if (format == Single)
                                    {
                                        directoryName += SettingsHandler.GetReleaseFormat(Single);
                                        information[2] = format;
                                    }
                                    a++;
                                } break;
                            case ('x'):
                                {
                                    string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                    if (format == Bootleg)
                                    {
                                        directoryName += SettingsHandler.GetReleaseFormat(Bootleg);
                                        information[2] = format;
                                    }
                                    a++;
                                } break;
                            case ('s'):
                                {
                                    string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                    if (format == Soundtrack)
                                    {
                                        directoryName += SettingsHandler.GetReleaseFormat(Soundtrack);
                                        information[2] = format;
                                    }
                                    a++;
                                } break;
                            case ('m'):
                                {
                                    string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                    if (format == Mixtape)
                                    {
                                        directoryName += SettingsHandler.GetReleaseFormat(Mixtape);
                                        information[2] = format;
                                    }
                                    a++;
                                } break;
                            case ('u'):
                                {
                                    string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                    if (format == Unknown)
                                    {
                                        directoryName += SettingsHandler.GetReleaseFormat(Unknown);
                                        information[2] = format;
                                    }
                                    a++;
                                } break;
                            case ('f'):
                                {
                                    string format = (information[2] == null) ? ExtractAlbumFormat(birth, file, file.Contains("[CSL]--Temp")) : information[2];

                                    if (format == Album)
                                    {
                                        directoryName += SettingsHandler.GetReleaseFormat(Album);
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

                information[13] = SettingsHandler.GetDownloadDirectory().Trim() + directoryName.Trim();

                if (SettingsHandler.GetUppercaseAllFolderNames())
                    information[13] = information[13].ToUpper();
                else if (SettingsHandler.GetLowercaseAllFolderNames())
                    information[13] = information[13].ToLower();

            ReturnTorrent:
                information[10] = file;
                information[11] = DirectoryHandler.GetFileName(file, true);
                information[12] = birth;
                return new Torrent(information);
            }
            catch (Exception e)
            {
                ew.IssueGeneralWarning("Error processing torrent", "Please report", e.Message + "\n" + e.StackTrace);
                information[14] = "true";
                return new Torrent(information);
            }
        }
        public Torrent VerifyTorrent(Torrent torrent)
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

                            if (SettingsHandler.GetDoubleSpaceRemoval())
                            {
                                while (information[0].Contains("  "))
                                    information[0] = information[0].Replace("  ", " ");
                            }

                            /*MusicBrainzXMLDocumentCreator createXML = new MusicBrainzXMLDocumentCreator("http://musicbrainz.org/ws/1/artist/?type=xml&name=" + information[0]);
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

                            /*MusicBrainzXMLDocumentCreator createXML = new MusicBrainzXMLDocumentCreator("http://musicbrainz.org/ws/1/release/?type=xml&title=" + information[1]);
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
                                information[3] = IssueWarning("Bitrate is not perfect match", information[10], null);
                            }


                        }
                        break;
                    case 4:
                        {
                            if (information[a] == null)
                                goto default;

                            int year = 1899;

                        ParseYear:
                            try
                            {
                                year = int.Parse(information[4]);
                                if (year < 1900 || year > DateTime.Today.Year + 1)
                                {
                                    information[4] = IssueWarning("Year is unrealistic", information[10], information[4]);
                                    if (information[4] == null)
                                    {
                                        information[14] = "true";
                                        break;
                                    }
                                    else
                                    {
                                        goto ParseYear;
                                    }
                                }
                            }
                            catch (FormatException)
                            {
                                information[4] = IssueWarning("Year is unrealistic", information[10], information[4]);
                                if (information[4] == null)
                                {
                                    information[14] = "true";
                                    break;
                                }
                                else
                                {
                                    goto ParseYear;
                                }
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
                            if (information[13] != null)
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

                                if (SettingsHandler.GetUppercaseAllFolderNames())
                                    information[13] = information[13].ToUpper();
                                else if (SettingsHandler.GetLowercaseAllFolderNames())
                                    information[13] = information[13].ToLower();

                                try
                                {
                                    Match match = Regex.Match(information[13], "[<]|[>]|[/]|[|]|[?]|[*]");

                                    if (match.Success)
                                    {
                                        information[13] = IssueError("Illegal characters", information[13]);
                                    }
                                }
                                catch { }
                            }

                            if ((SettingsHandler.GetTorrentSaveFolder() + @"\[CSL] -- Handled Torrents\" + information[11]).Length >= 255)
                            {
                                IssueError("Torrent save location is greater than 255", information[10]);
                            }
                        }
                        break;
                    default:
                        break;
                        
                }
            }

            return new Torrent(information);
        }
        public static string RebuildCustomPath(string[] information)
        {
            string directoryName = null;
            char[] directoryArray = SettingsHandler.GetCustomDirectory().ToCharArray();

            for (int a = 0; a < directoryArray.Length; a++)
            {
                if (directoryArray[a] == ('%'))
                {
                    switch (directoryArray[a + 1])
                    {
                        /* information
                             * 0: artist
                             * 1: album
                             * 2: release format
                             * 3: bitrate
                             * 4: year
                             * 5: physical format
                             * 6: bit format
                             * */

                        case ('a'):
                            {

                                string artist = information[0];
                                directoryName += artist;

                                a++;
                            } break;
                        case ('y'):
                            {

                                string year = information[4];
                                directoryName += year;

                                a++;
                            } break;
                        case ('b'):
                            {

                                string bitrate = information[3];
                                directoryName += bitrate;

                                a++;
                            } break;
                        case ('d'):
                            {

                                string bitformat = information[6];
                                directoryName += bitformat;

                                a++;
                            } break;
                        case ('p'):
                            {

                                string pformat = information[5];
                                directoryName += pformat;

                                a++;
                            } break;
                        case ('t'):
                            {
                                string album = information[1];
                                directoryName += album;

                                a++;
                            } break;
                        case ('z'):
                            {
                                string format = information[2];
                                directoryName += format;

                                a++;
                            } break;
                        case ('l'):
                            {

                                string format = information[2];
                                if (format.Equals(Live))
                                {
                                    directoryName += Live;
                                }

                                a++;
                            } break;
                        case ('c'):
                            {

                                string format = information[2];

                                if (format.Equals(Compilation))
                                {
                                    directoryName += Compilation;
                                }

                                a++;
                            } break;
                        case ('e'):
                            {

                                string format = information[2];

                                if (format.Equals(EP))
                                {
                                    directoryName += EP;
                                }

                                a++;
                            } break;
                        case ('r'):
                            {

                                string format = information[2];

                                if (format.Equals(Remix))
                                {
                                    directoryName += Remix;
                                }

                                a++;
                            } break;
                        case ('v'):
                            {

                                string format = information[2];

                                if (format.Equals(Interview))
                                {
                                    directoryName += Interview;
                                }

                                a++;
                            } break;
                        case ('n'):
                            {

                                string format = information[2];

                                if (format.Equals(Single))
                                {
                                    directoryName += Single;
                                }

                                a++;
                            } break;
                        case ('x'):
                            {

                                string format = information[2];

                                if (format.Equals(Bootleg))
                                {
                                    directoryName += Bootleg;
                                }

                                a++;
                            } break;
                        case ('s'):
                            {
                                string format = information[2];

                                if (format.Equals(Soundtrack))
                                {
                                    directoryName += Soundtrack;
                                }

                                a++;
                            } break;
                        case ('m'):
                            {
                                string format = information[2];

                                if (format.Equals(Mixtape))
                                {
                                    directoryName += Mixtape;
                                }

                                a++;
                            } break;
                        case ('u'):
                            {

                                string format = information[2];
                                if (format.Equals(Unknown))
                                {
                                    directoryName += Unknown;
                                }

                                a++;
                            } break;
                        case ('f'):
                            {
                                string format = information[2];
                                if (format.Equals(Album))
                                {
                                    directoryName += Album;
                                }

                                a++;
                            } break;
                        case ('i'):
                            {

                                directoryName += information[0][0];
                                a++;

                            } break;
                    }
                }
                else
                {
                    directoryName += directoryArray[a];
                }
            }

            return SettingsHandler.GetDownloadDirectory() + directoryName.Trim();
        }

        public string GetTorrentBirth(string file)
        {
            string value;
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);

                string fileContents = sr.ReadToEnd();
                value = null;

                if (fileContents.Contains("waffles"))
                    value = "waffles";
                else if (fileContents.Contains("what"))
                    value = "what";
            }
            catch(Exception e)
            {
                ew.IssueGeneralWarning("Error reading from torrent file", "Please report", e.Message + "\n" + e.StackTrace);
                value = null;
            }
            finally
            {
                if(fs != null)
                fs.Dispose();
                if(sr != null)
                sr.Dispose();
            }

            if (value == null)
                return IssueError("Can't parse birth", file);
            else
                return value;
        }
        public string IssueError(string error, string file)
        {
            string returnString = null;

            switch (error)
            {
                case "Can't parse artist":
                    {
                        returnString =  ew.IssueArtistError(file);    
                    }
                    break;
                case "Can't parse album":
                    {
                        returnString =  ew.IssueAlbumError(file);    
                    }
                    break;
                case "Can't parse year":
                    {
                        returnString =  ew.IssueYearError(file);
                    }
                    break;
                case "Can't parse release format":
                    {
                        if(SettingsHandler.GetHandleLoneTAsAlbum())
                            returnString = (file.Contains("[CSL]--Temp"))? ew.IssueReleaseFormatError(file) : Album;
                        else
                            returnString =  ew.IssueReleaseFormatError(file);
                    }
                    break;
                case "Can't parse bitrate":
                    {
                        returnString =  ew.IssueBitrateError(file);    
                    }
                    break;
                case "Can't parse physical format":
                    {
                        returnString =  ew.IssuePhysicalFormatError(file);    
                    }
                    break;
                case "Can't parse bit format":
                    {
                        returnString =  ew.IssueBitformatError(file);    
                    }
                    break;
                case "Can't parse birth":
                    {
                        returnString = ew.IssueBirthError(file);
                    }
                    break;
                case "Illegal characters":
                    {
                         
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

            if (returnString == null)
            {
                DiscardTorrent(file);
                return null;
            }
            else
                return returnString;
        }
        public string IssueWarning(string warning, string file, string value)
        {
            string returnString = null;
            switch (warning)
            {
                case "Artist is not a perfect match":
                    {
                        returnString = ew.IssueArtistWarning(file, (value == null) ? information[0] : value);
                    }
                    break;
                case "Album is not a perfect match":
                    {  
                        returnString = ew.IssueAlbumWarning(file, (value == null)? information[1] : value);    
                    }
                    break;
                case "Year is unrealistic":
                    {
                        returnString = ew.IssueYearWarning(file, (value == null) ? information[4] : value);
                    }
                    break;
            }

            if (returnString == null || returnString.Equals("discard torrent"))
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
            switch (birth)
            {
                case "waffles":
                    {
                        file = DirectoryHandler.GetFileName(file, true);
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
                        try
                        {
                            bool zipped = file.Contains("[CSL]--Temp");

                            switch (zipped) //Zipped or not zipped.. switch statement improves readability
                            {
                                case (true):
                                    {
                                        file = DirectoryHandler.GetDirectoryName(file);
                                        //...\[CSL]--Temp\artist\[physicalformat]\[files]
                                        int firstSlash = file.IndexOf("[CSL]--Temp") - 1;
                                        int secondSlash = file.IndexOf(@"\", firstSlash + 1) + 1;
                                        int thirdSlash = file.IndexOf(@"\", secondSlash + 1);

                                        return file.Substring(secondSlash, thirdSlash - secondSlash);
                                    }
                                case (false):
                                    {
                                        /*Look for a space on either side of the dash*/
                                        int startingPosition = 0;
                                        int dashes = file.Split('-').Length;
                                        string filename = DirectoryHandler.GetFileName(file, true);
                                        for (int a = 0; a <= dashes; a++)
                                        {
                                            int dash = filename.IndexOf('-', startingPosition);

                                            if (filename[dash - 1].Equals(' ') && filename[dash + 1].Equals(' '))
                                            {
                                                string artist = filename.Substring(0, dash - 1);
                                                return artist;
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
                        catch (Exception e)
                        {
                            ew.IssueGeneralWarning("Error extracting artist", "Please report", e.Message + "\n" + e.StackTrace);
                            return IssueError("Can't parse artist", file);
                        }

                    }
                default:
                    return IssueError("Can't parse artist", file);
            }
        }
        public string ExtractAlbum(string birth, string file)
        {
            file = DirectoryHandler.GetFileName(file, true);

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
                        try
                        {
                            /*Look for a space on either side of the dash*/
                            int startingPosition = 0;
                            int dashes = file.Split('-').Length;
                            for (int a = 0; a <= dashes; a++)
                            {
                                int dash = file.IndexOf('-', startingPosition);

                                if (file[dash - 1].Equals(' ') && file[dash + 1].Equals(' '))
                                {
                                    try
                                    {
                                        return file.Substring(dash + 2, ((file.IndexOf('-', dash + 1)) - (dash + 2))); //return the album
                                    }
                                    catch (Exception e)
                                    {
                                        information[14] = "true";
                                        ew.IssueGeneralWarning("File error while extracting album name", "Please report", e.Message + "\n" + e.StackTrace);
                                        return null;
                                    }
                                }

                                startingPosition = dash + 1;
                            }
                            /*If that fails...*/
                            return IssueError("Can't parse album", file);
                        }
                        catch (Exception e)
                        {
                            ew.IssueGeneralWarning("Error extracting album", "Please report", e.Message + "\n" + e.StackTrace);
                            return IssueError("Can't parse album", file);
                        }
                    }
                    
                default:
                    return IssueError("Can't parse album", file);
            }
        }
        public string ExtractYear(string birth, string file)
        {
            string filename = DirectoryHandler.GetFileName(file, true);

            switch (birth)
            {
                case "waffles":
                    {
                        Match match = Regex.Match(DirectoryHandler.GetFileName(file, true), "[1-2]{1}[09][0-9][0-9]");
                        if (match.Success)
                        {
                            int year = Int32.Parse(match.Value);
                            int year2 = year;

                            if (match.Captures.Count > 1)
                            {
                                match.NextMatch();
                                year2 = Int32.Parse(match.Value);
                            }

                            if (year == year2)
                                return match.Value;
                            else
                                return IssueError("Can't parse year", filename);
                        }
                        else
                        {
                            return IssueError("Can't parse year", filename);
                        }
                    }
                case "what":
                    {
                        try
                        {
                            if (filename == null)
                            {
                                DirectoryHandler.GetFileName(file, false); //Try again..
                            }
                            Match match = Regex.Match(filename, "[1-2]{1}[09][0-9][0-9]");
                            if (match.Success)
                            {
                                int year = Int32.Parse(match.Value);
                                int year2 = year;

                                if (match.Captures.Count > 1)
                                {
                                    match.NextMatch();
                                    year2 = Int32.Parse(match.Value);
                                }

                                if (year == year2)
                                    return match.Value;
                                else
                                    return IssueError("Can't parse year", file);
                            }
                            else
                                return IssueError("Can't parse year", file);
                        }
                        catch (Exception e)
                        {
                            ew.IssueGeneralWarning("Error parsing year", "Please report", e.Message + "\n" + e.StackTrace);
                            return IssueError("Can't parse year", file);
                        }
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
                            {
                                string structure = SettingsHandler.GetBitrate("VBR");
                                string bitrate = "V0(VBR)";
                                if (structure.Contains("#"))
                                {
                                    bitrate = "";

                                    while (structure.Contains("#"))
                                        structure = structure.Replace('#', '0');

                                    bitrate = structure;
                                }
                                return bitrate;
                            }
                            else if (file.Contains("V10"))
                            {
                                string structure = SettingsHandler.GetBitrate("VBR");
                                string bitrate = "V10(VBR)";
                                if (structure.Contains("#"))
                                {
                                    bitrate = "";

                                    while (structure.Contains("#"))
                                        structure = structure.Replace("#", "10");

                                    bitrate = structure;
                                }
                                return bitrate;
                            }
                            else if (file.Contains("V2"))
                            {
                                string structure = SettingsHandler.GetBitrate("VBR");
                                string bitrate = "V2(VBR)";
                                if (structure.Contains("#"))
                                {
                                    bitrate = "";

                                    while (structure.Contains("#"))
                                        structure = structure.Replace('#', '2');

                                    bitrate = structure;
                                }
                                return bitrate;
                            }
                            else if (file.Contains("V4"))
                            {
                                string structure = SettingsHandler.GetBitrate("VBR");
                                string bitrate = "V4(VBR)";
                                if (structure.Contains("#"))
                                {
                                    bitrate = "";

                                    while (structure.Contains("#"))
                                        structure = structure.Replace('#', '4');

                                    bitrate = structure;
                                }
                                return bitrate;
                            }
                            else if (file.Contains("V6"))
                            {
                                string structure = SettingsHandler.GetBitrate("VBR");
                                string bitrate = "V6(VBR)";
                                if (structure.Contains("#"))
                                {
                                    bitrate = "";

                                    while (structure.Contains("#"))
                                        structure = structure.Replace('#', '6');

                                    bitrate = structure;
                                }
                                return bitrate;
                            }
                            else if (file.Contains("V8"))
                            {
                                string structure = SettingsHandler.GetBitrate("VBR");
                                string bitrate = "V8(VBR)";
                                if (structure.Contains("#"))
                                {
                                    bitrate = "";

                                    while (structure.Contains("#"))
                                        structure = structure.Replace('#', '8');

                                    bitrate = structure;
                                }
                                return bitrate;
                            }
                        }
                        else if (file.Contains("Lossless]"))
                            return SettingsHandler.GetBitrate("Lossless");
                        else
                        {
                            Match match = Regex.Match(file, "[-][1-2]{1}[0-9]{2}");

                            if (match.Success)
                            {
                                string value = match.Value;
                                string bitrate = match.Value.Substring(1, 3);
                                value = value.Substring(1, 3);

                                if (int.Parse(value) > 191 && int.Parse(value) < 321)
                                {
                                    string structure = SettingsHandler.GetBitrate("CBR");

                                    if (structure.Contains("###"))
                                    {
                                        bitrate = "";
                                        while (structure.Contains("###"))
                                        {
                                            structure = structure.Replace("###", value);
                                        }
                                        bitrate = structure;
                                    }

                                    return bitrate;
                                }
                                else
                                {
                                    return IssueError("Can't parse bitrate", file);
                                }
                            }
                            else
                            {
                                return IssueError("Can't parse bitrate", file);
                            }
                        }
                    }
                    return IssueError("Can't parse bitrate", file);
                case "what":
                    {
                        try
                        {
                            if (file.Contains("(VBR)"))
                            {
                                if (file.Contains("V0"))
                                {
                                    string structure = SettingsHandler.GetBitrate("VBR");
                                    string bitrate = "V0(VBR)";
                                    if (structure.Contains("#"))
                                    {
                                        bitrate = "";

                                        while (structure.Contains("#"))
                                            structure = structure.Replace('#', '0');

                                        bitrate = structure;
                                    }
                                    return bitrate;
                                }
                                else if (file.Contains("V10"))
                                {
                                    string structure = SettingsHandler.GetBitrate("VBR");
                                    string bitrate = "V10(VBR)";
                                    if (structure.Contains("#"))
                                    {
                                        bitrate = "";

                                        while (structure.Contains("#"))
                                            structure = structure.Replace("#", "10");

                                        bitrate = structure;
                                    }
                                    return bitrate;
                                }
                                else if (file.Contains("V1"))
                                {
                                    string structure = SettingsHandler.GetBitrate("VBR");
                                    string bitrate = "V1(VBR)";
                                    if (structure.Contains("#"))
                                    {
                                        bitrate = "";

                                        while (structure.Contains("#"))
                                            structure = structure.Replace('#', '1');

                                        bitrate = structure;
                                    }
                                    return bitrate;
                                }
                                else if (file.Contains("V2"))
                                {
                                    string structure = SettingsHandler.GetBitrate("VBR");
                                    string bitrate = "V2(VBR)";
                                    if (structure.Contains("#"))
                                    {
                                        bitrate = "";

                                        while (structure.Contains("#"))
                                            structure = structure.Replace('#', '2');

                                        bitrate = structure;
                                    }
                                    return bitrate;
                                }
                                else if (file.Contains("V4"))
                                {
                                    string structure = SettingsHandler.GetBitrate("VBR");
                                    string bitrate = "V4(VBR)";
                                    if (structure.Contains("#"))
                                    {
                                        bitrate = "";

                                        while (structure.Contains("#"))
                                            structure = structure.Replace('#', '4');

                                        bitrate = structure;
                                    }
                                    return bitrate;
                                }
                                else if (file.Contains("V6"))
                                {
                                    string structure = SettingsHandler.GetBitrate("VBR");
                                    string bitrate = "V6(VBR)";
                                    if (structure.Contains("#"))
                                    {
                                        bitrate = "";

                                        while (structure.Contains("#"))
                                            structure = structure.Replace('#', '6');

                                        bitrate = structure;
                                    }
                                    return bitrate;
                                }
                                else if (file.Contains("V8"))
                                {
                                    string structure = SettingsHandler.GetBitrate("VBR");
                                    string bitrate = "V8(VBR)";
                                    if (structure.Contains("#"))
                                    {
                                        bitrate = "";

                                        while (structure.Contains("#"))
                                            structure = structure.Replace('#', '8');

                                        bitrate = structure;
                                    }
                                    return bitrate;
                                }

                                else if (file.Contains("APX(VBR)"))
                                    return SettingsHandler.GetBitrate("APX");
                                else if (file.Contains("APS(VBR)"))
                                    return SettingsHandler.GetBitrate("APS");
                                else if (file.Contains("q8.x(VBR)"))
                                    return SettingsHandler.GetBitrate("q8.x");
                                else
                                    return IssueError("Can't parse bitrate", file);
                            }
                            else if (file.Contains("24bit Lossless"))
                                return SettingsHandler.GetBitrate("24bitLossless");
                            else
                            {
                                Match match = Regex.Match(file, "[1-2]{1}[0-9]{2}[)]");
                                if (match.Success)
                                {
                                    string value = match.Value;
                                    value = value.Substring(0, 3);
                                    string bitrate = value;

                                    if (int.Parse(value) > 191 && int.Parse(value) < 321)
                                    {
                                        string structure = SettingsHandler.GetBitrate("CBR");
                                        if (structure.Contains("###"))
                                        {
                                            bitrate = "";
                                            while (structure.Contains("###"))
                                            {
                                                structure = structure.Replace("###", value);
                                            }
                                            bitrate = structure;
                                        }
                                        return bitrate;
                                    }
                                    else
                                    {
                                        return IssueError("Can't parse bitrate", file);
                                    }
                                }
                                else
                                    return IssueError("Can't parse bitrate", file);
                            }
                        }
                        catch (Exception e)
                        {
                            ew.IssueGeneralWarning("Error parsing bitrate", "Please report", e.Message + "\n" + e.StackTrace);
                            return IssueError("Can't parse bitrate", file);
                        }
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
                        file = DirectoryHandler.GetFileName(file, true);

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
                        try
                        {
                            file = DirectoryHandler.GetFileName(file, true);
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
                        catch (Exception e)
                        {
                            ew.IssueGeneralWarning("Error parsing bit format", "Please report", e.Message + "\n" + e.StackTrace);
                            return IssueError("Can't parse bit format", file);
                        }
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
                        file = DirectoryHandler.GetFileName(file, true);

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
                        try
                        {
                            file = DirectoryHandler.GetFileName(file, true);

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
                            catch (Exception e)
                        {
                            ew.IssueGeneralWarning("Error parsing physical format", "Please report", e.Message + "\n" + e.StackTrace);
                            return IssueError("Can't parse physical format", file);
                        }
                    }
                default:
                    return IssueError("Can't parse physical format", file);
            }
        }
        public string ExtractAlbumFormat(string birth, string file, bool zipped)
        {
            try
            {
                switch (zipped)
                {
                    case (true):
                        {
                            if (file.Contains("\\Album\\"))
                                return (Album);
                            else if (file.Contains("\\Bootleg\\"))
                                return (Bootleg);
                            else if (file.Contains("\\Live album\\"))
                                return (Live);
                            else if (file.Contains("\\Mixtape\\"))
                                return (Mixtape);
                            else if (file.Contains("\\EP\\"))
                                return (EP);
                            else if (file.Contains("\\Compilation\\"))
                                return (Compilation);
                            else if (file.Contains("\\Interview\\"))
                                return (Interview);
                            else if (file.Contains("\\Remix\\"))
                                return (Remix);
                            else if (file.Contains("\\Single\\"))
                                return (Single);
                            else if (file.Contains("\\Unknown\\"))
                                return (Unknown);
                            else if (file.Contains("\\Soundtrack\\"))
                                return (Soundtrack);
                            else
                                return IssueError("Can't parse release format", file);
                        }
                    case (false):
                        {
                            string album = (information[1] == null) ? ExtractAlbum(birth, file) : information[1];
                            return IssueError("Can't parse release format", file);
                            /*MusicBrainzXML.MusicBrainzXMLDocumentCreator mb = new MusicBrainzXMLDocumentCreator("http://musicbrainz.org/ws/1/release/?type=xml&title=" + album);
                            MusicBrainzXMLDocumentRelease[] results = mb.ProcessRelease();
                            if (esults[0].ext_score.Equals("100")) //Look into making this a variable amount, or keep it concrete at 100
                                return results[0].releaseType;
                            else
                            {*/

                        }
                    default:
                        return IssueError("Can't parse release format", file);
                }
            }
            catch (Exception e)
            {
                ew.IssueGeneralWarning("Error parsing release format", "Please report", e.Message + "\n" + e.StackTrace);
                return IssueError("Can't parse release format", file);
            }
        }

        private bool SkipReleaseFormatCheck()
        {
            Match match = Regex.Match(SettingsHandler.GetCustomDirectory(), "%l|%s|%c|%e|%r|%v|%n|%x|%u|%v|%z");
            if (match.Success) return false; //Need release format to properly create save structure

            bool skip = true;
            foreach (string r in ReleaseTypes)
            {
                skip = SettingsHandler.GetDownloadFormatExists(r);
                if (!skip) //If a don't-download exists, need release format to filter out torrents
                    break;
            }
            return skip;
        }
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            string d_folder = SettingsHandler.GetDownloadFolder();
            string t_folder = SettingsHandler.GetTorrentSaveFolder();
            string u_folder = SettingsHandler.GetTorrentClientFolder();
            string custom = SettingsHandler.GetCustomDirectory();

            if (d_folder == null || d_folder == "" || t_folder == null || t_folder == ""
                || u_folder == null || u_folder == "" || custom == null || custom == "")
            {
                //Prevent null errors thrown..
                ew.IssueGeneralWarning("Go to options, and start again", "Not all options are set", null);
                e.Result = null;
            }
            else
            {
                Type t = e.Argument.GetType();
                string name = t.ToString();

                if (name.Equals("System.Collections.ArrayList"))
                    e.Result = this.BuildFromArrayList((ArrayList)e.Argument);
            }
        }
    }
}
