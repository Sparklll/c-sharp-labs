using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RoomAdvancedFeatures
{
    class Program
    {
        static void Main(string[] args)
        {
            Owner owner = new Owner("Elon","Musk","734643859");
            Owner newOwner = new Owner("X Æ A-12", "Musk", "956498765");
            SiteInfo siteInfo = new SiteInfo(owner,"FD-C-156782");
            Bedroom bedroom = new Bedroom("Bedroom", "No description", 2.5, 10, 5, true, true, siteInfo,
                HeatingSystem.ElectricHeating, new List<Item>(), new List<Bedroom.BedItem>());
            
            bedroom.ChangeOwner += o =>
            {
                Console.WriteLine($"   CHANGE OF OWNERSHIP\n" +
                                  $"*************************\n" +
                                  $"Previous room owner :\n" +
                                  $"Name : {bedroom.SiteInfo.Owner.Name}\n" +
                                  $"Surname : {bedroom.SiteInfo.Owner.Surname}\n" +
                                  $"Passport data : {bedroom.SiteInfo.Owner.PassportData}\n" +
                                  $"*************************\n" +
                                  $"New room owner :\n" +
                                  $"Name : {o.Name}\n" +
                                  $"Surname : {o.Surname}\n" +
                                  $"Passport data : {o.PassportData}\n" +
                                  $"*************************\n");
            };
            bedroom.SetSiteOwner(newOwner);
            

            bedroom.StartUp += delegate(object sender, EventArgs e)
            {
                if (sender is LivingQuarter livingQuarter)
                {
                    livingQuarter.ConfigureSystem();
                    livingQuarter.AnalyzeSensors();
                    livingQuarter.SetupSensors();
                }
            };
            bedroom.SwitchOnControlDevice();
            
            
            // Exception handling example
            object a = 1;
            Room room = new Room();
            Console.WriteLine();
            
            try
            {
                room.CompareTo(a);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}