using System;
using System.Collections.Generic;
using Persistence;
using BL;
namespace PL_console
{
    public class Consle_Pay
    {
        Consle_BuyTickets cb = new Consle_BuyTickets();
        OrderBL orderbl = new OrderBL();
        public void PayTicket(Customers cs, List<Tickets> listtk)
        {
            Order order = new Order();
            Console.Write("Nhap so tien thanh toan:");
            double moneypay = Convert.ToDouble(Console.ReadLine());
            order.Listticket = listtk;
            order.Cs.Id = cs.Id;
            try
            {
                bool check = orderbl.CreateOrder(order);
                if (check == true)
                {
                    Console.WriteLine("thanh toan thanh cong!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}