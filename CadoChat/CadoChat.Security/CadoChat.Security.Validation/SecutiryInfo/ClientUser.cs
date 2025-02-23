using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.ConfigLoad.Config;
using CadoChat.Security.Validation.SecutiryInfo.Interfaces;
using CadoChat.Web.Common.Services;
using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Validation.SecutiryInfo
{
    public class ClientUser : ISecurityUser
    {

        public string Id { get; private set; }

        public string[] Scopes { get; private set; }

        public int AccessTokenLifetime { get; private set; }

        public ICollection<string> AllowedGrantTypes { get; private set; }

        public bool RequireClientSecret { get; private set; }


        public ClientUser()
        {

            var settings = GlobalSettingsLoader.GetInstance().GlobalSettings;

            var clientUser = settings.Users.ClientUser;
            var authService = settings.Services.AuthService;
            var chatService = settings.Services.ChatService;

            string[] scopes = [chatService.SendMessageScope.Name, chatService.ReceiveMessageScope.Name];

            Id = clientUser.Id;
            Scopes = scopes;
            AccessTokenLifetime = clientUser.AccessTokenLifetime;
            AllowedGrantTypes = GrantTypes.ClientCredentials;
            RequireClientSecret = clientUser.RequireClientSecret;
        }
    }
}
