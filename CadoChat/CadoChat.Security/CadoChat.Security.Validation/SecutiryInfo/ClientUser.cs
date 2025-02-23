using CadoChat.Security.Validation.SecutiryInfo.Interfaces;
using CadoChat.Web.Common.Services;
using Duende.IdentityServer.Models;

namespace CadoChat.Security.Validation.SecutiryInfo
{

    /// <summary>
    /// Пользователь с правами клиента
    /// </summary>
    public class ClientUser : ISecurityUser
    {

        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Список разрешений
        /// </summary>
        public string[] Scopes { get; private set; }

        /// <summary>
        /// Время жизни токена доступа
        /// </summary>
        public int AccessTokenLifetime { get; private set; }

        /// <summary>
        /// Разрешенные типы грантов
        /// </summary>
        public ICollection<string> AllowedGrantTypes { get; private set; }

        /// <summary>
        /// Требуется ли секрет клиента
        /// </summary>
        public bool RequireClientSecret { get; private set; }

        /// <summary>
        /// Инициализировать клиента с правами пользователя
        /// </summary>
        public ClientUser()
        {

            var settings = GlobalSettingsLoader.Instance!.GlobalSettings;

            var clientUser = settings.Users.ClientUser;
            var authService = settings.Services.AuthService;
            var chatService = settings.Services.ChatService;

            string[] scopes = [chatService.ChatScopeConfig.ReceiveMessageScope.Name,
                chatService.ChatScopeConfig.SendMessageScope.Name];

            Id = clientUser.Id;
            Scopes = scopes;
            AccessTokenLifetime = clientUser.AccessTokenLifetime;
            AllowedGrantTypes = GrantTypes.ClientCredentials;
            RequireClientSecret = clientUser.RequireClientSecret;
        }
    }
}
