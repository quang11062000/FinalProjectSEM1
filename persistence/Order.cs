using System.Collections.Generic;

namespace persistence
{
    public class Order
    {
        private int orderID;
        private Customers cus;
        private string orderDate;
        public Order(){
            
        }

        public Order(int orderID, Customers cus, string orderDate)
        {
            this.orderID = orderID;
            cus = new Customers();
            this.orderDate = orderDate;
        }

        public int OrderID { get => orderID; set => orderID = value; }
        public Customers Cus { get => cus; set => cus = value; }
        public string OrderDate { get => orderDate; set => orderDate = value; }
    }
}