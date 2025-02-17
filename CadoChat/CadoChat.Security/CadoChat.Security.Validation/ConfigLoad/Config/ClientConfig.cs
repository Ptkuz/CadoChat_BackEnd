using CadoChat.Security.Validation.ConfigLoad.Config.Base;
using CadoChat.Security.Validation.SecutiryInfo;

namespace CadoChat.Security.Validation.ConfigLoad.Config
{
    public class ClientConfig : UserConfig
    {

        public override IList<string> Audiences { get; protected set; }

        public ClientConfig(int accessTokenLifetime)
            : base(accessTokenLifetime)
        {
            Audiences = [AudiencesAccess.AuthApi, AudiencesAccess.ChatApi];
        }


    }
}
