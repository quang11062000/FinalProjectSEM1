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
        public List<NumbersTicketofMatch> GetListNumbersTicket(int Match_id)
        {
            return ntmdal.GetListNumbersTicketofMatch(Match_id);
        }

    }    
}