using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.APIGateway.Services.Interfaces
{
    public interface IAPIGatewayConfigurationService
    {

        ForwardedHeadersOptions GetAPIGatewayOptions(WebApplication app);

    }
}
