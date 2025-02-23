using CadoChat.Web.Common.Services;
using CadoChat.Web.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common
{
    public class ConfigurationService
    {
        protected GlobalSettings GlobalSettings 
        { 
            get
            {
                var instance = GlobalSettingsLoader.GetInstance();

                if (instance == null)
                {
                    throw new ArgumentNullException($"Глобальный конфиг не проинициализирован!");
                }

                return instance.GlobalSettings;
            }
        }

    }
}
