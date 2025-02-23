using CadoChat.Security.Authentication.Services;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.APIGateway.Manager.Services
{
    public class ConfigurationAuthAPIGatewayService : ConfigurationAuthService, IConfigurationAuthService
    {
        public ConfigurationAuthAPIGatewayService(ISecurityKeyService<RsaSecurityKey> securityKeyService)
            : base(securityKeyService)
        {
        }

        public override void AddService(WebApplicationBuilder webApplicationBuilder)
        {

            var authService = GlobalSettingsLoader.GetInstance().GlobalSettings.Services.AuthService;

            webApplicationBuilder.Services.AddAuthentication(AuthenticationScheme)
    .AddJwtBearer(AuthenticationScheme, ConfigureAuthOptions);
        }

        protected override void ConfigureAuthOptions(JwtBearerOptions options)
        {

            var authService = GlobalSettingsLoader.GetInstance().GlobalSettings.Services.AuthService;

            options.Authority = authService.URL;
            options.RequireHttpsMetadata = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {

                ValidateIssuer = true,
                ValidIssuer = authService.URL,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                RequireSignedTokens = true
            };

        }
    }
}
