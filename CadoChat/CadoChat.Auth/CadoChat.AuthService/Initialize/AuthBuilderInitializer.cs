using CadoChat.Auth.IdentityServer.Services;
using CadoChat.AuthManager.WebConfigurations;
using CadoChat.AuthService.Services.Interfaces;
using CadoChat.IO.Json.Services.Interfaces;
using CadoChat.Security.APIGateway.Services;
using CadoChat.Security.APIGateway.Services.Interfaces;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Authorization.Services;
using CadoChat.Security.Authorization.Services.Interfaces;
using CadoChat.Security.Cors.Services;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.AspNetCore.WebConfigurations;
using CadoChat.Web.AspNetCore.WebConfigurations.Interfaces;
using CadoChat.Web.Common.Services;
using CadoChat.Web.Common.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.AuthService.Initialize
{
    public class AuthBuilderInitializer : IAuthBuilderInitializer
    {

        public ILoggingConfiguration LoggingConfigurationService { get; }

        public IAuthConfiguration ConfigurationAuthOptions { get; }

        public ISwaggerConfiguration SwaggerConfigurationService { get; }

        public ICorsConfiguration CorsConfigurationService { get; }

        public IAPIGatewayConfiguration ApiGatewayConfigurationService { get; }

        public IIdentityServiceConfiguration ConfigurationIdentityService { get; }

        public IAuthorizationConfiguration AuthorizationConfiguration { get; }

        private AuthBuilderInitializer(WebApplicationBuilder applicationBuilder,
            ISecurityKeyService<RsaSecurityKey>? securityKeyService, IFileSerializer fileSerializer)
        {

            var globalSettingsPath = applicationBuilder.Configuration["GlobalSettingsPath"];

            GlobalSettingsLoader.GetInstance(globalSettingsPath, fileSerializer);

            applicationBuilder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            LoggingConfigurationService = new LoggingConfiguration();
            ConfigurationAuthOptions = new AuthAuthConfiguration(securityKeyService);
            SwaggerConfigurationService = new AuthSwaggerConfiguration();
            CorsConfigurationService = new CorsConfiguration();
            ApiGatewayConfigurationService = new APIGatewayConfiguration();
            ConfigurationIdentityService = new IdentityServiceConfiguration(securityKeyService);
            AuthorizationConfiguration = new AuthorizationConfiguration();
        }

        public static IAuthBuilderInitializer CreateInstance(WebApplicationBuilder applicationBuilder,
            ISecurityKeyService<RsaSecurityKey>? securityKeyService, IFileSerializer fileSerializer)
        {

            var instance = new AuthBuilderInitializer(applicationBuilder, securityKeyService, fileSerializer);
            return instance;
        }
    }
}
