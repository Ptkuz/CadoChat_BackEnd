using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.SecutiryInfo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.Security.Authentication.Services
{
    public class InitAuthService : ConfigurationAuthService, IConfigurationAuthService
    {
        public override void AddService(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddAuthentication(AuthenticationScheme)
                .AddJwtBearer(AuthenticationScheme, ConfigureAuthOptions);
        }

        private void ConfigureAuthOptions(JwtBearerOptions options)
        {

            var rsaKey = RsaSecurityKeyService.GetKey();

            var authService = SecurityConfigLoader.SecurityConfig.AuthService;
            options.Authority = authService;
            options.Authority = authService;
            options.RequireHttpsMetadata = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {

                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = rsaKey, // Здесь используется ключ для подписи
                ValidIssuer = authService,
                ValidateIssuerSigningKey = true // Включаем валидацию подписи
            };
        }
    }
}
