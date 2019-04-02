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
using GymManagement.Api.Core;
using System.Security.Claims;
using System.IO;

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
            var publicKey = _settings.PublicKey;

            try
            {
                RSACryptoServiceProvider publicProvider = new RSACryptoServiceProvider();
                RSAUtility.FromXmlString(publicProvider, publicKey);
                var publicPemKey = new StringWriter();
                RSAUtility.ExportPublicKey(publicProvider, publicPemKey);

                var key = new
                {
                    Key = publicPemKey.ToString()
                };

                return Ok(key);
            }
            catch(Exception ex)
            {
                return Ok("There was an error Creating a public key");
            }
        }

        // api/Auth/Token
        [HttpPost("Token")]
        public IActionResult Login([FromBody]Login credentials)
        {
            if (credentials == null)
                return BadRequest("Invalid Request");

            try
            {
                var privateKey = _settings.PrivateKey;

                var encryptedUser = Convert.FromBase64String(credentials.UserName);
                var encryptedPass = Convert.FromBase64String(credentials.Password);

                RSACryptoServiceProvider privateProvider = new RSACryptoServiceProvider();
                RSAUtility.FromXmlString(privateProvider, privateKey);

                var decryptedUser = privateProvider.Decrypt(encryptedUser, false);
                var decryptedPass = privateProvider.Decrypt(encryptedPass, false);

                var plaintextUser = Encoding.ASCII.GetString(decryptedUser);
                var plaintextPass = Encoding.ASCII.GetString(decryptedPass);

                // See if the user exists
                var user = _context.Users
                    .Where(u => (u.Username == plaintextUser || u.Email == plaintextUser)
                        && u.IsDeleted == false)
                    .FirstOrDefault();

                // Decrypt the database password
                var dbPassBytes = user != null
                    ? Convert.FromBase64String(user.Password)
                    : null;

                var decryptedDbPass = dbPassBytes != null
                    ? privateProvider.Decrypt(dbPassBytes, false)
                    : null;

                var plainTextDbPass = decryptedDbPass != null
                    ? Encoding.ASCII.GetString(decryptedDbPass)
                    : null;

                // Return bad request if user doesn't exist, or password is expired
                if (user == null)
                    return Ok(new { Message = "User not found" });

                else if (user.PasswordExpiration < DateTime.Now)
                    return Ok(new { Message = "Password Expired" });

                else if (plainTextDbPass != plaintextPass)
                    return Ok(new { Message = "Username or Password is incorrect" });

                else
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.SecretKey));
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
                return Ok(ex.Message);
            }
        }
    }
}
