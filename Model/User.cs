using SQLite;

namespace MauiLoginDemo.Models
{
    /// <summary>
    /// Represents a user entity for storing login details.
    /// </summary>
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string UserName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
