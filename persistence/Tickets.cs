using System;
namespace persistence
{
    public class Tickets
    {
        private int ticketID;
        private string ticketType;
        private double ticketPrice;

        public int TicketID { get => ticketID; set => ticketID = value; }
        public string TicketType { get => ticketType; set => ticketType = value; }
        public double TicketPrice { get => ticketPrice; set => ticketPrice = value; }
        public Tickets() { }
        public Tickets(int ticketID, string ticketType, double ticketPrice)
        {
            this.TicketID = ticketID;
            this.TicketType = ticketType;
            this.TicketPrice = ticketPrice;
        }

    }
}