using System;
namespace persistence
{
    public class NumbersTicketofMatch
    {
        private Matches m;
        private Tickets t;
        private int amount;

        public Matches M { get => m; set => m = value; }
        public Tickets T { get => t; set => t = value; }
        public int Amount { get => amount; set => amount = value; }

        public NumbersTicketofMatch()
        {
             m = new Matches();
             t = new Tickets();
        }

        public NumbersTicketofMatch(Matches m, Tickets t, int amount)
        {
            this.M = m;
            this.T = t;
            this.Amount = amount;
        }
    }
}