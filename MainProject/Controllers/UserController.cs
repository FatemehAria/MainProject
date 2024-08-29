using Microsoft.AspNetCore.Mvc;
using Services;

namespace MainProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userService;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
