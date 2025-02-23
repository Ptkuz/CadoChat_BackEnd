using CadoChat.Security.APIGateway.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.APIGateway.Services
{

    /// <summary>
    /// Конфигуратор API Gateway
    /// </summary>
    public class APIGatewayConfiguration : IAPIGatewayConfiguration
    {

        /// <summary>
        /// Добавить сервис API Gateway
        /// </summary>
        /// <param name="webApplicationBuilder">Строитель приложения</param>
        /// <exception cref="NotImplementedException">Сервис не может быть добавлен</exception>
        public void AddService(WebApplicationBuilder webApplicationBuilder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Использовать сервис API Gateway
        /// </summary>
        /// <param name="applicationBuilder">Собранное приложение</param>
        public void UseService(WebApplication applicationBuilder)
        {
            var options = GetAPIGatewayOptions(applicationBuilder);
            applicationBuilder.UseForwardedHeaders(options);
        }

        /// <summary>
        /// Получить опции API Gateway
        /// </summary>
        /// <param name="app">Собранное приложение</param>
        /// <returns>Опции API Gateway</returns>
        private ForwardedHeadersOptions GetAPIGatewayOptions(WebApplication app)
        {
            var forwardedHeadersOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedHost,
                RequireHeaderSymmetry = false,
                ForwardLimit = null
            };
            forwardedHeadersOptions.KnownNetworks.Clear();
            forwardedHeadersOptions.KnownProxies.Clear();

            return forwardedHeadersOptions;
        }
    }
}
