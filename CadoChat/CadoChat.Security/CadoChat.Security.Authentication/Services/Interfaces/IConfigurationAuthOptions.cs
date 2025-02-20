using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Authentication.Services.Interfaces
{
    public interface IConfigurationAuthOptions
    {
        void ConfigureAuthOptions(JwtBearerOptions options);
    }
}
