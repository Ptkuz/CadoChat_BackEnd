using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Settings.Service
{
    public class ServiceObjects
    {
        public ServiceConfig AuthService { get; set; }
        public ServiceConfig ChatService { get; set; }
        public ServiceConfig API_Gateway { get; set; }
    }
}
