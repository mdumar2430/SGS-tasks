namespace demoSqlite.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;    
        public bool IsActive { get; set; } = true;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
