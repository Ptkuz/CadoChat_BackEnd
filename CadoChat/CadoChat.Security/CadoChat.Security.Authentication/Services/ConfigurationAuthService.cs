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
    public abstract class ConfigurationAuthService : IConfigurationAuthService
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


        protected abstract void ConfigureAuthOptions(JwtBearerOptions options);
    }
}
