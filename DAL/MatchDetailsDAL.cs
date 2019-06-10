using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;
namespace DAL
{
    public class MatchDetailsDAL
    {
        private string querry;
        private static MySqlDataReader reader;
        private static MySqlConnection connection;
        List<MatchDetails> listmdt = new List<MatchDetails>();

        public MatchDetailsDAL()
        {
            connection = DBHelper.OpenConnection();
        }

        private static MatchDetails GetMatchDetails(MySqlDataReader reader)
        {
            MatchDetails mdt = new MatchDetails();
            mdt.M.MatchID = reader.GetInt32("match_id");
            mdt.T.TeamName = reader.GetString("team_name");
            mdt.TeamAway = reader.GetString("team_away");
            mdt.M.MatchTime = reader.GetString("match_time");
            mdt.M.MatchDay = reader.GetString("match_date");
            mdt.T.St.StadiumName = reader.GetString("st_name");
            return mdt;
        }
        private List<MatchDetails> GetListMatchDetails(MySqlCommand command)
        {
            List<MatchDetails> listlol = new List<MatchDetails>();
            while (reader.Read())
            {
                MatchDetails md = GetMatchDetails(reader);
                listlol.Add(md);
            }
            connection.Close();
            return listlol;
        }
        public List<MatchDetails> GetMatchDetailInfo()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            querry = @"select m.match_id,t.team_name,md.team_away,m.match_time,m.match_date,s.st_name
                             from matches m inner join matchdetails md on m.match_id = md.match_id inner join teams t on t.team_id = md.team_id 
                             inner join stadiums s on s.st_id = t.st_id;";
            MySqlCommand command = new MySqlCommand(querry, connection);
            reader = command.ExecuteReader();
            if (reader != null)
            {
                listmdt = GetListMatchDetails(command);
            }
            reader.Close();
            DBHelper.CloseConnection();
            return listmdt;
        }
    }
}