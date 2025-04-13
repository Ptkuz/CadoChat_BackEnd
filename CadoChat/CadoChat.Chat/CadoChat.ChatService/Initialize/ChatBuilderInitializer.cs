using CadoChat.ChatManager.Services;
using CadoChat.IO.Json.Services.Interfaces;
using CadoChat.Security.APIGateway.Services;
using CadoChat.Security.APIGateway.Services.Interfaces;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Authorization.Services.Interfaces;
using CadoChat.Security.Cors.Services;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.AspNetCore.WebConfigurations;
using CadoChat.Web.AspNetCore.WebConfigurations.Interfaces;
using CadoChat.Web.Common.Services;
using CadoChat.Web.Common.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.ChatService.Initialize
{
    public class ChatBuilderInitializer : IChatBuilderInitializer
    {

        public ILoggingConfiguration LoggingConfigurationService { get; }

        public IAuthConfiguration ConfigurationAuthOptions { get; }

        public IAuthorizationConfiguration ConfigurationAuthorizationService { get; }

        public ISwaggerConfiguration SwaggerConfigurationService { get; }

        public ICorsConfiguration CorsConfigurationService { get; }

        public IAPIGatewayConfiguration ApiGatewayConfigurationService { get; }

        private ChatBuilderInitializer(WebApplicationBuilder applicationBuilder,
            ISecurityKeyService<RsaSecurityKey> securityKeyService, IFileSerializer fileSerializer)
        {

            var globalSettingsPath = applicationBuilder.Configuration["GlobalSettingsPath"];

            var globalSettings = GlobalSettingsLoader.GetInstance(globalSettingsPath, fileSerializer);
            var authService = globalSettings.GlobalSettings.Services.AuthService;
            var clientUser = globalSettings.GlobalSettings.Users.ClientUser;

            applicationBuilder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            LoggingConfigurationService = new LoggingConfiguration();
            ConfigurationAuthOptions = new ChatAuthConfiguration(securityKeyService);
            SwaggerConfigurationService = new ChatSwaggerConfiguration();
            CorsConfigurationService = new CorsConfiguration();
            ApiGatewayConfigurationService = new APIGatewayConfiguration();
            ConfigurationAuthorizationService = new ChatAuthorizationConfiguration();
        }

        public static IChatBuilderInitializer CreateInstance(WebApplicationBuilder applicationBuilder,
            ISecurityKeyService<RsaSecurityKey> securityKeyService, IFileSerializer fileSerializer)
        {

            var instance = new ChatBuilderInitializer(applicationBuilder, securityKeyService, fileSerializer);
            return instance;
        }
    }
}
