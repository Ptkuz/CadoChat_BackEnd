using CadoChat.ChatService.Initialize;
using CadoChat.IO.Json.Services;
using CadoChat.IO.Json.Services.Interfaces;
using CadoChat.Security.APIGateway.Services.Interfaces;
using CadoChat.Security.Authentication.Middlewaers;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Authorization.Services.Interfaces;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Security.Validation.Services;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.AspNetCore.Logging.Interfaces;
using CadoChat.Web.AspNetCore.Swagger.Interfaces;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ISecurityKeyService<RsaSecurityKey>, RsaSecurityKeyService>();
builder.Services.AddSingleton<IFileSerializer, FileSerializer>();
using var serviceProvider = builder.Services.BuildServiceProvider();

var securityKeyService = serviceProvider.GetRequiredService<ISecurityKeyService<RsaSecurityKey>>();
var fileSerializer = serviceProvider.GetRequiredService<IFileSerializer>();

var InitializedBuilder = ApplicationBuilderInitializer.CreateInstance(builder, securityKeyService, fileSerializer);

var loggingService = InitializedBuilder.GetService<ILoggingConfiguration>(typeof(ILoggingConfiguration));
var authService = InitializedBuilder.GetService<IAuthConfiguration>(typeof(IAuthConfiguration));
var swaggerService = InitializedBuilder.GetService<ISwaggerConfiguration>(typeof(ISwaggerConfiguration));
var corsService = InitializedBuilder.GetService<ICorsConfiguration>(typeof(ICorsConfiguration));
var apiGatewayService = InitializedBuilder.GetService<IAPIGatewayConfiguration>(typeof(IAPIGatewayConfiguration));
var authorizationService = InitializedBuilder.GetService<IAuthorizationConfiguration>(typeof(IAuthorizationConfiguration));

builder.Services.AddRouting();
loggingService.AddService(builder);

authService.AddService(builder);
authorizationService.AddService(builder);
swaggerService.AddService(builder);

builder.Services.AddControllers();

corsService.AddService(builder);

var app = builder.Build();

apiGatewayService.UseService(app);

app.UseMiddleware<AccessAPIGatewayMiddleware>();

corsService.UseService(app);
swaggerService.UseService(app);

app.UseRouting();

app.UseMiddleware<AuthenticationErrorMiddleware>();

authService.UseService(app);
authorizationService.UseService(app);

app.MapControllers();

app.Run();
