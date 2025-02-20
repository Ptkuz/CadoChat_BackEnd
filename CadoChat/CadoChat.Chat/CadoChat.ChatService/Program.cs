using CadoChat.Security.APIGateway.Extentions;
using CadoChat.Security.Authentication.Middlewaers;
using CadoChat.Security.Authentication.Services;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.SecutiryInfo;
using CadoChat.Web.AspNetCore.Logging;
using CadoChat.Web.AspNetCore.Logging.Interfaces;
using CadoChat.Web.AspNetCore.Swagger;
using CadoChat.Web.AspNetCore.Swagger.Interfaces;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
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

IConfigurationLoggings configurationLoggings = new ConfigurationLoggings();
IConfigurationAuthOptions configurationAuthOptions = new ConfigurationAuthOptions();
ISwaggerSettings swaggerSettings = new SwaggerSettings();

builder.Services.AddLogging(configurationLoggings.ConfigureLogging);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configurationAuthOptions.ConfigureAuthOptions);

builder.Services.AddSwaggerGen(swaggerSettings.ApplySettingsWithAuthorization);

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

var applicationBuilder = (IApplicationBuilder)app;
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
