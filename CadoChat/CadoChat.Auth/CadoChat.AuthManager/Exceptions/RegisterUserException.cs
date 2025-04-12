using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.AuthManager.Exceptions
{
    public class RegisterUserException : Exception
    {
        public RegisterUserException()
            : base()
        {

        }

        public RegisterUserException(string message)
            : base(message)
        {

        }
    }
}
