using CadoChat.AuthManager;
using CadoChat.AuthService.Services.Interfaces;
using CadoChat.Security.Validation.SecutiryInfo;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.Auth.IdentityServer.Services
{

    /// <summary>
    /// Конфигуратор IdentityServer
    /// </summary>
    public class IdentityServiceConfiguration : ConfigurationService, IIdentityServiceConfiguration
    {

        /// <summary>
        /// Сервис ключей безопасности
        /// </summary>
        private readonly ISecurityKeyService<RsaSecurityKey> _securityKeyService;

        /// <summary>
        /// Инициализировать конфигуратор IdentityServer
        /// </summary>
        /// <param name="securityKeyService">Сервис ключей безопасности</param>
        public IdentityServiceConfiguration(ISecurityKeyService<RsaSecurityKey> securityKeyService)
        {
            _securityKeyService = securityKeyService;
        }

        /// <summary>
        /// Добавить сервис IdentityServer
        /// </summary>
        /// <param name="webApplicationBuilder">Строитель приложения</param>
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

        /// <summary>
        /// Использовать сервис IdentityServer
        /// </summary>
        /// <param name="applicationBuilder">Собранное приложение</param>
        public void UseService(WebApplication applicationBuilder)
        {
            applicationBuilder.UseIdentityServer();
        }
    }
}
