using CadoChat.Web.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Authentication.Services.Interfaces
{
    public interface IConfigurationAuthService : IConfigurationService
    {
        string AuthenticationScheme { get; }
    }
}
