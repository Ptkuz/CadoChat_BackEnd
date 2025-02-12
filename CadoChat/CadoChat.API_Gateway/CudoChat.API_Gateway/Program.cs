using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gateway", Version = "v1" });

    // Добавляем ссылки на микросервисы
    //c.AddServer(new OpenApiServer { Url = "https://localhost:7220", Description = "Auth Service" });

    // Загружаем OpenAPI-документы микросервисов
    //c.IncludeXmlComments("https://localhost:7220/swagger/v1/swagger.json");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("https://localhost:7220/swagger/v1/swagger.json", "Auth Service API");
    });
}

await app.UseOcelot();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
