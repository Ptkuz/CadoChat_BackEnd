using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.IO.Json.Services.Interfaces
{
    public interface IFileSerializer
    {

        TResult DeserializeFile<TResult>(string filePath, bool propertyNameCaseInsensitive) 
            where TResult : class;

    }
}
