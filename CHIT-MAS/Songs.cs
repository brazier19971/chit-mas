using HtmlAgilityPack;
using System.Net;

namespace CHIT_MAS
{
    public class Songs
    {
        public string trackName { get; set; }
        public string artistName { get; set; }
        public string stationName { get; set; }
        public string stationLogo { get; set; }
        public bool isAlarming { get; set; }
        public string artistSeverity { get; set; }
        public string artistImage { get; set; }

        public static List<Songs> songList = new List<Songs>();

      

        public static void GetSongs()
        {

            foreach (Stations station in Stations.stationList)
            {
                string siteHtML;

                //Method updated on 30/10 to RESOLVE for dual-alarm. 
                using (WebClient client = new WebClient())
                {
                    //Changed on 17/10 to use the Radiobox site, due to horrible site. 
                    siteHtML = client.DownloadString(station.dataSourceURL);
                }

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(siteHtML);
                try
                {
                    var artistName = htmlDocument.DocumentNode.SelectSingleNode("/html/body/div[3]/div[5]/div/div[1]/div/section/table/tbody/tr[1]/td[2]/a").InnerText;
                    string[] multiArray = artistName.Split(" - ");
                    artistName = multiArray[0];
                    var track = multiArray[1];

                    artistName = System.Net.WebUtility.HtmlDecode(artistName);
                    track = System.Net.WebUtility.HtmlDecode(track);

                    Songs song = new Songs();
                    song.stationName = station.stationName;
                    song.stationLogo = station.stationLogo;
                    song.trackName = track;
                    song.artistName = artistName;

                    //WE NOW HAVE THE SONG AND TRACK NAME. WE MUST DECIDE THE ALARM STATE
                    artistName = artistName.ToUpper();
                    foreach (Artist artist in Artist.artistList)
                    {
                        //Convert the found song name to upper - as that's how we're doing it
                        //Some stations don't seem to know the difference between 'GAYLE' and 'Gayle'.....smh...
                        //WHY WON'T THIS FREAKING WORK!
                        if (artistName.Contains(artist.artistName))
                        {
                            // 20/10/22 Will break and stop on the first alarming artist. If there is one alarming artist, there is no need to carry on. We'll take what we can get! 
                            song.isAlarming = true;
                            song.artistSeverity = artist.artistSeverity;
                            song.artistImage = artist.artistImageLink;
                            break;
                        }

                     
                    }
                        Songs.songList.Add(song);
                }   
                catch (Exception ex)
                {
                    Songs song = new Songs();
                    song.stationName = station.stationName;
                    song.stationLogo = station.stationLogo;
                    song.trackName = "No Track";
                    song.artistName = "No Artist";
                    Songs.songList.Add(song);

                }

            }

        }

       
    }
}
