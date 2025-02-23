using CadoChat.Web.Common.Settings.Service.Audience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Settings.Service
{
    public class ServiceConfig
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public AudiencesAccess AudiencesAccess { get; set; }
        public Dictionary<string, string> Scopes { get; set; } = new Dictionary<string, string>();

    }
}
