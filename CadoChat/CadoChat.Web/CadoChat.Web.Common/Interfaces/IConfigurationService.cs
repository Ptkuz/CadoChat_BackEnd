using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Interfaces
{
    public interface IConfigurationService
    {
        void AddService(WebApplicationBuilder webApplicationBuilder);

        void UseService(WebApplication applicationBuilder);
    }
}
