using System;
using DAL;
using Persistence;
using System.Collections.Generic;
namespace BL
{
    public class CustomerBL
    {
        private CustomerDAL customerDAL;
        public CustomerBL()
        {
            customerDAL = new CustomerDAL();
        }
        public Customer GetUserByUsernameAndPass(string usname,string pw)
        {
            
            return customerDAL.GetUserByUsernameAndPass(usname,pw);
        }
    }
}