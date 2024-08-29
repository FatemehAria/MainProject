using Dapper;
using Models;
using System.Data.SqlClient;

namespace Repositories
{
    public interface IUserRepositories
    {
        Task<bool> createUser(UserModel model);

        Task<List<UserModel>> getUsers();

        Task<UserModel> getUserById();

        Task<UserModel> editUser(UserModel model);

        Task<bool> deleteContactById(int id);
        
    }
    public class UserRepository : IUserRepositories
    {
        public async Task<bool> createUser(UserModel model)
        {
            try
            {
                using (var connection = new SqlConnection("server=localhost;database=main_project_db;user=root;password=;"))
                {
                    var command = "prc_create_user";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add(name: "first_name", value: model.firstName);
                    parameters.Add(name: "last_name", value: model.lastName);
                    parameters.Add(name: "phone_number", value: model.phoneNumber);

                    await connection.ExecuteAsync(command, parameters, commandType: System.Data.CommandType.Text);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> deleteContactById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> editUser(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> getUserById()
        {
            throw new NotImplementedException();
        }

        public Task<List<UserModel>> getUsers()
        {
            throw new NotImplementedException();
        }

        Task<bool> IUserRepositories.createUser(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
