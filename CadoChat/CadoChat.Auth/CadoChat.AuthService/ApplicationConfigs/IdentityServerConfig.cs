using Duende.IdentityServer.Models;

public static class IdentityServerConfig
{

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "chat_client",
                RequireClientSecret = false, // Убираем секрет, используем RSA
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "chat_api" },
                AccessTokenLifetime = 600
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("chat_api", "Chat API Access")
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
}