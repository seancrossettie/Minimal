using System;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Minimal.Models;

namespace Minimal.DataAccess
{
    public class CategoryRepository
    {
        readonly string _connectionString;
        public CategoryRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Minimal");
        }
        internal void CreateNewCategory(Category newCategory)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO Category(categoryName, userId, categoryDescription, totalCategoryItems, totalCategoryItemsRemoved)
                        OUTPUT inserted.categoryId
                        VALUES(@categoryName, @userId, @categoryDescription, @totalCategoryItems, @totalCategoryItemsRemoved)";

            var id = db.ExecuteScalar<Guid>(sql, newCategory);
            newCategory.CategoryId = id;
        }
        internal object GetAllCategories()
        {
            using var db = new SqlConnection(_connectionString);
            var allCategories = db.Query<Category>(@"SELECT * FROM Category");
            return allCategories;
        }
        internal object GetCategoryByName(string categoryName)
        {
            string likeString = "%" + categoryName + "%";
            using var db = new SqlConnection(_connectionString);
            var namedCategory = db.Query<Category>(@"SELECT * FROM Category c WHERE c.categoryName LIKE @likeString", new { likeString });
            return namedCategory;
        }
        internal object GetCategoryById(Guid categoryId)
        {
            using var db = new SqlConnection(_connectionString);
            var selectedCategory = db.QueryFirstOrDefault<Category>(@"SELECT * FROM Category WHERE categoryId = @categoryId", new { categoryId });
            return selectedCategory;
        }
        internal object UpdateCategory(Guid categoryId, Category category)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"UPDATE Category
                        SET
                        categoryName = @categoryName,
                        userId = @userId,
                        categoryDescription = @categoryDescription,
                        totalCategoryItems = @totalCategoryItems,
                        totalCategoryItemsRemoved = @totalCategoryItemsRemoved
                        WHERE categoryId = @categoryId";
            category.CategoryId = categoryId;

            var updatedCategory = db.QueryFirstOrDefault<User>(sql, category);

            return updatedCategory;
        }
        internal void DeleteCategory(Guid categoryId)
        {
            using var db = new SqlConnection(_connectionString);
            var deletedCategory = db.Execute(@"DELETE FROM Category WHERE categoryId = @categoryId", new { categoryId });
        }
    }
}
