﻿using CadoChat.Security.APIGateway.Services;
using CadoChat.Security.APIGateway.Services.Interfaces;
using CadoChat.Security.Authentication.Services;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Cors.Services;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Web.AspNetCore.Initialize.Interfaces;
using CadoChat.Web.AspNetCore.Logging;
using CadoChat.Web.AspNetCore.Logging.Interfaces;
using CadoChat.Web.AspNetCore.Swagger;
using CadoChat.Web.AspNetCore.Swagger.Interfaces;

namespace CadoChat.ChatService.Initialize
{
    public class ApplicationBuilderInitializer : IApplicationBuilderInitializer
    {

        private readonly ILoggingConfigurationService _loggingConfigurationService;
        private readonly IConfigurationAuthService _configurationAuthOptions;
        private readonly ISwaggerConfigurationService _swaggerConfigurationService;
        private readonly ICorsConfigurationService _corsConfigurationService;
        private readonly IAPIGatewayConfigurationService _apiGatewayConfigurationService;

        private readonly WebApplicationBuilder _applicationBuilder;

        private ApplicationBuilderInitializer(WebApplicationBuilder applicationBuilder)
        {
            SecurityConfigLoader.Init(applicationBuilder.Configuration);
            applicationBuilder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _applicationBuilder = applicationBuilder;

            var configuration = _applicationBuilder.Configuration;

            _loggingConfigurationService = new LoggingConfigurationService();
            _configurationAuthOptions = new ConfigurationAuthService();
            _swaggerConfigurationService = new SwaggerConfigurationService();
            _corsConfigurationService = new CorsConfigurationService(configuration);
            _apiGatewayConfigurationService = new APIGatewayConfigurationService();
        }

        public static IApplicationBuilderInitializer CreateInstance(WebApplicationBuilder applicationBuilder)
        {

            var instance = new ApplicationBuilderInitializer(applicationBuilder);
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
                default:
                    throw new InvalidCastException($"Cannot cast {type} to {typeof(TService)}");
            }
        }
    }
}
