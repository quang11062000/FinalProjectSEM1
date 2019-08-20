using System;
using Xunit;
using BL;
using Persistence;

namespace BL_test
{
    public class CustomerTest
    {
        [Fact]
        public void LoginTest1()
        {
            CustomerBL userBL = new CustomerBL();
            Assert.NotNull(userBL.GetUserByUsernameAndPass("customer01", "123456"));
        }

        [Fact]
        public void LoginTest3()
        {
            CustomerBL userBL = new CustomerBL();
            Assert.NotNull(userBL.GetUserByUsernameAndPass("customer02", "234567"));
        }
        [Fact]
        public void LoginTest4()
        {
            CustomerBL userBL = new CustomerBL();
            Customer cs = null;
            Assert.Equal(cs,userBL.GetUserByUsernameAndPass("customer02", "2345677897"));
        }
    }
}