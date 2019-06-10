using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;
namespace DAL
{
    public class CustomerDAL
    {
        private static MySqlDataReader reader;

        private  Customers GetACC(MySqlDataReader reader)
        {
            Customers cs = new Customers();
            cs.Id = reader.GetInt32("cus_id");
            cs.Username = reader.GetString("cus_username");
            cs.Pass = reader.GetString("cus_password");
            cs.CusName = reader.GetString("cus_name");
            cs.CusPhone = reader.GetString("cus_phone");
            return cs;
        }
        public Customers Login(string usname, string pw)
        {
            string query = @"select * from customers where cus_username = '" + usname + "'and cus_password = '" + pw + "';";
            DBHelper.OpenConnection();
            reader = DBHelper.ExecuteQuery(query);
            Customers cs = null;
            if (reader.Read())
            {
                cs = GetACC(reader);
            }
            DBHelper.CloseConnection();
            return cs;
        }
    }
}