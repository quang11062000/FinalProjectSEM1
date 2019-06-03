using System;
using DAL;
using persistence;
using System.Collections.Generic;
namespace BL
{
    public class ScheduleBL
    {
        private ScheduleDAL sche;
        public ScheduleBL()
        {
            sche = new ScheduleDAL();
        }
        public List<Schedule> DisplaySchedule() => sche.GetListMatchDetail();
    }
}