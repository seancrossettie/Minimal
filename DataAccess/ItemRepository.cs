using System;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Minimal.Models;

namespace Minimal.DataAccess
{
    public class ItemRepository
    {
        readonly string _connectionString;
        public ItemRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Minimal");
        }
        internal void CreateItem(Item newItem)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO Item(userId, itemName, itemDescription, timeOwned, quantity, isDuplicate, isRemoved, necessityRank)
                        OUTPUT inserted.itemId
                        VALUES(@userId, @itemName, @itemDescription, @timeOwned, @quantity, @isDuplicate, @isRemoved, @necessityRank)";

            var id = db.ExecuteScalar<Guid>(sql, newItem);
            newItem.ItemId = id;
        }
        internal object GetAllItems()
        {
            using var db = new SqlConnection(_connectionString);
            var items = db.Query<Item>(@"SELECT * FROM Item");
            return items;
        }
        internal object GetItemById(Guid itemId)
        {
            using var db = new SqlConnection(_connectionString);
            var item = db.Query<Item>(@"SELECT * FROM Item WHERE itemId = @itemId", new { itemId });
            if (item == null) return null;
            return item;
        }
        internal object GetItemByName(string itemName)
        {
            string likeString = "%" + itemName + "%";
            using var db = new SqlConnection(_connectionString);
            var namedItem = db.Query<Item>(@"SELECT * FROM Item i WHERE i.itemName LIKE @likeString", new { likeString });
            return namedItem;
        }

        internal object GetDuplicateItems()
        {
            using var db = new SqlConnection(_connectionString);
            var duplicateItems = db.Query<Item>(@"SELECT * FROM Item i Where i.isDuplicate = 1");
            if (duplicateItems == null) return null;
            return duplicateItems;
        }
        internal object GetFiveMostUselessItems()
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT TOP 5 * FROM Item
                        ORDER BY necessityRank ASC";
            var fiveMostUseless = db.Query<Item>(sql);
            if (fiveMostUseless == null) return null;
            return fiveMostUseless;
        }
        internal object GetPageItems(int pageNumber)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT * FROM Item ORDER BY itemName
                        OFFSET (@pageNumber - 1) * 10 ROWS
                        FETCH NEXT 10 ROWS ONLY";
            var pageItems = db.Query<Item>(sql, new { pageNumber });
            return pageItems;
        }
        internal object UpdateItem(Guid itemId, Item item)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"UPDATE Item
                        SET
                        userId = @userId,
                        itemName = @itemName,
                        itemDescription = @itemDescription,
                        timeOwned = @timeOwned,
                        quantity = @quantity,
                        isDuplicate = @isDuplicate,
                        isRemoved = @isRemoved,
                        necessityRank = @necessityRank
                        WHERE itemId = @itemId";
            item.ItemId = itemId;

            var updatedItem = db.QueryFirstOrDefault<User>(sql, item);

            return updatedItem;
        }
        internal object RemoveItem(Guid itemId, Item item)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"UPDATE Item
                        SET
                        userId = @userId,
                        itemName = @itemName,
                        itemDescription = @itemDescription,
                        timeOwned = @timeOwned,
                        quantity = @quantity,
                        isDuplicate = @isDuplicate,
                        isRemoved = 0,
                        necessityRank = @necessityRank
                        WHERE itemId = @itemId";
            item.ItemId = itemId;

            var removedItem = db.QueryFirstOrDefault<User>(sql, item);

            return removedItem;
        }
        internal void DeleteItem(Guid itemId)
        {
            using var db = new SqlConnection(_connectionString);

            db.Execute(@"DELETE FROM Item WHERE itemId = @itemId", new { itemId });
        }
    }
}
