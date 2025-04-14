using API.Repository.DBContext;
using API.Services.password;
using Dapper;
using Serilog;
using System.Data;
using API.Models;

namespace API.Repositoy.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IPasswordService _passwordService;

        public AuthRepository(IDbContext dbContext, IPasswordService passwordService)
        {
            _dbContext = dbContext;
            _passwordService = passwordService;
        }

        public int ValidateLoginUser(ValidateLoginUserRequest request)
        {

            int response = 0;

            try
            {
                using (var connection = _dbContext.Connection)
                {
                    var getUserByEmail = "GetUserByEmail";
                    var parametersByEmail = new DynamicParameters();
                    parametersByEmail.Add("@prmEmail", request.Email, DbType.String, ParameterDirection.Input, 50);
                    var user = connection.Query<User>(getUserByEmail, parametersByEmail, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (user != null)
                    {
                        var isValid = _passwordService.VerifyPassword(user.Hash, request.Hash);
                        if (isValid)
                        {
                            response = 1;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ValidateLoginUser");
                throw new Exception("Ha ocurrido un error interno!");
            }

            return response;

        }

        public User GetUserByEmail(string email)
        {

            User response = new User();

            try
            {
                using (var connection = _dbContext.Connection)
                {
                    var getUserByEmail = "GetUserByEmail";
                    var parametersByEmail = new DynamicParameters();
                    parametersByEmail.Add("@prmEmail", email, DbType.String, ParameterDirection.Input, 50);
                    var user = connection.Query<User>(getUserByEmail, parametersByEmail, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (user != null)
                    {
                        response = user;
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ValidateLoginUser");
                throw new Exception("Ha ocurrido un error interno!");
            }

            return response;

        }

        public string RegisterUser(User request)
        {
            string response = "";

            try
            {
                using (var connection = _dbContext.Connection)
                {
                    var procedure = "RegisterUser";
                    var hash = _passwordService.HashPassword(request.Hash);

                    var parameters = new DynamicParameters();
                    parameters.Add("@prmEmail", request.Email, DbType.String, ParameterDirection.Input, 50);
                    parameters.Add("@prmHash", hash, DbType.String, ParameterDirection.Input, 250);
                    parameters.Add("@prmName", request.Name, DbType.String, ParameterDirection.Input, 50);
                    parameters.Add("@prmLastName", request.LastName, DbType.String, ParameterDirection.Input, 50);
                    parameters.Add("@prmMessageResponse", dbType: DbType.String, size: 250, direction: ParameterDirection.Output);
                    parameters.Add("@prmIsValid", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);

                    var isvalid = parameters.Get<int>("@prmIsValid");

                    if (isvalid == 0)
                    {
                        response = parameters.Get<string>("@prmMessageResponse");
                    }
                    else
                    {
                        response = "";
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "RegisterUser");
                throw new Exception("Ha ocurrido un error interno!");
            }

            return response;

        }

    }
}
