namespace Room
{
    public class Room
    {
        private double ceilingHeight;
        private double length;
        private double width;
        public bool WindowsPresence { get; set; }
        public bool ElectricitySupply { get; set; }
        public string RoomName { get; set; }
        public string RoomDescription { get; set; }

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
            RoomDescription = "Room Template";
        }

        public Room(string roomName, string roomDescription, double ceilingHeight, double length, double width, bool windowsPresence,
            bool electricitySupply)
        {
            CeilingHeight = ceilingHeight;
            Length = length;
            Width = width;
            WindowsPresence = windowsPresence;
            ElectricitySupply = electricitySupply;
            RoomName = roomName;
            RoomDescription = roomDescription;
        }

        public double GetRoomArea()
        {
            return Length * Width;
        }

        public double GetRoomVolume()
        {
            return Length * Width * CeilingHeight;
        }

        public override string ToString()
        {
            return "Room : " + RoomName + ", description :  " + RoomDescription + ", ceiling height : " +
                   CeilingHeight + "m, length : " + Length +
                   "m, width : " + Width + "m.";
        }
    }
}