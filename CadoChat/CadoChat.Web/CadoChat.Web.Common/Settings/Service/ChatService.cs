using CadoChat.Web.Common.Settings.Service.Base;
using CadoChat.Web.Common.Settings.Service.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Settings.Service
{

    /// <summary>
    /// Конфигурация сервиса чата
    /// </summary>
    public class ChatService : ServiceConfig
    {

        /// <summary>
        /// Области видимости
        /// </summary>
        public ChatScopeConfig ChatScopeConfig { get; set; } = null!;
    }
}
