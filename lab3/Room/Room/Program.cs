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
        }
    }
}