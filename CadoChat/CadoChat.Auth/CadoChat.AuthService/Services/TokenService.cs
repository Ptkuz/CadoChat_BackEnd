using CadoChat.AuthService.Services.Interfaces;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CadoChat.AuthService.Services
{
    public class TokenService : ITokenGenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public TokenService(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> CreateAccessTokenAsync(IdentityUser user)
        {
            var rsa = RSA.Create();
            //rsa.ImportRSAPrivateKey(Convert.FromBase64String("YOUR_PRIVATE_KEY"), out _);

            var key = new RsaSecurityKey(rsa)
            {
                KeyId = "supersecretkey1234564564564756757665756"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "https://localhost:7220",
                Audience = "chat_api",
                Subject = new ClaimsIdentity(new[] { new Claim("scope", "chat_api") }),
                Expires = DateTime.UtcNow.AddMinutes(600),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return jwt;
        }
    }
}
