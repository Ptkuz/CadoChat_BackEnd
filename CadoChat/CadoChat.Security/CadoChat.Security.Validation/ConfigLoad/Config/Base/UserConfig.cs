using CadoChat.Security.Validation.SecutiryInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Validation.ConfigLoad.Config.Base
{
    public abstract class UserConfig
    {
        public int AccessTokenLifetime { get; private set; }

        public abstract IList<string> Audiences { get; protected set; }

        public UserConfig(int accessTokenLifetime)
        {
            AccessTokenLifetime = accessTokenLifetime;
        }
    }
}
