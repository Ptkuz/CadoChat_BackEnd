using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Services.Interfaces
{

    /// <summary>
    /// Базовый конфигуратор сервисов
    /// </summary>
    public interface IConfigurationService
    {

        /// <summary>
        /// Добавить сервис
        /// </summary>
        /// <param name="webApplicationBuilder">Строитель приложения</param>
        void AddService(WebApplicationBuilder webApplicationBuilder);


        /// <summary>
        /// Использовать сервис
        /// </summary>
        /// <param name="applicationBuilder">Собранное приложение</param>
        void UseService(WebApplication applicationBuilder);
    }
}
