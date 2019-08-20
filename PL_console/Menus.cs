using System;
using BL;
using Persistence;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Text;

namespace PL_console
{
    public class Menus
    {
        public void LoginInterface()
        {
            Console.Clear();
            string line1 = "+==============================================+";
            string line2 = "+----------------------------------------------+";
            string menuname = "Dang nhap";
            Console.WriteLine(line1);
            Console.WriteLine("|{0,27}                   |", menuname);
            Console.WriteLine(line2);
            string usname;
            string pw;
            string choice;
            bool checkchoice = false;
            Customer customer = null;
            while (true)
            {
                CustomerBL customerBL  = new CustomerBL();
                Console.Write("Nhap ten tai khoan: ");
                usname = Console.ReadLine();
                Console.Write("Nhap mat khau: ");
                pw = Password();
                Console.WriteLine("Dang dang nhap...");
                Thread.Sleep(500);
                if (validate(usname) != true || validate(pw) != true)
                {
                    Console.Write("Tai khoan va mat khau khong duoc chua ki tu dac biet,Ban co muon nhap lai khong(C/K)?");
                    choice = Console.ReadLine().ToUpper();
                    checkchoice = Choose(choice);
                    if (checkchoice == true)
                    {
                        continue;
                    }
                    else
                    {
                        Console.Clear();
                        Environment.Exit(0);
                    }
                }
                try
                {
                    customer = customerBL.GetUserByUsernameAndPass(usname, pw);
                }
                catch (Exception)
                {
                
                    Console.Write("Loi!! Ban co muon dang nhap lai khong(C/K)?");
                    choice = Console.ReadLine().ToUpper();
                    checkchoice = Choose(choice);
                    if (checkchoice == true)
                    {
                        continue;
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
                if (customer == null)
                {
                    Console.Write("Tai khoan hoac mat khau khong dung, Ban co muon nhap lai khong(C/K)? ");
                    choice = Console.ReadLine().ToUpper();
                    checkchoice = Choose(choice);
                    if (checkchoice == true)
                    {
                        continue;
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
                if (customer != null)
                {
                    break;
                }
            }
            Console.WriteLine("Dang nhap thanh cong!\nChao mung ban den voi He Thong Ban Ve Cua Cau Lac Bo Bong Da HAGL!");
            Thread.Sleep(1000);
            MenuMain(customer);
        }
        public void MenuMain(Customer customer)
        {
            Console.Clear();
            Console_Statistical consoleStatistic = new Console_Statistical();
            string title = "He Thong Ban Ve Cua Cau Lac Bo Bong Da HAGL";
            string[] item = { "Mua ve", "Thong ke so luong ve da mua cua ban", "Thoat" };
            int choice = Menu(title, item);
            switch (choice)
            {
                case 1:
                    MenuTicket(customer);
                    break;
                case 2:
                    Console.Clear();
                    try
                    {
                        consoleStatistic.DisplayStatistic(customer);
                    }
                    catch (System.Exception)
                    {
                        Console.WriteLine("Loi !! Moi dang nhap lai");
                        LoginInterface();
                    }
                    break;
                case 3:
                    LoginInterface();
                    break;
            }
        }
        public bool Choose(string choice)
        {
            while (true)
            {
                if (choice != "C" && choice != "K")
                {
                    Console.Write("Ban chi duoc nhap (C/K): ");
                    choice = Console.ReadLine().ToUpper();
                    continue;
                }
                break;
            }
            switch (choice)
            {
                case "C":
                    return true;
                case "c":
                    return true;
                case "K":
                    return false;
                case "k":
                    return false;
            }
            return false;
        }
        public bool validate(string str)
        {
            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionstr = regex.Matches(str);
            if (matchCollectionstr.Count < str.Length)
            {
                return false;
            }
            return true;
        }
        public string Password()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        sb.Length--;
                    }
                    continue;
                }
                Console.Write('*');

                sb.Append(cki.KeyChar);
            }
            return sb.ToString();
        }
        public void MenuTicket(Customer customer)
        {
            Console.Clear();
            Console_BuyTickets cb = new Console_BuyTickets();
            string[] item = { "Tao gio hang", "Xem gio hang", "Thoat" };
            int choice = Menu("He thong ban ve", item);
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    try
                    {
                        cb.BuyTicket(customer);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Loi !! Moi dang nhap lai");
                        LoginInterface();
                    }
                    break;
                case 2:
                    Console.Clear();
                    MenuPay(customer);
                    break;
                case 3:
                    MenuMain(customer);
                    break;
            }
        }
        public void MenuPay(Customer customer)
        {
            Console.Clear();
            Consle_Pay cp = new Consle_Pay();
            string line1 = "=================================================================";
            string line2 = "-----------------------------------------------------------------";
            string title = "Gio hang cua toi";
            Console.WriteLine(line1);
            Console.WriteLine("{0,30}", title);
            Console.WriteLine(line2);
            cp.DisplayCart(customer);
            Console.WriteLine(line1);
            string[] item = { "Xoa mat hang", "Thanh toan gio hang", "Thoat" };
            int choice = Menu("Gio Hang", item);
            switch (choice)
            {
                case 1:
                    // Console.Clear();
                    try
                    {
                        cp.EditCart(customer);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Loi !! Moi dang nhap lai");
                        LoginInterface();
                    }
                    break;
                case 2:
                    Console.Clear();
                    try
                    {
                        cp.PayTicket(customer);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Loi !! Moi dang nhap lai");
                        LoginInterface();
                    }
                    break;
                case 3:
                    MenuTicket(customer);
                    break;
            }
        }
        public short Menu(string title, string[] menuItems)
        {
            short choice = 0;
            string line1 = "+=============================================+";
            string line2 = "+---------------------------------------------+";
            Console.WriteLine(line1);
            Console.WriteLine("|{0,-45}|", title);
            Console.WriteLine(line2);
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine("|{0}.{1,-43}|", i + 1, menuItems[i]);
            }
            Console.WriteLine(line2);
            try
            {
                Console.Write("Chon:");
                choice = Int16.Parse(Console.ReadLine());
            }
            catch (System.Exception)
            {
            }
            if (choice == 0 || choice > menuItems.Length)
            {
                do
                {
                    try
                    {
                        Console.Write("Nhap sai moi nhap lai:");
                        choice = Int16.Parse(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }

                } while (choice <= 0 || choice > menuItems.Length);
            }
            return choice;
        }
    }
}