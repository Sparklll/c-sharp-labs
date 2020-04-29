using System;
using System.Linq;

namespace RoomHierarchy
{
    class Program
    {
        static void Main(string[] args)
        {
            LivingQuarter bedroom = new Bedroom();
            Owner owner = new Owner("Keanu","Reeves","KH2764595");
            bedroom.SetSiteInfo(owner,"docs");
            bedroom.MakeRepairs();
            
            bedroom.ConfigureSystem();
            bedroom.AnalyzeSensors();
            
            
            
            Room firstRoom = new Room() {CeilingHeight = 5.5, Length = 10, Width = 15};
            Room secondRoom = new Room() {CeilingHeight = 2, Length = 5, Width = 4};

            if (firstRoom.CompareTo(secondRoom) > 0)
            {
                Console.WriteLine("The first room larger than the second.");
            }
            else
            {
                Console.WriteLine("The second room larger than the first.");
            }
        }
    }
}