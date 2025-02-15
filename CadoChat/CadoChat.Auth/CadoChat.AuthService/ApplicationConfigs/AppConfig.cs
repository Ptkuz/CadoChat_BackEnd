using CadoChat.AuthService.ApplicationConfigs.Interfaces;

namespace CadoChat.AuthService.ApplicationConfigs
{
    public class AppConfig : IAppConfig
    {
        public string APIGatewayURL { get; set; }

        public string AuthServiceURL { get; set; }

        public AppConfig(string apiGatewayURL, string authServiceURL)
        {
            APIGatewayURL = apiGatewayURL;
            AuthServiceURL = authServiceURL;
        }
    }
}
