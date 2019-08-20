using System;
using System.Collections.Generic;
using DAL;
using Persistence;
namespace BL
{
    public class TicketBL
    {
        private TicketDAL ticketDAL;
        public TicketBL()
        {
            ticketDAL = new TicketDAL();
        }
        public List<Ticket> GetListTicketByMatchID(int matchID)
        {
           return ticketDAL.GetTicketByMatchID(matchID);
        }
    }
}