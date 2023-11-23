using Church.API.Filters;
using Church.Core.Dtos.Security;
using Church.Core.Interfaces.Service;
using Church.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Church.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ICommand comand;
        private readonly IUserService userService;
       

        public LoginController(ICommand comand,IUserService userService)
        {
            this.comand = comand;
            this.userService = userService;
        }

                // ask copilot something

      

        [HttpPost("Generate")]
        
        public async Task<IActionResult> Generate(SecurityCommand entrada)
        {
            
            UserDto user = await userService.GetUserbiNameAndPassword(entrada.User, entrada.Password);

            if (user.email != "")
            {

                var jwt = comand.GenerateToken(name: entrada.User, user: entrada.User , permision: user.permision);

                return Ok(jwt);

            }
            
            
           return Unauthorized(); 
           

        }
    }
}
