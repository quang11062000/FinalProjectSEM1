using System;
using System.Collections.Generic;
namespace Persistence
{
    public class Order
    {
        private int orderID;
        private Customer customer;
        // private int customerID;
        private DateTime orderDate;
        private int orderStatus;
        public List<Ticket> listticket{get;set;}

        public Order()
        {
            customer = new Customer();
            listticket = new List<Ticket>();
        }
        public Order(int orderID, Customer customer, DateTime orderDate, int orderStatus)
        {
            this.orderID = orderID;
            this.customer = customer;
            this.orderDate = orderDate;
            this.orderStatus = orderStatus;
        }

        public int OrderID { get => orderID; set => orderID = value; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get => customer; set => customer = value; }
        // public int CustomerID { get => customerID; set => customerID = value; }
        public int OrderStatus { get => orderStatus; set => orderStatus = value; }
    }
}