using System;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Minimal.Models;

namespace Minimal.DataAccess
{
    public class UserRepository
    {
        readonly string _connectionString;
        public UserRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Minimal");
        }
        internal void CreateNewUser(User newUser)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO Users(firstName, lastName, email, userGoalTier, totalItemsOwned, totalItemsRemoved, firebaseKey)
                        OUTPUT inserted.userId
                        VALUES(@firstName, @lastName, @email, @userGoalTier, @totalItemsOwned, @totalItemsRemoved, @firebaseKey)";

            var id = db.ExecuteScalar<Guid>(sql, newUser);
            newUser.UserId = id;
        }
        internal object GetAllUsers()
        {
            using var db = new SqlConnection(_connectionString);
            var users = db.Query<User>(@"SELECT * FROM Users");
            return users;
        }
        internal object GetUserById(Guid userId)
        {
            using var db = new SqlConnection(_connectionString);
            var user = db.Query<User>(@"SELECT * FROM Users WHERE userId = @userId", new { userId });
            if (user == null) return null;
            return user;
        }
        internal object UpdateUser(Guid userId, User user)
        {
            using var db = new SqlConnection(_connectionString);

            var sql = @"UPDATE Users
                        SET
                        firstName = @firstName,
                        lastName = @lastName,
                        email = @email,
                        userGoalTier = 4,
                        totalItemsOwned = @totalItemsOwned,
                        totalItemsRemoved = @totalItemsRemoved
                        WHERE userId = @userId";
            user.UserId = userId;

            var updatedUser = db.QueryFirstOrDefault<User>(sql, user);

            return updatedUser;
        }
        internal object SoftDeleteUser(Guid userId, User user)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"UPDATE Users
                        SET
                        firstName = @firstName,
                        lastName = @lastName,
                        email = @email,
                        userGoalTier = 4,
                        totalItemsOwned = @totalItemsOwned,
                        totalItemsRemoved = @totalItemsRemoved
                        WHERE userId = @userId";
            user.UserId = userId;
            
            var softDeletedUser = db.QueryFirstOrDefault<User>(sql, user);

            return softDeletedUser;
        }
        internal void HardDeleteUser(Guid userId)
        {
            using var db = new SqlConnection(_connectionString);

            db.Execute(@"DELETE FROM Users WHERE userId = @userId", new { userId });
        }
    }
}
