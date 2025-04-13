using CadoChat.AuthManager;
using CadoChat.Security.Validation.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.Auth.IdentityServer.WebConfigurations
{

    /// <summary>
    /// Конфигуратор IdentityServer
    /// </summary>
    public static class IdentityServiceConfiguration
    {


        /// <summary>
        /// Добавить сервис IdentityServer
        /// </summary>
        /// <param name="webApplicationBuilder">Строитель приложения</param>
        public static void AddIdentityService(this WebApplicationBuilder webApplicationBuilder, ISecurityKeyService<RsaSecurityKey> securityKeyService)
        {

            webApplicationBuilder.Services.AddIdentityServer(options =>
            {
                options.KeyManagement.Enabled = false;
            })
            .AddSigningCredential(securityKeyService.SigningCredentials)
            .AddInMemoryClients(IdentityServerConfig.GetClients())
            .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes())
            .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
    ;
        }

        /// <summary>
        /// Использовать сервис IdentityServer
        /// </summary>
        /// <param name="applicationBuilder">Собранное приложение</param>
        public static void UseIdentityService(this WebApplication applicationBuilder)
        {
            applicationBuilder.UseIdentityServer();
        }
    }
}
