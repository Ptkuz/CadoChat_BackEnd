using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.SecutiryInfo;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.Common.Services;
using CadoChat.Web.Common.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.Security.Authentication.Services
{
    public class ConfigurationAuthService : IConfigurationAuthService
    {

        protected readonly ISecurityKeyService<RsaSecurityKey> _securityKeyService;

        public virtual string AuthenticationScheme => 
            JwtBearerDefaults.AuthenticationScheme;

        public ConfigurationAuthService(ISecurityKeyService<RsaSecurityKey> securityKeyService)
        {
            _securityKeyService = securityKeyService;
        }

        public virtual void AddService(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddAuthentication(AuthenticationScheme)
                .AddJwtBearer(AuthenticationScheme, ConfigureAuthOptions);
        }

        public virtual void UseService(WebApplication applicationBuilder)
        {
            applicationBuilder.UseAuthentication();
        }


        private void ConfigureAuthOptions(JwtBearerOptions options)
        {
            var globalSettings = GlobalSettingsLoader.GetInstance();
            var authService = globalSettings.GlobalSettings.Services.AuthService;
            var clientUser = globalSettings.GlobalSettings.Users.ClientUser;

            options.Authority = authService.URL;
            options.RequireHttpsMetadata = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {

                ValidateIssuer = true,
                ValidIssuer = authService.URL,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = _securityKeyService.Key,
                ValidAudiences = [ authService.AudiencesAccess.Name ], 
                ValidateIssuerSigningKey = true 
            };
        }
    }
}
