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

        private readonly IConfigurationLoggings _configurationLoggings;
        private readonly IConfigurationAuthOptions _configurationAuthOptions;
        private readonly ISwaggerSettings _swaggerSettings;
        private readonly ICorsSettings _corsSettings;

        private readonly WebApplicationBuilder _applicationBuilder;

        private ApplicationBuilderInitializer(WebApplicationBuilder applicationBuilder)
        {
            _applicationBuilder = applicationBuilder;

            var configuration = _applicationBuilder.Configuration;

            _configurationLoggings = new ConfigurationLoggings();
            _configurationAuthOptions = new ConfigurationAuthOptions();
            _swaggerSettings = new SwaggerSettings();
            _corsSettings = new CorsOptionsApiGateway(configuration);
        }

        public static IApplicationBuilderInitializer CreateInstance(WebApplicationBuilder applicationBuilder)
        {
            SecurityConfigLoader.Init(applicationBuilder.Configuration);
            applicationBuilder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var instance = new ApplicationBuilderInitializer(applicationBuilder);
            return instance;
        }


        public TService GetService<TService>(Type type) 
            where TService : class
        {

            switch (type) 
            {
                case Type t when t == typeof(IConfigurationLoggings):
                    return (TService)_configurationLoggings;
                case Type t when t == typeof(IConfigurationAuthOptions):
                    return (TService)_configurationAuthOptions;
                case Type t when t == typeof(ISwaggerSettings):
                    return (TService)_swaggerSettings;
                case Type t when t == typeof(ICorsSettings):
                    return (TService)_corsSettings;
                default:
                    throw new InvalidCastException($"Cannot cast {type} to {typeof(TService)}");
            }
        }
    }
}
