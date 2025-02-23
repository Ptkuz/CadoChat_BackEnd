using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Common.Exceptions
{

    /// <summary>
    /// Исключение, возникающее при отсутствии URL API Gateway в конфигурационном файле.
    /// </summary>
    public class ApiGatewayURLNotFoundException : Exception
    {

        /// <summary>
        /// Исключение, возникающее при отсутствии URL API Gateway в конфигурационном файле.
        /// </summary>
        public ApiGatewayURLNotFoundException()
            : base("API Gateway URL not found in configuration file.")
        {

        }

        /// <summary>
        /// Исключение, возникающее при отсутствии URL API Gateway в конфигурационном файле.
        /// </summary>
        /// <param name="message">Сообщение</param>
        public ApiGatewayURLNotFoundException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Исключение, возникающее при отсутствии URL API Gateway в конфигурационном файле.
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="inner">Внутрение исключение</param>
        public ApiGatewayURLNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
