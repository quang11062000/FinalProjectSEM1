using System;
namespace persistence
{
    public class Schedule
     {
         private Matches m;
         private Teams t;
         private string teamAway;

        public Matches M { get => m; set => m = value; }
        public Teams T { get => t; set => t = value; }
        public string TeamAway { get => teamAway; set => teamAway = value; }

        public Schedule()
        {
            m = new Matches();
            t = new Teams();
        }
    }
}