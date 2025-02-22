using CadoChat.Security.Authentication.Middlewaers;
using CadoChat.Security.Validation.ConfigLoad;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CadoChat.Auth.IdentityServer.Middlewaers
{
    public class IdentityServerURLMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationErrorMiddleware> _logger;

        public IdentityServerURLMiddleware(RequestDelegate next, ILogger<AuthenticationErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var authService = SecurityConfigLoader.SecurityConfig.AuthService;

            // Проверка заголовка X-Forwarded-For, добавленного API Gateway
            var host = context.Request.Headers["Host"].ToString();

            if ((string.IsNullOrEmpty(host) || authService.Contains(host))
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
