using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System.Net;

namespace MainProject.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
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
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _userService.getUsers());
            }
            else
            {
                return BadRequest(ModelState);
            }
            ;
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _userService.loginUser(model));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _userService.editUser(model));
            }
            else
            {
                return BadRequest(ModelState);

            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteUserById([FromBody] int user_id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _userService.deleteUserById(user_id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
