using CadoChat.IO.Json.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CadoChat.IO.Json.Services
{
    public class FileSerializer : IFileSerializer
    {

        public FileSerializer()
        {

        }

        public TResult DeserializeFile<TResult>(string filePath, bool propertyNameCaseInsensitive) 
            where TResult : class
        {

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Config file not found: {filePath}");

            var json = File.ReadAllText(filePath);
            var configObject =
                JsonSerializer.Deserialize<TResult>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (configObject == null)
            {
                throw new ArgumentNullException("Config object is null");
            }

            return configObject;

        }
    }
}
