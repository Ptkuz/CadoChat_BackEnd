using CadoChat.AuthService.Services.Interfaces;
using IdentityModel;
using IdentityServer8;
using IdentityServer8.Models;
using IdentityServer8.Services;
using IdentityServer8.Validation;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CadoChat.AuthService.Services
{
    public class TokenService : ITokenGenService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TokenService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GenerateTokenAsync()
        {
            var tokenEndpoint = _configuration["IdentityServer:TokenEndpoint"];

            var client = new HttpClient();
            var response = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", "chat_client" },
            { "client_secret", "secret" },
            { "grant_type", "client_credentials" },
            { "scope", "chat_api" }
        }));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Ошибка при получении токена");
            }

            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return tokenResponse.AccessToken;
        }
    }

    public class TokenResponse
    {
        public string AccessToken { get; set; }
    }
}
