using System;
namespace persistence
{
   public class Matches
    {
         private int matchID;
         private string matchTime;
         private string matchDay;

        public int MatchID { get => matchID; set => matchID = value; }
        public string MatchTime { get => matchTime; set => matchTime = value; }
        public string MatchDay { get => matchDay; set => matchDay = value; }
        public Matches(){}
        public Matches(int matchID,string matchTime,string matchDay)
        {
            this.MatchID = matchID;
            this.MatchTime = matchTime;
            this.MatchDay = matchDay;
        }
    }
}