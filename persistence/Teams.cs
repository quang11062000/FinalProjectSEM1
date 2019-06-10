using System;
namespace Persistence
{
    public class Teams
    {
        private int teamID;
        private string teamName;
        private Stadiums st;
        public Teams()
        {
            st = new Stadiums();
        }

        public Teams(int teamID, string teamName, Stadiums st)
        {
            this.teamID = teamID;
            this.teamName = teamName;
            this.st = st;
        }

        public int TeamID { get => teamID; set => teamID = value; }
        public string TeamName { get => teamName; set => teamName = value; }
        public Stadiums St { get => st; set => st = value; }
    }
}