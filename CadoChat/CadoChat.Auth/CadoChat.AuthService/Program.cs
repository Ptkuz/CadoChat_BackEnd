using CadoChat.Auth.EF.Entities;
using CadoChat.Auth.IdentityServer.Middlewaers;
using CadoChat.Auth.IdentityServer.WebConfigurations;
using CadoChat.AuthManager.Services;
using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.AuthManager.WebConfigurations;
using CadoChat.AuthService.DI;
using CadoChat.AuthService.Initialize;
using CadoChat.IO.Json.Services;
using CadoChat.IO.Json.Services.Interfaces;
using CadoChat.Security.APIGateway.WebConfigurations;
using CadoChat.Security.Authorization.WebConfigurations;
using CadoChat.Security.Cors.WebConfigurations;
using CadoChat.Security.Validation.Services;
using CadoChat.Security.Validation.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ISecurityKeyService<RsaSecurityKey>, RsaSecurityKeyService>();
builder.Services.AddSingleton<IFileSerializer, FileSerializer>();


using var serviceProvider = builder.Services.BuildServiceProvider();

var securityKeyService = serviceProvider.GetRequiredService<ISecurityKeyService<RsaSecurityKey>>();
var fileSerializer = serviceProvider.GetRequiredService<IFileSerializer>();

builder.InitWebApplicationSettings(securityKeyService, fileSerializer);

var services = builder.Services;

builder.Services.AddTransient<ITokenManagerService<User>, TokenManagerService<User>>();

// Настройка базы данных
services.AddDBContext(builder);

builder.AddAuthSwaggerService();

// Добавляем IdentityServer
builder.AddIdentityService(securityKeyService);

services.AddAuthMappers();

builder.AddAuthenticationService();
builder.AddAuthorizationService();

services.AddControllers();

builder.AddCorsService();
services.AddAuthRepositories();
services.AddAuthFacadeRepositories();
services.AddAuthUnitOfWork();

services.AddUserManager();

services.AddAuthMediatR();

var app = builder.Build();

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});
app.UseAPIGatewayService();

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

app.UseMiddleware<IdentityServerURLMiddleware>();

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

app.UseCorsService();

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

app.UseAuthSwaggerService();

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

app.UseRouting();

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});
app.UseIdentityService();

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

app.UseAuthenticatioService();

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

app.UseAuthorizationService();

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

app.MapControllers();

app.Run();
