using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DAL;
using persistence;
namespace DAL
{
    public class CustomerDAL
    {
        private static MySqlDataReader reader;


        private static Customers GetACC(MySqlDataReader reader)
        {
            Customers cs = new Customers();
            cs.UserName = reader.GetString("cus_username");
            cs.Password = reader.GetString("cus_password");
            return cs;
        }
        public Customers Login(string usname, string pw)
        {
            Customers cs = null;
            try
            {
                string query = @"select * from customers where cus_username = '" + usname + "'and cus_password = '" + pw + "';";
                DBHelper.OpenConnection();
                reader = DBHelper.ExecuteQuerry(query);

                if (reader.Read())
                {
                    cs = GetACC(reader);
                }
               DBHelper.CloseConnection();

            }
            catch (System.Exception)
            {
                cs = null;
            }
            return cs;
        }
    }
}

