using CadoChat.Auth.IdentityServer.Middlewaers;
using CadoChat.AuthManager;
using CadoChat.AuthManager.Services;
using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.AuthService;
using CadoChat.AuthService.Initialize;
using CadoChat.AuthService.Services.Interfaces;
using CadoChat.Security.APIGateway.Services.Interfaces;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.SecutiryInfo;
using CadoChat.Web.AspNetCore.Logging.Interfaces;
using CadoChat.Web.AspNetCore.Swagger.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var InitializedBuilder = ApplicationBuilderInitializer.CreateInstance(builder);

var loggingService = InitializedBuilder.GetService<ILoggingConfigurationService>(typeof(ILoggingConfigurationService));
var initAuthService = InitializedBuilder.GetService<IConfigurationAuthService>(typeof(IConfigurationAuthService));
var swaggerService = InitializedBuilder.GetService<ISwaggerConfigurationService>(typeof(ISwaggerConfigurationService));
var corsService = InitializedBuilder.GetService<ICorsConfigurationService>(typeof(ICorsConfigurationService));
var apiGatewayService = InitializedBuilder.GetService<IAPIGatewayConfigurationService>(typeof(IAPIGatewayConfigurationService));
var identityServerService = InitializedBuilder.GetService<IConfigurationIdentityService>(typeof(IConfigurationIdentityService));

var services = builder.Services;

builder.Services.AddTransient<ITokenManagerService, TokenManagerService>();

// Настройка базы данных
services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Настройка Identity
services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

swaggerService.AddService(builder);

// Добавляем IdentityServer
identityServerService.AddService(builder);

// Настройка аутентификации с использованием IdentityServer
initAuthService.AddService(builder);


services.AddControllers();

corsService.AddService(builder);

var app = builder.Build();

var options = apiGatewayService.GetAPIGatewayOptions(app);
app.UseForwardedHeaders(options);

app.UseMiddleware<IdentityServerURLMiddleware>();

corsService.UseService(app);

swaggerService.UseService(app);

app.UseRouting();
identityServerService.UseService(app);
initAuthService.UseService(app);
app.UseAuthorization();
app.MapControllers();

app.Run();
