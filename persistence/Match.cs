using System;

namespace Persistence
{
    public class Match
    {
        private int matchID;
        private string matchName;
        private DateTime matchDay;
        private string matchTime;
        private string matchStadium;
        public Match() { }
        public Match(int matchID, string matchName, DateTime matchDay, string matchTime, string matchStadium)
        {
            this.MatchID = matchID;
            this.matchName = matchName;
            this.MatchDay = matchDay;
            this.MatchTime = matchTime;
            this.matchStadium = matchStadium;
        }

        public int MatchID { get => matchID; set => matchID = value; }
        public DateTime MatchDay { get => matchDay; set => matchDay = value; }
        public string MatchTime { get => matchTime; set => matchTime = value; }
        public string MatchName { get => matchName; set => matchName = value; }
        public string MatchStadium { get => matchStadium; set => matchStadium = value; }
    }
}
