using System;
namespace Persistence
{
    public class Stadiums
    {
        private int stadiumID;
        private string stadiumName;
        private int capacity;
        public Stadiums()
        {

        }
        public Stadiums(int stadiumID, string stadiumName, int capacity)
        {
            this.stadiumID = stadiumID;
            this.stadiumName = stadiumName;
            this.capacity = capacity;
        }

        public int StadiumID { get => stadiumID; set => stadiumID = value; }
        public string StadiumName { get => stadiumName; set => stadiumName = value; }
        public int Capacity { get => capacity; set => capacity = value; }
    }
}