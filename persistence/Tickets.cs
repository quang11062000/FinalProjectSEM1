using System;

namespace Persistence
{
    public class Tickets
    {
        private int ticketID;
        private int matchID;
        private string ticketType;
        private double ticketPrice;
        private int amount;
        public Tickets() { }
        public Tickets(int ticketID, int matchID, string ticketType, double ticketPrice, int amount)
        {
            this.TicketID = ticketID;
            this.matchID = matchID;
            this.TicketType = ticketType;
            this.TicketPrice = ticketPrice;
            this.Amount = amount;
        }

        public int TicketID { get => ticketID; set => ticketID = value; }
        public string TicketType { get => ticketType; set => ticketType = value; }
        public double TicketPrice { get => ticketPrice; set => ticketPrice = value; }
        public int Amount { get => amount; set => amount = value; }
        public int MatchID { get => matchID; set => matchID = value; }
    }
}