using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using BL;
using Newtonsoft.Json;
using Persistence;
namespace PL_console
{
    public class Console_BuyTickets
    {
        Menus m = new Menus();
        public void DisplayMatchDetails(Customer customer)
        {
            MatchBL mbl = new MatchBL();
            List<Persistence.Match> listm = new List<Persistence.Match>();
            try
            {
                listm = mbl.GetListMatchDetail();
            }
            catch (Exception)
            {
                Console.Write("Loi!! Nhan phim bat ki de tro lai man hinh dang nhap!");
                Console.ReadKey();
                m.LoginInterface();
            }
            if (listm.Count != 0)
            {
                var table = new ConsoleTable("Ma Tran", "Ten Tran", "Gio Thi Dau", "Ngay Thi Dau", "San Van Dong");
                foreach (var item in listm)
                {
                    table.AddRow(item.MatchID, item.MatchName, item.MatchTime, String.Format("{0:dd/MM/yyyy}", item.MatchDay), item.MatchStadium);
                }
                table.Write(Format.Default);
            }
        }
        public List<Ticket> GetListTicketByMatchID(int matchID, Customer customer)
        {
            TicketBL tkbl = new TicketBL();
            List<Ticket> listShowTicket = new List<Ticket>();
            try
            {
                listShowTicket = tkbl.GetListTicketByMatchID(matchID);
                if (listShowTicket.Count != 0)
                {
                    var table = new ConsoleTable("Ma Ve", "Loai Ve", "Gia", "So Luong");
                    foreach (var item in listShowTicket)
                    {
                        string ticketprice = pricevalid(item.TicketPrice);
                        table.AddRow(item.TicketID, item.TicketType, ticketprice, item.Amount);
                    }
                    table.Write(Format.Default);
                    return listShowTicket;
                }
            }
            catch (Exception)
            {
                Console.Write("Loi!! Nhan phim bat ki de tro lai man hinh dang nhap!");
                Console.ReadKey();
                m.LoginInterface();
            }
            return listShowTicket;
        }
        public void BuyTicket(Customer customer)
        {
            bool checkchoice = false;
            while (true)
            {
                Console.Clear();
                Ticket t = new Ticket();
                List<Ticket> listticket = new List<Ticket>();
                DisplayMatchDetails(customer);
                string path = Path.GetFullPath("DataCart" + customer.Username + ".txt");
                string choice;
                if (File.Exists(path) == true)
                {
                    listticket = ReadFile(customer);
                }
                t = InputTicketInfo(customer);
                foreach (var item in listticket)
                {
                    if (t.TicketID == item.TicketID)
                    {
                        item.Amount += t.Amount;
                        t = null;
                        break;
                    }
                }
                if (t != null)
                {
                    listticket.Add(t);
                }
                if (InputTicketOnFile(customer, listticket) == true)
                {
                    Console.Write("Them vao gio hang thanh cong!Ban co muon mua tiep khong?(C/K)");
                    choice = Console.ReadLine().ToUpper();
                    checkchoice = m.Choose(choice);
                    if (checkchoice == true)
                    {
                        continue;
                    }
                    else
                    {
                        m.MenuTicket(customer);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Them vao gio hang khong thanh cong,An phim bat ky de tro ve menu");
                    Console.ReadKey();
                    m.MenuTicket(customer);
                    break;
                }
                // InputTicketOnFile(customer, listticket);
            }
        }
        public bool InputTicketOnFile(Customer customer, List<Ticket> list)
        {
            string sJSONResponse = JsonConvert.SerializeObject(list);
            try
            {
                using (StreamWriter sw = new StreamWriter("DataCart" + customer.Username + ".txt"))
                {
                    sw.WriteLine(sJSONResponse);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Ticket> ReadFile(Customer customer)
        {
            List<Ticket> listticket = new List<Ticket>();
            string json;
            try
            {
                using (StreamReader sd = new StreamReader("DataCart" + customer.Username + ".txt"))
                {
                    json = sd.ReadLine();
                    listticket = JsonConvert.DeserializeObject<List<Ticket>>(json);
                    return listticket;
                }
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        public int GetAmountBoughtByMatchID(int matchID, Customer customer, string path)
        {
            int sumAmount = 0;
            OrderBL odbl = new OrderBL();
            try
            {
                sumAmount = odbl.GetSumAmountTicketBoughtByMatchID(customer.Id, matchID);
                List<Ticket> listticketinCart = new List<Ticket>();
                if (File.Exists(path) == true)
                {
                    try
                    {
                        listticketinCart = ReadFile(customer);
                        foreach (var item in listticketinCart)
                        {
                            if (matchID == item.M.MatchID)
                            {
                                sumAmount += item.Amount;
                            }
                        }
                        return sumAmount;
                    }
                    catch (System.Exception)
                    {
                        sumAmount = 0;
                    }
                }
                else
                {
                    return sumAmount;
                }
            }
            catch (Exception)
            {
                // Console.WriteLine("Loi:" + e.Message);
                sumAmount = 0;
            }
            return sumAmount;
        }
        public Ticket InputTicketInfo(Customer customer)
        {
            string choice;
            string path = Path.GetFullPath("DataCart" + customer.Username + ".txt");
            TicketBL tkbl = new TicketBL();
            List<Ticket> listShowTicket = new List<Ticket>();
            Ticket t = new Ticket();
            bool checkchoice = false;
            bool check1 = false;
            bool check2 = false;
            bool check3 = false;
            while (check1 == false)
            {
                Console.Write("Nhap ma tran:");
                t.M.MatchID = Input(Console.ReadLine());
                listShowTicket = GetListTicketByMatchID(t.M.MatchID, customer);
                if (listShowTicket.Count == 0)
                {
                    Console.WriteLine("Ma tran khong dung!Moi Ban nhap lai:");
                    check1 = false;
                    continue;
                }
                int sumAmount = GetAmountBoughtByMatchID(t.M.MatchID, customer, path);
                if (sumAmount == 10)
                {
                    Console.WriteLine("Ban da mua du 10 ve cho tran nay,Ban co muon mua ve tran khac khong?(C/K):");
                    choice = Console.ReadLine().ToUpper();
                    checkchoice = m.Choose(choice);
                    if (checkchoice == true)
                    {
                        check1 = false;
                    }
                    else
                    {
                        check1 = true;
                        m.MenuTicket(customer);
                    }
                }
                if (sumAmount < 10)
                {
                    check1 = true;
                }
            }
            while (check2 == false)
            {
                Console.Write("Nhap loai ve:");
                t.TicketType = Console.ReadLine().ToUpper();
                if (validateString(t.TicketType) != true)
                {
                    Console.WriteLine("Loai ve la ki tu chu cai A-Z!Moi ban nhap lai");
                    continue;
                }
                try
                {
                    foreach (var item in listShowTicket)
                    {
                        if (t.TicketType == item.TicketType)
                        {
                            t.TicketID = item.TicketID;
                            t.TicketPrice = item.TicketPrice;
                            check2 = true;
                        }
                    }
                }
                catch (System.Exception)
                {
                }
                if (check2 == false)
                {
                    Console.WriteLine("Loai ve khong dung!Moi ban nhap lai:");
                    continue;
                }
            }
            while (check3 == false)
            {
                int amountCount = 0;
                amountCount = GetAmountBoughtByMatchID(t.M.MatchID, customer, path);
                Console.Write("Nhap so luong ve muon mua:");
                t.Amount = Input(Console.ReadLine());
                if ((t.Amount + amountCount) > 10)
                {
                    Console.WriteLine("Moi nguoi chi duoc mua toi da 10 ve cho 1 tran!Ban co muon nhap lai khong?(C/K)");
                    choice = Console.ReadLine().ToUpper();
                    checkchoice = m.Choose(choice);
                    if (checkchoice == true)
                    {
                        check1 = false;
                        continue;
                    }
                    else
                    {
                        check3 = true;
                        m.MenuTicket(customer);
                    }
                }
                try
                {
                    foreach (var item in listShowTicket)
                    {
                        if (t.TicketType == item.TicketType && item.Amount == 0)
                        {
                            check3 = true;
                            Console.WriteLine("Tran nay da ban het ve!An phim bat ky de tro ve menu!");
                            Console.ReadKey();
                            m.MenuTicket(customer);
                        }
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
                    Console.WriteLine("so luong ve con lai nho hon so ve muon mua!Ban co muon nhap lai khong?(C/K)");
                    choice = Console.ReadLine().ToUpper();
                    checkchoice = m.Choose(choice);
                    if (checkchoice == true)
                    {
                        continue;
                    }
                    else
                    {
                        check3 = true;
                        m.MenuTicket(customer);
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
        public string pricevalid(double pricez)
        {
            string prices = pricez.ToString();
            string price = "";
            int balance = (prices.Length - 1) % 3;
            for (int i = 0; i < prices.Length; i++)
            {
                if (i == prices.Length - 1)
                {
                    price = price + prices[i];
                }
                else if ((i - balance) % 3 == 0)
                {
                    price = price + prices[i] + ".";
                }
                else
                {
                    price = price + prices[i].ToString();
                }
            }
            price = price + "VND";
            return price;
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