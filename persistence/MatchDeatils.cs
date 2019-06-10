using System;
namespace Persistence
{
    public class MatchDetails
    {
        private Matches m;
        private string teamAway;
        private Teams t;

        public MatchDetails() 
        { 
               m = new Matches(); 
               T = new Teams();
        }

        public MatchDetails(Matches m, string teamAway, Teams t)
        {
            this.m = m;
            this.TeamAway = teamAway;
            this.T = t;
        }

        public Matches M { get => m; set => m = value; }
        public string TeamAway { get => teamAway; set => teamAway = value; }
        public Teams T { get => t; set => t = value; }
    }
}