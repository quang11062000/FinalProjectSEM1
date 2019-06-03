using System;
using BL;
using persistence;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Text;

namespace PL
{
    public class Menus
    {
        public void Menuchoice(string err)
        {
            if (err != null)
            {
                Console.Write(err);
            }
            string[] item = { "Login", "Exit" };
            int choice = Menu("Footballclubtickets", item);
            switch (choice)
            {
                case 1:
                    MenuLogin();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
            }
        }
        public void MenuLogin()
        {
            Console.Clear();
            string line1 = "==============================================";
            string line2 = "----------------------------------------------";
            Console.WriteLine(line1);
            Console.WriteLine("Login");
            Console.WriteLine(line2);
            string usname;
            string pw;
            string choice;
            CustomerBL csbl = new CustomerBL();
            Customers cs = null;
            while (true)
            {
                Console.Write("Enter username: ");
                usname = Console.ReadLine();
                Console.Write("Enter password: ");
                pw = Password();
                Console.WriteLine("Dang dang nhap...");
                Thread.Sleep(500);
                if (validate(usname) != true || validate(pw) != true)
                {
                    Console.Write("Tai khoan va mat khau khong duoc chua ki tu dac biet,Ban co muon nhap lai khong(Y/N)?");
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
                            Menuchoice(null);
                            break;
                        case "n":
                            Menuchoice(null);
                            break;
                        default:
                            continue;
                    }
                }
                try
                {
                    cs = csbl.LoginWithUserandPass(usname, pw);
                }
                catch (System.NullReferenceException)
                {
                    Console.Write("Mất kết nối, bạn có muốn đăng nhập lại không? (Y/N)");
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
                            Menuchoice(null);
                            break;
                        case "n":
                            Menuchoice(null);
                            break;
                        default:
                            continue;
                    }
                }

                if (cs == null)
                {
                    Console.Write("Tai khoan hoac mat khau khong dung, Ban co muon nhap lai khong(Y/N)? ");
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
                            Menuchoice(null);
                            break;
                        case "n":
                            Menuchoice(null);
                            break;
                        default:
                            continue;
                    }
                }
                break;
            }
            if (cs != null)
            {
                Console.WriteLine("Dang nhap thanh cong! Chao mung ban den voi FootballClub Tikceting System!");
                Thread.Sleep(1000);
                MenuTicket(cs);
            }
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
        public void MenuTicket(Customers cs)
        {
            Console.Clear();
            string[] item = { "Create Cart", "View Cart", "Exit" };
            int choice = Menu("FootballClubTicketing System", item);
            switch (choice)
            {
                case 1:
                    Console_BuyTickets cb = new Console_BuyTickets();
                    cb.Display();
                    // cb.DisplayNumberTicketofMatch();
                    break;
                case 2:
                    break;
                case 3:
                    Menuchoice(null);
                    break;
            }
        }
        public short Menu(string title, string[] menuItems)
        {
            short choice = 0;
            string line1 = "==============================================";
            string line2 = "----------------------------------------------";
            Console.WriteLine(line1);
            Console.WriteLine("{0}", title);
            Console.WriteLine(line2);
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine("{0}.{1}", i + 1, menuItems[i]);
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