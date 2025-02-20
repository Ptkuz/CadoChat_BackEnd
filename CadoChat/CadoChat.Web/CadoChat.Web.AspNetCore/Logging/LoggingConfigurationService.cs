using CadoChat.Web.AspNetCore.Logging.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.AspNetCore.Logging
{
    public class LoggingConfigurationService : ILoggingConfigurationService
    {
        public void AddService(WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddLogging(ConfigureLogging);
        }

        public void UseService(WebApplication applicationBuilder)
        {
            throw new NotImplementedException();
        }

        private void ConfigureLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        }
    }
}
