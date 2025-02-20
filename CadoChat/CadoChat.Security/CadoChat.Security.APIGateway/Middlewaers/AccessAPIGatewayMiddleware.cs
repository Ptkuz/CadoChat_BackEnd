using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CadoChat.Security.Authentication.Middlewaers
{
    public class AccessAPIGatewayMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AccessAPIGatewayMiddleware> _logger;
        private readonly IConfiguration _configuration;

        public AccessAPIGatewayMiddleware(RequestDelegate next, ILogger<AccessAPIGatewayMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestHost = context.Request.Host.Value;
            var apiGateway = _configuration["ServiceUrls:API_Gateway"]!;

            if (string.IsNullOrEmpty(requestHost) || !apiGateway.Contains(requestHost))
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
