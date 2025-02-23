using CadoChat.Security.Validation.ConfigLoad.Config.Base;
using CadoChat.Security.Validation.SecutiryInfo;
using CadoChat.Web.Common.Services;

namespace CadoChat.Security.Validation.ConfigLoad.Config
{
    public class ClientConfig : UserConfig
    {

        public override IList<string> Audiences { get; protected set; }

        public ClientConfig(int accessTokenLifetime)
            : base(accessTokenLifetime)
        {
            //var globalSettings = GlobalSettingsLoader.GetInstance().GlobalSettings;

            //Audiences = [globalSettings.];
        }


    }
}
