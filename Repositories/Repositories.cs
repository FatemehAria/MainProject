using Dapper;
using Models;

namespace Repositories
{
    public interface IUserRepositories
    {
        Task<CustomActionResult> createUser(UserModel model);

        Task<CustomActionResult<List<UserModelAfterRegistration>>> getUsers();

        Task<UserModel> getUserById();

        Task<UserModel> editUser(UserModel model);

        Task<bool> deleteUSerById(int id);

    }
    public class UserRepository : IUserRepositories
    {
        private readonly IDatabaseConnection _dbConnection;

        public UserRepository(IDatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<CustomActionResult> createUser(UserModel model)
        {
            CustomActionResult result = new CustomActionResult();
            try
            {
                var connection = await _dbConnection.connectToDatabase();
                if (!connection.success) return result;
                using (connection.data)
                {
                    var command = "prc_create_user";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add(name: "first_name", value: model.firstName);
                    parameters.Add(name: "last_name", value: model.lastName);
                    parameters.Add(name: "phone_number", value: model.phoneNumber);
                    parameters.Add(name: "password", value: model.password);

                    await connection.data.ExecuteAsync(command, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    result.message = "user created.";
                    result.success = true;
                }
            }
            catch
            {
                result.message = "user creation failed.";
                result.success = false;
            }
            return result;
        }

        public async Task<CustomActionResult<List<UserModelAfterRegistration>>> getUsers()
        {
            var result = new CustomActionResult<List<UserModelAfterRegistration>>();
            try
            {
                var connection = await _dbConnection.connectToDatabase();
                if (!connection.success) return result;
                var command = "prc_get_users";
                result.data = (await connection.data.QueryAsync<UserModelAfterRegistration>(command, null, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                result.message = "";
                result.success = true;
            }
            catch
            {
                result.message = "error getting users.";
                result.success = false;
            }
            return result;
        }

        public async Task<bool> deleteUSerById(int id)
        {
            CustomActionResult result = new CustomActionResult();
            try
            {
                var connection = await _dbConnection.connectToDatabase();
                if (!connection.success) return result.success;
                using (connection.data)
                {
                    var command = "prc_delete_user";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add(name: "id", value: id);
                    await connection.data.ExecuteAsync(command, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    result.message = "user deleted.";
                    result.success = true;
                }
            }
            catch
            {
                result.message = "user deletion failed.";
                result.success = false;
            }
            return result.success;
        }

        public Task<UserModel> editUser(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> getUserById()
        {
            throw new NotImplementedException();
        }


    }
}
