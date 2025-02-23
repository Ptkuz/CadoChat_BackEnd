using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CadoChat.Security.Authentication.Middlewaers
{

    /// <summary>
    /// Middleware для обработки ошибок маршрутизации API Gateway
    /// </summary>
    public class AccessAPIGatewayMiddleware
    {
        /// <summary>
        /// Следующий обработчик запроса
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Логгер
        /// </summary>
        private readonly ILogger<AccessAPIGatewayMiddleware> _logger;

        /// <summary>
        /// Инициализировать Middleware для обработки ошибок маршрутизации API Gateway
        /// </summary>
        /// <param name="next">Следующий обработчик запроса</param>
        /// <param name="logger">Логгер</param>
        public AccessAPIGatewayMiddleware(RequestDelegate next, ILogger<AccessAPIGatewayMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Обработать запрос
        /// </summary>
        /// <param name="context">Http контекст</param>
        public async Task Invoke(HttpContext context)
        {
            var apiGateway = GlobalSettingsLoader.Instance!.GlobalSettings.Services.API_Gateway;

            var requestHost = context.Request.Host.Value;

            if (string.IsNullOrEmpty(requestHost) || !apiGateway.URL.Contains(requestHost))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Forbidden: Access only through API Gateway.");
                return;
            }
            var xForwardedFor = context.Connection?.RemoteIpAddress?.ToString();
            var xForwardedHost = context.Request.Host.Value?.ToString();
            context.Request.Headers.Append("X-Forwarded-For", xForwardedFor);
            context.Request.Headers.Append("X-Forwarded-Host", xForwardedHost);

            await _next(context);
        }
    }
}
