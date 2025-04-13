using CadoChat.ChatManager.WebConfigurations;
using CadoChat.ChatService.Initialize;
using CadoChat.IO.Json.Services;
using CadoChat.IO.Json.Services.Interfaces;
using CadoChat.Security.APIGateway.WebConfigurations;
using CadoChat.Security.Authentication.Middlewaers;
using CadoChat.Security.Cors.WebConfigurations;
using CadoChat.Security.Validation.Services;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.AspNetCore.WebConfigurations;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ISecurityKeyService<RsaSecurityKey>, RsaSecurityKeyService>();
builder.Services.AddSingleton<IFileSerializer, FileSerializer>();
using var serviceProvider = builder.Services.BuildServiceProvider();

var securityKeyService = serviceProvider.GetRequiredService<ISecurityKeyService<RsaSecurityKey>>();
var fileSerializer = serviceProvider.GetRequiredService<IFileSerializer>();

builder.InitWebApplicationSettings(securityKeyService, fileSerializer);

builder.Services.AddRouting();
builder.AddLoggingService();
builder.AddChatAuthenticationService();
builder.AddChatAuthorizationService();
builder.AddChatSwaggerService();

builder.Services.AddControllers();

builder.AddCorsService();

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

app.UseMiddleware<AccessAPIGatewayMiddleware>();

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

app.UseChatSwaggerService();

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

app.UseMiddleware<AuthenticationErrorMiddleware>();

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

app.UseChatAuthenticationService();

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

app.UseChatAuthorizationService();

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

app.MapControllers();

app.Run();
