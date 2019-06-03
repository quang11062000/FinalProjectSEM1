using System;
namespace persistence
{
     class Schedule
     {
         private Matches m;
         private Teams t;

        internal Matches M { get => m; set => m = value; }
        internal Teams T { get => t; set => t = value; }

        public Schedule()
        {
            m = new Matches();
            t = new Teams();
        }
    }
}