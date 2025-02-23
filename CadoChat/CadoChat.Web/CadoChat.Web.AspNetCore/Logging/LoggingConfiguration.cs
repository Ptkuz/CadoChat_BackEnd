using CadoChat.Web.AspNetCore.Logging.Interfaces;
using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.AspNetCore.Logging
{

    /// <summary>
    /// Конфигуратор логирования
    /// </summary>
    public class LoggingConfiguration : ConfigurationService, ILoggingConfiguration
    {

        /// <summary>
        /// Добавить сервис логирования
        /// </summary>
        /// <param name="webApplicationBuilder">Строитель приложения</param>
        public void AddService(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddLogging(ConfigureLogging);
        }

        /// <summary>
        /// Использовать сервис логирования
        /// </summary>
        /// <param name="applicationBuilder">Собранное приложение</param>
        /// <exception cref="NotImplementedException">Сервис не используется</exception>
        public void UseService(WebApplication applicationBuilder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Настройка логирования
        /// </summary>
        /// <param name="loggingBuilder">Строитель логирования</param>
        private void ConfigureLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        }
    }
}
