using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.Common.Services;
using CadoChat.Web.Common.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Validation.Services
{
    public class RsaSecurityKeyService : ISecurityKeyService<RsaSecurityKey>
    {

        private RsaSecurityKey? key;

        public RsaSecurityKey Key 
        {
            get
            {

                if (key == null)
                {
                    key = GetKey();
                }
                return key;

            } 
        }

        private SigningCredentials? signingCredentials;

        public SigningCredentials SigningCredentials
        {
            get
            {
                if (signingCredentials == null)
                {
                    signingCredentials = GetSigningCredentials();
                }
                return signingCredentials;
            }
        }

        public RsaSecurityKeyService()
        {

        }

        private RsaSecurityKey GetKey()
        {
            var globalSettins = GlobalSettingsLoader.GetInstance().GlobalSettings;

            var rsa = RSA.Create(2048);
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(globalSettins._privateKey), out _);
            var signingKey = new RsaSecurityKey(rsa)
            {
                KeyId = globalSettins.SecretKey,
            };

            return signingKey;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var rsaKey = GetKey();
            return new SigningCredentials(rsaKey, SecurityAlgorithms.RsaSha256);
        }
    }
}
