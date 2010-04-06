using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace MusicBrainzXML
{
    class MusicBrainzXMLDocumentCreator
    {
        XPathDocument document;
        XPathNavigator nav;
        MusicBrainzXMLDocumentRelease[] results = new MusicBrainzXMLDocumentRelease[50];

        public MusicBrainzXMLDocumentCreator(string xmlurl)
        {
            document = new XPathDocument(xmlurl);
            nav = document.CreateNavigator();
        }

        public MusicBrainzXMLDocumentRelease[] ProcessRelease()
        {
            nav.MoveToChild(XPathNodeType.Element);//metadata
            nav.MoveToChild(XPathNodeType.Element);//release-list
            nav.MoveToChild(XPathNodeType.Element);//release

            for (int a = 0; a < 50; a++)
            {
                results[a] = new MusicBrainzXMLDocumentRelease();

                /*Get Attributes*/
                if (nav.HasAttributes)
                {
                    nav.MoveToFirstAttribute();//album-type
                    int counter = 0;
                    do
                    {
                        switch (counter)
                        {
                            case 0:
                                results[a].releaseType = nav.Value;
                                break;
                            case 1:
                                results[a].releaseID = nav.Value;
                                break;
                            case 2:
                                results[a].ext_score = nav.Value;
                                break;
                        }
                        counter++;
                    } while (nav.MoveToNextAttribute());
                    nav.MoveToParent(); //Back to release 
                }

                nav.MoveToFirstChild(); //First child

                do
                {
                    switch (nav.Name)
                    {
                        case "title":
                            /*Get Title*/
                            results[a].releaseTitle = nav.Value;
                            break;
                        case "text-representation":
                            //skip
                            break;
                        case "asin":
                            /*Get Amazon Standard Identification Number (ASIN)*/
                            results[a].releaseASIN = nav.Value;
                            break;
                        case "artist":
                            /*Get Artist*/
                            if (nav.HasAttributes)
                            {
                                nav.MoveToFirstAttribute(); //Artist ID (used to query Musicbrainz: http://musicbrainz.org/artist/<UID>)
                                results[a].artistId = nav.Value;
                                nav.MoveToParent();//Get out of attributes --> Artist
                            }
                            nav.MoveToChild(XPathNodeType.Element); //Name
                            results[a].artist = nav.Value;
                            nav.MoveToParent(); //Artist
                            break;
                        case "release-event-list":
                            /*Get Releases Event List*/
                            nav.MoveToChild(XPathNodeType.Element); //Event
                            for (int c = 0; c < 50; c++)
                            {
                                if (nav.HasAttributes)
                                {
                                    nav.MoveToFirstAttribute();//format
                                    int counter = 1;
                                    do
                                    {
                                        results[a].releaseEventList[c][counter] = (nav.Name + "|" + nav.Value);
                                        counter++;
                                    } while (nav.MoveToNextAttribute());
                                    nav.MoveToParent(); //Get out of attributes --> Event
                                }

                                nav.MoveToChild(XPathNodeType.Element); //Event
                                nav.MoveToChild(XPathNodeType.Element); //Label

                                if (!nav.Name.Equals("event"))// Make sure it went to label
                                {
                                    if (nav.Name.Equals("label") && nav.HasChildren) //This is what we expect
                                    {
                                        nav.MoveToChild(XPathNodeType.Element); //name
                                        results[a].releaseEventList[c][0] = "label" + "|" + nav.Value;
                                    }
                                    else //This is to handle if something else went on
                                    {
                                        results[a].releaseEventList[c][0] = nav.Name + "|" + nav.Value;
                                    }
                                    nav.MoveToParent(); //Label
                                    nav.MoveToParent(); //Event
                                }

                                if (!nav.MoveToNext())
                                    break;
                            }
                            nav.MoveToParent(); //Release-Event-List
                            break;
                        case "disc-list":
                            /*Get Disc-List count*/
                            if (nav.HasAttributes)
                            {
                                nav.MoveToFirstAttribute(); //Count
                                results[a].discListCount = nav.Value;
                                nav.MoveToParent(); //Get out of attributes --> Disc-list
                            }
                            break;
                        case "track-list":
                            /*Get track-list count*/
                            if (nav.HasAttributes)
                            {
                                nav.MoveToNext(); //Track-List
                                nav.MoveToFirstAttribute(); //Count
                                results[a].trackListCount = nav.Value;
                                nav.MoveToParent(); //Get out of attributes --> Track-List
                            }
                            break;
                        default:
                            //Skip
                            break;
                    }
                } while (nav.MoveToNext());

                int counter1 = 0;
                while (!nav.Name.Equals("release"))
                {
                    if (nav.Name.Equals("metadata") || nav.Name.Equals("release-list"))
                    {
                        nav.MoveToChild(XPathNodeType.Element);
                    }
                    else
                        nav.MoveToParent();

                    if (counter1 > 10)
                    {
                        a--;
                        break;
                    }

                    counter1++;
                }

                if (!nav.MoveToNext()) //Move to next release, if it can't, break out of loop
                    break;
            }
            return results;
        }
        public MusicBrainzXMLDocumentArtist[] ProcessArtist()
        {
            MusicBrainzXMLDocumentArtist[] results = new MusicBrainzXMLDocumentArtist[50];
            for (int a = 0; a < 50; a++)
                results[a] = new MusicBrainzXMLDocumentArtist();


            nav.MoveToChild(XPathNodeType.Element); //metadata
            nav.MoveToChild(XPathNodeType.Element); //artist-list
            nav.MoveToChild(XPathNodeType.Element); //artist
            int counter = -1;

            do
            {
                ++counter;

                if (nav.HasAttributes)
                {
                    nav.MoveToFirstAttribute();
                    try
                    {
                        results[counter].type = nav.Value;
                    }
                    catch (Exception e)
                    {
                    }
                    if (nav.MoveToNextAttribute())
                        results[counter].artistId = nav.Value;
                    if (nav.MoveToNextAttribute())
                        results[counter].ext_score = nav.Value;

                    nav.MoveToParent();
                }

                nav.MoveToChild(XPathNodeType.Element);

                do
                {
                    switch (nav.Name)
                    {
                        case "name":
                            results[counter].name = nav.Value;
                            break;
                        case "sort-name":
                            results[counter].sort_name = nav.Value;
                            break;
                        case "life-span":
                            if (nav.HasAttributes)
                            {
                                nav.MoveToFirstAttribute();
                                results[counter].birth = nav.Value;
                                if (nav.MoveToNextAttribute())
                                results[counter].death = nav.Value;
                            }
                            break;
                    }
                }
                while (nav.MoveToNext());

                nav.MoveToParent(); //Artist

                while (!nav.Name.Equals("artist"))
                {
                    if (nav.Name.Equals("metadata") || nav.Name.Equals("artist-list"))
                    {
                        nav.MoveToChild(XPathNodeType.Element);
                    }
                    else
                        nav.MoveToParent();
                }
            }
            while (nav.MoveToNext());

            return results;
        }
    }
}
