using CadoChat.Security.Validation.ConfigLoad;
using CadoChat.Security.Validation.ConfigLoad.Config;
using CadoChat.Security.Validation.SecutiryInfo.Interfaces;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Validation.SecutiryInfo
{
    public static class RsaSecurityKeyService 
    {

        public static RsaSecurityKey GetKey()
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

        public static SigningCredentials GetSigningCredentials(RsaSecurityKey rsaSecurityKey)
        {
            return new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);
        }

        public static SigningCredentials GetSigningCredentials()
        {
            var rsaKey = GetKey();
            return new SigningCredentials(rsaKey, SecurityAlgorithms.RsaSha256);
        }
    }
}
