using System;
using System.IO;
using MySql.Data.MySqlClient;
namespace DAL
{
    public class DBHelper
    {
        private static string Connection_String = @"server=localhost;user id=root;
                    port=3306;password=11062000;database=footballclubtickets;SslMode=None;";
        public static MySqlConnection OpenDefaultConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection
                {
                    ConnectionString = Connection_String
                };
                connection.Open();

                return connection;
            }
            catch
            {
                return null;
            }
        }
        public static MySqlConnection OpenConnection()
        {
            try
            {
                string connectionString;
                FileStream fileStream = File.OpenRead("ConnectionString.txt");
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    connectionString = reader.ReadLine();
                }
                fileStream.Close();
                return OpenConnection(connectionString);
            }
            catch
            {
                return null;
            }
        }
        public static MySqlConnection OpenConnection(string connectionString)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection
                {
                    ConnectionString = connectionString
                };
                connection.Open();
                return connection;
            }
            catch
            {
                return null;
            }
        }
        // public static void CloseConnection()
        // {
        //     if (connection != null)
        //     {
        //         connection.Close();
        //     }
        // }
        // public static MySqlDataReader ExecuteQuery(string query)
        // {
        //     MySqlCommand cm = new MySqlCommand(query, connection);
        //     return cm.ExecuteReader();
        // }
    }
}
