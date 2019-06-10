using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;
namespace DAL
{
    public class TicketDAL
    {
        private static string querry;
        private MySqlConnection connection;
        private static MySqlDataReader reader;


        List<Tickets> listticket = new List<Tickets>();

        public TicketDAL()
        {
            connection = DBHelper.OpenConnection();
        }
        public List<Tickets> GetTicketByMatchID(int matchID)
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            querry = @"select * from tickets where match_id = " + matchID + ";";
            MySqlCommand command = new MySqlCommand(querry, connection);
            reader = command.ExecuteReader();
            if (reader != null)
            {
                listticket = GetListTicket(command);
            }
            reader.Close();
            DBHelper.CloseConnection();
            return listticket;
        }

        private Tickets GetTicketInfo(MySqlDataReader reader)
        {
            Tickets t = new Tickets();
            t.TicketID = reader.GetInt32("ticket_id");
            t.MatchID = reader.GetInt32("match_id");
            t.TicketType = reader.GetString("ticket_type");
            t.TicketPrice = reader.GetDouble("ticket_price");
            t.Amount = reader.GetInt32("ticket_amount");
            return t;
        }
        private List<Tickets> GetListTicket(MySqlCommand command)
        {
            List<Tickets> listt = new List<Tickets>();
            while (reader.Read())
            {
                Tickets t = GetTicketInfo(reader);
                listt.Add(t);
            }
            connection.Close();
            return listt;
        }

    }
}