using System;
using DAL;
using Persistence;
using System.Collections.Generic;
namespace BL
{
    public class CustomerBL
    {
        private CustomerDAL csdal;
        public CustomerBL()
        {
            csdal = new CustomerDAL();
        }
        public Customers Login(string usname,string pw)
        {
            return csdal.Login(usname,pw);
        }
    }
}