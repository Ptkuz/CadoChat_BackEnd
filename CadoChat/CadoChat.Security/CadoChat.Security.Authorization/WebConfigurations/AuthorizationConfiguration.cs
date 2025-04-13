using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CadoChat.Security.Authorization.WebConfigurations
{

    /// <summary>
    /// Конфигуратор авторизации
    /// </summary>
    public static class AuthorizationConfiguration
    {

        /// <summary>
        /// Добавить сервис авторизации
        /// </summary>
        /// <param name="webApplicationBuilder">Строитель приложения</param>
        public static void AddAuthorizationService(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddAuthorization();
        }

        /// <summary>
        /// Использовать сервис авторизации
        /// </summary>
        /// <param name="applicationBuilder">Собранное приложение</param>
        public static void UseAuthorizationService(this WebApplication applicationBuilder)
        {
            applicationBuilder.UseAuthorization();
        }
    }
}
