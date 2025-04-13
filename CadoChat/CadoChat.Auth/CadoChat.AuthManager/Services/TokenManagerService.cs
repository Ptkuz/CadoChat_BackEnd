using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.DAL.Entity.BaseEntity;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.Common.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CadoChat.AuthManager.Services
{
    public class TokenManagerService<TUser> : ITokenManagerService<TUser> where TUser : IEntity
    {

        private readonly ISecurityKeyService<RsaSecurityKey> _securityKeyService;

        public TokenManagerService(ISecurityKeyService<RsaSecurityKey> securityKeyService)
        {
            _securityKeyService = securityKeyService;
        }

        public string GenerateToken(string userId, string username, List<string> roles, Dictionary<string, string> customClaims, List<string> scopes)
        {

            roles.Add("Client");
            customClaims.Add("create_chat", "true");
            scopes.Add("chat:read");    

            var globalSettings = GlobalSettingsLoader.Instance;

            var authService = globalSettings.GlobalSettings.Services.AuthService;
            var chatService = globalSettings.GlobalSettings.Services.ChatService;
            var clientUser = globalSettings.GlobalSettings.Users.ClientUser;

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

            // Добавим роли
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Добавим скоупы
            foreach (var scope in scopes)
            {
                claims.Add(new Claim("scope", scope));
            }

            // Кастомные клеймы
            foreach (var kvp in customClaims)
            {
                claims.Add(new Claim(kvp.Key, kvp.Value));
            }

            var creds = _securityKeyService.SigningCredentials;

            var token = new JwtSecurityToken(
                issuer: authService.URL,
                audience: chatService.Name,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(clientUser.AccessTokenLifetime),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
