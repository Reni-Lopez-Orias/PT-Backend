using API.Models;

namespace API.Services.Auth
{
    public interface IAuthService
    {
        ResponseBase<User> ValidateLoginUser(ValidateLoginUserRequest request);
        ResponseBase<User> RegisterUser(User request);
        ResponseBase<User> GetUser(string email); 
    }
}
