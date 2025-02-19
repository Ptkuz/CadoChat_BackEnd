using CadoChat.Security.APIGateway.Extentions;
using CadoChat.Security.Authentication.Middlewaers;
using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.SecutiryInfo;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting();
// Подключаем Ocelot
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

SecurityConfigLoader.Init(builder.Configuration);

var configuration = builder.Configuration;
builder.Services.AddSingleton<IConfiguration>(configuration);

var apiGateway = builder.Configuration["ServiceUrls:API_Gateway"]!;
var authService = SecurityConfigLoader.SecurityConfig.AuthService;

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
    });


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gateway", Version = "v1" });

    // Добавляем поддержку авторизации через JWT (если используется)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Введите токен в формате: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
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

var applicationBuilder = (IApplicationBuilder)app.GetAPIGatewayOptions();
var options = applicationBuilder.GetAPIGatewayOptions();
app.UseForwardedHeaders(options);


app.UseMiddleware<ReplaceRequestHostMiddleware>();
app.UseMiddleware<AccessAPIGatewayMiddleware>();

app.UseCors("AllowGateway");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseMiddleware<AuthenticationErrorMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
