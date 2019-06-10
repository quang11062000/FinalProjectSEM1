using System;
using System.Collections.Generic;

namespace Persistence
{
    public class OrderDetail
    {
        private Order order;
        private Tickets t;

        public OrderDetail()
        {
            order = new Order();
            T = new Tickets();
        }

        public Order Order { get => order; set => order = value; }
        public Tickets T { get => t; set => t = value; }
    }
}