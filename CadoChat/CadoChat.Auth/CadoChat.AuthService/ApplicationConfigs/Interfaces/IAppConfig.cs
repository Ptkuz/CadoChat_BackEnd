namespace CadoChat.AuthService.ApplicationConfigs.Interfaces
{
    public interface IAppConfig
    {
        string APIGatewayURL { get; set; }

        string AuthServiceURL { get; set; }
    }
}
