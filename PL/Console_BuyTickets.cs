using System;
using System.Collections.Generic;
using BL;
using persistence;

namespace PL
{
    public class Console_BuyTickets
    {
        NumbersTicketofMatchBL ntmbl = new NumbersTicketofMatchBL();
         List<NumbersTicketofMatch> listntm = new List<NumbersTicketofMatch>();
        public void Display()
        {
            ScheduleBL schebl = new ScheduleBL();
            List<Schedule> list = schebl.DisplaySchedule();
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|{0,-10}|{1,-50}|{2,-20}|{3,-15}|{4,-10}|", "Match_id", "Match_name", "Match_day", "Match_time", "Stadium");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            foreach (var item1 in list)
            {
                Console.WriteLine("|{0,-10}|{1,-50}|{2,-20}|{3,-15}|{4,-10}|", item1.M.MatchID, string.Concat(item1.T.TeamName, ' ', "vs", ' ', item1.TeamAway), item1.M.MatchDay.Substring(0, 9), item1.M.MatchTime, item1.T.St.StadiumName);
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
        }
        public void DisplayNumberTicketofMatch()
        {
            int flag = 0;
            
            while (true)
            {
                flag = 0;
                try
                {
                    Console.Write("input matchID: ");
                    int Mat_id = Convert.ToInt32(Console.ReadLine());
                    listntm = ntmbl.GetListNumbersTicket(Mat_id);
                    foreach (var item2 in listntm)
                    {
                        Console.WriteLine("----------------------------------------------------");
                        Console.WriteLine("|{0,-10}|{1,-15}|{2,-15}|{3,-10}|", "Match_id", "Ticket_type", "Ticket_price", "Amount");
                        Console.WriteLine("----------------------------------------------------");
                        if (Mat_id == item2.M.MatchID)
                        {
                            Console.WriteLine("|{0,-10}|{1,-15}|{2,-15}|{3,-10}|", item2.M.MatchID, item2.T.TicketType, string.Concat(item2.T.TicketPrice, ' ', "VND"), item2.Amount);
                            flag = 1;
                        }
                    }
                    Console.WriteLine("---------------------------------------------------------");
                }
                catch (Exception)
                {
                }
                if (flag == 1)
                {
                    break;
                }
                else if (flag == 0)
                {
                    Console.WriteLine("---------------------------------------------------------");
                    Console.WriteLine("Invalid Match ID");
                    Console.WriteLine("---------------------------------------------------------");
                    continue;
                }
            }
        }
        public bool BuyTicket(){
            Console.Write("input type of ticket : ");
            string TicketType = Console.ReadLine();
            foreach (var item in listntm)
            {
                if (TicketType == item.T.TicketType)
                {
                    Console.Write("Enter the number of tickets to buy:");
                    int BuyAmount = Convert.ToInt32(Console.ReadLine());
                }
            }
            return true;
        }
    }
}