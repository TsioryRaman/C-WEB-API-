using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tache.Entities.Contexte;
using Tache.Entities.User;

namespace Tache.Controllers
{
    [Route("api/")]
    [ApiController]
    [EnableCors]
    public class AuthentificationController : ControllerBase
    {
        private IConfiguration _conf;
        private readonly DbContext context;

        public AuthentificationController(IConfiguration config,TacheContext context)
        {
            this._conf = config;
            this.context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]Users User)
        {
            IActionResult response = BadRequest(new { error="User not found" });

            var user = Users.IsUser(User);

            if (user!=null)
            {
                var tokenString = GenerateToken(user);
                response = Ok(new { token = tokenString });
                return response;

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
 
        if(currentUser.HasClaim(c=>c.Type == "Username"))
            {
                // Get User Id.
                var _idUser = currentUser.Claims.FirstOrDefault(c => c.Type == "IdUser").Value;
                var user = Users.Find(int.Parse(_idUser));
                response = Ok(new { user });
                return response;
            }
            return response;
            
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]Users User)
        {
            IActionResult response = BadRequest();
            Console.WriteLine("Le mot de passe hashé : "+ this.HashPassword(User.password));


            if (Users.verfify(User))
            {
                // Mila encodena
                var hashed = this.HashPassword(User.password);
                Console.WriteLine("Le mot de passe hashé : "+hashed);
                if (this.VerifyPassword(hashed, User.password))
                {
                    Console.WriteLine("D'accord");
                }

/*
                this.context.Add(User);
                this.context.SaveChanges();
                response = Ok(new { message = "User saved successfully" });*/
            }

            return response;
        }

        private string GenerateToken(Users User)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("IdUser",User.IdUser+""),
                new Claim("Username",User.username)
            };

            var token       = new JwtSecurityToken(_conf["Jwt:Issuer"],
                                    _conf["Jwt:Issuer"],
                                    claims,
                                    expires:DateTime.Now.AddHours(8),
                                    signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private string HashPassword(string password,byte[] salt = null)
        {
            if (salt == null)
            {
                salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }
            

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf:KeyDerivationPrf.HMACSHA1,
                iterationCount:1000,
                numBytesRequested:256/8
                ));
        }
        private bool VerifyPassword(string PasswordHashed,string Password)
        {
            if(PasswordHashed.Length == 0) return false;
            var salt = Convert.FromBase64String(PasswordHashed);
            if (salt == null) return false;

            var passwordHashed = this.HashPassword(Password, salt);

            if(string.Compare(PasswordHashed,passwordHashed) == 0)
            {
                return true;
            }
            return false;
        }
    }
}
