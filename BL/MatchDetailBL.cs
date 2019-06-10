using System;
using System.Collections.Generic;
using DAL;
using Persistence;
namespace BL
{
    public class MatchDetailBL
    {
        private MatchDetailsDAL mdtdal;
        public MatchDetailBL()
        {
            mdtdal = new MatchDetailsDAL();
        }
        public List<MatchDetails> GetListMatchDetail()
        {
            return mdtdal.GetMatchDetailInfo();
        }

    }
}
