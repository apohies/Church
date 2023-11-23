using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Church.Security;

namespace Church.API.Filters
{
    public class TokenFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {


            
            var myService = context.HttpContext.RequestServices.GetService<ICommand>();

         


            // Intentar obtener el token del encabezado "Authorization"
            if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues tokenHeader))
            {
                var token = tokenHeader.FirstOrDefault()?.Split(" ").LastOrDefault();

                var idioto = myService.ValidateToken(token);

                // Puedes hacer algo con el token, como verificar su validez o almacenarlo en el contexto para su posterior uso
                if (!string.IsNullOrEmpty(token))
                {
                    // Almacenar el token en el contexto
                    context.HttpContext.Items["AuthToken"] = token;
                }
                else
                {
                    // Manejar el caso en que el token esté vacío o no sea válido
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                // Manejar el caso en que no se proporcionó un encabezado "Authorization"
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
