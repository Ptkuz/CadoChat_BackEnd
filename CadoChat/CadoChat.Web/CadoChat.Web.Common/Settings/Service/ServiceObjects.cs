using CadoChat.Web.Common.Settings.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Settings.Service
{

    /// <summary>
    /// Конфигурация сервисов
    /// </summary>
    public class ServiceObjects
    {

        /// <summary>
        /// Конфигурация сервиса авторизации
        /// </summary>
        public ServiceConfig AuthService { get; set; }

        /// <summary>
        /// Конфигурация сервиса чата
        /// </summary>
        public ChatService ChatService { get; set; }

        /// <summary>
        /// Конфигурация API шлюза
        /// </summary>
        public ServiceConfig API_Gateway { get; set; }
    }
}
