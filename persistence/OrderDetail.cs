using System.Collections.Generic;
namespace persistence
{
    public class OrderDetail
    {
        private int orderID;
        private Tickets t;
        private string description;
        public OrderDetail() { }
        public OrderDetail(string OrderID, Tickets t, string Description)
        {
            this.OrderID = orderID;
            T = new Tickets();
            this.Description = description;

        }
        public Tickets T { get => t; set => t = value; }
        public string Description { get => description; set => description = value; }
        public int OrderID { get => orderID; set => orderID = value; }
    }
}