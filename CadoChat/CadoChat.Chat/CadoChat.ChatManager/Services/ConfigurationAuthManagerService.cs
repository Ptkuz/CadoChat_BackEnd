using CadoChat.Security.Authentication.Services;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.ChatManager.Services
{

    /// <summary>
    /// Конфигуратор аутентификации
    /// </summary>
    public class ConfigurationAuthManagerService : ConfigurationAuthService, IConfigurationAuthService
    {

        /// <summary>
        /// Инициализировать конфигуратор аутентификации
        /// </summary>
        /// <param name="securityKeyService">Сервис ключей безопасности</param>
        public ConfigurationAuthManagerService(ISecurityKeyService<RsaSecurityKey> securityKeyService) 
            : base(securityKeyService)
        {
        }

        /// <summary>
        /// Настроить опции аутентификации
        /// </summary>
        /// <param name="options">Опции аутентификации</param>
        protected override void ConfigureAuthOptions(JwtBearerOptions options)
        {
            var chatService = GlobalSettings.Services.ChatService;
            var authService = GlobalSettings.Services.AuthService;
            var clientUser = GlobalSettings.Users.ClientUser;

            options.Authority = authService.URL;
            options.RequireHttpsMetadata = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {

                ValidateIssuer = true,
                ValidIssuer = authService.URL,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = _securityKeyService.Key,
                ValidAudiences = [chatService.AudiencesAccess.Name],
                ValidateIssuerSigningKey = true
            };
        }
    }
}
