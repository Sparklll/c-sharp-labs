using System;

namespace RoomHierarchy
{
    public struct Owner
    {
        public string Name { get; }
        public string Surname { get; }
        public string PassportData { get; }
        
        public Owner(string name, string surname, string passportData)
        {
            Name = name;
            Surname = surname;
            PassportData = passportData;
        }
    }
    
    public struct SiteInfo
    {
     public Owner Owner { get; }
     public string SiteDocuments { get; }

     public SiteInfo(Owner owner, string siteDocuments)
     {
         Owner = owner;
         SiteDocuments = siteDocuments;
     }
    }
    
    public class Room
    {
        private double ceilingHeight;
        private double length;
        private double width;
        public bool ElectricitySupply { get; set; }
        public bool WindowsPresence { get; set; }
        public string RoomDescription { get; set; }
        public string RoomName { get; set; }
        public SiteInfo SiteInfo { get; set; }

        public double CeilingHeight
        {
            get => ceilingHeight;
            set => ceilingHeight = value > 0 ? value : 0;
        }

        public double Length
        {
            get => length;
            set => length = value > 0 ? value : 0;
        }

        public double Width
        {
            get => width;
            set => width = value > 0 ? value : 0;
        }

        public Room()
        {
            CeilingHeight = 0;
            Length = 0;
            Width = 0;
            WindowsPresence = false;
            ElectricitySupply = false;
            RoomName = "Room";
            RoomDescription = "Room Description";
            SiteInfo = new SiteInfo(new Owner("", "", ""), "");
        }

        public Room(string roomName, string roomDescription, double ceilingHeight, double length, double width, bool windowsPresence,
            bool electricitySupply, SiteInfo siteInfo)
        {
            CeilingHeight = ceilingHeight;
            Length = length;
            Width = width;
            WindowsPresence = windowsPresence;
            ElectricitySupply = electricitySupply;
            RoomName = roomName;
            RoomDescription = roomDescription;
            SiteInfo = siteInfo;
        }
        
        public void SetSiteInfo(SiteInfo siteInfo)
        {
            SiteInfo = siteInfo;
        }

        public void SetSiteInfo(Owner owner, string siteDocuments)
        {
            SiteInfo = new SiteInfo(owner, siteDocuments);
        }

        public void SetSiteOwner(Owner owner)
        {
            string currentSiteDocuments = SiteInfo.SiteDocuments;
            SiteInfo = new SiteInfo(owner, currentSiteDocuments);
        }

        public double GetRoomArea()
        {
            return Length * Width;
        }

        public double GetRoomVolume()
        {
            return Length * Width * CeilingHeight;
        }
        
        public static double CalculateRoomIndex(Room room, double lightingSuspensionHeight)
        {
            return room.GetRoomArea() / ((room.Length + room.Width) * lightingSuspensionHeight);
        }

        public override string ToString()
        {
            return "Room : " + RoomName + ", description :  " + RoomDescription + ", ceiling height : " +
                   CeilingHeight + "m, length : " + Length +
                   "m, width : " + Width + "m.";
        }
        
        public virtual void MakeRepairs()
        {
            Console.WriteLine("Doing repair in the room!");
        }
    }
}