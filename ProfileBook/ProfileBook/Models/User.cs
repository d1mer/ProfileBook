using SQLite;

namespace ProfileBook.Models
{
    [Table("users")]
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Unique]
        public string Login { get; set; }
        public string  Password { get; set; }
    }
}