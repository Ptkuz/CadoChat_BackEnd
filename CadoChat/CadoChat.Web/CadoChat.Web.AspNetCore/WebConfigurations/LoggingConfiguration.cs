using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CadoChat.Web.AspNetCore.WebConfigurations
{

    /// <summary>
    /// Конфигуратор логирования
    /// </summary>
    public static class LoggingConfiguration
    {

        /// <summary>
        /// Добавить сервис логирования
        /// </summary>
        /// <param name="webApplicationBuilder">Строитель приложения</param>
        public static void AddLoggingService(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddLogging(ConfigureLogging);
        }

        /// <summary>
        /// Настройка логирования
        /// </summary>
        /// <param name="loggingBuilder">Строитель логирования</param>
        private static void ConfigureLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        }
    }
}
