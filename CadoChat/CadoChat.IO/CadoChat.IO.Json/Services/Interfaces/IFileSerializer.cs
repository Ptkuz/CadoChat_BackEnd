using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.IO.Json.Services.Interfaces
{

    /// <summary>
    /// Сериализатор файлов
    /// </summary>
    public interface IFileSerializer
    {

        /// <summary>
        /// Десериализовать файл в объект
        /// </summary>
        /// <typeparam name="TResult">Тип Десериализованного объекта</typeparam>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="propertyNameCaseInsensitive">Регистронезависимость соответствия имен</param>
        /// <returns>Десериализованный объект</returns>
        TResult DeserializeFile<TResult>(string filePath, bool propertyNameCaseInsensitive) 
            where TResult : class;

    }
}
