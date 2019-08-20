using System;
using Xunit;
using DAL;
namespace DAL_test
{
    public class DBHelperTest
    {
        [Fact]
        public void OpenConnectionTest()
        {
            Assert.NotNull(DBHelper.OpenConnection());
        }
    }
}
