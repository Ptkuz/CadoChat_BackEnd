using CadoChat.Security.Authentication.Services;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Validation.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.APIGateway.Manager.Services
{

    /// <summary>
    /// Конфигуратор аутентификации
    /// </summary>
    public class ConfigurationAuthAPIGatewayService : ConfigurationAuthService, IConfigurationAuthService
    {
        /// <summary>
        /// Инициализировать конфигуратор аутентификации
        /// </summary>
        /// <param name="securityKeyService">Строитель приложения</param>
        public ConfigurationAuthAPIGatewayService(ISecurityKeyService<RsaSecurityKey> securityKeyService)
            : base(securityKeyService)
        {
        }

        /// <summary>
        /// Добавить сервис аутентификации
        /// </summary>
        /// <param name="webApplicationBuilder"></param>
        public override void AddService(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services
                .AddAuthentication(AuthenticationScheme)
                .AddJwtBearer(AuthenticationScheme, ConfigureAuthOptions);
        }

        /// <summary>
        /// Настроить опции аутентификации
        /// </summary>
        /// <param name="options">Опции аутентификации</param>
        protected override void ConfigureAuthOptions(JwtBearerOptions options)
        {

            var authService = GlobalSettings.Services.AuthService;

            options.Authority = authService.URL;
            options.RequireHttpsMetadata = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {

                ValidateIssuer = true,
                ValidIssuer = authService.URL,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                RequireSignedTokens = true
            };

        }
    }
}
