using CadoChat.Security.Common.Exceptions;
using CadoChat.Security.Cors.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Cors.Services
{
    public class CorsOptionsApiGateway : ICorsSettings
    {

        private readonly IConfiguration _configuration;

        private string apiGateway;

        private string polilyName = "AllowGateway";

        public CorsOptionsApiGateway(IConfiguration configuration) 
        {
            _configuration = configuration;

            apiGateway = apiGateway ?? _configuration["ServiceUrls:API_Gateway"]!;

            if (string.IsNullOrEmpty(apiGateway))
            {
                throw new ApiGatewayURLNotFoundException();
            }
        }

        public void SetCorsOptions(CorsOptions options)
        {

            options.AddPolicy(polilyName, SetCorsPolicy);
        }

        public void SetCorsPolicy(CorsPolicyBuilder corsPolicyBuilder)
        {

            corsPolicyBuilder.WithOrigins(apiGateway)
                      .AllowAnyMethod()
                      .AllowAnyHeader();
        }

        public void UseCors(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseCors(polilyName);
        }
    }
}
