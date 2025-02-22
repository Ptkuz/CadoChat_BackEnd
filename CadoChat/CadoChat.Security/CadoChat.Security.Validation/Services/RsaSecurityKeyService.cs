using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.Services.Interfaces;
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

        private RsaSecurityKeyService()
        {

        }

        private static ISecurityKeyService<RsaSecurityKey>? instance;

        public static ISecurityKeyService<RsaSecurityKey> GetInstance()
        {

            if (instance == null)
            {
                instance = new RsaSecurityKeyService();
            }

            return instance;
        }

        private RsaSecurityKey GetKey()
        {
            var privateKey = SecurityConfigLoader.SecurityConfig._privateKey;

            var rsa = RSA.Create(2048);
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
            var signingKey = new RsaSecurityKey(rsa)
            {
                KeyId = SecurityConfigLoader.SecurityConfig.SecretKey,
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
