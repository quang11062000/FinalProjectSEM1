namespace persistence
{
    public class Schedule
    {
        public int Match_id{get;set;}
        public string Home_team{get;set;}
         public string Away_team{get;set;}
        public string Stadium_name{get;set;}
        public string Match_time{get;set;}
        public string Match_day{get;set;}
        public Schedule(){

        }
        public Schedule( int Match_id, string Match_name, string Home_team,string Away_team, string Match_day, string Stadium_name){
            this.Match_id = Match_id;
            this.Home_team = Home_team;
            this.Away_team = Away_team;
            this.Match_day = Match_day;
            this.Match_time = Match_time;
            this.Stadium_name = Stadium_name;
        }
     }
}