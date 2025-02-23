using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.Security.Authentication.Services
{

    /// <summary>
    /// Конфигуратор аутентификации
    /// </summary>
    public abstract class AuthConfiguration : ConfigurationService, IAuthConfiguration
    {

        /// <summary>
        /// Сервис ключей безопасности
        /// </summary>
        protected readonly ISecurityKeyService<RsaSecurityKey> _securityKeyService;

        /// <summary>
        /// Схема аутентификации
        /// </summary>
        public virtual string AuthenticationScheme =>
            JwtBearerDefaults.AuthenticationScheme;

        /// <summary>
        /// Инициализировать конфигуратор аутентификации
        /// </summary>
        /// <param name="securityKeyService">Сервис ключей безопасности</param>
        public AuthConfiguration(ISecurityKeyService<RsaSecurityKey> securityKeyService)
        {
            _securityKeyService = securityKeyService;
        }

        /// <summary>
        /// Добавить сервис аутентификации
        /// </summary>
        /// <param name="webApplicationBuilder">Строитель приложения</param>
        public virtual void AddService(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddAuthentication(AuthenticationScheme)
                .AddJwtBearer(AuthenticationScheme, ConfigureAuthOptions);
        }

        /// <summary>
        /// Использовать сервис аутентификации
        /// </summary>
        /// <param name="applicationBuilder">Собранное приложение</param>
        public virtual void UseService(WebApplication applicationBuilder)
        {
            applicationBuilder.UseAuthentication();
        }

        /// <summary>
        /// Настроить опции аутентификации
        /// </summary>
        /// <param name="options">Опции аутентификации</param>
        protected abstract void ConfigureAuthOptions(JwtBearerOptions options);
    }
}
