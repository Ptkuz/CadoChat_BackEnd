using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Validation.SecutiryInfo.Interfaces
{

    /// <summary>
    /// Пользователь с правами безопасности
    /// </summary>
    public interface ISecurityUser
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Список разрешений
        /// </summary>
        string[] Scopes { get; }

        /// <summary>
        /// Время жизни токена доступа
        /// </summary>
        int AccessTokenLifetime { get; }

        /// <summary>
        /// Разрешенные типы грантов
        /// </summary>
        ICollection<string> AllowedGrantTypes { get; }

        /// <summary>
        /// Требуется ли секрет клиента
        /// </summary>
        bool RequireClientSecret { get; }
    }
}
