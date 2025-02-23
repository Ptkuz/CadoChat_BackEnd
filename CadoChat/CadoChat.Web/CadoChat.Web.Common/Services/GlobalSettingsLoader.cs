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
    public class GlobalSettingsLoader : IGlobalSettingsLoader
    {

        private readonly string _globalConfigPath;
        private readonly IFileSerializer _serializer;


        private GlobalSettings globalSettings;

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

        private static GlobalSettingsLoader instance;
        
        public static GlobalSettingsLoader GetInstance(string? globalConfigPath = null, IFileSerializer? fileSerializer = null)
        {
            if (instance == null)
            {
                instance = new GlobalSettingsLoader(globalConfigPath, fileSerializer);
            }

            return instance;
        }

        private GlobalSettingsLoader(string globalConfigPath, IFileSerializer fileSerializer)
        {

            if (string.IsNullOrEmpty(globalConfigPath))
            {
                throw new ArgumentNullException($"Путь до CadoChat_global.json не указан");
            }

            _globalConfigPath = globalConfigPath;
            _serializer = fileSerializer;
        }

        private GlobalSettings Init()
        {
            var settings = _serializer.DeserializeFile<GlobalSettings>(_globalConfigPath, true);

            return settings;
        }
    }
}
