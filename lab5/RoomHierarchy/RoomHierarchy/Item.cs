namespace RoomHierarchy
{
    public enum ItemType
    {
        ElectricalAppliance,
        Furniture,
        Another
    }
    public class Item
    {
        public string ItemName { get; }
        public ItemType ItemType { get; }

        public Item(string itemName, ItemType itemType)
        {
            ItemName = itemName;
            ItemType = itemType;
        }
    }
}