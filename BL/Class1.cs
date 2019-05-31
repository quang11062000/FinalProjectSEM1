using System;
using System.Collections.Generic;
using DAL;
using persistence;

namespace BL
{
    public class Class1
    {
       private UserNameDAL DAL;
       public Class1(){
           DAL = new UserNameDAL();
       }
       public List<Customers> GetUsernameandPass(string UserName, string Password){
           return DAL.GetUser(UserName,Password);
       }
    }
}
