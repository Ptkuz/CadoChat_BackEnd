using CadoChat.AuthService.ApplicationConfigs;
using CadoChat.AuthService.ApplicationConfigs.Interfaces;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var apiGateway = builder.Configuration["ServiceUrls:API_Gateway"]!;
var authService = builder.Configuration["ServiceUrls:API_Gateway"]!;

builder.Services.AddSingleton<IAppConfig>(new
    AppConfig(apiGateway, authService));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

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
    var forwardedFor = context.Request.Headers["X-Forwarded-For"].ToString();

    if (string.IsNullOrEmpty(forwardedFor))
    {
        // Ответ, если запрос не прошел через API Gateway
        context.Response.StatusCode = 403; // Forbidden
        await context.Response.WriteAsync("Forbidden: Access only through API Gateway.");
        return;
    }

    await next();
});

app.UseCors(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.Run();
