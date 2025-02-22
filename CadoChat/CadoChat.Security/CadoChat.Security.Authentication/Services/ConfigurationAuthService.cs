using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.SecutiryInfo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.Security.Authentication.Services
{
    public class ConfigurationAuthService : IConfigurationAuthService
    {
        public virtual string AuthenticationScheme => 
            JwtBearerDefaults.AuthenticationScheme;

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
            var authService = SecurityConfigLoader.SecurityConfig.AuthService;
            options.Authority = authService;
            options.RequireHttpsMetadata = true;

            var signingKey = RsaSecurityKeyService.GetKey();

            options.TokenValidationParameters = new TokenValidationParameters
            {

                ValidateIssuer = true,
                ValidIssuer = authService,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = signingKey, // Здесь используется ключ для подписи
                ValidAudience = AudiencesAccess.ChatApi, // Указание правильной аудитории
                ValidateIssuerSigningKey = true // Включаем валидацию подписи
            };
        }
    }
}
