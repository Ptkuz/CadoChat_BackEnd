using CadoChat.AuthManager;
using CadoChat.AuthManager.Services;
using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.AuthService;
using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.SecutiryInfo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

SecurityConfigLoader.Init(builder.Configuration);

var apiGateway = builder.Configuration["ServiceUrls:API_Gateway"]!;
var authService = SecurityConfigLoader.SecurityConfig.AuthService;
var key = SecurityConfigLoader.SecurityConfig.SecretKey;

var services = builder.Services;

// Генерация RSA-ключей
var rsaKey = RsaSecurityKeyService.GetKey();
var signingCredentials = RsaSecurityKeyService.GetSigningCredentials(rsaKey);

builder.Services.AddTransient<ITokenManagerService, TokenManagerService>();

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
        options.Authority = authService;
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {

            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = rsaKey, // Здесь используется ключ для подписи
            ValidIssuer = authService,
            //ValidAudience = "chat_api", // Указание правильной аудитории
            ValidateIssuerSigningKey = true // Включаем валидацию подписи
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {

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

app.Use(async (context, next) =>
{

    // Проверка заголовка X-Forwarded-For, добавленного API Gateway
    var host = context.Request.Headers["Host"].ToString();

    if ((string.IsNullOrEmpty(host) || authService.Contains(host)) 
    && (!context.Request.Path.HasValue && context.Request.Path.Value != "/.well-known/openid-configuration"))
    {


        // Ответ, если запрос не прошел через API Gateway
        context.Response.StatusCode = 403; // Forbidden
        await context.Response.WriteAsync("Forbidden: Access only through API Gateway.");
        return;
    }

    await next();
});

app.UseCors(MyAllowSpecificOrigins);

app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
