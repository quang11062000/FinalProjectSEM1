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
        public List<Schedule> Display()
        {
            try
            {
                String querry = @"select m.match_id,t.team_name,md.team_away,m.match_date, m.match_time, s.st_name
                                from matches m inner join matchdetails md on m.match_id = md.match_id inner join teams t on t.team_id = md.team_id 
                                inner join stadiums s on s.st_id = t.st_id;";
                Dbhelper.OpenConnection();
                reader = Dbhelper.ExecuteQuerry(querry);

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
            Dbhelper.CloseConnection();
            return listsche;

        }
        public Schedule GetSchedule(MySqlDataReader reader)
        {
            Schedule sche = new Schedule();
            try
            {
               sche.
            }
            catch (System.Exception)
            {

                throw;
            }
            return get;
        }
    }
}