using CadoChat.Security.Common.Exceptions;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Web.Common;
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
    public class CorsConfigurationService : ConfigurationService, ICorsConfigurationService
    {
        private string polilyName = "AllowGateway";

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

            var apiGateway = GlobalSettings.Services.API_Gateway;

            corsPolicyBuilder.WithOrigins(apiGateway.URL)
                      .AllowAnyMethod()
                      .AllowAnyHeader();
        }
    }
}
