using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Settings.Service.Scope
{

    /// <summary>
    /// Конфигурация области видимости
    /// </summary>
    public class ScopeConfig
    {
        /// <summary>
        /// Идентификатор области видимости
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Отображаемое значение области видимости
        /// </summary>
        public string DisplayValiue { get; set; } = null!;
    }
}
