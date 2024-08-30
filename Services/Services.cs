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

        Task<bool> deleteUSerById(int id);
    }
    public class UserService : IUserServices
    {
        private readonly IUserRepositories _repositories;
        private readonly IUserLoginRepository _userLoginRepo;
        private readonly JWTConfigModel _jwtConfigModel;
        public UserService(IUserRepositories _repos, IUserLoginRepository userLoginRepo, IOptions<JWTConfigModel> jwtConfig)
        {
            _repositories = _repos;
            _userLoginRepo = userLoginRepo;
            _jwtConfigModel = jwtConfig.Value;
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
            CustomActionResult<string> result = new CustomActionResult<string>();
            var checkResult = await _userLoginRepo.getUserByUsernameAndPassword(model);
            result.success = checkResult.success;
            if (!result.success) return checkResult;

            SymmetricSecurityKey secrectKey = new(Encoding.UTF8.GetBytes(_jwtConfigModel.Key));

            SigningCredentials signingCredentials = new(secrectKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenOptions = new(
                claims: new List<Claim>
                {
                     new("UserId", checkResult.data.ToString()),
                },
                expires: DateTime.Now.AddMinutes(_jwtConfigModel.ExpireMinute),
                signingCredentials: signingCredentials
            );
            result.success = true;
            result.data = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            checkResult.data.Add(result.data);
            result.message = "";

            return checkResult;
        }
        public Task<bool> deleteUSerById(int id)
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
