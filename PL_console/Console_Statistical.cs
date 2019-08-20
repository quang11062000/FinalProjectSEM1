using System;
using System.Collections.Generic;
using BL;
using Persistence;
namespace PL_console
{
    public class Console_Statistical
    {
        Console_BuyTickets consoleticket = new Console_BuyTickets();
        Menus menu = new Menus();
        public void DisplayStatistic(Customer customer)
        {
            bool checkorder = false;
            string choice;
            string status = "";
            List<Order> ListOrders = new List<Order>();
            OrderBL Orders = new OrderBL();
            try
            {
                ListOrders = Orders.GetOrdersByCustomerID(customer.Id);
            }
            catch (System.Exception)
            {
            }
            if (ListOrders.Count != 0)
            {
                var table = new ConsoleTable("Ma Don Hang", "Ngay Mua", "Trang Thai Don Hang");
                foreach (var item in ListOrders)
                {
                    if (item.OrderStatus == 0)
                    {
                        status = "Da Thanh Toan";
                    }
                    table.AddRow(item.OrderID, String.Format("{0:dd/MM/yyyy}", item.OrderDate), status);
                }
                table.Write(Format.Default);
            }
            else
            {
                Console.WriteLine("Ban Chua mua hang!Nhan phim bat ky de tro ve menu chinh");
                Console.ReadKey();
                menu.MenuMain(customer);
            }
            while (checkorder == false)
            {
                Console.Write("Nhap ma dat hang:");
                int OrderID = consoleticket.Input(Console.ReadLine());
                foreach (var item in ListOrders)
                {
                    if (OrderID == item.OrderID)
                    {
                        checkorder = PrintTableTicket(customer, OrderID);
                    }
                }
                if (checkorder == false)
                {
                    Console.Write("Ma hoa don can tra cuu khong dung!Ban co muon nhap lai khong(C/K):");
                    choice = Console.ReadLine().ToUpper();
                    bool checkchoice = menu.Choose(choice);
                    if (checkchoice == true)
                    {
                        checkorder = false;
                        continue;
                    }
                    else
                    {
                        checkorder = true;
                        menu.MenuMain(customer);
                    }
                }
            }
            Console.WriteLine("An phim bat ky de tro ve menu chinh!");
            Console.ReadKey();
            menu.MenuMain(customer);
        }
        public bool PrintTableTicket(Customer customer, int OrderID)
        {
            string line1 = "===================================================================================================";
            string line = "---------------------------------------------------------------------------------------------------";
            OrderBL OrderBL = new OrderBL();
            List<Ticket> listtk = new List<Ticket>();
            double totaldueOrder = 0;
            try
            {
                listtk = OrderBL.GetListTicketByOrderID(OrderID);
            }
            catch (Exception e)
            {
                Console.WriteLine("Loi:" + e.Message);
                Console.WriteLine("An phim bat ky de tro ve menu chinh:");
                Console.ReadKey();
                menu.MenuMain(customer);
            }
            if (listtk.Count != 0)
            {
                Console.WriteLine(line1);
                Console.WriteLine("----------------------------------------Chi Tiet Don Hang------------------------------------------");
                Console.WriteLine(line1);
                var table1 = new ConsoleTable("Ma ve", "Mo Ta", "So luong", "Don gia", "Tong tien");
                foreach (var item in listtk)
                {
                    totaldueOrder += (item.TicketPrice * item.Amount);
                    string unitprice = consoleticket.pricevalid(item.TicketPrice);
                    string totalmoney = consoleticket.pricevalid((item.TicketPrice * item.Amount));
                    table1.AddRow(item.TicketID, string.Concat("Ve Loai ", item.TicketType, " Tran ", item.M.MatchName), item.Amount, unitprice, totalmoney);
                }
                table1.Write(Format.Default);
                Console.WriteLine(line);
                Console.WriteLine("Tong Tien Da Thanh Toan:{0}", consoleticket.pricevalid(totaldueOrder));
                Console.WriteLine(line);
                Console.WriteLine(line1);
                return true;
            }
            else
            {
                return false; 
            }
        }
    }
}