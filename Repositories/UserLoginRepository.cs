using Dapper;
using Models;

namespace Repositories
{
    public interface IUserLoginRepository
    {
        Task<CustomActionResult<decimal>> getUserByUsernameAndPassword(UserModel model);
    }

    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly IDatabaseConnection _dbConnection;

        public UserLoginRepository(IDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<CustomActionResult<decimal>> getUserByUsernameAndPassword(UserModel model)
        {

            CustomActionResult<decimal> result = new CustomActionResult<decimal>();
            try
            {
                var connection = await _dbConnection.connectToDatabase();
                if (!connection.success) return result;

                var command = "prc_login_user";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(name: "username", value: model.phoneNumber);
                parameters.Add(name: "password", value: model.password);

                await connection.data.ExecuteAsync(command, parameters, commandType: System.Data.CommandType.StoredProcedure);
                result.message = "login successful.";
                result.success = true;

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
