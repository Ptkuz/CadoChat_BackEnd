using CadoChat.AuthManager;
using CadoChat.AuthService.Services.Interfaces;
using CadoChat.Security.Validation.SecutiryInfo;
using CadoChat.Security.Validation.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.Auth.IdentityServer.Services
{
    public class ConfigurationIdentityService : IConfigurationIdentityService
    {

        private readonly ISecurityKeyService<RsaSecurityKey> _securityKeyService;

        public ConfigurationIdentityService(ISecurityKeyService<RsaSecurityKey> securityKeyService)
        {
            _securityKeyService = securityKeyService;
        }

        public void AddService(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddIdentityServer(options =>
            {
                options.KeyManagement.Enabled = false; 
            })
            .AddSigningCredential(_securityKeyService.SigningCredentials)
            .AddInMemoryClients(IdentityServerConfig.GetClients())
            .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes())
            .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
    ;
        }

        public void UseService(WebApplication applicationBuilder)
        {
            applicationBuilder.UseIdentityServer();
        }
    }
}
