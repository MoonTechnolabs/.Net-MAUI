using SQLite;
using MauiLoginDemo.Models;

namespace MauiLoginDemo.Services
{
    /// <summary>
    /// Handles SQLite database operations for user authentication.
    /// </summary>
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            // Define database path
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            // Create Users table if it doesn't exist
            _database.CreateTableAsync<User>().Wait();
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        public Task<int> AddUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        /// <summary>
        /// Gets a user by username and password for login validation.
        /// </summary>
        public Task<User> GetUserAsync(string username, string password)
        {
            return _database.Table<User>()
                .Where(u => u.UserName == username && u.Password == password)
                .FirstOrDefaultAsync();
        }
    }
}
