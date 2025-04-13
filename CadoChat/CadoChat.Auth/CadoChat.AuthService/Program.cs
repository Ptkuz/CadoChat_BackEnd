using CadoChat.Auth.EF.Entities;
using CadoChat.Auth.IdentityServer.Middlewaers;
using CadoChat.AuthManager.Services;
using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.AuthService.DI;
using CadoChat.AuthService.Initialize;
using CadoChat.AuthService.Services.Interfaces;
using CadoChat.IO.Json.Services;
using CadoChat.IO.Json.Services.Interfaces;
using CadoChat.Security.APIGateway.Services.Interfaces;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Authorization.Services.Interfaces;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Security.Validation.Services;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.AspNetCore.WebConfigurations.Interfaces;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ISecurityKeyService<RsaSecurityKey>, RsaSecurityKeyService>();
builder.Services.AddSingleton<IFileSerializer, FileSerializer>();


using var serviceProvider = builder.Services.BuildServiceProvider();

var securityKeyService = serviceProvider.GetRequiredService<ISecurityKeyService<RsaSecurityKey>>();
var fileSerializer = serviceProvider.GetRequiredService<IFileSerializer>();

var InitializedBuilder = AuthBuilderInitializer.CreateInstance(builder, securityKeyService, fileSerializer);

var services = builder.Services;

builder.Services.AddTransient<ITokenManagerService<User>, TokenManagerService<User>>();

// Настройка базы данных
services.AddDBContext(builder);

InitializedBuilder.SwaggerConfigurationService.AddService(builder);

// Добавляем IdentityServer
InitializedBuilder.ConfigurationIdentityService.AddService(builder);

services.AddAuthMappers();

InitializedBuilder.ConfigurationAuthOptions.AddService(builder);
InitializedBuilder.AuthorizationConfiguration.AddService(builder);

services.AddControllers();

InitializedBuilder.CorsConfigurationService.AddService(builder);
services.AddAuthRepositories();
services.AddAuthFacadeRepositories();
services.AddAuthUnitOfWork();

services.AddUserManager();

services.AddAuthMediatR();

var app = builder.Build();

InitializedBuilder.ApiGatewayConfigurationService.UseService(app);

app.UseMiddleware<IdentityServerURLMiddleware>();

InitializedBuilder.CorsConfigurationService.UseService(app);

InitializedBuilder.SwaggerConfigurationService.UseService(app);

app.UseRouting();
InitializedBuilder.ConfigurationIdentityService.UseService(app);
InitializedBuilder.ConfigurationAuthOptions.UseService(app);
InitializedBuilder.AuthorizationConfiguration.UseService(app);
app.MapControllers();

app.Run();
