using Authentication.WebAPI.Infrastructure.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Authentication.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _config;
     
        public AuthenticationController(IConfiguration Configuration)
        {
            _config = Configuration;
        }
        
        [HttpPost]
        public IActionResult Login([FromBody] User detailsLogin)
        {
            bool result = UserValidation(detailsLogin);
            if (result)
            {
                var tokenString = CreateTokenJWT();
                return Ok( new { token = tokenString,
                        UserName = detailsLogin.UserName,
                        Password = detailsLogin.Password
                    });
            }
            else
            {
                return Unauthorized("Usuário não autorizado.");
            }
        }
        private string CreateTokenJWT()
        {
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: null, audience: null,
                expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
        private bool UserValidation(User detailsLogin)
        {
            if (detailsLogin.UserName == "Mellon" && detailsLogin.Password == "Numsey$19")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
