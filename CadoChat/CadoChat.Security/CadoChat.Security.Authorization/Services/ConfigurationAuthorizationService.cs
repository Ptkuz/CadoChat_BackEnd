using CadoChat.Security.Authorization.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Authorization.Services
{
    public class ConfigurationAuthorizationService : IConfigurationAuthorizationService
    {
        public virtual void AddService(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddAuthorization();
        }

        public virtual void UseService(WebApplication applicationBuilder)
        {
            applicationBuilder.UseAuthorization();
        }
    }
}
