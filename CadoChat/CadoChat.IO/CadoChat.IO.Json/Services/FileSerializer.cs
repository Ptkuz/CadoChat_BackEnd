using CadoChat.IO.Json.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CadoChat.IO.Json.Services
{

    /// <summary>
    /// Сериализатор файлов
    /// </summary>
    public class FileSerializer : IFileSerializer
    {

        /// <summary>
        /// Инициализировать сериализатор файлов
        /// </summary>
        public FileSerializer()
        {

        }

        /// <summary>
        /// Десериализовать файл в объект
        /// </summary>
        /// <typeparam name="TResult">Тип Десериализованного объекта</typeparam>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="propertyNameCaseInsensitive">Регистронезависимость соответствия имен</param>
        /// <returns>Десериализованный объект</returns>
        /// <exception cref="FileNotFoundException">Исключение, возникающее при отсутствии файла</exception>
        /// <exception cref="ArgumentNullException">Исключение, возникающее при ошибке в десериализации файла</exception>
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
