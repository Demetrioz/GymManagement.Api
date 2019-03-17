using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using GymManagement.DataModel;
using GymManagement.Api.Context;
using GymManagement.Api.Config;
using GymManagement.Api.Controllers.DTO;
using System.Security.Claims;

namespace GymManagement.Api.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GymManagementDataContext _context;
        private readonly SecuritySettings _settings;

        public AuthController(GymManagementDataContext context, IOptions<SecuritySettings> settings) { _context = context; _settings = settings.Value; }

        // api/Auth/Key
        [HttpPost("Key")]
        public IActionResult GetPublicKey()
        {
            return Ok(new { _settings.PublicKey });
        }

        // api/Auth/Token
        [HttpPost("Token")]
        public IActionResult Login([FromBody]Login user)
        {
            if (user == null)
                return BadRequest("Invalid Request");

            try
            {
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //decryptedUserName = 
                    //decryptedPassword =

                    return Ok(true);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }


            //    if (user.Password == dbUser.Password)
            //    {
            //        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            //        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //        var claims = new List<Claim>
            //        {
            //            new Claim("Name", user.UserName),
            //            new Claim("Role", "Manager")
            //        };

            //        var tokenOptions = new JwtSecurityToken(
            //            issuer: _settings.Issuer,
            //            audience: _settings.Audience,
            //            claims: claims, // List of roles (ie student, instructor, owner, admin)
            //            expires: DateTime.Now.AddMinutes(5),
            //            signingCredentials: signinCredentials
            //        );

            //        var handler = new JwtSecurityTokenHandler();
            //        var tokenString = handler.WriteToken(tokenOptions);
            //        return Ok(new { Token = tokenString });
            //    }
            //    else
            //        return Unauthorized();
            //}
            //else
            //    return BadRequest("User not found");
        }
    }
}
