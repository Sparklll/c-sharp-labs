using System;

namespace RoomAdvancedFeatures
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
    
    public class Room : IComparable, ICloneable
    {
        private double ceilingHeight;
        private double length;
        private double width;
        public bool ElectricitySupply { get; set; }
        public bool WindowsPresence { get; set; }
        public string RoomDescription { get; set; }
        public string RoomName { get; set; }
        public SiteInfo SiteInfo { get; set; }
        public delegate void RoomOwnerHandler(Owner owner);
        public event RoomOwnerHandler ChangeOwner;
        
        public double CeilingHeight
        {
            get => ceilingHeight;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                ceilingHeight = value;
            }
        }

        public double Length
        {
            get => length;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                length = value;
            }
        }

        public double Width
        {
            get => width;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                width = value;
            }
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
            ChangeOwner?.Invoke(owner);
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
        
        public virtual void MakeRepairs()
        {
            Console.WriteLine("Doing repair in the room!");
        }

        public override string ToString()
        {
            return "Room : " + RoomName + ", description :  " + RoomDescription + ", ceiling height : " +
                   CeilingHeight + "m, length : " + Length +
                   "m, width : " + Width + "m.";
        }

        public object Clone()
        {
            // There aren't reference fields in class, we can use memberwise clone
            return this.MemberwiseClone();
        }

        public int CompareTo(object obj)
        {
            if (obj is Room room)
            {
                return this.GetRoomVolume().CompareTo(room.GetRoomVolume());
            }
            else
            {
                throw new ArgumentException("Parameter is not a Room!");
            }
        }
    }
}