using CadoChat.ChatManager.Services;
using CadoChat.IO.Json.Services.Interfaces;
using CadoChat.Security.APIGateway.Services;
using CadoChat.Security.APIGateway.Services.Interfaces;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Authorization.Services.Interfaces;
using CadoChat.Security.Cors.Services;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.AspNetCore.Logging;
using CadoChat.Web.AspNetCore.Logging.Interfaces;
using CadoChat.Web.AspNetCore.Swagger;
using CadoChat.Web.AspNetCore.Swagger.Interfaces;
using CadoChat.Web.Common.Services;
using CadoChat.Web.Common.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.ChatService.Initialize
{
    public class ApplicationBuilderInitializer : IApplicationBuilderInitializer
    {

        private readonly ILoggingConfiguration _loggingConfigurationService;
        private readonly IAuthConfiguration _configurationAuthOptions;
        private readonly IAuthorizationConfiguration _configurationAuthorizationService;
        private readonly ISwaggerConfiguration _swaggerConfigurationService;
        private readonly ICorsConfiguration _corsConfigurationService;
        private readonly IAPIGatewayConfiguration _apiGatewayConfigurationService;

        private readonly WebApplicationBuilder _applicationBuilder;

        private ApplicationBuilderInitializer(WebApplicationBuilder applicationBuilder,
            ISecurityKeyService<RsaSecurityKey> securityKeyService, IFileSerializer fileSerializer)
        {

            var globalSettingsPath = applicationBuilder.Configuration["GlobalSettingsPath"];

            var globalSettings = GlobalSettingsLoader.GetInstance(globalSettingsPath, fileSerializer);
            var authService = globalSettings.GlobalSettings.Services.AuthService;
            var clientUser = globalSettings.GlobalSettings.Users.ClientUser;

            applicationBuilder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _applicationBuilder = applicationBuilder;

            var configuration = _applicationBuilder.Configuration;

            _loggingConfigurationService = new LoggingConfiguration();
            _configurationAuthOptions = new ChatAuthConfiguration(securityKeyService);
            _swaggerConfigurationService = new ChatSwaggerConfiguration();
            _corsConfigurationService = new CorsConfiguration();
            _apiGatewayConfigurationService = new APIGatewayConfiguration();
            _configurationAuthorizationService = new ChatAuthorizationConfiguration();
        }

        public static IApplicationBuilderInitializer CreateInstance(WebApplicationBuilder applicationBuilder,
            ISecurityKeyService<RsaSecurityKey> securityKeyService, IFileSerializer fileSerializer)
        {

            var instance = new ApplicationBuilderInitializer(applicationBuilder, securityKeyService, fileSerializer);
            return instance;
        }


        public TService GetService<TService>(Type type)
            where TService : IConfigurationService
        {

            switch (type)
            {
                case Type t when t == typeof(ILoggingConfiguration):
                    return (TService)_loggingConfigurationService;
                case Type t when t == typeof(IAuthConfiguration):
                    return (TService)_configurationAuthOptions;
                case Type t when t == typeof(ISwaggerConfiguration):
                    return (TService)_swaggerConfigurationService;
                case Type t when t == typeof(ICorsConfiguration):
                    return (TService)_corsConfigurationService;
                case Type t when t == typeof(IAPIGatewayConfiguration):
                    return (TService)_apiGatewayConfigurationService;
                case Type t when t == typeof(IAuthorizationConfiguration):
                    return (TService)_configurationAuthorizationService;
                default:
                    throw new InvalidCastException($"Cannot cast {type} to {typeof(TService)}");
            }
        }
    }
}
