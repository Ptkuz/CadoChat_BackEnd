using CadoChat.ChatService.Initialize;
using CadoChat.Security.APIGateway.Services.Interfaces;
using CadoChat.Security.Authentication.Middlewaers;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Web.AspNetCore.Logging.Interfaces;
using CadoChat.Web.AspNetCore.Swagger.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var InitializedBuilder = ApplicationBuilderInitializer.CreateInstance(builder);

var loggingService = InitializedBuilder.GetService<ILoggingConfigurationService>(typeof(ILoggingConfigurationService));
var authService = InitializedBuilder.GetService<IConfigurationAuthService>(typeof(IConfigurationAuthService));
var swaggerService = InitializedBuilder.GetService<ISwaggerConfigurationService>(typeof(ISwaggerConfigurationService));
var corsService = InitializedBuilder.GetService<ICorsConfigurationService>(typeof(ICorsConfigurationService));
var apiGatewayService = InitializedBuilder.GetService<IAPIGatewayConfigurationService>(typeof(IAPIGatewayConfigurationService));

builder.Services.AddRouting();
loggingService.AddService(builder);

authService.AddService(builder);
swaggerService.AddService(builder);

builder.Services.AddAuthorization();
builder.Services.AddControllers();

corsService.AddService(builder);

var app = builder.Build();

var options = apiGatewayService.GetAPIGatewayOptions(app);
app.UseForwardedHeaders(options);


app.UseMiddleware<ReplaceRequestHostMiddleware>();
app.UseMiddleware<AccessAPIGatewayMiddleware>();

corsService.UseService(app);
swaggerService.UseService(app);

app.UseRouting();

app.UseMiddleware<AuthenticationErrorMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
