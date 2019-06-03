using System;
namespace persistence
{
     public  class Stadiums
    {
        private int stadiumID;
        private string stadiumName;
        private int capacity;

        public int StadiumID { get => stadiumID; set => stadiumID = value; }
        public string StadiumName { get => stadiumName; set => stadiumName = value; }
        public int Capacity { get => capacity; set => capacity = value; }
        public Stadiums() { }
        public Stadiums(int stadiumID, string stadiumName, int capacity)
        {
            this.StadiumID = stadiumID;
            this.StadiumName = stadiumName;
            this.Capacity = capacity;
        }
    }
}