using persistence;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DAL
{
    public class UserNameDAL
    {
        private static MySqlDataReader reader;
        public List<Customers> GetUser(string UserName, string Password)
        {
            List<Customers> listUser = new List<Customers>();
            try
            {
                string querry = @"select cus_username,cus_password from customers; ";
                Dbhelper.OpenConnection();
                reader = Dbhelper.ExecuteQuerry(querry);
                Customers User = null;
                while (reader.Read())
                {
                    User = GetUserUP(reader);
                    listUser.Add(User);
                }

            }
            catch (System.Exception)
            {

                listUser = null;
            }
            Dbhelper.CloseConnection();
            return listUser;
        }
        private Customers GetUserUP(MySqlDataReader reader)
        {
            Customers c = new Customers();
            c.UserName = reader.GetString("cus_username");
            c.Password = reader.GetString("cus_password");
            // c.CustomerName = reader.GetString("cus_name");
            return c;
        }
    }
}
