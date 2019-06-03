using System;
namespace persistence
{
   public class Teams
    {
        private int teamID;
        private string teamName;
        private Stadiums st;

        public int TeamID { get => teamID; set => teamID = value; }
        public string TeamName { get => teamName; set => teamName = value; }
        public Stadiums St { get => st; set => st = value; }

        public Teams()
        {
            st = new Stadiums();
        }
        public Teams(int teamID, string teamName, Stadiums st)
        {
            this.TeamID = teamID;
            this.TeamName = teamName;
            this.St = st;
        }
    }
}