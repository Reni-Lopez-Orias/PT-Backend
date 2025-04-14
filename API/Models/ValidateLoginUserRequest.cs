namespace API.Models
{
    public class ValidateLoginUserRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;

    }
}
