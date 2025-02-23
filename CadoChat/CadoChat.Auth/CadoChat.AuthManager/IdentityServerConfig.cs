using CadoChat.Security.Validation.SecutiryInfo;
using CadoChat.Security.Validation.SecutiryInfo.Interfaces;
using CadoChat.Web.Common.Services;
using CadoChat.Web.Common.Settings;
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


        public static IEnumerable<ApiResource> GetApis()
        {

            var authService = GlobalSettings.Services.AuthService;
            var chatService = GlobalSettings.Services.ChatService;

            var authResource = new ApiResource(authService.AudiencesAccess.Name, authService.AudiencesAccess.DisplayValue)
            {
                Scopes = authService.Scopes.Keys.ToList()
            };

            var chatResource = new ApiResource(chatService.AudiencesAccess.Name, chatService.AudiencesAccess.DisplayValue)
            {
                Scopes = chatService.Scopes.Keys.ToList()
            };

            return new List<ApiResource>()
            {
                authResource,
                chatResource
            };
        }


        public static IEnumerable<ApiScope> ApiScopes() 
        {

            var authService = GlobalSettings.Services.AuthService;
            var chatService = GlobalSettings.Services.ChatService;

            var aggregateScopes = authService.Scopes.Concat(chatService.Scopes);

            foreach (var scope in aggregateScopes)
            {
                yield return new ApiScope(scope.Key, scope.Value);
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
