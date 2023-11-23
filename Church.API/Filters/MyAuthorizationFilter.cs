using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Church.Security;

namespace Church.API.Filters
{
    public class MyAuthorizationFilter : TypeFilterAttribute
    {
        public MyAuthorizationFilter(string[] requiredRoles) : base(typeof(MyAuthorizationFilterImpl))
        {
            Arguments = new object[] { requiredRoles };
        }

        private class MyAuthorizationFilterImpl : IActionFilter
        {
            private readonly string[] _requiredRoles;

            public MyAuthorizationFilterImpl(string[] requiredRoles)
            {
                _requiredRoles = requiredRoles;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                var chu = _requiredRoles;


                var myService = context.HttpContext.RequestServices.GetService<ICommand>();




                // Intentar obtener el token del encabezado "Authorization"
                if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues tokenHeader))
                {
                    var token = tokenHeader.FirstOrDefault()?.Split(" ").LastOrDefault();

                    var idioto = myService!.ValidateToken(token!);

             
                    if (!string.IsNullOrEmpty(token))
                    {
                        var ce = idioto.Item2;

                        if (ce is DatosUsuarioToken datosUsuarioToken)
                        {
                            var permisos = datosUsuarioToken.permisos;

                            var permisosArray = permisos.Split(",").ToList();

                            var permisosFaltantes = _requiredRoles.Except(permisosArray);

                            if (permisosFaltantes.Count() > 0) { 
                                
                                context.Result = new UnauthorizedResult();
                            }
                           
                        }
                       
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

            public void OnActionExecuting(ActionExecutingContext context)
            {
               
            }
        }
    }
}
