using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Common.Exceptions
{
    public class ApiGatewayURLNotFoundException : Exception
    {

        public ApiGatewayURLNotFoundException()
            : base("API Gateway URL not found in configuration file.")
        {

        }
        public ApiGatewayURLNotFoundException(string message)
            : base(message)
        {
        }
        public ApiGatewayURLNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
