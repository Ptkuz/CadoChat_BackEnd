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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway V1");
                    c.SwaggerEndpoint("/auth/swagger/v1/swagger.json", "AuthService"); 
                    c.SwaggerEndpoint("/chat/swagger/v1/swagger.json", "ChatService");
                    c.RoutePrefix = "swagger";
                });
            }
        }
    }
}
