using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Settings.User
{
    public class UserConfig
    {
        public string Id { get; set; }
        public int AccessTokenLifetime { get; set; }
        public bool RequireClientSecret { get; set; }
    }
}
