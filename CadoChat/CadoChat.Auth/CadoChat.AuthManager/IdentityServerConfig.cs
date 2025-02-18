using CadoChat.Security.Validation.SecutiryInfo;
using CadoChat.Security.Validation.SecutiryInfo.Interfaces;
using Duende.IdentityServer.Models;

namespace CadoChat.AuthManager
{
    public static class IdentityServerConfig
    {

        public static ISecurityUser ClientUser =>
            new ClientUser();   

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
            new Client
            {
                ClientId = ClientUser.Id,
                RequireClientSecret = ClientUser.RequireClientSecret, 
                AllowedGrantTypes = ClientUser.AllowedGrantTypes,
                AllowedScopes = ClientUser.Scopes,
                AccessTokenLifetime = ClientUser.AccessTokenLifetime
            }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
            new ApiScope(AccessScopes.SendMessage.Key, AccessScopes.ReceiveMessage.Value)
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
            };
    }
}
