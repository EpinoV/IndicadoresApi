using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace SN.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<Models.TokenConfiguration> TokenConfig;

        public AuthController(IOptions<Models.TokenConfiguration> tokenConfiguration)
        {
            TokenConfig = tokenConfiguration;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public ActionResult RequestToken(Models.Credentials credentials)
        {
            if (credentials != null && credentials.Username == credentials.Password)
            {
                var generatedToken = GenerateToken(credentials);

                return Ok(generatedToken);
            }
            else
            {
                return Unauthorized(TokenConfig.Value.UnauthorizedMessage);
            }
        }


        #region [ Private Methods ]
        private string GenerateToken(Models.Credentials credentials)
        {
            var simmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Utilities.Base64Decode(TokenConfig.Value.EncryptedKey)));

            var signingCredentials = new SigningCredentials(simmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, credentials.Username),
                new Claim(ClaimTypes.Country, "CL")
            };

            var token = new JwtSecurityToken(
                    issuer: TokenConfig.Value.Issuer,
                    audience: TokenConfig.Value.Audience,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddHours(TokenConfig.Value.HoursToExpire),
                    signingCredentials: signingCredentials,                    
                    claims: claims
                );

            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return generatedToken;
        }
        
        #endregion
    }
}