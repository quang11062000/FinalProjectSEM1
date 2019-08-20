using System;

namespace Persistence
{
    public class Ticket
    {
        private int ticketID;
        private Match m;
        private string ticketType;
        private double ticketPrice;
        private int amount;
        public Ticket()
        {
            m = new Match();
        }
        public Ticket(int ticketID, Match m, string ticketType, double ticketPrice, int amount)
        {
            this.TicketID = ticketID;
            this.M = m;
            this.TicketType = ticketType;
            this.TicketPrice = ticketPrice;
            this.Amount = amount;
        }

        public int TicketID { get => ticketID; set => ticketID = value; }
        public string TicketType { get => ticketType; set => ticketType = value; }
        public double TicketPrice { get => ticketPrice; set => ticketPrice = value; }
        public int Amount { get => amount; set => amount = value; }
        public Match M { get => m; set => m = value; }
    }
}