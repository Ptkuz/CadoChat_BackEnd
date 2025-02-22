using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.SecutiryInfo;
using CadoChat.Security.Validation.Services;
using CadoChat.Security.Validation.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CadoChat.AuthManager.Services
{
    public class TokenManagerService : ITokenManagerService
    {

        public TokenManagerService()
        {

        }

        public string CreateAccessTokenAsync(IdentityUser user)
        {

            var securityKey = RsaSecurityKeyService.GetInstance();

            var authService = SecurityConfigLoader.SecurityConfig.AuthService;
            var clientUser = SecurityConfigLoader.SecurityConfig.ClientUser;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = authService,
                Audience = string.Join(" ", clientUser.Audiences),
                Subject = new ClaimsIdentity(new[]
                { 
                    new Claim("scope", AccessScopes.SendMessage.Key) 
                }),
                Expires = DateTime.UtcNow.AddMinutes(clientUser.AccessTokenLifetime),
                SigningCredentials = securityKey.SigningCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return jwt;
        }
    }
}
