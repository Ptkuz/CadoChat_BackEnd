using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.ConfigLoad.Config;
using CadoChat.Security.Validation.SecutiryInfo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting();
// Подключаем Ocelot
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

SecurityConfigLoader.Init(builder.Configuration);

var apiGateway = builder.Configuration["ServiceUrls:API_Gateway"]!;
var authService = SecurityConfigLoader.SecurityConfig.AuthService;

// В Startup.cs или Program.cs
builder.Services.AddLogging(options =>
{
    options.AddConsole(); // Логирование в консоль
    options.AddDebug(); // Логирование в дебаг
});

builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = authService;
        options.RequireHttpsMetadata = true;

        var signingKey = RsaSecurityKeyService.GetKey();

        options.TokenValidationParameters = new TokenValidationParameters
        {

            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = signingKey, // Здесь используется ключ для подписи
            ValidIssuer = authService, // Указание правильного издателя
            ValidAudience = AudiencesAccess.ChatApi, // Указание правильной аудитории
            ValidateIssuerSigningKey = true // Включаем валидацию подписи
        };

        options.Events = new JwtBearerEvents
        {

            OnTokenValidated = context =>
            {
                return Task.CompletedTask;
            },

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


builder.Services.AddAuthorization();
builder.Services.AddControllers();

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

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

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
    var requestHost = context.Request.Headers["origin"].ToString();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
