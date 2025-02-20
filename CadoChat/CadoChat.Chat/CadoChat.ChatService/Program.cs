using CadoChat.ChatService.Initialize;
using CadoChat.Security.APIGateway.Extentions;
using CadoChat.Security.Authentication.Middlewaers;
using CadoChat.Security.Authentication.Services.Interfaces;
using CadoChat.Security.Cors.Services.Interfaces;
using CadoChat.Web.AspNetCore.Logging.Interfaces;
using CadoChat.Web.AspNetCore.Swagger.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var InitializedBuilder = ApplicationBuilderInitializer.CreateInstance(builder);


var loggingService = InitializedBuilder.GetService<IConfigurationLoggings>(typeof(IConfigurationLoggings));
var configurationAuthOptions = InitializedBuilder.GetService<IConfigurationAuthOptions>(typeof(IConfigurationAuthOptions));
var swaggerSettings = InitializedBuilder.GetService<ISwaggerSettings>(typeof(ISwaggerSettings));
var corsSettings = InitializedBuilder.GetService<ICorsSettings>(typeof(ICorsSettings));

builder.Services.AddRouting();
builder.Services.AddLogging(loggingService.ConfigureLogging);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configurationAuthOptions.ConfigureAuthOptions);

builder.Services.AddSwaggerGen(swaggerSettings.ApplySettingsWithAuthorization);

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddCors(corsSettings.SetCorsOptions);

var app = builder.Build();

var applicationBuilder = (IApplicationBuilder)app;
var options = applicationBuilder.GetAPIGatewayOptions();
app.UseForwardedHeaders(options);


app.UseMiddleware<ReplaceRequestHostMiddleware>();
app.UseMiddleware<AccessAPIGatewayMiddleware>();

corsSettings.UseCors(applicationBuilder);

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
