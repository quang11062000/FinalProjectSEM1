using System.Collections.Generic;
namespace persistence
{
    public class OrderDetail
    {
        private Order order;
        private Tickets t;
        private string description;
        public OrderDetail() { }
        public OrderDetail(Order order, Tickets t, string Description)
        {
            Order = new Order();
            T = new Tickets();
            this.Description = Description;
        }

        public Order Order { get => order; set => order = value; }
        public Tickets T { get => t; set => t = value; }
        public string Description { get => description; set => description = value; }
    }
}