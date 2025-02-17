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
rsa.ImportRSAPrivateKey(Convert.FromBase64String("MIIEpAIBAAKCAQEAtC1E0SnF93EgjBahw/0yQ4BZpPZkeYBwp2+pHBQMBgQjgCxzxE8jhcDfOSr1KQWVUvfbWt2cVJIuLude382C2meN4TD9wKTqWhB+MgwKRJWicS2+uQS9/05LsJUR4czPLvLEMgUqhqxWiadb8slw8vaVJ3utFRNBHUJNoeOT9Ztwcy7qobq0YrbDG+DCReubYdyjbdH2ITykPZf2yNp2YX7wFG1QH0lVU4ItQoNBsTyXEyee9akvWMSiZATdWOtm4+zaMZIJGyVX6VL3EAsQ1V5QLGXyTA1N74BonBB9+NHHdy+WdoQe/JLSpGBj6syKFwQtsgrMCBxlUGkxeYgWCQIDAQABAoIBABerwti/5jRF9oKxDnuTLiFUIXLctAKKb0JwFwWLVLENpiRWsrbdtssBtdHq5N6Izz9hNL5RUxKBSfP7jalVdJWA+VDWgN/oSqmedRXaIxczmW3JFr9z8goynRsL2peRsr52QnRX3WhoB8554EibUm15G8teIjUcnHddmJlmLrAbjQC1gk0quvCk7ggFmz0r5CoccECslO+CXhZ9rFXKZ1PFbtxZ8yIMlQ3ghK+3yMf8tM0NBDxk9Hjsq1titNp6P6nyEqK1dJ1CmUfcEzducnzVHcgLNo+vZfLS9Oysv+BoWuzWIA62kZld7A+G5w3wEp+OEHAgR28khwBRk2FYKqkCgYEA7skVPHPy9W7cdwE+30P4bvPg/LYpEOAjdyqXVtm76QECjiktYaTuxiMH6RKMLUA8AVxqJYHg7ABYtA7+Qb15kyeIqRIOFq5G109BIWfoZD/eLx9B8yOgPST0uVWc7P4TDXnIDs8H6RLUGft4TJA9T/tAojDNKA1PON/gdAKGGFcCgYEAwSqH9ZO7uwvhePjtEsuBUQBANFTr/Abm8IM4rSbB3xPfE6sfHy4vxUoeu1mEiLQngu4Md1Q64yInp+xOBOLFUBKvGfe66ddG2UBqG/W/jBPggTt+i4ehw1w1tGZ0JQPG5Kac2BYh33OpCInldRsx6JeNExTBA3B8PIeW9hu9yJ8CgYEA4cGZY0tYdDT5GUZDNADmO7g1iZeLodnXjg3lgYZfw35h9Rf3QO8XlJqAGxqfDxVA5iSCcq2lglsdgjb+qhbCf58L9JUOXuEsNtpGgJflvgooPTL3PjH7iHONMEBCGkpopv/xZhbUqsZTY7E93l0sqpaoV+99t5VFxkbbxbKxJwcCgYEAmprV8wJpUU4zCsYByfdD63cN7FTEBBXqJTqB1GSe61NWSsG9yREIfxnR+xWs9FVtAmhRZfjuoPinUMnbsCFo16v8pgYXfi4lsKDTzMkmpJEMMaNSp47JNDnLajZOY4ngWQXZp0Ifnl9OPV1RYCeCDK2v5kPIMF6JsVC8zQJrJfUCgYAJe5Ag0yNKwPaCQ4Fm08s2HfzVFRSLBWKyutr9351+bpkfUyXjBRSDKjJNSTXkt7VhWJpW+ocQjP1mChk8ov0VGK8p/5gYBuy4HdY/ddjqY5icFHgZYHyrXg/072h60IyFc4CO6/7eE7u+I+xNKiy2Vt85E3efo4PM0jHhTgzyYg=="), out _);
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
builder.Services.AddIdentityServer(options =>
{
    options.KeyManagement.Enabled = false; // ❌ Отключает автоматическую генерацию ключей
})
    .AddSigningCredential(signingCredentials)
    .AddInMemoryClients(IdentityServerConfig.Clients)
    .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
    .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
    ;

