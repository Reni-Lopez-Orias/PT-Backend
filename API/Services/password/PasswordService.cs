using System.Security.Cryptography;
using System.Text;

namespace API.Services.password
{
    public class PasswordService : IPasswordService
    {

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var providedPasswordHash = HashPassword(providedPassword);
            return hashedPassword == providedPasswordHash;
        }
    }

}
