using CadoChat.Web.AspNetCore.Swagger.Interfaces;
using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CadoChat.Web.AspNetCore.Swagger
{

    /// <summary>
    /// Конфигуратор Swagger
    /// </summary>
    public class SwaggerConfigurationService : ConfigurationService, ISwaggerConfigurationService
    {

        /// <summary>
        /// Добавить сервис Swagger
        /// </summary>
        /// <param name="webApplicationBuilder">Строитель приложения</param>
        public virtual void AddService(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddSwaggerGen(ApplySettingsWithAuthorization);
        }


        /// <summary>
        /// Использовать сервис Swagger
        /// </summary>
        /// <param name="applicationBuilder">Собранное приложение</param>
        public virtual void UseService(WebApplication applicationBuilder)
        {
            if (applicationBuilder.Environment.IsDevelopment())
            {
                applicationBuilder.UseSwagger();
                applicationBuilder.UseSwaggerUI();
            }
        }

        /// <summary>
        /// Применить настройки Swagger с авторизацией
        /// </summary>
        /// <param name="options"></param>
        private void ApplySettingsWithAuthorization(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gateway", Version = "v1" });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Введите токен в формате: Bearer {token}",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        }
    }
}
