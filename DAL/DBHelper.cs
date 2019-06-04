using MySql.Data.MySqlClient;
class DBHelper
{
    private static MySqlConnection connection;
    public static MySqlConnection GetConnection()
    {
        if (connection == null)
        {
            connection = new MySqlConnection
            {
                ConnectionString = @"server = localhost;
                                 userid = root;
                                password = dong2k;
                                 port = 3306;
                                 database = footballclubtickets;"
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
    public static MySqlConnection CloseConnection()
    {
        if (connection != null)
        {
            connection.Close();
        }
        return connection;
    }
    public static MySqlDataReader ExecuteQuerry(string Querry)
    {
        MySqlCommand command = new MySqlCommand(Querry, connection);
        return command.ExecuteReader();
    }
}