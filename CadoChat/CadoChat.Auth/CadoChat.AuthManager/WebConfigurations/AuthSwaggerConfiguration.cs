using CadoChat.Web.AspNetCore.WebConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.AuthManager.WebConfigurations
{
    public class AuthSwaggerConfiguration : SwaggerConfiguration
    {
        public override string SwaggerTitle
        {
            get
            {
                return GlobalSettings.Services.AuthService.Name;
            }
        }
    }
}
