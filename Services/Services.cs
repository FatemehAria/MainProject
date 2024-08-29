using Models;
using Repositories;

namespace Services
{
    public interface IUserServices
    {
        Task<bool> createUser(UserModel model);

        Task<List<UserModel>> getUsers();

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

        public Task<bool> createUser(UserModel model)
        {
            throw new NotImplementedException();
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
