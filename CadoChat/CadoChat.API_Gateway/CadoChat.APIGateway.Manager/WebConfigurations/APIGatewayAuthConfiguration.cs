using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.APIGateway.Manager.WebConfigurations
{

    /// <summary>
    /// Конфигуратор аутентификации
    /// </summary>
    public static class APIGatewayAuthConfiguration
    {

        public static string AuthenticationScheme =>
                JwtBearerDefaults.AuthenticationScheme;

        /// <summary>
        /// Добавить сервис аутентификации
        /// </summary>
        /// <param name="webApplicationBuilder"></param>
        public static void AddAPIGatewayAuthenticationhService(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services
                .AddAuthentication(AuthenticationScheme)
                .AddJwtBearer(AuthenticationScheme, ConfigureAuthOptions);
        }

        /// <summary>
        /// Использовать сервис аутентификации
        /// </summary>
        /// <param name="applicationBuilder">Собранное приложение</param>
        public static void UseAPIGatewayAuthenticationhService(this WebApplication applicationBuilder)
        {
            applicationBuilder.UseAuthentication();
        }

        /// <summary>
        /// Настроить опции аутентификации
        /// </summary>
        /// <param name="options">Опции аутентификации</param>
        private static void ConfigureAuthOptions(JwtBearerOptions options)
        {

            var globalInstance = GlobalSettingsLoader.Instance ?? throw new ArgumentNullException();

            var authService = globalInstance.GlobalSettings.Services.AuthService;

            options.Authority = authService.URL;
            options.RequireHttpsMetadata = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {

                //ValidateIssuer = true,
                //ValidIssuer = authService.URL,
                //ValidateAudience = false,
                //ValidateLifetime = true,
                //ValidateIssuerSigningKey = true,
                //RequireSignedTokens = true
            };

        }
    }
}
