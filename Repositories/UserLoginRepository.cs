using Dapper;
using Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Repositories
{
    public interface IUserLoginRepository
    {
        Task<CustomActionResult<List<UserModelAfterRegistration>>> getUserByUsernameAndPassword(LoginModel model);
    }

    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly IDatabaseConnection _dbConnection;
        public UserLoginRepository(IDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
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
                    
                    result.data.Add(user);
                    result.message = "login successful.";
                    result.success = true;
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
