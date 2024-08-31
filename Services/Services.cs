using Models;
using Repositories;

namespace Services
{
    public interface IUserServices
    {
        Task<CustomActionResult> createUser(UserModel model);

        Task<CustomActionResult<List<UserInfoModel>>> getUsers();

        Task<CustomActionResult<List<UserModelAfterRegistration>>> loginUser(LoginModel model);

        Task<CustomActionResult<List<UserInfoModel>>> editUser(UserModel model);

        Task<CustomActionResult<bool>> deleteUserById(int user_id);

      
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
        public async Task<CustomActionResult<List<UserInfoModel>>> getUsers()
        {
            return await _repositories.getUsers();
        }

        public async Task<CustomActionResult<List<UserModelAfterRegistration>>> loginUser(LoginModel model)
        {

            var checkResult = await _userLoginRepo.getUserByUsernameAndPassword(model);

            return checkResult;
        }
        public async Task<CustomActionResult<bool>> deleteUserById(int user_id)
        {
            return await _repositories.deleteUserById(user_id);
        }

        public async Task<CustomActionResult<List<UserInfoModel>>> editUser(UserModel model)
        {
            return await _repositories.editUser(model);
        }
    }
}
