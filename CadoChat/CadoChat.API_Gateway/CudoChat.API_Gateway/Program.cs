using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRouting();
// Подключаем Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();

// Включаем CORS, разрешаем запросы только через API Gateway
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGateway", policy =>
    {
        policy.WithOrigins("https://localhost:5000")
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

    if (string.IsNullOrEmpty(requestHost) || !requestHost.Contains("localhost:5000"))
    {
        context.Response.StatusCode = 403;
        await context.Response.WriteAsync("Forbidden: Access only through API Gateway.");
        return;
    }

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
