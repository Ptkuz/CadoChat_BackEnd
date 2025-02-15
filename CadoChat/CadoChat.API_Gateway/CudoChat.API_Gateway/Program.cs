using CudoChat.API_Gateway.ApplicationConfigs;
using CudoChat.API_Gateway.ApplicationConfigs.Interfaces;
using Microsoft.AspNetCore.HttpOverrides;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting();
// Подключаем Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var apiGateway = builder.Configuration["ServiceUrls:API_Gateway"]!;
var authService = builder.Configuration["ServiceUrls:AuthService"]!;

builder.Services.AddSingleton<IAppConfig>(new 
    AppConfig(apiGateway, authService));


builder.Services.AddOcelot();

// Включаем CORS, разрешаем запросы только через API Gateway
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGateway", policy =>
    {
        policy.WithOrigins(apiGateway)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddHeaderRouting();

var app = builder.Build();

var forwardedHeadersOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedHost,
    RequireHeaderSymmetry = false,
    ForwardLimit = null
};
forwardedHeadersOptions.KnownNetworks.Clear();
forwardedHeadersOptions.KnownProxies.Clear();

app.UseForwardedHeaders(forwardedHeadersOptions);


app.Use(async (context, next) =>
{
    var requestHost = context.Request.Host.Value;

    if (string.IsNullOrEmpty(requestHost) || !apiGateway.Contains(requestHost))
    {
        context.Response.StatusCode = 403;
        await context.Response.WriteAsync("Forbidden: Access only through API Gateway.");
        return;
    }
    var xForwardedFor = context.Connection?.RemoteIpAddress?.ToString();
    var xForwardedHost = context.Request.Host.Value?.ToString();
    context.Request.Headers.Append("X-Forwarded-For", xForwardedFor);
    context.Request.Headers.Append("X-Forwarded-Host", xForwardedHost);

    await next();
});

// Подключаем CORS
app.UseCors("AllowGateway");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

// Подключаем Ocelot
await app.UseOcelot();

app.Run();
