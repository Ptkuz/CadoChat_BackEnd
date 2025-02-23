using CadoChat.Security.Authentication.Middlewaers;
using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CadoChat.Auth.IdentityServer.Middlewaers
{

    /// <summary>
    /// Middleware для проверки URL IdentityServer
    /// </summary>
    public class IdentityServerURLMiddleware
    {
        /// <summary>
        /// Следующий обработчик запроса
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Логгер
        /// </summary>
        private readonly ILogger<AuthenticationErrorMiddleware> _logger;

        /// <summary>
        /// Инициализировать Middleware для проверки URL IdentityServer
        /// </summary>
        /// <param name="next">Следующий обработчик запроса</param>
        /// <param name="logger">Логгер</param>
        public IdentityServerURLMiddleware(RequestDelegate next, ILogger<AuthenticationErrorMiddleware> logger)
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

            var authService = GlobalSettingsLoader.Instance!.GlobalSettings.Services.AuthService;

            // Проверка заголовка X-Forwarded-For, добавленного API Gateway
            var host = context.Request.Headers["Host"].ToString();

            if ((string.IsNullOrEmpty(host) || authService.URL.Contains(host))
            && (!context.Request.Path.HasValue && context.Request.Path.Value != "/.well-known/openid-configuration"))
            {


                // Ответ, если запрос не прошел через API Gateway
                context.Response.StatusCode = 403; // Forbidden
                await context.Response.WriteAsync("Forbidden: Access only through API Gateway.");
                return;
            }

            await _next(context);
        }
    }
}
