using System;
using Xunit;
using BL;
using Persistence;
using System.Collections.Generic;
namespace DAL_test
{
    public class OrderUnitTest
    {
        [Fact]
        public void CreateOrderTest()
        {
            OrderBL orderBL = new OrderBL();
            DateTime dateTime = new DateTime(2019, 12, 22);
            Order order = new Order(1, null, dateTime, 10);
            Assert.False(orderBL.CreateOrder(order));
        }
        [Fact]
        public void CreateOrderTest2()
        {
            OrderBL orderBL = new OrderBL();
            DateTime dateTime = new DateTime(2019, 1, 8);
            Order order = new Order(2, null, dateTime, 13);
            Assert.False(orderBL.CreateOrder(order));
        }
        [Fact]
        public void GetListTicketByOrderIDTest1()
        {
            OrderBL orderBL = new OrderBL();
            Assert.NotNull(orderBL.GetListTicketByOrderID(1));
        }
        [Fact]
        public void GetListTicketByOrderIDTest2()
        {
            OrderBL orderBL = new OrderBL();
            Assert.NotNull(orderBL.GetListTicketByOrderID(3));
        }
        [Fact]
        public void GetListTicketByOrderIDTest3()
        {
            OrderBL orderBL = new OrderBL();
            Assert.NotNull(orderBL.GetListTicketByOrderID(5));
        }
        [Fact]
        public void GetOrderInfoByOrderIDTest()
        {
            OrderBL orderBL = new OrderBL();
            Order test = orderBL.GetOrderInfoByOrderID(3);
            Assert.NotNull(test);
        }
        [Fact]
         public void GetSumAmountTicketBoughtByMatchID()
         {
             OrderBL orderBL = new OrderBL();
             Assert.Equal(0,orderBL.GetSumAmountTicketBoughtByMatchID(0,0));
         }

    }
}