using System;
using Xunit;
using DAL;
using Persistence;
using System.Collections.Generic;
namespace DAL_test
{
    public class TicketDALTest
    {
        TicketDAL ticketDAL = new TicketDAL();
        [Fact]
        public void GetTicketByMatchIDTest1()
        {
            List<Ticket> listticket = ticketDAL.GetTicketByMatchID(1);
            Assert.NotNull(listticket);
        }
        [Fact]
        public void GetTicketByMatchIDTest2()
        {
            Assert.Equal(new List<Ticket>(), ticketDAL.GetTicketByMatchID(0));
        }
    }
}