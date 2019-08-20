using System;
using System.Collections.Generic;
using DAL;
using Persistence;
namespace BL
{
    public class MatchBL
    {
        private MatchDAL matchDAL;
        public MatchBL()
        {
            matchDAL = new MatchDAL();
        }
        public List<Match> GetListMatchDetail()
        {
            return matchDAL.GetMatchDetailInfo();
        }

    }
}
