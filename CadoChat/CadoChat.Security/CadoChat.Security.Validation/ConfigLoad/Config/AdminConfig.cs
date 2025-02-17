using CadoChat.Security.Validation.ConfigLoad.Config.Base;
using CadoChat.Security.Validation.SecutiryInfo;

namespace CadoChat.Security.Validation.ConfigLoad.Config
{
    public class AdminConfig : UserConfig
    {
        public override IList<string> Audiences { get; protected set; }

        public AdminConfig(int accessTokenLifetime)
            : base(accessTokenLifetime)
        {
            Audiences = [AudiencesAccess.AuthApi, AudiencesAccess.ChatApi, AudiencesAccess.AdminApi];
        }
    }
}
