using System;
using Persistence;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace DAL
{
    public class OrderDAL
    {
        private static MySqlTransaction trans;
        private static MySqlConnection connection;
        private static MySqlDataReader reader;
        private static string query;
        public OrderDAL()
        {
            connection = DBHelper.OpenConnection();
        }
        public bool CreateOrder(Order order)
        {
            if (order == null || order.listticket == null || order.listticket.Count == 0)
            {
                return false;
            }
            bool result = false;
            MySqlConnection connection = DBHelper.OpenConnection();
            MySqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            try
            {
                command.CommandText = "lock tables orders write,orderdetails write,tickets write";
                command.ExecuteNonQuery();
                trans = connection.BeginTransaction();
                command.Transaction = trans;
                MySqlDataReader reader = null;
                command.CommandText = "insert into orders(cus_id,order_date,order_status) values(@customerId,@orderDate,@orderStatus)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@customerId", order.Customer.Id);
                command.Parameters.AddWithValue("@orderDate", order.OrderDate);
                command.Parameters.AddWithValue("@orderStatus", order.OrderStatus);
                command.ExecuteNonQuery();
                command.CommandText = "select LAST_INSERT_ID() as order_id";
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    order.OrderID = reader.GetInt32("order_id");
                }
                reader.Close();
                foreach (var item in order.listticket)
                {
                    if (item.TicketID == 0 || item.Amount <= 0)
                    {
                        throw new Exception("Not exits item");
                    }
                    command.CommandText = "select tkt.tickettype_price from tickets t inner join typeoftickets tkt on t.ticket_type = tkt.tickettype_name where ticket_id = @TicketID";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@TicketID", item.TicketID);
                    reader = command.ExecuteReader();
                    if (!reader.Read())
                    {
                        throw new Exception("Not exits item");
                    }
                    item.TicketPrice = reader.GetDouble("tickettype_price");
                    reader.Close();
                    command.CommandText = @"insert into orderdetails(order_id,ticket_id,amount,unit_price) values
                                       (" + order.OrderID + "," + item.TicketID + "," + item.Amount + "," + item.TicketPrice + ")";
                    command.ExecuteNonQuery();
                    command.CommandText = @"update tickets set  ticket_amount = ticket_amount -@quantity 
                                       where ticket_id = " + item.TicketID + ";";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@quantity", item.Amount);
                    command.ExecuteNonQuery();
                }
                trans.Commit();
                result = true;
            }
            catch (Exception)
            {
                // Console.WriteLine(e.Message);
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
                connection.Close();
                // DBHelper.CloseConnection();
            }
            return result;
        }
        public int GetSumAmountTicketBoughtByMatchID(int cusID, int matchID)
        {
            MySqlDataReader reader = null;
            int sumAmount = 0;
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
                query = @"select sum(amount) as sumAmount from customers cs inner join orders o on cs.cus_id = o.cus_id
                           inner join orderdetails od on od.order_id = o.order_id inner join tickets t
                           on t.ticket_id = od.ticket_id where cs.cus_id = " + cusID + " and t.match_id = " + matchID + ";";
                MySqlCommand command = new MySqlCommand(query, connection);
                reader = command.ExecuteReader();
                if (reader != null)
                {
                    reader.Read();
                    sumAmount = reader.GetInt32("sumAmount");
                }
                reader.Close();
                connection.Close();
                return sumAmount;
            }
            catch (System.Exception)
            {
                sumAmount = 0;
                return sumAmount;
            }
        }

        public bool DeleteOrder(int orderID)
        {
            try
            {
                string query;
                MySqlConnection connection = DBHelper.OpenConnection();
                MySqlCommand command = new MySqlCommand("", connection);
                query = @"Delete from orderdetails where order_id = @orderID;";
                command.Parameters.AddWithValue("@orderID", orderID);
                command.CommandText = query;
                command.ExecuteNonQuery();
                query = @"Delete from orders where order_id = @orderID;";
                // command.Parameters.AddWithValue("@orderID", orderID);
                command.CommandText = query;
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        public List<Order> GetOrdersByCustomerID(int customerID)
        {
            List<Order> orders = new List<Order>();
            query = @"select * from orders where cus_id = " + customerID + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            reader = command.ExecuteReader();
            if (reader != null)
            {
                orders = GetOrders(command);
            }
            reader.Close();
            connection.Close();
            return orders;
        }
        public List<Ticket> GetListTicketByOrderID(int orderID)
        {
            List<Ticket> listtk = new List<Ticket>();
            query = @"select od.ticket_id,t.ticket_type,od.amount,od.unit_price,m.match_name from customers cs inner join orders o on cs.cus_id = o.cus_id
                      inner join orderdetails od on o.order_id = od.order_id
                      inner join tickets t on t.ticket_id = od.ticket_id inner join matches m on m.match_id = t.match_id
                      where od.order_id =" + orderID + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            reader = command.ExecuteReader();
            if (reader != null)
            {
                listtk = GetListTicketInOrder(command);
            }
            reader.Close();
            connection.Close();
            return listtk;
        }
        public Order GetOrderInfoByOrderID(int orderID)
        {
            Order order = new Order();
            query = @"select *  from orders where order_id = " + orderID + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                order = GetOrderInfo(reader);
            }
            reader.Close();
            connection.Close();
            return order;
        }
        private Order GetOrderInfo(MySqlDataReader reader)
        {
            Order order = new Order();
            order.OrderID = reader.GetInt32("order_id");
            order.OrderDate = reader.GetDateTime("order_date");
            order.OrderStatus = reader.GetInt16("order_status");
            return order;
        }
        private Ticket GetTicketInfoInOrder(MySqlDataReader reader)
        {
            Ticket t = new Ticket();
            t.TicketID = reader.GetInt32("ticket_id");
            t.M.MatchName = reader.GetString("match_name");
            t.Amount = reader.GetInt32("amount");
            t.TicketType = reader.GetString("ticket_type");
            t.TicketPrice = reader.GetDouble("unit_price");
            return t;
        }
        private List<Ticket> GetListTicketInOrder(MySqlCommand command)
        {
            List<Ticket> listt = new List<Ticket>();
            while (reader.Read())
            {
                Ticket t = GetTicketInfoInOrder(reader);
                listt.Add(t);
            }
            connection.Close();
            return listt;
        }
        private List<Order> GetOrders(MySqlCommand command)
        {
            List<Order> listt = new List<Order>();
            while (reader.Read())
            {
                Order t = GetOrderInfo(reader);
                listt.Add(t);
            }
            connection.Close();
            return listt;
        }
    }
}