using System;
using BL;
using persistence;
using System.Collections.Generic;
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
            int flag = 0;
            while (true)
            {
                flag = 0;
                Console.Write("Enter username: ");
                usname = Console.ReadLine();
                Console.Write("Enter password: ");
                pw = Console.ReadLine();
                Class1 user1 = new Class1();
                List<Customers> list = user1.GetUsernameandPass(usname, pw);
                foreach (var item in list)
                {
                    if (item.UserName == usname && item.Password == pw)
                    {
                        flag = 1;
                        Console.WriteLine("Welcome {0} to HAGL Ticketing system", item.CustomerName);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("error username or password. Please Enter again!");
                        continue;
                    }
                }
                if(flag == 1){
                    break;
                }
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