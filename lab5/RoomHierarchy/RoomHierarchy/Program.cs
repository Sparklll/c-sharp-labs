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
        }
    }
}