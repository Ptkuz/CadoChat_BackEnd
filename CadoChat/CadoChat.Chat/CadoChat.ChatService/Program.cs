using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting();
// Подключаем Ocelot
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var apiGateway = builder.Configuration["ServiceUrls:API_Gateway"]!;
var authService = builder.Configuration["ServiceUrls:AuthService"]!;

// В Startup.cs или Program.cs
builder.Services.AddLogging(options =>
{
    options.AddConsole(); // Логирование в консоль
    options.AddDebug(); // Логирование в дебаг
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = authService;
        options.RequireHttpsMetadata = true;
        options.Audience = "chat_api";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            RequireSignedTokens = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
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


builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Включаем CORS, разрешаем запросы только через API Gateway
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGateway", policy =>
    {
        policy.WithOrigins()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

app.Use(async (context, next) =>
{
    logger.LogInformation("Incoming request: {Method} {Path}", context.Request.Method, context.Request.Path);

    await next();

    logger.LogInformation("Response: {StatusCode}", context.Response.StatusCode);
});

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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Подключаем CORS
app.UseCors("AllowGateway");

app.UseHttpsRedirection();


app.Run();
