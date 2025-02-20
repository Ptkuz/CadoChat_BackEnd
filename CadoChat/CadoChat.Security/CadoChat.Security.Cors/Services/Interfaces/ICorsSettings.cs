using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Cors.Services.Interfaces
{
    public interface ICorsSettings
    {
        void SetCorsOptions(CorsOptions options);

        void SetCorsPolicy(CorsPolicyBuilder corsPolicyBuilder);

        void UseCors(IApplicationBuilder applicationBuilder);
    }
}
