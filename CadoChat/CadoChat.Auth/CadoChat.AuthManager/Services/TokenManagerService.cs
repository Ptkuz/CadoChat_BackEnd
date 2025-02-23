using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.SecutiryInfo;
using CadoChat.Security.Validation.Services;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CadoChat.AuthManager.Services
{
    public class TokenManagerService : ITokenManagerService
    {

        private readonly ISecurityKeyService<RsaSecurityKey> _securityKeyService;

        public TokenManagerService(ISecurityKeyService<RsaSecurityKey> securityKeyService)
        {
            _securityKeyService = securityKeyService;
        }

        public string CreateAccessTokenAsync(IdentityUser user)
        {

            var globalSettings = GlobalSettingsLoader.GetInstance();

            var authService = globalSettings.GlobalSettings.Services.AuthService;
            var chatService = globalSettings.GlobalSettings.Services.ChatService;

            var clientUser = globalSettings.GlobalSettings.Users.ClientUser;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Issuer = authService.URL,
                Audience = chatService.AudiencesAccess.Name,
                Subject = new ClaimsIdentity(new[]
                { 
                    new Claim("scope", AccessScopes.SendMessage.Key) 
                }),
                Expires = DateTime.UtcNow.AddMinutes(clientUser.AccessTokenLifetime),
                SigningCredentials = _securityKeyService.SigningCredentials
            };

            //tokenDescriptor.Subject.AddClaim(new Claim("aud", chatService.AudiencesAccess.Name));

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return jwt;
        }
    }
}
