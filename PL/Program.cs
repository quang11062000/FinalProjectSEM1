using System;
using System.Collections.Generic;
using BL;
using persistence;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
        //    Menus m = new Menus();
        //     m.Menuchoice(null);
                ScheduleBL schedu = new ScheduleBL();
                List<Schedule> list = schedu.DisplaySchedule();
                Console.WriteLine("|{0,-10}|{1,-30}|{2,-20}|{3,-20}|{4,-20}|","Match_id","Match_name","Match_day","Match_time","Stadium");
                foreach (var item1 in list)
                {
                    Console.WriteLine("|{0,-10}|{1,-30}|{2,-20}|{3,-20}|{4,-20}|", item1.Match_id, string.Concat(item1.Home_team,' ',"vs",' ',item1.Away_team),item1.Match_day,item1.Match_time,item1.Stadium_name);
                }
        }   
    }
}
