using System;

namespace Room
{
    class Program
    {
        static void Main(string[] args)
        {
            Room room = new Room();
            Room hangar = new Room(
                "Hangar", 
                "Premises for livestock and grain storage", 
                8.5, 
                30, 
                10, 
                true, 
                true);
            
            Console.WriteLine(room);
            Console.WriteLine(hangar);

            Console.WriteLine("Hangar area : " + hangar.GetRoomArea());
            Console.WriteLine("Hangar volume : " + hangar.GetRoomVolume());

            string hangarElectricityConnectionState = hangar.ElectricitySupply ? "+" : "-";
            Console.WriteLine("Hangar electricity connection :  "+ hangarElectricityConnectionState);
        }
    }
}