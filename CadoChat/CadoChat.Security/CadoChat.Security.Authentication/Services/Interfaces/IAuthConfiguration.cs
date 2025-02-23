using CadoChat.Web.Common.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Authentication.Services.Interfaces
{

    /// <summary>
    /// Конфигуратор аутентификации
    /// </summary>
    public interface IAuthConfiguration : IConfigurationService
    {
        /// <summary>
        /// Схема аутентификации
        /// </summary>
        string AuthenticationScheme { get; }
    }
}
