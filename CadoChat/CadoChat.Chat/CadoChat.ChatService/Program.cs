using CadoChat.ChatService.Initialize;
using CadoChat.IO.Json.Services;
using CadoChat.IO.Json.Services.Interfaces;
using CadoChat.Security.Authentication.Middlewaers;
using CadoChat.Security.Validation.Services;
using CadoChat.Security.Validation.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ISecurityKeyService<RsaSecurityKey>, RsaSecurityKeyService>();
builder.Services.AddSingleton<IFileSerializer, FileSerializer>();
using var serviceProvider = builder.Services.BuildServiceProvider();

var securityKeyService = serviceProvider.GetRequiredService<ISecurityKeyService<RsaSecurityKey>>();
var fileSerializer = serviceProvider.GetRequiredService<IFileSerializer>();

var InitializedBuilder = ChatBuilderInitializer.CreateInstance(builder, securityKeyService, fileSerializer);

builder.Services.AddRouting();

InitializedBuilder.LoggingConfigurationService.AddService(builder);
InitializedBuilder.ConfigurationAuthOptions.AddService(builder);
InitializedBuilder.ConfigurationAuthorizationService.AddService(builder);
InitializedBuilder.SwaggerConfigurationService.AddService(builder);

builder.Services.AddControllers();

InitializedBuilder.CorsConfigurationService.AddService(builder);

var app = builder.Build();

InitializedBuilder.ApiGatewayConfigurationService.UseService(app);

app.UseMiddleware<AccessAPIGatewayMiddleware>();

InitializedBuilder.CorsConfigurationService.UseService(app);
InitializedBuilder.SwaggerConfigurationService.UseService(app);

app.UseRouting();

app.UseMiddleware<AuthenticationErrorMiddleware>();

InitializedBuilder.ConfigurationAuthOptions.UseService(app);
InitializedBuilder.ConfigurationAuthorizationService.UseService(app);

app.MapControllers();

app.Run();
