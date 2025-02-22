using CadoChat.AuthManager;
using CadoChat.AuthService.Services.Interfaces;
using CadoChat.Security.Validation.SecutiryInfo;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.Auth.IdentityServer.Services
{
    public class ConfigurationIdentityService : IConfigurationIdentityService
    {

        private readonly RsaSecurityKey _rsaKey;
        private readonly SigningCredentials _signingCredentials;

        public ConfigurationIdentityService()
        {
            _rsaKey = RsaSecurityKeyService.GetKey();
            _signingCredentials = RsaSecurityKeyService.GetSigningCredentials(_rsaKey);
        }

        public void AddService(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddIdentityServer(options =>
            {
                options.KeyManagement.Enabled = false; // ❌ Отключает автоматическую генерацию ключей
            })
            .AddSigningCredential(_signingCredentials)
            .AddInMemoryClients(IdentityServerConfig.Clients)
            .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
            .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
    ;
        }

        public void UseService(WebApplication applicationBuilder)
        {
            applicationBuilder.UseIdentityServer();
        }
    }
}
