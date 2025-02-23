using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.SecutiryInfo;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.Security.Authentication.Services
{
    public class InitAuthService : ConfigurationAuthService, IConfigurationAuthService
    {
        public InitAuthService(ISecurityKeyService<RsaSecurityKey> securityKeyService) :
            base(securityKeyService)
        {
        }

        public override void AddService(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddAuthentication(AuthenticationScheme)
                .AddJwtBearer(AuthenticationScheme, ConfigureAuthOptions);
        }

        private void ConfigureAuthOptions(JwtBearerOptions options)
        {

            var authService = GlobalSettingsLoader.GetInstance().GlobalSettings.Services.AuthService;

            options.Authority = authService.URL;
            options.RequireHttpsMetadata = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {

                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = _securityKeyService.Key, 
                ValidIssuer = authService.URL,
                ValidateIssuerSigningKey = true
            };
        }
    }
}
