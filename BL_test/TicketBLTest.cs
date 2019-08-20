using System;
using Xunit;
using BL;
using Persistence;
using System.Collections.Generic;
namespace DAL_test
{
    public class TicketBLTest{
        [Fact]
        public void TicketTest(){
            TicketBL ticketTest = new TicketBL();
            Assert.NotNull(ticketTest.GetListTicketByMatchID(1));
        }
        [Fact]
        public void TicketTest2(){
            TicketBL ticketTest = new TicketBL();
            Assert.NotNull(ticketTest.GetListTicketByMatchID(5));
        }
    }
}