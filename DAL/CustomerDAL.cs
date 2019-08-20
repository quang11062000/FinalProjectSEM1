using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;
namespace DAL
{
    public class CustomerDAL
    {
        private static MySqlDataReader reader;
        private MySqlConnection connection;
        public CustomerDAL()
        {
            connection = DBHelper.OpenConnection();
        }

        private Customer GetCustomerInfo(MySqlDataReader reader)
        {
            Customer cs = new Customer();
            cs.Id = reader.GetInt32("cus_id");
            cs.Username = reader.GetString("cus_username");
            cs.Pass = reader.GetString("cus_password");
            cs.CusName = reader.GetString("cus_name");
            cs.CusAddress = reader.GetString("cus_address");
            cs.CusPhone = reader.GetString("cus_phone");
            return cs;
        }
        public Customer GetUserByUsernameAndPass(string usname, string pw)
        {
            // try
            // {
                string query = @"select * from customers where cus_username = '" + usname + "'and cus_password = '" + pw + "';";
                MySqlCommand command = new MySqlCommand(query, connection);
                reader = command.ExecuteReader();
                Customer cs = null;
                if (reader.Read())
                {
                    cs = GetCustomerInfo(reader);
                }
                connection.Close();
                // DBHelper.CloseConnection();
                return cs;
            // }
            // catch (System.Exception)
            // {
            //     return null;
            // }
        }
    }
}