using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.SecutiryInfo;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CadoChat.AuthManager.Services
{
    public class TokenManagerService : ITokenManagerService
    {
        public string CreateAccessTokenAsync(IdentityUser user)
        {
            var key = RsaSecurityKeyService.GetKey();
            var authService = SecurityConfigLoader.SecurityConfig.AuthService;
            var clientUser = SecurityConfigLoader.SecurityConfig.ClientUser;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = authService,
                Audience = "chat_api",
                Subject = new ClaimsIdentity(new[] { new Claim("scope", "chat_api") }),
                Expires = DateTime.UtcNow.AddMinutes(clientUser.AccessTokenLifetime),
                SigningCredentials = RsaSecurityKeyService.GetSigningCredentials(key)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return jwt;
        }
    }
}
