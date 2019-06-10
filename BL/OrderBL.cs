using System;
using DAL;
using Persistence;
namespace BL
{
    public class OrderBL
    {
        private OrderDAL odal;
        public OrderBL()
        {
            odal = new OrderDAL();
        }
        public bool CreateOrder(Order order)
        {
            return odal.CreateOrder(order);
        }
    }
}