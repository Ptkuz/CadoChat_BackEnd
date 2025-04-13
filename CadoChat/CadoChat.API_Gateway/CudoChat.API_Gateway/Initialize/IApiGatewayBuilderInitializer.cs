using CadoChat.AuthService.Services.Interfaces;
using CadoChat.Security.APIGateway.Services.Interfaces;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Web.AspNetCore.WebConfigurations.Interfaces;
using CadoChat.Web.Common.Services.Interfaces;

namespace CudoChat.API_Gateway.Initialize
{
    public interface IApiGatewayBuilderInitializer : IApplicationBuilderInitializer
    {
        ILoggingConfiguration LoggingConfigurationService { get; }
        IAuthConfiguration ConfigurationAuthOptions { get; }
        ISwaggerConfiguration SwaggerConfigurationService { get; }
        ICorsConfiguration CorsConfigurationService { get; }
        IAPIGatewayConfiguration ApiGatewayConfigurationService { get; }
        IIdentityServiceConfiguration ConfigurationIdentityService { get; }
    }
}
