using SQLite;

namespace ItemInventory.Models
{
    /// <summary>
    /// An item saved in the Items table.
    /// </summary>
    public class Item
    {
        // Database key; SQLite fills this in automatically
        [PrimaryKey, AutoIncrement]
        public int Key { get; set; }

        // ID the user types in (string so it can hold letters, like "A-100")
        public string ItemId { get; set; } = string.Empty;

        // Name of the item
        public string ItemName { get; set; } = string.Empty;

        // Short description of the item
        public string ItemDescription { get; set; } = string.Empty;
    }
}