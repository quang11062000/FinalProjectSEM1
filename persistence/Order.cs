using System;
using System.Collections.Generic;
namespace Persistence
{
    public class Order
    {
        private int orderID;
        private Customers cs;
        private string orderDate;
        private List<Tickets> listticket;

        public Order()
        {
            cs = new Customers();
            listticket = new List<Tickets>();
        }

        public Order(int orderID, Customers cs, string orderDate)
        {
            this.orderID = orderID;
            this.cs = cs;
            this.orderDate = orderDate;
        }

        public int OrderID { get => orderID; set => orderID = value; }
        public string OrderDate { get => orderDate; set => orderDate = value; }
        public Customers Cs { get => cs; set => cs = value; }
        public List<Tickets> Listticket { get => listticket; set => listticket = value; }
    }
}