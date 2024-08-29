using Dapper;
using Models;
using System.Collections.Generic;

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
            try
            {
                var connection = await _dbConnection.connectToDatabase();
                if (!connection.success) return result;

                var command = "prc_login_user";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(name: "username", value: model.phoneNumber);
                parameters.Add(name: "password", value: model.password);

                result.data = (await connection.data.QueryFirstOrDefaultAsync(command, parameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                if (result.data.Count > 0)
                {
                    result.message = "login successful.";
                    result.success = true;
                }
                else
                {
                    result.message = "invalid username or password.";
                    result.success = false;
                }
            }
            catch
            {
                result.message = "login failed.";
                result.success = false;
            }
            return result;
        }
    }
}
