using SQLite;
using ItemInventory.Models;

namespace ItemInventory.DataAccess
{
    /// <summary>
    /// Data access layer: connects to SQLite, saves items, then reads them back.
    /// </summary>
    public class ItemData
    {
        // Connection to the SQLite database (null until first use)
        private SQLiteAsyncConnection? database;

        /// <summary>
        /// Opens the database and creates the Items table the first time it's needed.
        /// </summary>
        private async Task Init()
        {
            if (database is not null)
            {
                return;
            }

            database = new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
            await database.CreateTableAsync<Item>();
        }

        /// <summary>
        /// Output: a list of every item in the database.
        /// </summary>
        public async Task<List<Item>> GetItemsAsync()
        {
            await Init();
            return await database!.Table<Item>().ToListAsync();
        }

        /// <summary>
        /// Input: the item to save. Adds it as a new row in the Items table.
        /// </summary>
        public async Task<int> AddItemAsync(Item item)
        {
            await Init();
            return await database!.InsertAsync(item);
        }
    }
}