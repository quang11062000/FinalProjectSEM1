using System;
using Xunit;
using DAL;
using Persistence;
namespace DAL_test
{
    public class CustomerTest
    {
        private CustomerDAL customerDAL = new CustomerDAL();
        [Theory]
        [InlineData("customer01", "123456")]
        [InlineData("customer02", "234567")]
        public void GetUserByUsernameAndPassTest1(string usname, string pass)
        {
            Customer cus = customerDAL.GetUserByUsernameAndPass(usname, pass);
            Assert.NotNull(usname);
            Assert.Equal(usname, cus.Username);
        }
        [Theory]
        [InlineData("customer_01", "123456789")]
        public void GetUserByUsernameAndPassTest2(string usname, string pass)
        {
            Assert.Null(customerDAL.GetUserByUsernameAndPass(usname,pass));
        }
    }
}