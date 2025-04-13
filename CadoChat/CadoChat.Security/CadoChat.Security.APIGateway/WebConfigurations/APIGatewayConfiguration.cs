using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;

namespace CadoChat.Security.APIGateway.WebConfigurations
{

    /// <summary>
    /// Конфигуратор API Gateway
    /// </summary>
    public static class APIGatewayConfiguration
    {

        /// <summary>
        /// Использовать сервис API Gateway
        /// </summary>
        /// <param name="applicationBuilder">Собранное приложение</param>
        public static void UseAPIGatewayService(this WebApplication webApplication)
        {
            var options = GetAPIGatewayOptions(webApplication);
            webApplication.UseForwardedHeaders(options);
        }

        /// <summary>
        /// Получить опции API Gateway
        /// </summary>
        /// <param name="app">Собранное приложение</param>
        /// <returns>Опции API Gateway</returns>
        private static ForwardedHeadersOptions GetAPIGatewayOptions(WebApplication app)
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
