using CadoChat.AuthManager.WebConfigurations;
using CadoChat.AuthService.Initialize;
using CadoChat.IO.Json.Services;
using CadoChat.IO.Json.Services.Interfaces;
using CadoChat.Security.Authentication.Middlewaers;
using CadoChat.Security.Validation.Services;
using CadoChat.Security.Validation.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using CadoChat.APIGateway.Manager.WebConfigurations;
using CadoChat.Security.APIGateway.WebConfigurations;
using CadoChat.Security.Cors.WebConfigurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ISecurityKeyService<RsaSecurityKey>, RsaSecurityKeyService>();
builder.Services.AddSingleton<IFileSerializer, FileSerializer>();


using var serviceProvider = builder.Services.BuildServiceProvider();

var securityKeyService = serviceProvider.GetRequiredService<ISecurityKeyService<RsaSecurityKey>>();
var fileSerializer = serviceProvider.GetRequiredService<IFileSerializer>();

builder.InitWebApplicationSettings(securityKeyService, fileSerializer);

builder.Services.AddRouting();

builder.AddAPIGatewayAuthenticationhService();
builder.AddAPIGatewaySwaggerService();

builder.Services.AddOcelot();
builder.Services.AddAuthorization();

builder.AddCorsService();

builder.Services.AddHeaderRouting();

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

app.UseAPIGatewaySwaggerService();

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

app.UseHttpsRedirection();

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

app.UseAPIGatewayAuthenticationhService();

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

app.UseAuthorization();

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

await app.UseOcelot();

app.Run();
