using CadoChat.Security.Authorization.Services.Interfaces;
using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CadoChat.Security.Authorization.Services
{

    /// <summary>
    /// Конфигуратор авторизации
    /// </summary>
    public class ConfigurationAuthorizationService : ConfigurationService, IConfigurationAuthorizationService
    {

        /// <summary>
        /// Добавить сервис авторизации
        /// </summary>
        /// <param name="webApplicationBuilder">Строитель приложения</param>
        public virtual void AddService(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddAuthorization();
        }

        /// <summary>
        /// Использовать сервис авторизации
        /// </summary>
        /// <param name="applicationBuilder">Собранное приложение</param>
        public virtual void UseService(WebApplication applicationBuilder)
        {
            applicationBuilder.UseAuthorization();
        }
    }
}
