using API.Models;

namespace API.Repositoy.Auth
{
    public interface IAuthRepository
    {
        int ValidateLoginUser(ValidateLoginUserRequest request);
        string RegisterUser(User request);
        User GetUserByEmail(string email);
    }
}
