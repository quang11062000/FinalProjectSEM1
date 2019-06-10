using System;

namespace Persistence
{
    public class Matches
    {
        private int matchID ;
        private string matchDay ;
        private string matchTime ;
        public Matches() { }
        public Matches(int matchID, string matchDay, string matchTime)
        {
            this.MatchID = matchID;
            this.MatchDay = matchDay;
            this.MatchTime = matchTime;
        }

        public int MatchID { get => matchID; set => matchID = value; }
        public string MatchDay { get => matchDay; set => matchDay = value; }
        public string MatchTime { get => matchTime; set => matchTime = value; }
    }
}
