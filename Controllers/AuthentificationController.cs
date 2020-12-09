using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tache.Entities.User;

namespace Tache.Controllers
{
    [Route("api/")]
    [ApiController]
    [EnableCors]
    public class AuthentificationController : ControllerBase
    {
        private IConfiguration _conf;

        public AuthentificationController(IConfiguration config)
        {
            this._conf = config;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]Users User)
        {

            Console.WriteLine("L'utilisateur : "+User.username);
            IActionResult response = Unauthorized();

            var user = Users.IsUser(User);

            if (user!=null)
            {
                var tokenString = GenerateToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
   
        [HttpGet]
        [Authorize]
        [Route("getUser")]
        public IActionResult Get()
        {
            var currentUser = HttpContext.User;
            IActionResult response = Unauthorized();
            Console.WriteLine("cool");
 
        if(currentUser.HasClaim(c=>c.Type == "Username"))
            {
                Console.WriteLine("Nom d'utilisateur "+currentUser.Claims.FirstOrDefault(c => c.Type == "Username").Value);
                var _idUser =currentUser.Claims.FirstOrDefault(c => c.Type == "IdUser").Value;
                Console.WriteLine("L'id de l'admin : " + _idUser);
                var user = Users.Find(_idUser);
                response = Ok(new { user });
                return response;
            }
            return response;
            
        }

        private string GenerateToken(Users User)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("IdUser",User.Id),
                new Claim("Username",User.username)
            };

            var token       = new JwtSecurityToken(_conf["Jwt:Issuer"],
                                    _conf["Jwt:Issuer"],
                                    claims,
                                    expires:DateTime.Now.AddHours(8),
                                    signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
