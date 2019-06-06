
using System.Collections.Generic;

namespace persistence
{
    public class Customers
    {
        public int CustomerId{get;set;}
        public string UserName{get;set;}
        public string Password{get;set;}
        public string CustomerName{get;set;}
        public string CustomerPhone{get;set;}
        public Customers(){

        }
        public Customers(int CustomerId, string UserName,string Password, string CustomerName,string CustomerPhone){
            this.CustomerId = CustomerId;
            this.UserName = UserName;
            this.Password = Password;
            this.CustomerName = CustomerName;
            this.CustomerPhone = CustomerPhone;
        }

    }
}
