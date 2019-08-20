using System;
using DAL;
using System.Collections.Generic;
using Persistence;
namespace BL
{
    public class OrderBL
    {
        private OrderDAL orderDAL;
        public OrderBL()
        {
            orderDAL = new OrderDAL();
        }
        public bool CreateOrder(Order order)
        {
            return orderDAL.CreateOrder(order);
        }
        public int GetSumAmountTicketBoughtByMatchID(int cusID, int matchID)
        {
            return orderDAL.GetSumAmountTicketBoughtByMatchID(cusID, matchID);
        }
        public List<Ticket> GetListTicketByOrderID(int orderID)
        {
            return orderDAL.GetListTicketByOrderID(orderID);
        }
        public Order GetOrderInfoByOrderID(int orderID)
        {
            return orderDAL.GetOrderInfoByOrderID(orderID);
        }
        public List<Order> GetOrdersByCustomerID(int customerID)
        {
            return orderDAL.GetOrdersByCustomerID(customerID);
        }
    }
}