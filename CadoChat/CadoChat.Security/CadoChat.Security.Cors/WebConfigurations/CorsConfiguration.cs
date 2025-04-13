using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CadoChat.Security.Cors.WebConfigurations
{

    /// <summary>
    /// Конфигуратор CORS
    /// </summary>
    public static class CorsConfiguration
    {

        private static GlobalSettingsLoader GlobalSettingsLoader
            => GlobalSettingsLoader.Instance ??
            throw new ArgumentNullException(nameof(GlobalSettingsLoader.Instance));

        /// <summary>
        /// Имя политики CORS
        /// </summary>
        private static string polilyName = "AllowGateway";

        /// <summary>
        /// Добавить сервис
        /// </summary>
        /// <param name="webApplicationBuilder">Строитель приложения</param>
        public static void AddCorsService(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddCors(SetCorsOptions);
        }

        /// <summary>
        /// Использовать сервис
        /// </summary>
        /// <param name="applicationBuilder">Собранное приложение</param>
        public static void UseCorsService(this WebApplication applicationBuilder)
        {
            applicationBuilder.UseCors(polilyName);
        }

        /// <summary>
        /// Установить опции CORS
        /// </summary>
        /// <param name="options">Опции CORS</param>
        private static void SetCorsOptions(CorsOptions options)
        {
            options.AddPolicy(polilyName, SetCorsPolicy);
        }

        /// <summary>
        /// Установить политику CORS
        /// </summary>
        /// <param name="corsPolicyBuilder">Политика CORS</param>
        private static void SetCorsPolicy(CorsPolicyBuilder corsPolicyBuilder)
        {

            var apiGateway = GlobalSettingsLoader.GlobalSettings.Services.API_Gateway;

            corsPolicyBuilder.WithOrigins(apiGateway.URL)
                      .AllowAnyMethod()
                      .AllowAnyHeader();
        }
    }
}
