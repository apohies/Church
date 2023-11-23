using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Church.Security
{
    public class SecurityCommandHandler : ICommand
    {
        private readonly Authentication _Authenticacion;
        private readonly Microsoft.Extensions.Configuration.IConfiguration Configuration;

        public SecurityCommandHandler(IOptions<Authentication> Authenticacion, IConfiguration configuration)
        {
            this._Authenticacion = Authenticacion.Value;
            this.Configuration = configuration;

        }


        public string GenerateToken(string name, string user, List<string> permision)
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Authenticacion.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim("Name", name),
                new Claim("User", user),
                new Claim("Permission", string.Join(",", permision))

            };


 
            var payload = new JwtPayload
            (
                _Authenticacion.Issuer,
                _Authenticacion.Audience,
                claims,
                DateTime.Now ,
                DateTime.Now.AddMinutes(Convert.ToDouble(_Authenticacion.MinutesToken))
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Tuple<bool, object> ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_Authenticacion.SecretKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                return new Tuple<bool, object>(true, ObtenerDataToken(jwtToken));
            }
            catch
            {
                return new Tuple<bool, object>(false, null);
            }
        }

        private DatosUsuarioToken ObtenerDataToken(JwtSecurityToken jwtToken)
        {
            string userName = jwtToken.Claims.First(x => x.Type == "User").Value;
            DateTimeOffset dateTimeOffsetExpiracion = DateTimeOffset.FromUnixTimeSeconds(int.Parse(jwtToken.Claims.First(x => x.Type == "exp").Value)).LocalDateTime;
            DateTimeOffset dateTimeOffsetCreado = DateTimeOffset.FromUnixTimeSeconds(int.Parse(jwtToken.Claims.First(x => x.Type == "nbf").Value)).LocalDateTime;

            var dataToken = new DatosUsuarioToken
            {
                usuario = userName,
                fecha_creacion = dateTimeOffsetCreado.DateTime,
                fecha_expiracion = dateTimeOffsetExpiracion.DateTime,
                permisos = jwtToken.Claims.First(x => x.Type == "Permission").Value

            };
            return dataToken;
        }

        public Tuple<bool, DatosUsuarioToken> ValidateJwtTokenExpirado(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_Authenticacion.SecretKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                return new Tuple<bool, DatosUsuarioToken>(true, ObtenerDataToken(jwtToken));
            }
            catch
            {
                return new Tuple<bool, DatosUsuarioToken>(false, null);
            }
        }

        public string RefreshToken(string usuario)
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Authenticacion.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim("Name", usuario),
                new Claim("User", usuario)

            };

            //Payload
            var payload = new JwtPayload
            (
                _Authenticacion.Issuer,
                _Authenticacion.Audience,
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(Convert.ToDouble(_Authenticacion.MinutesToken))
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }




        public string GenerateToken1(string email)
        {
            //Header fqweqw1234232sfwqfcdfsdg342352265gsdgsfdeddff
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);
            var claims = new[]
            {

                new Claim("User",email)
            };
            var payload = new JwtPayload
            (Configuration["Authentication:Issuer"],
                Configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(Convert.ToDouble(Configuration["Authentication:MinutesToken"]))
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
