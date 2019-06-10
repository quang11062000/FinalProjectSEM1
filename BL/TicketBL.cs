using System;
using System.Collections.Generic;
using DAL;
using Persistence;
namespace BL
{
    public class TicketBL
    {
        private TicketDAL tkdal;
        public TicketBL()
        {
            tkdal = new TicketDAL();
        }
        public List<Tickets> GetListTicketByMatchID(int matchID)
        {
           return tkdal.GetTicketByMatchID(matchID);
        }
    }
}