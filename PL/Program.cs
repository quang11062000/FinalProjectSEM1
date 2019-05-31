using System;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string[] Menu1 = new string[] { "Login", "Exit" };
                string[] Menu2 = new string[] { "Create Cart", "Display Cart", "Back to main menu" };
                string[] Menu3 = new string[] { "Pay", "Back to main menu" };
                int choice;
                // int choice1;
                // int choice2;
                do
                {
                    choice = menu(Menu1, 2, "Chao mung den voi VTC Academy", "Choice#", "Invalid");
                    switch (choice)
                    {
                        case 1:
                           break;
                        case 2:
                            break;

                    }

                } while (choice != 2);
            }
        }
        public static int menu(string[] MenuItem, int ItemCount, string Title, string mgChoice, string mgInvalid)
        {

            int choice = -1;
            Console.WriteLine("=====================================");
            Console.WriteLine("{0}", Title);
            Console.WriteLine("=====================================");
            int i;
            for (i = 0; i < ItemCount-1 ; i++)
            {
                Console.WriteLine("{0}.{1}", i + 1, MenuItem[i]);
            }
            Console.WriteLine("{0}.{1}", i = 0, MenuItem[1]);
            while (true)
            {
                bool check;
                while (true)
                {
                    Console.WriteLine("=====================================");
                    Console.Write("{0} : ", mgChoice);
                    check = int.TryParse(Console.ReadLine(), out choice);
                    if (check)
                    {
                        break;
                    }
                }
                if (choice < 0 && choice > ItemCount)
                {
                    Console.WriteLine("{0}", mgInvalid);
                }
                else
                {
                    break;
                }
            }
            return choice;
        }
    }
}
