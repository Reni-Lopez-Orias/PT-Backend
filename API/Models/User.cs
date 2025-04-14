namespace API.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
