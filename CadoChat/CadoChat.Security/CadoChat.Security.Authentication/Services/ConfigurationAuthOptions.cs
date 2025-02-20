using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.SecutiryInfo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Authentication.Services
{
    public class ConfigurationAuthOptions : IConfigurationAuthOptions
    {
        public void ConfigureAuthOptions(JwtBearerOptions options)
        {
            var authService = SecurityConfigLoader.SecurityConfig.AuthService;
            options.Authority = authService;
            options.RequireHttpsMetadata = true;

            var signingKey = RsaSecurityKeyService.GetKey();

            options.TokenValidationParameters = new TokenValidationParameters
            {

                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = signingKey, // Здесь используется ключ для подписи
                ValidIssuer = authService, // Указание правильного издателя
                ValidAudience = AudiencesAccess.ChatApi, // Указание правильной аудитории
                ValidateIssuerSigningKey = true // Включаем валидацию подписи
            };
        }
    }
}
