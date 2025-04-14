namespace API.Services.password
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string providedPassword);
    }
}
