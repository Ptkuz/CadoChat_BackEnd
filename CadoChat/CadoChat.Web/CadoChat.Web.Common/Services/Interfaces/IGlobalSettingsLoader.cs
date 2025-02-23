using CadoChat.IO.Json.Services.Interfaces;
using CadoChat.Web.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Services.Interfaces
{

    /// <summary>
    /// Загрузчик глобальных настроек окружения для межсервисного взаимодействия
    /// </summary>
    public interface IGlobalSettingsLoader
    {

        /// <summary>
        /// Глобальные настройки окружения
        /// </summary>
        GlobalSettings GlobalSettings { get; }
    }
}
