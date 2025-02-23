using CadoChat.Web.Common.Settings.Service.Audience;
using CadoChat.Web.Common.Settings.Service.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Settings.Service.Base
{

    /// <summary>
    /// Конфигурация сервиса
    /// </summary>
    public class ServiceConfig
    {

        /// <summary>
        /// Идентификатор сервиса
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// URL сервиса
        /// </summary>
        public string URL { get; set; } = null!;

        /// <summary>
        /// Получатель токена доступа
        /// </summary>
        public AudiencesAccess AudiencesAccess { get; set; } = null!;

    }
}
