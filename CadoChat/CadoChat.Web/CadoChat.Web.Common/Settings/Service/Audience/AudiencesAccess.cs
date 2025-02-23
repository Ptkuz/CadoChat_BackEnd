using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Settings.Service.Audience
{

    /// <summary>
    /// Конфигурация получателя токена доступа
    /// </summary>
    public class AudiencesAccess
    {

        /// <summary>
        /// Имя получателя
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Отображаемое значение
        /// </summary>
        public string DisplayValue { get; set; } = null!;
    }
}
