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

        public TicketDAL()
        {
            connection = DBHelper.OpenConnection();
        }
        public List<Ticket> GetTicketByMatchID(int matchID)
        {
            List<Ticket> listticket = new List<Ticket>();
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            querry = @"select t.ticket_id,t.match_id,t.ticket_type,tkt.tickettype_price,t.ticket_amount from tickets t 
                         inner join typeoftickets tkt on t.ticket_type = tkt.tickettype_name where t.match_id = " + matchID + ";";
            MySqlCommand command = new MySqlCommand(querry, connection);
            reader = command.ExecuteReader();
            if (reader != null)
            {
                listticket = GetListTicket(command);
            }
            reader.Close();
            connection.Close();
            return listticket;
        }

        private Ticket GetTicketInfo(MySqlDataReader reader)
        {
            Ticket t = new Ticket();
            t.TicketID = reader.GetInt32("ticket_id");
            t.M.MatchID = reader.GetInt32("match_id");
            t.TicketType = reader.GetString("ticket_type");
            t.TicketPrice = reader.GetDouble("tickettype_price");
            t.Amount = reader.GetInt32("ticket_amount");
            return t;
        }
        private List<Ticket> GetListTicket(MySqlCommand command)
        {
            List<Ticket> listt = new List<Ticket>();
            while (reader.Read())
            {
                Ticket t = GetTicketInfo(reader);
                listt.Add(t);
            }
            connection.Close();
            return listt;
        }
    }
}