
using System.Data.SqlClient;

namespace CHIT_MAS
{
    public class Stations
    {
        public string stationName { get; set; }
        public string stationLogo { get; set; }
        public string dataSourceURL { get; set; }
       
        public static List<Stations> stationList = new List<Stations>();


        public static void GetStations() 
        {

            using (SqlConnection sqlConn = new SqlConnection(SQL.sqlConnString))
            {
                try
                {
                    sqlConn.Open();
                    string sqlTxt = "SELECT * FROM STATION";

                    using (SqlCommand sqlComm = new SqlCommand(sqlTxt, sqlConn))
                    {
                        using (SqlDataReader dr = sqlComm.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Stations station = new Stations();
                                station.stationName = dr.GetString(0);
                                station.stationLogo = dr.GetString(1);
                                station.dataSourceURL = dr.GetString(2);
                                stationList.Add(station);
                            }
                        }
                    }
                    sqlConn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Problem with SQL Query! " +ex);
                }
            }
        }
    }
}
