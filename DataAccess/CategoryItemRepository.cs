using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Minimal.Models;

namespace Minimal.DataAccess
{
    public class CategoryItemRepository
    {
        readonly string _connectionString;
        public CategoryItemRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Minimal");
        }
        internal void CreateCategoryItem(CategoryItem newCategoryItem)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO CategoryItem(categoryId, itemId)
                        OUTPUT inserted.categoryItemId
                        VALUES(@categoryId, @itemId)";

            var id = db.ExecuteScalar<Guid>(sql, newCategoryItem);
            newCategoryItem.CategoryItemId = id;
        }
        internal object GetAllCategoryItems()
        {
            using var db = new SqlConnection(_connectionString);
            var allCategoryItems = db.Query<CategoryItem>(@"SELECT * FROM CategoryItem");
            return allCategoryItems;
        }

        internal object GetCategoryItemById(Guid categoryItemId)
        {
            using var db = new SqlConnection(_connectionString);
            var selectedCategoryItem = db.QueryFirstOrDefault<CategoryItem>(@"SELECT * FROM CategoryItem WHERE categoryItemId = @categoryItemId", new { categoryItemId });
            return selectedCategoryItem;
        }
        internal object GetCategoryItemByCategoryId(Guid categoryId)
        {
            using var db = new SqlConnection(_connectionString);
            var selectedCategoryItem = db.QueryFirstOrDefault<CategoryItem>(@"SELECT * FROM CategoryItem WHERE categoryId = @categoryId", new { categoryId });
            return selectedCategoryItem;
        }
        internal object GetCategoryItemByItemId(Guid itemId)
        {
            using var db = new SqlConnection(_connectionString);
            var selectedCategoryItem = db.QueryFirstOrDefault<CategoryItem>(@"SELECT * FROM CategoryItem WHERE itemId = @itemId", new { itemId });
            return selectedCategoryItem;
        }
        internal object UpdateCategoryItem(Guid categoryItemId, CategoryItem categoryItem)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"UPDATE CategoryItem
                        SET
                        categoryId = @categoryId,
                        itemId = @itemId
                        WHERE categoryItemId = @categoryItemId";

            categoryItem.CategoryItemId = categoryItemId;

            var updatedCategoryItem = db.QueryFirstOrDefault<CategoryItem>(sql, categoryItem);

            return updatedCategoryItem;
        }
        internal void DeleteCategoryItem(Guid categoryItemId)
        {
            using var db = new SqlConnection(_connectionString);

            var deletedCategoryItem = db.Execute(@"DELETE FROM CategoryItem WHERE categoryItemId = @categoryItemId", new { categoryItemId });
        }

    }
}
