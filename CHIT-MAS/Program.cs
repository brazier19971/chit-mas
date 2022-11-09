//THE CHELMSFORD IT COMPANY 2022 
//MUSIC ALERT SYSTEM (MAS) V2.6 


using System.Timers;
namespace CHIT_MAS
{
    public class Program
    {
        private static System.Timers.Timer runTimer;

        public static void Main(string[] args)
        {
            //Get set up. Bring in the artists and the stations.
            Stations.GetStations();
            Artist.GetArtists();
            //This version pulls the artists etc in once per run. 
            
            //A new timer to detemine how often we want the system to sample. 
            runTimer = new System.Timers.Timer(40000);
            // Hook up the Elapsed event for the timer. 
            runTimer.Elapsed += OnTimedEvent;
            runTimer.AutoReset = true;
            runTimer.Enabled = true;
            runTimer.Start();
            Console.ReadKey();
        }


        static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Songs.GetSongs();
            Console.Clear();
            Console.WriteLine("RADIO STATION STATUS AT " + DateTime.Now);

            foreach (Songs songs in Songs.songList)
            {
                string almTxt;
                //What do we do if there is NO ALARM?
                try
                {
                    if (songs.isAlarming == null)
                    {

                        almTxt = " Alarm Status: OK";
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(songs.stationName + ": " + songs.artistName + " - " + songs.trackName + " --- " + almTxt);
                        if (songs.artistName == "No Artist") { continue; }
                        //Upload to S Q L server only if the song is new. 
                        string lastTrkSQL = SQL.CheckLastSongSQL(songs.stationName);
                        if (lastTrkSQL == songs.trackName)
                        {
                            Console.WriteLine("This is an old track");
                            Console.WriteLine();
                            continue;
                        }
                        int alarm = 0;
                        SQL.sendSQL(songs.artistName, songs.trackName, alarm, songs.stationName);
                    }
                    else
                    {
                        almTxt = " Alarm Status: ALARM DETECTED";
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(songs.stationName + ": " + songs.artistName + " - " + songs.trackName + " --- " + almTxt);
                        if (songs.artistName == "No Artist") { continue; }
                        string lastTrkSQL = SQL.CheckLastSongSQL(songs.stationName);
                        if (lastTrkSQL == songs.trackName)
                        {
                            continue;
                        }
                        int alarm = 1;
                        DiscordMsg.SendMsg(songs.artistName, songs.stationName, songs.trackName, songs.artistSeverity, songs.stationLogo, songs.artistImage);
                        SQL.sendSQL(songs.artistName, songs.trackName, alarm, songs.stationName);
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.White;
               
            }
            Songs.songList.Clear();
        }
    }
}

//TO THE GIRL WHO DRIVES FILMS THOSE 'DRIVE WITH ME' VIDEOS...
//THANK YOU FOR OBSESSING 'YOU KNOW WHO' WITH EVEN MORE SHIT MUSIC...
//GOD DOES 2022 SUCK...
//(DROPS MIC)