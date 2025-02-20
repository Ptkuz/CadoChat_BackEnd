using CadoChat.Security.APIGateway.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.APIGateway.Services
{
    public class APIGatewayConfigurationService : IAPIGatewayConfigurationService
    {

        public ForwardedHeadersOptions GetAPIGatewayOptions(WebApplication app)
        {
            var forwardedHeadersOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedHost,
                RequireHeaderSymmetry = false,
                ForwardLimit = null
            };
            forwardedHeadersOptions.KnownNetworks.Clear();
            forwardedHeadersOptions.KnownProxies.Clear();

            return forwardedHeadersOptions;
        }

    }
}
