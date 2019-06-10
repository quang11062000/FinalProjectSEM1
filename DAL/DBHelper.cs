using System;
using MySql.Data.MySqlClient;
namespace DAL
{
    public class DBHelper
    {
        private static MySqlConnection connection;
        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection
                {
                    ConnectionString = @"server=localhost;user id=root;
                    port=3306;password=11062000;database=footballclubtickets"
                };
            }
            return connection;
        }
        public static MySqlConnection OpenConnection()
        {
            if (connection == null)
            {
                GetConnection();
            }
            connection.Open();
            return connection;
        }
        public static void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
        public static MySqlDataReader ExecuteQuery(string query)
        {
           MySqlCommand cm = new MySqlCommand(query,connection);
           return cm.ExecuteReader();
        }
    }
}
