using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public interface IUserServices
    {
        Task<CustomActionResult> createUser(UserModel model);

        Task<CustomActionResult<List<UserModelAfterRegistration>>> getUsers();

        Task<CustomActionResult<List<UserModelAfterRegistration>>> loginUser(LoginModel model);
        Task<UserModel> getUserById();

        Task<UserModel> editUser(UserModel model);

        Task<bool> deleteUserById(int id);

      
    }
    public class UserService : IUserServices
    {
        private readonly IUserRepositories _repositories;
        private readonly IUserLoginRepository _userLoginRepo;

        public UserService(IUserRepositories _repos, IUserLoginRepository userLoginRepo)
        {
            _repositories = _repos;
            _userLoginRepo = userLoginRepo;
           
        }

        public async Task<CustomActionResult> createUser(UserModel model)
        {
            return await _repositories.createUser(model);
        }
        public async Task<CustomActionResult<List<UserModelAfterRegistration>>> getUsers()
        {
            return await _repositories.getUsers();
        }

        public async Task<CustomActionResult<List<UserModelAfterRegistration>>> loginUser(LoginModel model)
        {

            var checkResult = await _userLoginRepo.getUserByUsernameAndPassword(model);

            return checkResult;
        }
        public async Task<bool> deleteUserById(int id)
        {
            return await _repositories.deleteUserById(id);
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
