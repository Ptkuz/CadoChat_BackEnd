using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.AspNetCore.Swagger.Interfaces
{
    public interface ISwaggerSettings
    {
        void ApplySettingsWithAuthorization(SwaggerGenOptions options);

    }
}
