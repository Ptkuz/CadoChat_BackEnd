using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.Common.Services;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace CadoChat.Security.Validation.Services
{

    /// <summary>
    /// Сервис для работы с ключом безопасности
    /// </summary>
    public class RsaSecurityKeyService : ISecurityKeyService<RsaSecurityKey>
    {

        /// <summary>
        /// Ключ безопасности RSA
        /// </summary>
        private RsaSecurityKey? key;

        /// <summary>
        /// Ключ безопасности RSA
        /// </summary>
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

        /// <summary>
        /// Параметры подписи
        /// </summary>
        private SigningCredentials? signingCredentials;

        /// <summary>
        /// Параметры подписи
        /// </summary>
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

        /// <summary>
        /// Инициализировать сервис для работы с ключом безопасности
        /// </summary>
        public RsaSecurityKeyService()
        {

        }

        /// <summary>
        /// Получить ключ безопасности
        /// </summary>
        /// <returns>Ключ безопасности RSA</returns>
        private RsaSecurityKey GetKey()
        {
            var globalSettins = GlobalSettingsLoader.Instance!.GlobalSettings;

            var rsa = RSA.Create(2048);
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(globalSettins._privateKey), out _);
            var signingKey = new RsaSecurityKey(rsa)
            {
                KeyId = globalSettins.SecretKey,
            };

            return signingKey;
        }

        /// <summary>
        /// Получить параметры подписи
        /// </summary>
        /// <returns>Параметры подписи</returns>
        private SigningCredentials GetSigningCredentials()
        {
            var rsaKey = GetKey();
            return new SigningCredentials(rsaKey, SecurityAlgorithms.RsaSha256);
        }
    }
}
