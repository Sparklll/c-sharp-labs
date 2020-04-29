using System;
using System.Collections.Generic;

namespace RoomHierarchy
{
    public class Bedroom : LivingQuarter
    {
        public List<BedItem> Beds;

        public Bedroom()
        {
            Beds = new List<BedItem>();
        }

        public Bedroom(string roomName, string roomDescription, double ceilingHeight, double length, double width,
            bool windowsPresence, bool electricitySupply, SiteInfo siteInfo, HeatingSystem heatingSystem,
            List<Item> items, List<BedItem> beds) : base(roomName, roomDescription, ceilingHeight, length, width,
            windowsPresence, electricitySupply, siteInfo, heatingSystem, items)
        {
            Beds = new List<BedItem>(beds);
        }

        public void AddBed(string bedName, BedItem.BedType bedType)
        {
            Beds.Add(new BedItem(bedName, ItemType.Furniture, bedType));
        }

        public void RemoveBed(BedItem bedItem)
        {
            Beds.Remove(bedItem);
        }

        public override void MakeRepairs()
        {
            Console.WriteLine("Doing repair in the bedroom!");
        }

        public class BedItem : Item
        {
            public enum BedType
            {
                SingleBed,
                DoubleBed,
                Couch
            }

            public BedItem(string itemName, ItemType itemType, BedType bedT) : base(itemName, itemType)
            {
                BedT = bedT;
            }

            public BedType BedT { get; }
        }
    }
}