using CadoChat.Security.Common.Exceptions;
using CadoChat.Security.Cors.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CadoChat.Security.Cors.Services
{
    public class CorsConfigurationService : ICorsConfigurationService
    {
        private readonly IConfiguration _configuration;
        private string apiGateway;
        private string polilyName = "AllowGateway";

        public CorsConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
            apiGateway = apiGateway ?? _configuration["ServiceUrls:API_Gateway"]!;

            if (string.IsNullOrEmpty(apiGateway))
            {
                throw new ApiGatewayURLNotFoundException();
            }
        }

        public void AddService(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddCors(SetCorsOptions);
        }

        public void UseService(WebApplication applicationBuilder)
        {
            applicationBuilder.UseCors(polilyName);
        }

        private void SetCorsOptions(CorsOptions options)
        {
            options.AddPolicy(polilyName, SetCorsPolicy);
        }

        private void SetCorsPolicy(CorsPolicyBuilder corsPolicyBuilder)
        {
            corsPolicyBuilder.WithOrigins(apiGateway)
                      .AllowAnyMethod()
                      .AllowAnyHeader();
        }
    }
}
