using System;
using Persistence;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class OrderDAL
    {
        MySqlTransaction trans;
        public bool CreateOrder(Order order)
        {
            if (order == null || order.Listticket == null || order.Listticket.Count == 0)
            {
                return false;
            }
            bool result = false;
            MySqlConnection connection = DBHelper.OpenConnection();
            MySqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            try
            {
                command.CommandText = "lock tables orders write,orderdetails write,tickets write,numberticketsofmatch write";
                command.ExecuteNonQuery();
                trans = connection.BeginTransaction();
                command.Transaction = trans;
                MySqlDataReader reader = null;
                command.CommandText = "insert into orders(cus_id) values(@customerId)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@customerId",order.Cs.Id);
                command.ExecuteNonQuery();
                command.CommandText = "select LAST_INSERT_ID() as order_id";
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    order.OrderID = reader.GetInt32("order_id");
                }
                reader.Close();
                foreach (var item in order.Listticket)
                {
                    if (item.TicketType == null || item.Amount <= 0)
                    {
                        throw new Exception("Not exits item");
                    }
                    command.CommandText = "select ticket_price from tickets where ticket_type =@ticketType";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@TicketType", item.TicketType);
                    reader = command.ExecuteReader();
                    if (!reader.Read())
                    {
                        throw new Exception("Not exits item");
                    }
                    item.TicketPrice = reader.GetDouble("ticket_price");
                    reader.Close();
                    command.CommandText = @"insert into orderdetails(order_id,ticket_type,ticket_price,quantity) values
                                       (" + order.OrderID + "," + item.TicketType + "," + item.TicketPrice + "," + item.Amount + ")";
                    command.ExecuteNonQuery();
                    command.CommandText = @"update numbersticketsofmatch set amountticket =amountticket -@quantity 
                                       where ticket_type = " + item.TicketType + ";";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@quantity", item.Amount);
                    command.ExecuteNonQuery();
                }
                trans.Commit();
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = false;
                try
                {
                    trans.Rollback();
                }
                catch (System.Exception)
                {
                }
            }
            finally
            {
                command.CommandText = "unlock tables;";
                command.ExecuteNonQuery();
                DBHelper.CloseConnection();
            }
            return result;
        }
    }
}