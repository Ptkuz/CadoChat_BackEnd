using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.AspNetCore.Logging.Interfaces
{
    public interface IConfigurationLoggings
    {

        void ConfigureLogging(ILoggingBuilder loggingBuilder);
    }
}
