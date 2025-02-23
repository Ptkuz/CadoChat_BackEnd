using CadoChat.Security.Common.Exceptions;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Web.Common.Services;
using CadoChat.Web.Common.Settings.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CadoChat.Security.Cors.Services
{
    public class CorsConfigurationService : ICorsConfigurationService
    {
        private ServiceConfig apiGateway = GlobalSettingsLoader.GetInstance().GlobalSettings.Services.API_Gateway;
        private string polilyName = "AllowGateway";

        public CorsConfigurationService()
        {

            if (string.IsNullOrEmpty(apiGateway.URL))
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
            corsPolicyBuilder.WithOrigins(apiGateway.URL)
                      .AllowAnyMethod()
                      .AllowAnyHeader();
        }
    }
}
