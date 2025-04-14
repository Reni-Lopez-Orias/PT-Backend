using API.Models;
using API.Repositoy.Auth;
using API.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("ValidateLoginUser")]
        public ResponseBase<User> ValidateLoginUser(ValidateLoginUserRequest request)
        {
           return _authService.ValidateLoginUser(request);
        }

        [HttpPost("RegisterUser")]
        public ResponseBase<User> RegisterUser(User request)
        {
            return _authService.RegisterUser(request);
        }

        [HttpGet("GetUser")]
        public ResponseBase<User> GetUser(string email)
        {
            return _authService.GetUser(email);
        }

    }
}
