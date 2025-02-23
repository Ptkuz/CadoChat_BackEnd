using Microsoft.IdentityModel.Tokens;

namespace CadoChat.Security.Validation.Services.Interfaces
{

    /// <summary>
    /// Сервис для работы с ключом безопасности
    /// </summary>
    /// <typeparam name="TKey">Ключ безопасности</typeparam>
    public interface ISecurityKeyService<TKey>
        where TKey : SecurityKey
    {

        /// <summary>
        /// Ключ безопасности
        /// </summary>
        TKey Key { get; }


        /// <summary>
        /// Параметры подписи
        /// </summary>
        SigningCredentials SigningCredentials { get; }
    }
}
