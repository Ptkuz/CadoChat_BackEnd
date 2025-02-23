using CadoChat.Web.AspNetCore.Swagger;
using CadoChat.Web.AspNetCore.Swagger.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.APIGateway.Manager.Services
{

    /// <summary>
    /// Конфигуратор Swagger
    /// </summary>
    public class APIGatewaySwaggerConfiguration : SwaggerConfiguration, ISwaggerConfiguration
    {
        public override string SwaggerTitle
        {
            get
            {
                return GlobalSettings.Services.ChatService.Name;
            }
        }

        /// <summary>
        /// Инициализировать конфигуратор Swagger
        /// </summary>
        /// <param name="applicationBuilder">Собранное приложение</param>
        public override void UseService(WebApplication applicationBuilder)
        {
            if (applicationBuilder.Environment.IsDevelopment())
            {
                applicationBuilder.UseSwagger();
                applicationBuilder.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", GlobalSettings.Services.API_Gateway.Name);
                    c.SwaggerEndpoint("/auth/swagger/v1/swagger.json", GlobalSettings.Services.AuthService.Name); 
                    c.SwaggerEndpoint("/chat/swagger/v1/swagger.json", GlobalSettings.Services.ChatService.Name);
                    c.RoutePrefix = "swagger";
                });
            }
        }
    }
}
