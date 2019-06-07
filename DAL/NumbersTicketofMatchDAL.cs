using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using persistence;
namespace DAL
{
    public class NumbersTicketofMatchDAL
    {
        private static MySqlDataReader reader;
        List<NumbersTicketofMatch> listntm = new List<NumbersTicketofMatch>();
        private NumbersTicketofMatch GetTickets(MySqlDataReader reader)
        {
            NumbersTicketofMatch ntm = new NumbersTicketofMatch();
            ntm.M.MatchID = reader.GetInt32("match_id");
            ntm.T.TicketType = reader.GetString("ticket_type");
            ntm.Amount = reader.GetInt32("amountticket");
            ntm.T.TicketPrice = reader.GetDouble("ticket_price");
            return ntm;
        }
        public List<NumbersTicketofMatch> GetListNumbersTicketofMatch(int MatchID)
        {

            string query = @"select m.match_id,t.ticket_type,tm.amountticket,t.ticket_price from matches m inner join numberticketsofmatch tm on m.match_id = tm.match_id
                             inner join tickets t on tm.ticket_type = t.ticket_type where m.match_id  = " + MatchID + ";";
            DBHelper.OpenConnection();
            reader = DBHelper.ExecuteQuerry(query);
            NumbersTicketofMatch ntm = null;
            while (reader.Read())
            {
                ntm = GetTickets(reader);
                listntm.Add(ntm);
            }
            DBHelper.CloseConnection();
            return listntm;
        }
        public bool CreatCart(Order Order)
        {
            bool result = true;
            if (Order == null || Order.OrderDetail == null || Order.OrderDetail.Count == 0)
            {
                result = false;
            }
            MySqlConnection connection = DBHelper.OpenConnection();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.Connection = connection;
            cmd.CommandText = "lock tables  orders, numberticketsofmatch, orderdetails;";
            cmd.ExecuteNonQuery();
            MySqlTransaction trans = connection.BeginTransaction();
            cmd.Transaction = trans;
            try
            {
               
            }
            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

    }
}
