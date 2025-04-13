using CadoChat.Security.APIGateway.Services.Interfaces;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Authorization.Services.Interfaces;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Web.AspNetCore.WebConfigurations.Interfaces;
using CadoChat.Web.Common.Services.Interfaces;

namespace CadoChat.ChatService.Initialize
{
    public interface IChatBuilderInitializer : IApplicationBuilderInitializer
    {
        ILoggingConfiguration LoggingConfigurationService { get; }
        IAuthConfiguration ConfigurationAuthOptions { get; }
        IAuthorizationConfiguration ConfigurationAuthorizationService { get; }
        ISwaggerConfiguration SwaggerConfigurationService { get; }
        ICorsConfiguration CorsConfigurationService { get; }
        IAPIGatewayConfiguration ApiGatewayConfigurationService { get; }
    }
}
