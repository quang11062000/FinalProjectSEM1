using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DAL;
using persistence;
namespace DAL
{

    public class ScheduleDAL
    {
        private static MySqlDataReader reader;
        List<Schedule> listsche = new List<Schedule>();
        public List<Schedule> GetListMatchDetail()
        {
            try
            {
                String querry = @"select m.match_id,t.team_name,md.team_away,m.match_date, m.match_time, s.st_name
                                from matches m inner join matchdetails md on m.match_id = md.match_id inner join teams t on t.team_id = md.team_id 
                                inner join stadiums s on s.st_id = t.st_id;";
                DBHelper.OpenConnection();
                reader = DBHelper.ExecuteQuerry(querry);

                Schedule Schedule = null;
                while (reader.Read())
                {
                    Schedule = GetSchedule(reader);
                    listsche.Add(Schedule);
                }
            }

            catch (System.Exception)
            {
                listsche = null;
            }
           DBHelper.CloseConnection();
            return listsche;

        }
        public Schedule GetSchedule(MySqlDataReader reader)
        {
            Schedule sche = new Schedule();
            sche.M.MatchID = reader.GetInt32("match_id");
            sche.T.TeamName = reader.GetString("team_name");
            sche.TeamAway = reader.GetString("team_away");
            sche.M.MatchDay = reader.GetString("match_date");
            sche.M.MatchTime = reader.GetString("match_time");
            sche.T.St.StadiumName = reader.GetString("st_name");
            return sche;
        }
    }
}