using CadoChat.Security.Common.Exceptions;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Web.Common.Services;
using CadoChat.Web.Common.Settings;
using CadoChat.Web.Common.Settings.Service.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;

namespace CadoChat.Security.Cors.Services
{

    /// <summary>
    /// Конфигуратор CORS
    /// </summary>
    public class CorsConfigurationService : ConfigurationService, ICorsConfigurationService
    {
        /// <summary>
        /// Имя политики CORS
        /// </summary>
        protected string polilyName = "AllowGateway";

        /// <summary>
        /// Добавить сервис
        /// </summary>
        /// <param name="webApplicationBuilder">Строитель приложения</param>
        public virtual void AddService(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddCors(SetCorsOptions);
        }

        /// <summary>
        /// Использовать сервис
        /// </summary>
        /// <param name="applicationBuilder">Собранное приложение</param>
        public virtual void UseService(WebApplication applicationBuilder)
        {
            applicationBuilder.UseCors(polilyName);
        }

        /// <summary>
        /// Установить опции CORS
        /// </summary>
        /// <param name="options">Опции CORS</param>
        private void SetCorsOptions(CorsOptions options)
        {
            options.AddPolicy(polilyName, SetCorsPolicy);
        }

        /// <summary>
        /// Установить политику CORS
        /// </summary>
        /// <param name="corsPolicyBuilder">Политика CORS</param>
        private void SetCorsPolicy(CorsPolicyBuilder corsPolicyBuilder)
        {

            var apiGateway = GlobalSettings.Services.API_Gateway;

            corsPolicyBuilder.WithOrigins(apiGateway.URL)
                      .AllowAnyMethod()
                      .AllowAnyHeader();
        }
    }
}
