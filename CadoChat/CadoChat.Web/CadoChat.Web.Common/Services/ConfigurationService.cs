using CadoChat.Web.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Services
{

    /// <summary>
    /// Базовый конфигуратор сервисов
    /// </summary>
    public class ConfigurationService
    {

        /// <summary>
        /// Глобальные настройки окружения
        /// </summary>
        protected GlobalSettings GlobalSettings 
        { 
            get
            {
                var instance = GlobalSettingsLoader.Instance;

                if (instance == null)
                {
                    throw new ArgumentNullException($"Глобальный конфиг не проинициализирован!");
                }

                return instance.GlobalSettings;
            }
        }

    }
}
