using CadoChat.AuthService.Initialize;
using CadoChat.AuthService.Services.Interfaces;
using CadoChat.IO.Json.Services;
using CadoChat.IO.Json.Services.Interfaces;
using CadoChat.Security.APIGateway.Services.Interfaces;
using CadoChat.Security.Authentication.Middlewaers;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Security.Validation.Services;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.AspNetCore.Logging.Interfaces;
using CadoChat.Web.AspNetCore.Swagger.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ISecurityKeyService<RsaSecurityKey>, RsaSecurityKeyService>();
builder.Services.AddSingleton<IFileSerializer, FileSerializer>();


using var serviceProvider = builder.Services.BuildServiceProvider();

var securityKeyService = serviceProvider.GetRequiredService<ISecurityKeyService<RsaSecurityKey>>();
var fileSerializer = serviceProvider.GetRequiredService<IFileSerializer>();

var InitializedBuilder = ApplicationBuilderInitializer.CreateInstance(builder, securityKeyService, fileSerializer);


var loggingService = InitializedBuilder.GetService<ILoggingConfigurationService>(typeof(ILoggingConfigurationService));
var authApiGatewayService = InitializedBuilder.GetService<IConfigurationAuthService>(typeof(IConfigurationAuthService));
var swaggerService = InitializedBuilder.GetService<ISwaggerConfigurationService>(typeof(ISwaggerConfigurationService));
var corsService = InitializedBuilder.GetService<ICorsConfigurationService>(typeof(ICorsConfigurationService));
var apiGatewayService = InitializedBuilder.GetService<IAPIGatewayConfigurationService>(typeof(IAPIGatewayConfigurationService));
var identityServerService = InitializedBuilder.GetService<IConfigurationIdentityService>(typeof(IConfigurationIdentityService));

builder.Services.AddRouting();

authApiGatewayService.AddService(builder);

swaggerService.AddService(builder);

builder.Services.AddOcelot();
builder.Services.AddAuthorization();

corsService.AddService(builder);

builder.Services.AddHeaderRouting();

var app = builder.Build();

apiGatewayService.UseService(app);

app.UseMiddleware<AccessAPIGatewayMiddleware>();

swaggerService.UseService(app);

corsService.UseService(app);

app.UseHttpsRedirection();

app.UseRouting();
authApiGatewayService.UseService(app);
app.UseAuthorization();

await app.UseOcelot();

app.Run();
