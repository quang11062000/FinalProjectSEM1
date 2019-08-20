using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;
namespace DAL
{
    public class MatchDAL
    {
        private string querry;
        private static MySqlDataReader reader;
        private static MySqlConnection connection;

        public MatchDAL()
        {
            connection = DBHelper.OpenConnection();
        }

        private Match GetMatchDetails(MySqlDataReader reader)
        {
            Match m = new Match();
            m.MatchID = reader.GetInt32("match_id");
            m.MatchName = reader.GetString("match_name");
            m.MatchTime = reader.GetString("match_time");
            m.MatchDay = reader.GetDateTime("match_date");
            m.MatchStadium = reader.GetString("match_stadium");
            return m;
        }
        private List<Match> GetListMatchDetails(MySqlCommand command)
        {
            List<Match> listmdt = new List<Match>();
            while (reader.Read())
            {
                Match m = GetMatchDetails(reader);
                listmdt.Add(m);
            }
            connection.Close();
            return listmdt;
        }
        public List<Match> GetMatchDetailInfo()
        {
            List<Match> listmdt = new List<Match>();
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            querry = @"select * from matches;";
            MySqlCommand command = new MySqlCommand(querry, connection);
            reader = command.ExecuteReader();
            if (reader != null)
            {
                listmdt = GetListMatchDetails(command);
            }
            reader.Close();
            connection.Close();
            // DBHelper.CloseConnection();
            return listmdt;
        }
    }
}