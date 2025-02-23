using CadoChat.Auth.IdentityServer.Services;
using CadoChat.AuthService.Services;
using CadoChat.AuthService.Services.Interfaces;
using CadoChat.IO.Json.Services.Interfaces;
using CadoChat.Security.APIGateway.Services;
using CadoChat.Security.APIGateway.Services.Interfaces;
using CadoChat.Security.Authentication.Services;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Cors.Services;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.AspNetCore.Initialize.Interfaces;
using CadoChat.Web.AspNetCore.Logging;
using CadoChat.Web.AspNetCore.Logging.Interfaces;
using CadoChat.Web.AspNetCore.Swagger;
using CadoChat.Web.AspNetCore.Swagger.Interfaces;
using CadoChat.Web.Common.Services;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.AuthService.Initialize
{
    public class ApplicationBuilderInitializer : IApplicationBuilderInitializer
    {

        private readonly ILoggingConfigurationService _loggingConfigurationService;
        private readonly IConfigurationAuthService _configurationAuthOptions;
        private readonly ISwaggerConfigurationService _swaggerConfigurationService;
        private readonly ICorsConfigurationService _corsConfigurationService;
        private readonly IAPIGatewayConfigurationService _apiGatewayConfigurationService;
        private readonly IConfigurationIdentityService _configurationIdentityService;

        private readonly WebApplicationBuilder _applicationBuilder;

        private ApplicationBuilderInitializer(WebApplicationBuilder applicationBuilder,
            ISecurityKeyService<RsaSecurityKey>? securityKeyService, IFileSerializer fileSerializer)
        {

            var globalSettingsPath = applicationBuilder.Configuration["GlobalSettingsPath"];

            GlobalSettingsLoader.GetInstance(globalSettingsPath, fileSerializer);

            applicationBuilder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _applicationBuilder = applicationBuilder;

            var configuration = _applicationBuilder.Configuration;

            _loggingConfigurationService = new LoggingConfigurationService();
            _configurationAuthOptions = new InitAuthService(securityKeyService);
            _swaggerConfigurationService = new SwaggerConfigurationService();
            _corsConfigurationService = new CorsConfigurationService(configuration);
            _apiGatewayConfigurationService = new APIGatewayConfigurationService();
            _configurationIdentityService = new ConfigurationIdentityService(securityKeyService);
        }

        public static IApplicationBuilderInitializer CreateInstance(WebApplicationBuilder applicationBuilder, 
            ISecurityKeyService<RsaSecurityKey>? securityKeyService, IFileSerializer fileSerializer)
        {

            var instance = new ApplicationBuilderInitializer(applicationBuilder, securityKeyService, fileSerializer);
            return instance;
        }


        public TService GetService<TService>(Type type) 
            where TService : class
        {
            switch (type)
            {
                case Type t when t == typeof(ILoggingConfigurationService):
                    return (TService)_loggingConfigurationService;
                case Type t when t == typeof(IConfigurationAuthService):
                    return (TService)_configurationAuthOptions;
                case Type t when t == typeof(ISwaggerConfigurationService):
                    return (TService)_swaggerConfigurationService;
                case Type t when t == typeof(ICorsConfigurationService):
                    return (TService)_corsConfigurationService;
                case Type t when t == typeof(IAPIGatewayConfigurationService):
                    return (TService)_apiGatewayConfigurationService;
                case Type t when t == typeof(IConfigurationIdentityService):
                    return (TService)_configurationIdentityService;
                default:
                    throw new InvalidCastException($"Cannot cast {type} to {typeof(TService)}");
            }
        }
    }
}
