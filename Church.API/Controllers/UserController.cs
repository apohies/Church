using Church.Core.Interfaces.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Church.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService userService;

        public UserController(IUserService userService) { 
        this.userService = userService;

        }

      
        [HttpGet("UserMongos")]
        [Authorize]
        public IActionResult Get() {

           return Ok(userService.GetAllMongos());
        }

    }
}
