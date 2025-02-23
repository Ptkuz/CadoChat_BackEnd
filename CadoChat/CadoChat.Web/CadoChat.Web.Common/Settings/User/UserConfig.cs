using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Settings.User
{

    /// <summary>
    /// Конфигурация пользователя
    /// </summary>
    public class UserConfig
    {

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string Id { get; set; } = null!;

        /// <summary>
        /// Время жизни токена доступа
        /// </summary>
        public int AccessTokenLifetime { get; set; }

        /// <summary>
        /// Разрешенные типы грантов
        /// </summary>
        public bool RequireClientSecret { get; set; }
    }
}
