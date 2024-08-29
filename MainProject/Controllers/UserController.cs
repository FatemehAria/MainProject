using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace MainProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userService;

        public UserController(IUserServices userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> CreateUser(UserModel model)
        {
            return Ok(await _userService.createUser(model));
        }
    }
}
