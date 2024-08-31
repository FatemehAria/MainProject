using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace MainProject.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userService;

        public UserController(IUserServices userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserModel model)
        {
            return Ok(await _userService.createUser(model));
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.getUsers());
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginModel model)
        {
            return Ok(await _userService.loginUser(model));
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserModel model)
        {
            return Ok(await _userService.editUser(model));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            return Ok(await _userService.deleteUserById(id));
        }
    }
}
