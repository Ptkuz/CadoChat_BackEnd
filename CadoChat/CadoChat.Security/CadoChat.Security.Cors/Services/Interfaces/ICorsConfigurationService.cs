using CadoChat.Web.Common.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Cors.Services.Interfaces
{
    /// <summary>
    /// Конфигуратор CORS
    /// </summary>
    public interface ICorsConfigurationService : IConfigurationService
    {

    }
}
