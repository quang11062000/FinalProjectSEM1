using System;
using Xunit;
using DAL;
using Persistence;
using System.Collections.Generic;

namespace DAL_test
{
    public class OrderUnitTest
    {
        private OrderDAL orderDAL = new OrderDAL();
        [Fact]
        public void CreateOrderTest()
        {
            DateTime dateTime = new DateTime(2019, 12, 22);
            Order orderTest = new Order(0, null, dateTime, 0);
            Assert.False(orderDAL.CreateOrder(orderTest));
        }
        [Fact]
        public void CreateOrderTest2()
        {
            Ticket ticket = new Ticket(1, null, "A", 70000, 1);
            Order ordertest1 = new Order();
            ordertest1.listticket.Add(ticket);
            ordertest1.Customer.Id = 1;
            ordertest1.OrderDate = DateTime.Now;
            ordertest1.OrderStatus = 0;
            Assert.True(orderDAL.CreateOrder(ordertest1));
            Assert.True(orderDAL.DeleteOrder(ordertest1.OrderID));
        }
        [Fact]
        public void GetListTicketByOrderIDTest1()
        {
            List<Ticket> listticket = orderDAL.GetListTicketByOrderID(1);
            Assert.NotNull(listticket);
        }
        [Fact]
        public void GetListTicketByOrderIDTest2()
        {
            List<Ticket> listticket = orderDAL.GetListTicketByOrderID(4);
            Assert.NotNull(listticket);
        }
        [Fact]
        public void GetListTicketByOrderIDTest3()
        {
            List<Ticket> listticket = orderDAL.GetListTicketByOrderID(6);
            Assert.NotNull(listticket);
        }
            
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetOrderInfoByOrderIDTest(int orderID)
        {
            Order test = orderDAL.GetOrderInfoByOrderID(orderID);
            Assert.NotNull(test);
        }
        [Theory]
        [InlineData(1, 3)] 
        [InlineData(2, 4)]
        public void GetSumAmountTicketBoughtByMatchID(int cusID, int matchID)
        {
            int orderTicket = orderDAL.GetSumAmountTicketBoughtByMatchID(cusID, matchID);
            Assert.NotEqual(orderTicket, cusID);
        }

    }
}