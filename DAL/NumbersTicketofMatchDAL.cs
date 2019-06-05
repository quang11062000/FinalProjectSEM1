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
        private static NumbersTicketofMatch GetDetails(MySqlDataReader reader)
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

            string query = @"select m.match_id,t.ticket_type,t.ticket_price,tm.amountticket from matches m inner join numberticketsofmatch tm on
                             m.match_id = tm.match_id inner join tickets t.ticket_type on tm.ticket_type = t. where m.match_id =" + MatchID + ";";
            DBHelper.OpenConnection();
            reader = DBHelper.ExecuteQuerry(query);
            NumbersTicketofMatch ntm = null;
            while (reader.Read())
            {
                ntm = GetDetails(reader);
                listntm.Add(ntm);
            }
            DBHelper.CloseConnection();
            return listntm;
        }
    }
}