// Настройка аутентификации с использованием IdentityServer
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration["ServiceUrls:AuthService"];
        options.RequireHttpsMetadata = false;
        options.Audience = "chat_api";

        var rsa = RSA.Create(2048);
        rsa.ImportRSAPrivateKey(Convert.FromBase64String("MIIEpAIBAAKCAQEAtC1E0SnF93EgjBahw/0yQ4BZpPZkeYBwp2+pHBQMBgQjgCxzxE8jhcDfOSr1KQWVUvfbWt2cVJIuLude382C2meN4TD9wKTqWhB+MgwKRJWicS2+uQS9/05LsJUR4czPLvLEMgUqhqxWiadb8slw8vaVJ3utFRNBHUJNoeOT9Ztwcy7qobq0YrbDG+DCReubYdyjbdH2ITykPZf2yNp2YX7wFG1QH0lVU4ItQoNBsTyXEyee9akvWMSiZATdWOtm4+zaMZIJGyVX6VL3EAsQ1V5QLGXyTA1N74BonBB9+NHHdy+WdoQe/JLSpGBj6syKFwQtsgrMCBxlUGkxeYgWCQIDAQABAoIBABerwti/5jRF9oKxDnuTLiFUIXLctAKKb0JwFwWLVLENpiRWsrbdtssBtdHq5N6Izz9hNL5RUxKBSfP7jalVdJWA+VDWgN/oSqmedRXaIxczmW3JFr9z8goynRsL2peRsr52QnRX3WhoB8554EibUm15G8teIjUcnHddmJlmLrAbjQC1gk0quvCk7ggFmz0r5CoccECslO+CXhZ9rFXKZ1PFbtxZ8yIMlQ3ghK+3yMf8tM0NBDxk9Hjsq1titNp6P6nyEqK1dJ1CmUfcEzducnzVHcgLNo+vZfLS9Oysv+BoWuzWIA62kZld7A+G5w3wEp+OEHAgR28khwBRk2FYKqkCgYEA7skVPHPy9W7cdwE+30P4bvPg/LYpEOAjdyqXVtm76QECjiktYaTuxiMH6RKMLUA8AVxqJYHg7ABYtA7+Qb15kyeIqRIOFq5G109BIWfoZD/eLx9B8yOgPST0uVWc7P4TDXnIDs8H6RLUGft4TJA9T/tAojDNKA1PON/gdAKGGFcCgYEAwSqH9ZO7uwvhePjtEsuBUQBANFTr/Abm8IM4rSbB3xPfE6sfHy4vxUoeu1mEiLQngu4Md1Q64yInp+xOBOLFUBKvGfe66ddG2UBqG/W/jBPggTt+i4ehw1w1tGZ0JQPG5Kac2BYh33OpCInldRsx6JeNExTBA3B8PIeW9hu9yJ8CgYEA4cGZY0tYdDT5GUZDNADmO7g1iZeLodnXjg3lgYZfw35h9Rf3QO8XlJqAGxqfDxVA5iSCcq2lglsdgjb+qhbCf58L9JUOXuEsNtpGgJflvgooPTL3PjH7iHONMEBCGkpopv/xZhbUqsZTY7E93l0sqpaoV+99t5VFxkbbxbKxJwcCgYEAmprV8wJpUU4zCsYByfdD63cN7FTEBBXqJTqB1GSe61NWSsG9yREIfxnR+xWs9FVtAmhRZfjuoPinUMnbsCFo16v8pgYXfi4lsKDTzMkmpJEMMaNSp47JNDnLajZOY4ngWQXZp0Ifnl9OPV1RYCeCDK2v5kPIMF6JsVC8zQJrJfUCgYAJe5Ag0yNKwPaCQ4Fm08s2HfzVFRSLBWKyutr9351+bpkfUyXjBRSDKjJNSTXkt7VhWJpW+ocQjP1mChk8ov0VGK8p/5gYBuy4HdY/ddjqY5icFHgZYHyrXg/072h60IyFc4CO6/7eE7u+I+xNKiy2Vt85E3efo4PM0jHhTgzyYg=="), out _);
        var signingKey = new RsaSecurityKey(rsa)
        {
            KeyId = builder.Configuration["Jwt:SecretKey"]
        };

        options.TokenValidationParameters = new TokenValidationParameters
        {

            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = signingKey, // Здесь используется ключ для подписи
            ValidIssuer = "https://localhost:7220", // Указание правильного издателя
            ValidAudience = "chat_api", // Указание правильной аудитории
            ValidateIssuerSigningKey = true // Включаем валидацию подписи
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
