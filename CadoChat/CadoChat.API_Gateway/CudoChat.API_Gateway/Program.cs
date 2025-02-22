using CadoChat.AuthService.Initialize;
using CadoChat.AuthService.Services.Interfaces;
using CadoChat.Security.APIGateway.Services.Interfaces;
using CadoChat.Security.Authentication.Middlewaers;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Web.AspNetCore.Logging.Interfaces;
using CadoChat.Web.AspNetCore.Swagger.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var InitializedBuilder = ApplicationBuilderInitializer.CreateInstance(builder);

var loggingService = InitializedBuilder.GetService<ILoggingConfigurationService>(typeof(ILoggingConfigurationService));
var authApiGatewayService = InitializedBuilder.GetService<IConfigurationAuthService>(typeof(IConfigurationAuthService));
var swaggerService = InitializedBuilder.GetService<ISwaggerConfigurationService>(typeof(ISwaggerConfigurationService));
var corsService = InitializedBuilder.GetService<ICorsConfigurationService>(typeof(ICorsConfigurationService));
var apiGatewayService = InitializedBuilder.GetService<IAPIGatewayConfigurationService>(typeof(IAPIGatewayConfigurationService));
var identityServerService = InitializedBuilder.GetService<IConfigurationIdentityService>(typeof(IConfigurationIdentityService));

builder.Services.AddRouting();

SecurityConfigLoader.Init(builder.Configuration);

var apiGateway = builder.Configuration["ServiceUrls:API_Gateway"]!;

authApiGatewayService.AddService(builder);

swaggerService.AddService(builder);

builder.Services.AddOcelot();
builder.Services.AddAuthorization();

corsService.AddService(builder);

builder.Services.AddHeaderRouting();

var app = builder.Build();

var forwardedHeadersOptions = apiGatewayService.GetAPIGatewayOptions(app);
app.UseForwardedHeaders(forwardedHeadersOptions);

app.UseMiddleware<AccessAPIGatewayMiddleware>();

swaggerService.UseService(app);

corsService.UseService(app);

app.UseHttpsRedirection();

app.UseRouting();
authApiGatewayService.UseService(app);
app.UseAuthorization();

await app.UseOcelot();

app.Run();
