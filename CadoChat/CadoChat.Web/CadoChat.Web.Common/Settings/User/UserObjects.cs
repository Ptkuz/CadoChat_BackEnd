using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Settings.User
{

    /// <summary>
    /// Конфигурация пользователей
    /// </summary>
    public class UserObjects
    {

        /// <summary>
        /// Конфигурация клиента
        /// </summary>
        public UserConfig ClientUser { get; set; } = null!;

        /// <summary>
        /// Конфигурация администратора
        /// </summary>
        public UserConfig AdminUser { get; set; } = null!;
    }
}
