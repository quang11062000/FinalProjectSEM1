using System;
using DAL;
using persistence;
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
        public Customers LoginWithUserandPass(string usname,string pw)
        {
            return csdal.GetCustomerbyUserNameandPass(usname,pw);
        }
    }
}