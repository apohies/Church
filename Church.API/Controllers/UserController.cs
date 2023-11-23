using Church.API.Filters;
using Church.Core.Interfaces.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
        [MyAuthorizationFilter(new[] { "Admin", "Moderador" })]
        public IActionResult Get() {

           return Ok(userService.GetAllMongos());
        }

        [HttpGet("UserMongo/name")]
        public async Task<IActionResult> UserbiName(string name)
        {

            return Ok(await userService.GetUserbiName(name));
        }

        [HttpGet("FindMongo/{email}")]
        [MyAuthorizationFilter(new[] { "CU-K" })]
        public async Task<IActionResult> UserbyEmail(string email)
        {

            return Ok(await userService.GetUserbyEmail(email));
        }
    }
}
