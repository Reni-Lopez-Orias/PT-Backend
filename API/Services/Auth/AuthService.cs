using API.Models;
using API.Repositoy.Auth;
using Azure.Core;

namespace API.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public ResponseBase<User> ValidateLoginUser(ValidateLoginUserRequest request)
        {

            ResponseBase<User> response = new ResponseBase<User>();

            try
            {
                int responseValidateLoginUser = _authRepository.ValidateLoginUser(request);

                if (responseValidateLoginUser == 0)
                {
                    response.Error = true;
                    response.Message = "No existe usuario asociado!";
                    response.Response = null;
                }
                else
                {
                    User user = _authRepository.GetUserByEmail(request.Email);
                    if (user != null)
                    {
                        response.Response = user;
                    }
                    else
                    {
                        response.Response = null;
                    }
                    response.Error = false;
                    response.Message = "Incio con exito";
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
                response.Response = null;
            }

            return response;

        }

        public ResponseBase<User> RegisterUser(User request)
        {

            ResponseBase<User> response = new ResponseBase<User>();

            try
            {
                string responseRegisterUser = _authRepository.RegisterUser(request);

                if (responseRegisterUser == "")
                {
                    User user = _authRepository.GetUserByEmail(request.Email);
                    if (user != null)
                    {
                        response.Response = user;
                    }
                    else
                    {
                        response.Response = null;
                    }
                    response.Error = false;
                    response.Message = responseRegisterUser;
                    
                }
                else
                {
                    response.Error = true;
                    response.Message = responseRegisterUser;
                    response.Response = null;
                }
            }
            catch (Exception ex)
            {
                //validate diferent exceptions
                response.Error = true;
                response.Message = ex.Message;
                response.Response = null;
            }

            return response;
        }

        public ResponseBase<User> GetUser(string email)
        {
            ResponseBase<User> response = new ResponseBase<User>();

            try
            {
                User user = _authRepository.GetUserByEmail(email);
                if (user != null)
                {
                    response.Response = user;
                }
                else
                {
                    response.Response = null;
                }
                response.Error = false;
                response.Message = null;
            }
            catch (Exception ex)
            {
                //validate diferent exceptions
                response.Error = true;
                response.Message = ex.Message;
                response.Response = null;
            }

            return response;
        }

    }
}
