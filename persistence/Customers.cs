using System;
namespace Persistence
{
     public class Customers
     {
        private int id;
        private string username;
        private string pass;
        private string cusName;
        private string cusPhone;

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Pass { get => pass; set => pass = value; }
        public string CusName { get => cusName; set => cusName = value; }
        public string CusPhone { get => cusPhone; set => cusPhone = value; }

        public Customers()
        {   
        }

        public Customers(int id, string username, string pass, string cusName, string cusPhone)
        {
            this.id = id;
            this.username = username;
            this.pass = pass;
            this.cusName = cusName;
            this.cusPhone = cusPhone;
        }
    }
}