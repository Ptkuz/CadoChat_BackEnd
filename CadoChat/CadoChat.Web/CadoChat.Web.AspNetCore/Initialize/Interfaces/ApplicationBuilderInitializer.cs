using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.AspNetCore.Initialize.Interfaces
{
    public interface IApplicationBuilderInitializer
    {

        TService GetService<TService>(Type type) 
            where TService : class;
    }
}
