using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CHIT_MAS
{
    public class Artist
    {
        public string? artistName { get; set; } 
        public string? artistSeverity { get; set; }
        public string? artistImageLink { get; set; }

        public static List<Artist> artistList = new List<Artist>();

        public static void GetArtists()
        {
            using (SqlConnection sqlConn = new SqlConnection(SQL.sqlConnString))
            {
                try
                {
                    sqlConn.Open();
                    string sqlTxt = "SELECT * FROM ARTIST";

                    using (SqlCommand sqlComm = new SqlCommand(sqlTxt, sqlConn))
                    {
                        using (SqlDataReader dr = sqlComm.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Artist artist = new Artist();
                                artist.artistName = dr.GetString(0);
                                artist.artistSeverity = dr.GetString(1);
                                artist.artistImageLink = dr.GetString(2);
                                artistList.Add(artist);
                            }
                        }
                    }
                    sqlConn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Problem with SQL Query! " + ex);
                }
            }
        }
    }
}
