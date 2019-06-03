using System;
using System.Collections.Generic;
using DAL;
using persistence;

namespace BL
{
    public class NumbersTicketofMatchBL
    {
        private NumbersTicketofMatchDAL ntmdal;
        public NumbersTicketofMatchBL()
        {
            ntmdal = new NumbersTicketofMatchDAL();
        }
        public List<NumbersTicketofMatch> GetListNumbersTicket()
        {
            return ntmdal.GetListNumbersTicketofMatch();
        }

    }    
}