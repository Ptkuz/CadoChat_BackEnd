using CadoChat.AuthService;
using CadoChat.AuthService.Services;
using CadoChat.AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var apiGateway = builder.Configuration["ServiceUrls:API_Gateway"]!;
var authService = builder.Configuration["ServiceUrls:AuthService"]!;
var key = builder.Configuration["Jwt:SecretKey"];

var services = builder.Services;

// Генерация RSA-ключей
var rsa = RSA.Create(2048);
var signingKey = new RsaSecurityKey(rsa)
{
    KeyId = key
};

SigningCredentials signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.RsaSha256);



// Настройка базы данных
services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Настройка Identity
services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

// Добавляем IdentityServer
builder.Services.AddIdentityServer()
    .AddSigningCredential(signingCredentials)
    .AddInMemoryClients(IdentityServerConfig.Clients)
    .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
    .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources);

// Настройка аутентификации с использованием IdentityServer
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration["ServiceUrls:AuthService"];
        options.RequireHttpsMetadata = false;
        options.Audience = "chat_api";

        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidateIssuer = true,
            ValidIssuer = "https://localhost:7220",
            //ValidateAudience = true,
            ValidAudience = "chat_api",
            ValidateLifetime = true,
            IssuerSigningKeyResolver = (token, securityToken, identifier, parameters) =>
            {
                // Здесь вы можете добавить свой код для получения публичных ключей
                // Например, запросить их у IdentityServer через jwks endpoint:
                var jwksUri = $"{options.Authority}/.well-known/openid-configuration/jwks";
                var client = new HttpClient();
                var response = client.GetStringAsync(jwksUri).Result;
                var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(response);
                return keys.Keys;
            }
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

services.AddAuthorization();
services.AddControllers();
services.AddScoped<ITokenGenService, TokenService>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(apiGateway) // Только через API Gateway
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

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

//app.Use(async (context, next) =>
//{
//    // Проверка заголовка X-Forwarded-For, добавленного API Gateway
//    var host = context.Request.Headers["Host"].ToString();

//    if (string.IsNullOrEmpty(host) || authService.Contains(host))
//    {
//        // Ответ, если запрос не прошел через API Gateway
//        context.Response.StatusCode = 403; // Forbidden
//        await context.Response.WriteAsync("Forbidden: Access only through API Gateway.");
//        return;
//    }

//    await next();
//});

app.UseCors(MyAllowSpecificOrigins);

app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
