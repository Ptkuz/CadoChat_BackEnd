using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Authentication.Middlewaers
{
    public class ReplaceRequestHostMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationErrorMiddleware> _logger;
        private readonly IConfiguration _configuration;

        public ReplaceRequestHostMiddleware(RequestDelegate next, ILogger<AuthenticationErrorMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var apiOrigin = context.Request.Headers["Origin"].ToString();
            var apiGateway = _configuration["ServiceUrls:API_Gateway"]!;

            if (!apiOrigin.IsNullOrEmpty() && apiGateway.Equals(apiOrigin))
            {
                string url = apiOrigin;
                var uri = new Uri(url);
                string hostAndPort = $"{uri.Host}:{uri.Port}";
                context.Request.Host = new HostString(hostAndPort);
            }

            await _next(context);
        }

    }
}
