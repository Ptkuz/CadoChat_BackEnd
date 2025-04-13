using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.ChatManager.WebConfigurations
{

    /// <summary>
    /// Конфигуратор аутентификации
    /// </summary>
    public static class ChatAuthenticationConfiguration
    {

        private static GlobalSettingsLoader GlobalSettingsLoader =>
            GlobalSettingsLoader.Instance ?? throw new ArgumentNullException();

        public static string AuthenticationScheme =>
            JwtBearerDefaults.AuthenticationScheme;

        /// <summary>
        /// Добавить сервис аутентификации
        /// </summary>
        /// <param name="webApplicationBuilder">Строитель приложения</param>
        public static void AddChatAuthenticationService(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddAuthentication(AuthenticationScheme)
                .AddJwtBearer(AuthenticationScheme, ConfigureAuthOptions);
        }

        /// <summary>
        /// Использовать сервис аутентификации
        /// </summary>
        /// <param name="applicationBuilder">Собранное приложение</param>
        public static void UseChatAuthenticationService(this WebApplication applicationBuilder)
        {
            applicationBuilder.UseAuthentication();
        }

        /// <summary>
        /// Настроить опции аутентификации
        /// </summary>
        /// <param name="options">Опции аутентификации</param>
        private static void ConfigureAuthOptions(JwtBearerOptions options)
        {
            var chatService = GlobalSettingsLoader.GlobalSettings.Services.ChatService;
            var authService = GlobalSettingsLoader.GlobalSettings.Services.AuthService;
            var clientUser = GlobalSettingsLoader.GlobalSettings.Users.ClientUser;

            options.Authority = authService.URL;
            options.RequireHttpsMetadata = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {

                //ValidateIssuer = true,
                //ValidIssuer = authService.URL,
                //ValidateAudience = true,
                //ValidateLifetime = true,
                //IssuerSigningKey = _securityKeyService.Key,
                //ValidAudiences = [chatService.AudiencesAccess.Name],
                //ValidateIssuerSigningKey = true
            };
        }
    }
}
