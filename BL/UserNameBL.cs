using System;
using DAL;
using persistence;
using System.Collections.Generic;
namespace BL
{
    public class UserNameBL
    {
        private UserNameDAL csdal;
        public UserNameBL()
        {
            csdal = new UserNameDAL();
        }
        public Customers Login(string usname,string pw)
        {
            return csdal.Login(usname,pw);
        }
    }
}