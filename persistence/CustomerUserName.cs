
using System.Collections.Generic;

namespace persistence
{
    public class CustomerUserName
    {
        public string CustomerId{get;set;}
        public string UserName{get;set;}
        public string Password{get;set;}
        public string CustomerName{get;set;}
        public string CustomerPhone{get;set;}
        public CustomerUserName(){

        }
        public CustomerUserName(string CustomerId, string UserName,string Password, string CustomerName,string CustomerPhone){
            this.CustomerId = CustomerId;
            this.UserName = UserName;
            this.Password = Password;
            this.CustomerName = CustomerName;
            this.CustomerPhone = CustomerPhone;
        }

    }
}
