using Dapper;
using Models;
using System.Data.SqlClient;

namespace Repositories
{
    public interface IUserRepositories
    {
        bool createUser(UserModel model);

        List<UserModel> getUsers();

        UserModel getUserById();

        UserModel editUser(UserModel model);

        bool deleteContactById(int id);
    }
    public class UserRepository : IUserRepositories
    {
        public bool createUser(UserModel model)
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

                    connection.Execute(command, parameters, commandType: System.Data.CommandType.Text);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool deleteContactById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel editUser(UserModel model)
        {
            throw new NotImplementedException();
        }

        public UserModel getUserById()
        {
            throw new NotImplementedException();
        }

        public List<UserModel> getUsers()
        {
            throw new NotImplementedException();
        }
    }
}
