using CadoChat.Security.Authentication.Services;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Validation.ConfigLoad;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.APIGateway.Manager.Services
{
    public class ConfigurationAuthAPIGatewayService : ConfigurationAuthService, IConfigurationAuthService
    {

        public override void AddService(WebApplicationBuilder webApplicationBuilder)
        {

            var authService = SecurityConfigLoader.SecurityConfig.AuthService;

            webApplicationBuilder.Services.AddAuthentication(AuthenticationScheme)
    .AddJwtBearer(AuthenticationScheme, options =>
    {
        options.Authority = authService;
        options.RequireHttpsMetadata = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidateIssuer = true,
            ValidIssuer = authService,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RequireSignedTokens = true
        };
    });
        }
    }
}
