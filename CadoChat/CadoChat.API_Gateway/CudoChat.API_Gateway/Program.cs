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
using CadoChat.Web.AspNetCore.WebConfigurations.Interfaces;
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

builder.Services.AddRouting();

InitializedBuilder.ConfigurationAuthOptions.AddService(builder);
InitializedBuilder.SwaggerConfigurationService.AddService(builder);

builder.Services.AddOcelot();
builder.Services.AddAuthorization();

InitializedBuilder.CorsConfigurationService.AddService(builder);

builder.Services.AddHeaderRouting();

var app = builder.Build();

app.Use(async (context, next) =>
{
    
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

InitializedBuilder.ApiGatewayConfigurationService.UseService(app);

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

InitializedBuilder.SwaggerConfigurationService.UseService(app);

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

InitializedBuilder.CorsConfigurationService.UseService(app);

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

app.UseHttpsRedirection();

app.UseRouting();
InitializedBuilder.ConfigurationAuthOptions.UseService(app);
app.UseAuthorization();

await app.UseOcelot();

app.Run();
