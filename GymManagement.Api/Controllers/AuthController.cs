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
        public IActionResult Login([FromBody]Login credentials)
        {
            if (credentials == null)
                return BadRequest("Invalid Request");

            try
            {
                // TODO: Add encryption
                // TODO: Token shows invalid signature

                // See if the user exists
                var user = _context.Users
                    .Where(u => (u.Username == credentials.UserName || u.Email == credentials.UserName)
                        && u.IsDeleted == false)
                    .FirstOrDefault();

                // Return bad request if user doesn't exist, or password is expired
                if (user == null)
                    return BadRequest("User not found");

                else if (user.PasswordExpiration < DateTime.Now)
                    return BadRequest("Password Expired");

                else if (user.Password != credentials.Password)
                    return BadRequest("Username or Password is incorrect");

                else
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var role = _context.Roles
                        .Where(r => r.RoleId == user.RoleId)
                        .FirstOrDefault();

                    // Add specific claims
                    var claims = new List<Claim>
                    {
                        new Claim("User", $"{user.FirstName} {user.LastName}"),
                        new Claim("Role", role.Name)
                    };

                    var userClaimMaps = _context.UserRoleClaimMaps
                        .Where(map => map.RoleId == user.RoleId)
                        .ToList();

                    foreach (var claim in userClaimMaps)
                    {
                        var currentClaim = _context.Claims
                            .Where(c => c.ClaimId == claim.ClaimId)
                            .FirstOrDefault();

                        claims.Add(new Claim("Claim", currentClaim.Name));
                    }

                    // Create the token options
                    var tokenOptions = new JwtSecurityToken(
                        issuer: _settings.Issuer,
                        audience: _settings.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signingCredentials
                    );

                    var handler = new JwtSecurityTokenHandler();
                    var tokenString = handler.WriteToken(tokenOptions);

                    //Set last logon for user
                    user.LastLogon = DateTime.Now;
                    user.Modified = DateTime.Now;
                    _context.SaveChanges();

                    return Ok(new { Token = tokenString });
                }

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
