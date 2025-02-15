namespace CudoChat.API_Gateway.ApplicationConfigs.Interfaces
{
    public interface IAppConfig
    {
        string APIGatewayURL { get; set; }

        string AuthServiceURL { get; set; }
    }
}
