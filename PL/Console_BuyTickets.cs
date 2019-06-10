using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using BL;
using Newtonsoft.Json;
using Persistence;
namespace PL_console
{
    public class Consle_BuyTickets
    {
        Menus m = new Menus();

        public void DisplayMatchDetails(Customers cs)
        {
            Console.Clear();
            bool check = false;
            MatchDetailBL mdtbl = new MatchDetailBL();
            List<MatchDetails> listmdt = null;
            try
            {
                listmdt = mdtbl.GetListMatchDetail();
                check = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                check = false;
                Console.WriteLine("Nhan phim bat ky de tro ve menu chinh: ");
                Console.ReadKey();
                m.MenuTicket(cs);
            }
            if (check == true)
            {
                Console.WriteLine("+-------------------------------------------------------------------------------------------------------------+");
                Console.WriteLine("|{0,-10}|{1,-50}|{2,-20}|{3,-15}|{4,-10}|", "Match_id", "Match_name", "Match_day", "Match_time", "Stadium");
                Console.WriteLine("+-------------------------------------------------------------------------------------------------------------+");
                foreach (var item in listmdt)
                {
                    Console.WriteLine("|{0,-10}|{1,-50}|{2,-20}|{3,-15}|{4,-10}|", item.M.MatchID, string.Concat(item.T.TeamName, ' ', "vs", ' ', item.TeamAway), item.M.MatchDay.Substring(0, 9), item.M.MatchTime, item.T.St.StadiumName);
                }
                Console.WriteLine("+-------------------------------------------------------------------------------------------------------------+");
            }
        }
        public void BuyTciket(Customers cs)
        {
            string choice;
            Console.Write("Ban co muon mua tiep khong?");
            choice = Console.ReadLine().ToUpper();
            switch (choice)
            {
                case "Y":
                    CreateCart(cs);
                    break;
                case "y":
                    CreateCart(cs);
                    break;
                case "N":
                    m.MenuTicket(cs);
                    break;
                case "n":
                    m.MenuTicket(cs);
                    break;
            }
        }
        public void Getlist(Tickets t)
        {
            string sJSONResponse = JsonConvert.SerializeObject(t);
            BinaryWriter Bw;
            try
            {
                FileStream fs = new FileStream("Cart.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                Bw = new BinaryWriter(fs);
                Bw.Write((string)(object)sJSONResponse);
                fs.Close();
            }
            catch (System.Exception)
            {

                throw;
            }
            try
            {
                FileStream fs = new FileStream("Cart.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryReader br = new BinaryReader(fs);
                br.ReadString();

                br.Close();

            }
            catch (System.Exception)
            {

                throw;
            }
            Tickets NumberTicket = new Tickets();
            NumberTicket = JsonConvert.DeserializeObject<Tickets>(sJSONResponse);
        }
        public Tickets CreateCart(Customers cs)
        {
            string choice;
            DisplayMatchDetails(cs);
            TicketBL tkbl = new TicketBL();
            List<Tickets> listShowTicket = new List<Tickets>();
            Tickets t = new Tickets();
            bool check1 = false;
            bool check2 = false;
            bool check3 = false;
            while (check1 == false)
            {
                Console.Write("Nhap ma tran:");
                t.MatchID = Input(Console.ReadLine());
                try
                {
                    listShowTicket = tkbl.GetListTicketByMatchID(t.MatchID);
                    foreach (var item in listShowTicket)
                    {
                        if (t.MatchID == item.MatchID)
                        {
                            check1 = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                if (check1 == false)
                {
                    Console.WriteLine("Ma tran khong dung!Ban co muon nhap lai khong?(Y/N)");
                    check1 = false;
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
                check1 = true;
            }
            if (check1 == true)
            {
                Console.WriteLine("+-----------------------------------------------------+");
                Console.WriteLine("|{0,-10}|{1,-15}|{2,-15}|{3,-10}|", "TicketID", "Ticket_type", "Ticket_price", "Amount");
                Console.WriteLine("-------------------------------------------------------");
                foreach (var item in listShowTicket)
                {
                    Console.WriteLine("|{0,-10}|{1,-15}|{2,-15}|{3,-10}|", item.TicketID, item.TicketType, string.Concat(item.TicketPrice, ' ', "VND"), item.Amount);
                }
                Console.WriteLine("+-----------------------------------------------------+");
            }
            while (check2 == false)
            {
                Console.Write("Nhap loai ve:");
                t.TicketType = Console.ReadLine();
                if (validateString(t.TicketType) != true)
                {
                    Console.WriteLine("Loai ve la ki tu chu cai A-Z!Ban co muon nhap lai khong?(Y/N)");
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
                try
                {
                    foreach (var item in listShowTicket)
                    {
                        if (t.TicketType == item.TicketType)
                        {
                            check2 = true;
                        }
                    }
                }
                catch (System.Exception)
                {
                }
                if (check2 == false)
                {
                    Console.WriteLine("Loai ve khong dung!Ban co muon nhap lai khong?(Y/N)");
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
            while (check3 == false)
            {
                Console.Write("Nhap so luong ve muon mua:");
                t.Amount = Input(Console.ReadLine());
                if (t.Amount > 10 || t.Amount <= 0)
                {
                    Console.WriteLine("Moi nguoi chi duoc mua toi da 10 ve cho 1 tran!Ban co muon nhap lai khong?(Y/N)");
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
                try
                {
                    foreach (var item in listShowTicket)
                    {
                        if (t.TicketType == item.TicketType && t.Amount <= item.Amount)
                        {
                            check3 = true;
                        }
                    }
                }
                catch (System.Exception)
                {
                }
                if (check3 == false)
                {
                    Console.WriteLine("so luong ve da het hoac nho hon so ve muon mua!Ban co muon nhap lai khong?(Y/N)");
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
            return t;
        }
        public bool validateString(string str)
        {
            Regex regex = new Regex("[A-Z]");
            MatchCollection mc = regex.Matches(str);
            if (mc.Count < str.Length)
            {
                return false;
            }
            return true;
        }
        public int Input(string str)
        {
            Regex regex = new Regex("[0-9]");
            MatchCollection mc = regex.Matches(str);
            while ((mc.Count < str.Length) || (str == ""))
            {
                Console.Write("Du lieu nhap vao phai la so tu nhien,moi ban nhap lai: ");
                str = Console.ReadLine();
                mc = regex.Matches(str);
            }
            return Convert.ToInt32(str);
        }
    }
}