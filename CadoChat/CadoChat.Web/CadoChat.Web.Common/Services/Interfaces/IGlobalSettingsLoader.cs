using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadoChat.Web.Common.Settings;

namespace CadoChat.Web.Common.Services.Interfaces
{
    public interface IGlobalSettingsLoader
    {
        GlobalSettings GlobalSettings { get; }
    }
}
