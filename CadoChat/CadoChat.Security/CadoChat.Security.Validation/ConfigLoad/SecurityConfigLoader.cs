using CadoChat.Security.Validation.ConfigLoad.Config;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CadoChat.Security.Validation.ConfigLoad
{
    public static class SecurityConfigLoader
    {

        public static SecurityConfig SecurityConfig { get; private set; }
        public static void Init(IConfiguration configuration)
        {

            var config = configuration?["SecurityFilePath"];

            if (!File.Exists(config))
                throw new FileNotFoundException($"Config file not found: {config}");

            var json = File.ReadAllText(config);
            var configObject =
                JsonSerializer.Deserialize<SecurityConfig>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (configObject == null)
            {
                throw new ArgumentNullException("Config object is null");
            }

            SecurityConfig = configObject;
        }
    }
}
