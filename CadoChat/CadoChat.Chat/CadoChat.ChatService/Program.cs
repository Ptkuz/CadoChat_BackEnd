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

InitializedBuilder.CorsConfigurationService.UseService(app);

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

InitializedBuilder.ConfigurationAuthOptions.UseService(app);

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

InitializedBuilder.ConfigurationAuthorizationService.UseService(app);

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Запрос на путь: {Path}", context.Request.Path);
    await next();
});

app.MapControllers();

app.Run();
