using CadoChat.Web.AspNetCore.Logging.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.AspNetCore.Logging
{
    public class ConfigurationLoggings : IConfigurationLoggings
    {
        public void ConfigureLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConsole(); 
            loggingBuilder.AddDebug(); 
        }
    }
}
