﻿using Models;
using Repositories;

namespace Services
{
    public interface IUserServices
    {
        Task<CustomActionResult> createUser(UserModel model);

        Task<CustomActionResult<List<UserModel>>> getUsers();

        Task<UserModel> getUserById();

        Task<UserModel> editUser(UserModel model);

        Task<bool> deleteContactById(int id);
    }
    public class UserService : IUserServices
    {
        private readonly IUserRepositories _repositories;

        public UserService(IUserRepositories _repos)
        {
            _repositories = _repos;
        }

        public async Task<CustomActionResult> createUser(UserModel model)
        {
            return await _repositories.createUser(model);
        }
        public async Task<CustomActionResult<List<UserModel>>> getUsers()
        {
            return await _repositories.getUsers();
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

        
    }
}
