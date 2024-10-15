using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentApi.Data;
using StudentApi.Model.User;
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
            User? user = dbcontext.Set<User>().FirstOrDefault(x =>
                                    x.UserName == request.userName &&
                                    x.Password == request.password);

            if (user == null)
            {
                return Unauthorized();
            }

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityTokenDescriptor tokenDescriptor = new()
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
                    new(ClaimTypes.NameIdentifier, user.Id.ToString() ),
                    new(ClaimTypes.Name, user.UserName)
                })

            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string accessToken = tokenHandler.WriteToken(securityToken);

            return Ok(accessToken);
        }
    }
}
