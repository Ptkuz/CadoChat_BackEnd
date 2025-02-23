using CadoChat.Security.Validation.SecutiryInfo;
using CadoChat.Security.Validation.SecutiryInfo.Interfaces;
using CadoChat.Web.Common.Services;
using CadoChat.Web.Common.Settings;
using CadoChat.Web.Common.Settings.Service.Scope;
using Duende.IdentityServer.Models;

namespace CadoChat.AuthManager
{
    public static class IdentityServerConfig
    {

        public static GlobalSettings GlobalSettings { get; set; }

        static IdentityServerConfig()
        {
            GlobalSettings = GlobalSettingsLoader.GetInstance().GlobalSettings;
        }

        public static ISecurityUser ClientUser =>
            new ClientUser();

        public static IEnumerable<Client> GetClients()
        {

            var chatClient = new Client
            {
                ClientId = ClientUser.Id,
                RequireClientSecret = ClientUser.RequireClientSecret,
                AllowedGrantTypes = ClientUser.AllowedGrantTypes,
                AllowedScopes = ClientUser.Scopes,
                AccessTokenLifetime = ClientUser.AccessTokenLifetime
            };


            return new List<Client>
            {
                chatClient
            };
        }


        public static IEnumerable<ApiScope> ApiScopes() 
        {

            var authService = GlobalSettings.Services.AuthService;
            var chatService = GlobalSettings.Services.ChatService;

            ScopeConfig[] aggregateScopes = [chatService.ReceiveMessageScope, 
                chatService.SendMessageScope];
             
            foreach (var scope in aggregateScopes)
            {
                yield return new ApiScope(scope.Name, scope.DisplayValiue);
            }


        }

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
            };
    }
}
