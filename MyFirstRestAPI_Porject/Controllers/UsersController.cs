using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentApi.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController(JwtOptions jwtOptions, ApplicationDbcontext dbcontext) : ControllerBase
    {
        [HttpPost]
        [Route("auth")]

        public ActionResult<string> AuthenticateUser(AuthenticatinRequest request)
        {
            var user = dbcontext.Set<User>().FirstOrDefault(x =>
                                    x.UserName == request.userName &&
                                    x.Password == request.password);

            if (user == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                    SecurityAlgorithms.HmacSha256  
                ),

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString() ),
                    new Claim(ClaimTypes.Name, user.UserName)
                })

            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return Ok(accessToken);
        }
    }
}
