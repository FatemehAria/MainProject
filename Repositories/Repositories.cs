﻿using Dapper;
using Models;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Repositories
{
    public interface IUserRepositories
    {
        Task<CustomActionResult> createUser(UserModel model);

        Task<List<UserModel>> getUsers();

        Task<UserModel> getUserById();

        Task<UserModel> editUser(UserModel model);

        Task<bool> deleteContactById(int id);

    }
    public class UserRepository : IUserRepositories
    {
        public async Task<CustomActionResult> createUser(UserModel model)
        {
            CustomActionResult result = new CustomActionResult();
            try
            {
                using (var connection = new MySqlConnection("server=localhost;database=main_project_db;user=root;password=;"))
                {
                    var command = "prc_create_user";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add(name: "first_name", value: model.firstName);
                    parameters.Add(name: "last_name", value: model.lastName);
                    parameters.Add(name: "phone_number", value: model.phoneNumber);

                    await connection.ExecuteAsync(command, parameters, commandType: System.Data.CommandType.StoredProcedure);
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
    }
}
