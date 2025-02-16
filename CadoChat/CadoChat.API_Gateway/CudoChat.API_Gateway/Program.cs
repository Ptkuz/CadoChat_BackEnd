using CudoChat.API_Gateway.ApplicationConfigs;
using CudoChat.API_Gateway.ApplicationConfigs.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting();
// Подключаем Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var apiGateway = builder.Configuration["ServiceUrls:API_Gateway"]!;
var authService = builder.Configuration["ServiceUrls:AuthService"]!;

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["ServiceUrls:AuthService"];
        options.RequireHttpsMetadata = false;
        options.Audience = "chat_api";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, // Проверка подписи токена
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = authService,
            ValidAudience = "chat_api",
            //IssuerSigningKeyResolver = (token, securityToken, identifier, parameters) =>
            //{
            //    // Здесь вы можете добавить свой код для получения публичных ключей
            //    // Например, запросить их у IdentityServer через jwks endpoint:
            //    var jwksUri = $"{options.Authority}/.well-known/jwks.json";
            //    var client = new HttpClient();
            //    var response = client.GetStringAsync(jwksUri).Result;
            //    var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(response);
            //    return keys.Keys;
            //}
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                // Логируем ошибку
                Console.WriteLine("Authentication failed: {Message}", context.Exception.Message);

                // Возвращаем подробное сообщение о причине ошибки
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                var errorDetails = new
                {
                    error = "Unauthorized",
                    message = "Token is invalid or expired. Please authenticate again.",
                    details = context.Exception.Message
                };
                return context.Response.WriteAsJsonAsync(errorDetails);
            }
        };
    });


builder.Services.AddOcelot();
builder.Services.AddAuthorization();

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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Подключаем Ocelot
await app.UseOcelot();

app.Run();
