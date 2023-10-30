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
       

        public LoginController(ICommand comand)
        {
            this.comand = comand;
        }

      

        [HttpPost("Generate")]
       
        public  ActionResult<string> Generate(SecurityCommand entrada)
        {

                var jwt = comand.GenerateToken(name: entrada.User, user: entrada.User);

                return Ok(jwt);

        }
    }
}
