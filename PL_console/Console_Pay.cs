using System;
using System.Collections.Generic;
using Persistence;
using BL;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
namespace PL_console
{
    public class Consle_Pay
    {
        OrderBL orderbl = new OrderBL();
        Menus m = new Menus();
        public List<Ticket> ReadFileToListCart(Customer customer)
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
        public void DisplayCart(Customer customer)
        {
            List<Ticket> listCart = new List<Ticket>();
            try
            {
                listCart = ReadFileToListCart(customer);
                if (listCart.Count != 0)
                {
                    var table = new ConsoleTable("Ma Ve", "Loai Ve", "Gia", "So Luong");
                    foreach (var item in listCart)
                    {
                        string ticketprice = pricevalid(item.TicketPrice);
                        table.AddRow(item.TicketID, item.TicketType, ticketprice, item.Amount);
                    }
                    table.Write(Format.Default);
                }
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Chua tao gio hang!Moi quay tro lai menu de mua hang");
                Console.WriteLine("An phim bat ky de quay tro lai menu mua hang");
                Console.ReadKey();
                m.MenuTicket(customer);
            }
            if (listCart.Count == 0)
            {
                Console.WriteLine("Gio hang trong!Moi quay tro lai menu de mua hang");
                Console.WriteLine("An phim bat ky de quay tro lai menu mua hang");
                Console.ReadKey();
                m.MenuTicket(customer);
            }
        }
        public bool DeleteFileCart(string fileCart)
        {
            try
            {
                File.Delete(fileCart);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public void PayTicket(Customer customer)
        {
            double moneySumTicket = 0;
            string choice;
            List<Ticket> listCart = new List<Ticket>();
            listCart = ReadFileToListCart(customer);
            foreach (var item in listCart)
            {
                moneySumTicket += (item.TicketPrice * item.Amount);
            }
            Order order = new Order();
            order.listticket = listCart;
            order.Customer.Id = customer.Id;
            order.OrderDate = DateTime.Now;
            order.OrderStatus = 0;
            Console.WriteLine("Tong tien:{0} VND", pricevalid(moneySumTicket));
            Console.Write("Nhap so tien thanh toan(VND):");
            double moneypay;
            while (true)
            {
                try
                {
                    moneypay = Convert.ToDouble(Console.ReadLine());
                    if (moneypay <= 0)
                    {
                        Console.Write("So tien thanh toan phai lon hon 0!Moi ban nhap lai: ");
                        continue;
                    }
                    if (moneypay < moneySumTicket)
                    {
                        Console.Write("So tien nhap khong du!Moi ban nhap lai: ");
                        continue;
                    }
                    if (moneypay > 999999999)
                    {
                        Console.Write("So tien qua lon!Moi ban nhap lai: ");
                        continue;
                    }
                }
                catch
                {
                    Console.Write("Ban phai nhap so tien bang so!Moi ban nhap lai so tien can thanh toan(VND): ");
                    continue;
                }
                break;
            }
            Console.Write("Xac nhan thanh toan:(C/K)");
            choice = Console.ReadLine().ToUpper();
            bool checkInsertOrder = false;
            bool checkchoice = false;
            checkchoice = m.Choose(choice);
            if (checkchoice == true)
            {
                checkInsertOrder = InsertOrder(customer, order);
            }
            else
            {
                m.MenuPay(customer);
            }
            if (checkInsertOrder == true)
            {
                Console.WriteLine("Thanh toan thanh cong!");
                if (moneypay > moneySumTicket)
                {
                    Console.WriteLine("Tien thua tra lai:{0} VND", pricevalid((moneypay - moneySumTicket)));
                }
                Console.WriteLine("Ban co muon in hoa don khong?(C/K): ");
                choice = Console.ReadLine().ToUpper();
                checkchoice = m.Choose(choice);
                if (checkchoice == true)
                {
                    PrintBill(customer, order.OrderID, moneySumTicket);
                    Console.Write("In hoa don thanh cong!Nhan phim bat ky de tro ve menu chinh!");
                    Console.ReadKey();
                    m.MenuMain(customer);
                }
                else
                {
                    m.MenuMain(customer);
                }
            }
            if (checkInsertOrder == false)
            {
                Console.WriteLine("Thanh toan khong thanh cong!Nhan phim bat ky de quay tro lai Menu chinh");
                Console.ReadKey();
                m.MenuMain(customer);
            }
        }
        public void PrintBill(Customer customer, int orderID, double totaldue)
        {
            int count = 0;
            Order order = new Order();
            string line1 = "--------------------------------------------------------------------------------------------------------------------";
            string line2 = "====================================================================================================================";
            try
            {
                OrderBL orderbl = new OrderBL();
                order = orderbl.GetOrderInfoByOrderID(orderID);
            }
            catch (Exception)
            {
                Console.WriteLine("Loi!! An phim bat ky de tro ve man hinh dang nhap!");
                Console.ReadKey();
                m.LoginInterface();
            }
            try
            {
                OrderBL orderbl = new OrderBL();
                order.listticket = orderbl.GetListTicketByOrderID(orderID);
            }
            catch (Exception)
            {
                Console.WriteLine("Loi!! An phim bat ky de tro ve man hinh dang nhap!");
                Console.ReadKey();
                m.LoginInterface();
            }
            if (order.listticket.Count != 0)
            {
                Console.WriteLine(line2);
                Console.WriteLine("------------------------------------------------------Hoa Don-------------------------------------------------------");
                Console.WriteLine(line2);
                Console.WriteLine("Ma hoa don:{0,-59}", order.OrderID);
                Console.WriteLine("Ngay mua:{0,-61}", String.Format("{0:dd/MM/yyyy}", order.OrderDate));
                Console.WriteLine(line1);
                Console.WriteLine("Don Vi Ban Ve:CLB Bong Da HAGL");
                Console.WriteLine("Dia Chi:Thanh Pho Pleiku,Tinh Gia Lai");
                Console.WriteLine(line1);
                Console.WriteLine("Ten khach hang: {0,-54}", customer.CusName);
                Console.WriteLine("So dien thoai: {0,-55}", customer.CusPhone);
                Console.WriteLine("Dia chi: {0,-54}",customer.CusAddress);
                Console.WriteLine(line1);
                var table = new ConsoleTable("STT", "Ma Ve", "Mo ta", "So luong", "Don gia", "Thanh tien");
                foreach (var item in order.listticket)
                {
                    string price = pricevalid(item.TicketPrice);
                    string totalprice = pricevalid((item.TicketPrice * item.Amount));
                    table.AddRow(count += 1, item.TicketID, string.Concat("Ve Loai ", item.TicketType, " Tran ", item.M.MatchName), item.Amount, price, totalprice);
                }
                table.Write(Format.Default);
                Console.WriteLine(line1);
                Console.WriteLine("Tong Tien(VND):{0,-55}", pricevalid(totaldue));
                if (order.OrderStatus == 0)
                {
                    Console.WriteLine("Xac Nhan:Da Thanh Toan.");
                }
                Console.WriteLine(line1);
                Console.WriteLine(line2);
            }
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
        public bool InsertOrder(Customer customer, Order order)
        {
            bool checkpay = false;
            bool checkDeleteCart = false;
            string path = Path.GetFullPath("DataCart" + customer.Username + ".txt");
            try
            {
                checkpay = orderbl.CreateOrder(order);
                if (checkpay == true)
                {
                    checkDeleteCart = DeleteFileCart(path);
                    if (checkDeleteCart == true)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            if (checkpay == false)
            {
                checkDeleteCart = false;
                return false;
            }
            return false;
        }
        public bool EditCart(Customer customer)
        {
            List<Ticket> listItem = new List<Ticket>();
            Ticket t = new Ticket();
            bool checkInputInFile = false;
            listItem = ReadFileToListCart(customer);
            while (checkInputInFile == false)
            {
                Console.Write("Nhap ma ve can xoa:");
                t.TicketID = Input(Console.ReadLine());
                foreach (var item in listItem)
                {
                    if (t.TicketID == item.TicketID)
                    {
                        listItem.Remove(item);
                        checkInputInFile = InputListTicketOnFile(customer, listItem);
                        break;
                    }
                }
                if (checkInputInFile == false)
                {
                    Console.WriteLine("Ma ve khong dung!Moi ban nhap lai: ");
                    continue;
                }
            }
            if (checkInputInFile == true)
            {
                Console.WriteLine("xoa thanh cong!Nhan phim bat ky de quay tro lai Gio hang!");
                Console.ReadKey();
                m.MenuPay(customer);
                return true;
            }
            if (checkInputInFile == false)
            {
                Console.WriteLine("xoa khong thanh cong!Nhan phim bat ky de tro ve Gio hang!");
                Console.ReadKey();
                m.MenuPay(customer);
                return false;
            }
            return false;
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
        public bool InputListTicketOnFile(Customer customer, List<Ticket> list)
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
    }
}