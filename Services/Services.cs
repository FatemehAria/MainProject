using Models;
using Repositories;

namespace Services
{
    public interface IUserServices
    {
        Task<CustomActionResult> createUser(UserModel model);

        Task<CustomActionResult<List<UserModelAfterRegistration>>> getUsers();

        Task<CustomActionResult<List<UserModelAfterRegistration>>> loginUser(LoginModel model);

        Task<UserModelAfterRegistration> editUser(UserModelAfterRegistration model);

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

        public async Task<UserModelAfterRegistration> editUser(UserModelAfterRegistration model)
        {
            return await _repositories.editUser(model);
        }
    }
}
