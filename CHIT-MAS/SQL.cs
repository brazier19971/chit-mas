
using System.Data.SqlClient;

namespace CHIT_MAS
{
    public class SQL
    {
        //Provide an SQL Connection String
        public const string sqlConnString = @"";

        //Method for checking the last song
        public static string CheckLastSongSQL(string station)
        {
            string sqlQuery = @"SELECT TOP 1 (mas_data_test.track)
                                FROM mas_data_test
                                WHERE (((mas_data_test.station)='" + station + "')) ORDER BY (mas_data_test.datetime) DESC;";
            SqlConnection conn = new SqlConnection(sqlConnString);
            conn.Open();
            SqlCommand comm = new SqlCommand(sqlQuery, conn);
            string lastTrk = (string)comm.ExecuteScalar();
            conn.Close();
            return lastTrk;
        }
        //Send off SQL Data
        public static void sendSQL(string type, string song, int alarmState, string station)
        {
            System.Data.SqlClient.SqlConnection sqlConnection1 =
            new System.Data.SqlClient.SqlConnection(sqlConnString);

            string sysdate = DateTime.Now.ToString("s");
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT INTO mas_data_test ( artist, track, alarm, station, datetime) VALUES ( '" + type.Replace("'", "''") + "', '" + song.Replace("'", "''") + "', '" + alarmState + "', '" + station + "','" + sysdate + "')";
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
        }

    }
}
