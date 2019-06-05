using System;
namespace persistence
{
    public class Tickets
    {
       
        private string ticketType;
        private double ticketPrice;
        public string TicketType { get => ticketType; set => ticketType = value; }
        public double TicketPrice { get => ticketPrice; set => ticketPrice = value; }
        public Tickets() { }
        public Tickets(int ticketID, string ticketType, double ticketPrice)
        {
            this.TicketType = ticketType;
            this.TicketPrice = ticketPrice;
        }

    }
}