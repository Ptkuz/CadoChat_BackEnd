using CadoChat.AuthService.Services.Interfaces;
using CadoChat.Security.APIGateway.Services.Interfaces;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Authorization.Services.Interfaces;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Web.AspNetCore.WebConfigurations.Interfaces;
using CadoChat.Web.Common.Services.Interfaces;

namespace CadoChat.AuthService.Initialize
{
    public interface IAuthBuilderInitializer : IApplicationBuilderInitializer
    {
        ILoggingConfiguration LoggingConfigurationService { get; }
        IAuthConfiguration ConfigurationAuthOptions { get; }
        ISwaggerConfiguration SwaggerConfigurationService { get; }
        ICorsConfiguration CorsConfigurationService { get; }
        IAPIGatewayConfiguration ApiGatewayConfigurationService { get; }
        IIdentityServiceConfiguration ConfigurationIdentityService { get; }
        IAuthorizationConfiguration AuthorizationConfiguration { get; }
    }
}
