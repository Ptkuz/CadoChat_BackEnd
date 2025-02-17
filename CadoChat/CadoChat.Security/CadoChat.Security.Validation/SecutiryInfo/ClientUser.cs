using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.ConfigLoad.Config;
using CadoChat.Security.Validation.SecutiryInfo.Interfaces;
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
            Id = "client_id";
            Scopes = [AccessScopes.AdminApi.Key];
            AccessTokenLifetime = SecurityConfigLoader.SecurityConfig.ClientUser.AccessTokenLifetime;
            AllowedGrantTypes = GrantTypes.ClientCredentials;
            RequireClientSecret = false;
        }
    }
}
