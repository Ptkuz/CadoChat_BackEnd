using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Services.Interfaces
{

    /// <summary>
    /// Инициализатор приложения
    /// </summary>
    public interface IApplicationBuilderInitializer
    {

        /// <summary>
        /// Получить сервис для инициализации приложения
        /// </summary>
        /// <typeparam name="TService">Тип сервиса должен наследоваться от <see cref="IConfigurationService"/></typeparam>
        /// <param name="type">Тип сервиса</param>
        /// <returns>Сервис для инициализации приложения</returns>
        TService GetService<TService>(Type type) 
            where TService : IConfigurationService;
    }
}
