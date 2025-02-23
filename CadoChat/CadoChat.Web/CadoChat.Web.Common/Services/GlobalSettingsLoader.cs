using CadoChat.IO.Json.Services.Interfaces;
using CadoChat.Web.Common.Services.Interfaces;
using CadoChat.Web.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Services
{

    /// <summary>
    /// Загрузчик глобальных настроек окружения для межсервисного взаимодействия
    /// </summary>
    public class GlobalSettingsLoader : IGlobalSettingsLoader
    {

        /// <summary>
        /// Путь до файла с глобальными настройками окружения
        /// </summary>
        private readonly string _globalConfigPath;

        /// <summary>
        /// Сериализатор файлов
        /// </summary>
        private readonly IFileSerializer _serializer;

        /// <summary>
        /// Глобальные настройки окружения
        /// </summary>
        private GlobalSettings? globalSettings;

        /// <summary>
        /// Глобальные настройки окружения
        /// </summary>
        public GlobalSettings GlobalSettings 
        { 
            get
            {
                if (globalSettings == null)
                {
                    globalSettings = Init();
                }

                return globalSettings;
            }
        }

        /// <summary>
        /// Экземпляр загрузчика глобальных настроек окружения
        /// </summary>
        public static GlobalSettingsLoader? Instance { get; private set; }

        /// <summary>
        /// Получить экземпляр загрузчика глобальных настроек окружения
        /// </summary>
        /// <param name="globalConfigPath">Путь до файла с глобальными настройками окружения</param>
        /// <param name="fileSerializer">Сериализатор файлов</param>
        /// <returns>Экземпляр загрузчика глобальных настроек окружения</returns>
        public static GlobalSettingsLoader GetInstance(string globalConfigPath, IFileSerializer fileSerializer)
        {
            if (Instance == null)
            {
                Instance = new GlobalSettingsLoader(globalConfigPath, fileSerializer);
            }

            return Instance;
        }

        /// <summary>
        /// Инициализировать загрузчик глобальных настроек окружения
        /// </summary>
        /// <param name="globalConfigPath">Путь до файла с глобальными настройками окружения</param>
        /// <param name="fileSerializer">Сериализатор файлов</param>
        /// <exception cref="ArgumentNullException">Ошибка в получении файла CadoChat_global.json</exception>
        private GlobalSettingsLoader(string globalConfigPath, IFileSerializer fileSerializer)
        {

            if (string.IsNullOrEmpty(globalConfigPath))
            {
                throw new ArgumentNullException($"Путь до CadoChat_global.json не указан");
            }

            _globalConfigPath = globalConfigPath;
            _serializer = fileSerializer;
        }

        /// <summary>
        /// Инициализировать глобальные настройки окружения
        /// </summary>
        /// <returns>Глобальные настройки окружения</returns>
        /// <exception cref="ArgumentNullException">Ошибка в получении файла CadoChat_global.json</exception>
        private GlobalSettings Init()
        {

            if (_globalConfigPath == null)
            {
                throw new ArgumentNullException($"Путь до CadoChat_global.json не указан");
            }

            var settings = _serializer.DeserializeFile<GlobalSettings>(_globalConfigPath, true);

            return settings;
        }
    }
}
