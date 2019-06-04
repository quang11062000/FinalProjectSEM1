using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BL;
using persistence;

namespace PL
{
    public class Console_BuyTickets
    {
        NumbersTicketofMatchBL ntmbl = new NumbersTicketofMatchBL();
        List<NumbersTicketofMatch> listntm = new List<NumbersTicketofMatch>();
        ScheduleBL schebl = new ScheduleBL();
        List<Schedule> list = new List<Schedule>();
        Menus m = new Menus();
        NumbersTicketofMatch ntm = new NumbersTicketofMatch();
        public void DisplaySchedule(Customers cs)
        {
            string choice;
            bool check = false;
            try
            {
                list = schebl.DisplaySchedule();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|{0,-10}|{1,-50}|{2,-20}|{3,-15}|{4,-10}|", "Match_id", "Match_name", "Match_day", "Match_time", "Stadium");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            foreach (var item in list)
            {
                Console.WriteLine("|{0,-10}|{1,-50}|{2,-20}|{3,-15}|{4,-10}|", item.M.MatchID, string.Concat(item.T.TeamName, ' ', "vs", ' ', item.TeamAway), item.M.MatchDay.Substring(0, 9), item.M.MatchTime, item.T.St.StadiumName);
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            while (check == false)
            {
                Console.Write("input matchID: ");
                ntm.M.MatchID = Input(Console.ReadLine());
                try
                {
                    listntm = ntmbl.GetListNumbersTicket(ntm.M.MatchID);
                    foreach (var item in listntm)
                    {
                        if (ntm.M.MatchID == item.M.MatchID)
                        {
                            check = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                if (check == false)
                {
                    Console.WriteLine("Ma tran khong dung!Ban co muon nhap lai khong(Y/N)? ");
                    choice = Console.ReadLine().ToUpper();
                    while (true)
                    {
                        if (choice != "Y" && choice != "N")
                        {
                            Console.Write("Bạn chỉ được nhập (Y/N): ");
                            choice = Console.ReadLine().ToUpper();
                            continue;
                        }
                        break;
                    }
                    switch (choice)
                    {
                        case "Y":
                            continue;
                        case "y":
                            continue;
                        case "N":
                            m.MenuTicket(cs);
                            break;
                        case "n":
                            m.MenuTicket(cs);
                            break;
                        default:
                            continue;
                    }
                }
            }
            if (listntm != null)
            {
                Console.WriteLine("+-----------------------------------------------------+");
                Console.WriteLine("|{0,-10}|{1,-15}|{2,-15}|{3,-10}|", "Match_id", "Ticket_type", "Ticket_price", "Amount");
                Console.WriteLine("-------------------------------------------------------");
                foreach (var item in listntm)
                {
                    Console.WriteLine("|{0,-10}|{1,-15}|{2,-15}|{3,-10}|", item.M.MatchID, item.T.TicketType, string.Concat(item.T.TicketPrice, ' ', "VND"), item.Amount);
                }
                Console.WriteLine("+-----------------------------------------------------+");
            }
        }
        public int Input(string str)
        {
            Regex regex = new Regex("[0-9]");
            MatchCollection mc = regex.Matches(str);
            while ((mc.Count < str.Length) || (str == ""))
            {
                Console.Write("Ma tran nhap vao phai la so tu nhien,moi ban nhap lai: ");
                str = Console.ReadLine();
                mc = regex.Matches(str);
            }
            return Convert.ToInt32(str);
        }
        public bool BuyTicket(Customers cs)
        {
            DisplaySchedule(cs);
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