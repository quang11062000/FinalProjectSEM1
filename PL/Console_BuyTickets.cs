using System;
using System.Collections.Generic;
using BL;
using persistence;

namespace PL
{
    public class Console_BuyTickets
    {
        public void Display()
        {
            ScheduleBL schebl = new ScheduleBL();
            List<Schedule> list = schebl.DisplaySchedule();
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("|{0,-10}|{1,-50}|{2,-20}|{3,-15}|{4,-10}|", "Match_id", "Match_name", "Match_day", "Match_time", "Stadium");
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
                    foreach (var item1 in list)
                    {
                        Console.WriteLine("|{0,-10}|{1,-50}|{2,-20}|{3,-15}|{4,-10}|", item1.M.MatchID, string.Concat(item1.T.TeamName, ' ', "vs", ' ', item1.TeamAway), item1.M.MatchDay.Substring(0,9), item1.M.MatchTime, item1.T.St.StadiumName);
                    }
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
        }
        public void DisplayNumberTicketofMatch()
        {
            NumbersTicketofMatchBL ntmbl = new NumbersTicketofMatchBL();
            List<NumbersTicketofMatch> listntm = ntmbl.GetListNumbersTicket();
            foreach (var item in listntm)
            {
                Console.WriteLine("{0}",item.M.MatchID);
                Console.WriteLine("{0}",item.T.TicketType);
                Console.WriteLine("{0}",item.Amount);
            }
        }

    }
}