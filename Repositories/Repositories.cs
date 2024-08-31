using Dapper;
using Models;

namespace Repositories
{
    public interface IUserRepositories
    {
        Task<CustomActionResult> createUser(UserModel model);

        Task<CustomActionResult<List<UserInfoModel>>> getUsers();

        Task<CustomActionResult<List<UserInfoModel>>> editUser(UserModel model);

        Task<CustomActionResult<bool>> deleteUserById(int id);

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
            catch
            {
                result.message = "user creation failed.";
                result.success = false;
            }
            return result;
        }

        public async Task<CustomActionResult<List<UserInfoModel>>> getUsers()
        {
            var result = new CustomActionResult<List<UserInfoModel>>();
            try
            {
                var connection = await _dbConnection.connectToDatabase();
                if (!connection.success) return result;
                var command = "prc_get_users";
                result.data = (await connection.data.QueryAsync<UserInfoModel>(command, null, commandType: System.Data.CommandType.StoredProcedure)).ToList();
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

        public async Task<CustomActionResult<bool>> deleteUserById(int id)
        {
            CustomActionResult<bool> result = new CustomActionResult<bool>();
            try
            {
                var connection = await _dbConnection.connectToDatabase();
                if (!connection.success) return result;
                var command = "prc_delete_user";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(name: "user_id", value: id);
                await connection.data.ExecuteAsync(command, parameters, commandType: System.Data.CommandType.StoredProcedure);
                result.message = "user deleted.";
                result.success = true;
                result.data = true;

            }
            catch
            {
                result.message = "user deletion failed.";
                result.data = false;
            }
            return result;
        }

        public async Task<CustomActionResult<List<UserInfoModel>>> editUser(UserModel model)
        {
            CustomActionResult<List<UserInfoModel>> result = new CustomActionResult<List<UserInfoModel>>();
            try
            {
                var connection = await _dbConnection.connectToDatabase();
                if (!connection.success) return result;
                using (connection.data)
                {
                    var command = "prc_edit_user";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add(name: "user_id", value: model.userId);
                    parameters.Add(name: "new_first_name", value: model.firstName);
                    parameters.Add(name: "new_last_name", value: model.lastName);
                    parameters.Add(name: "new_phone_number", value: model.phoneNumber);
                    await connection.data.ExecuteAsync(command, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    result.message = "user modified.";
                    result.success = true;
                }
            }
            catch
            {
                result.message = "user modification failed.";
                result.success = false;
            }
            return result;
        }
    }
}
