using CadoChat.Auth.IdentityServer.Middlewaers;
using CadoChat.AuthManager.Services;
using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.AuthService;
using CadoChat.AuthService.Initialize;
using CadoChat.AuthService.Services.Interfaces;
using CadoChat.IO.Json.Services;
using CadoChat.IO.Json.Services.Interfaces;
using CadoChat.Security.APIGateway.Services.Interfaces;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Security.Validation.Services;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.AspNetCore.Logging.Interfaces;
using CadoChat.Web.AspNetCore.Swagger.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ISecurityKeyService<RsaSecurityKey>, RsaSecurityKeyService>();
builder.Services.AddSingleton<IFileSerializer, FileSerializer>();


using var serviceProvider = builder.Services.BuildServiceProvider();

var securityKeyService = serviceProvider.GetRequiredService<ISecurityKeyService<RsaSecurityKey>>();
var fileSerializer = serviceProvider.GetRequiredService<IFileSerializer>();

var InitializedBuilder = ApplicationBuilderInitializer.CreateInstance(builder, securityKeyService, fileSerializer);

var loggingService = InitializedBuilder.GetService<ILoggingConfiguration>(typeof(ILoggingConfiguration));
var authService = InitializedBuilder.GetService<IAuthConfiguration>(typeof(IAuthConfiguration));
var swaggerService = InitializedBuilder.GetService<ISwaggerConfiguration>(typeof(ISwaggerConfiguration));
var corsService = InitializedBuilder.GetService<ICorsConfiguration>(typeof(ICorsConfiguration));
var identityServerService = InitializedBuilder.GetService<IIdentityServiceConfiguration>(typeof(IIdentityServiceConfiguration));

var apiGatewayService = InitializedBuilder.GetService<IAPIGatewayConfiguration>(typeof(IAPIGatewayConfiguration));

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

authService.AddService(builder);


services.AddControllers();

corsService.AddService(builder);

var app = builder.Build();

apiGatewayService.UseService(app);

app.UseMiddleware<IdentityServerURLMiddleware>();

corsService.UseService(app);

swaggerService.UseService(app);

app.UseRouting();
identityServerService.UseService(app);
authService.UseService(app);
app.UseAuthorization();
app.MapControllers();

app.Run();
