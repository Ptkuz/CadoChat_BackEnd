using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

            var globalSettings = GlobalSettingsLoader.Instance;

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
                    new Claim("scope", chatService.ChatScopeConfig.SendMessageScope.Name),
                    new Claim("scope", chatService.ChatScopeConfig.ReceiveMessageScope.Name)
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
