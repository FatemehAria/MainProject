using Dapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repositories
{
    public interface IUserLoginRepository
    {
        Task<CustomActionResult<List<UserModelAfterRegistration>>> getUserByUsernameAndPassword(LoginModel model);

        string generateToken(CustomActionResult<List<UserModelAfterRegistration>> userData);
    }

    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly IDatabaseConnection _dbConnection;
        private readonly JWTConfigModel _jwtConfigModel;
        public UserLoginRepository(IDatabaseConnection dbConnection, IOptions<JWTConfigModel> jwtConfig)
        {
            _dbConnection = dbConnection;
            _jwtConfigModel = jwtConfig.Value;
        }
        public string generateToken(CustomActionResult<List<UserModelAfterRegistration>> userData)
        {
            SymmetricSecurityKey secrectKey = new(Encoding.UTF8.GetBytes(_jwtConfigModel.Key));

            SigningCredentials signingCredentials = new(secrectKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenOptions = new(
                claims: new List<Claim>
                {
                     new("UserId", userData.data.ToString()),
                },
                expires: DateTime.Now.AddMinutes(_jwtConfigModel.ExpireMinute),
                signingCredentials: signingCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }
        public async Task<CustomActionResult<List<UserModelAfterRegistration>>> getUserByUsernameAndPassword(LoginModel model)
        {
            CustomActionResult<List<UserModelAfterRegistration>> result = new CustomActionResult<List<UserModelAfterRegistration>>();
            result.data = new List<UserModelAfterRegistration>();

            try
            {
                var connection = await _dbConnection.connectToDatabase();
                if (!connection.success) return result;

                var command = "prc_login_user";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(name: "username", value: model.phoneNumber);
                parameters.Add(name: "password", value: model.password);

                var user = await connection.data.QueryFirstOrDefaultAsync<UserModelAfterRegistration>(command, parameters, commandType: System.Data.CommandType.StoredProcedure);

                if (user != null)
                {
                    if (user.userId != 0)
                    {
                        string token = generateToken(result);
                        var userModelIn = new UserModelAfterRegistration();
                        userModelIn.userId = user.userId;
                        userModelIn.firstName = user.firstName;
                        userModelIn.lastName = user.lastName;
                        userModelIn.phoneNumber = user.phoneNumber;
                        userModelIn.token = token;
                        userModelIn.is_deleted = user.is_deleted;
                        result.data.Add(userModelIn);
                        result.message = "login successful.";
                        result.success = true;
                    }
                    else
                    {
                        result.message = "user not found.";
                        result.success = false;
                    }
                }
                else
                {
                    result.message = "invalid username or password.";
                    result.success = false;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Exception occurred: {ex.Message}");
                result.message = "login failed.";
                result.success = false;
            }

            return result;
        }
    }
}
