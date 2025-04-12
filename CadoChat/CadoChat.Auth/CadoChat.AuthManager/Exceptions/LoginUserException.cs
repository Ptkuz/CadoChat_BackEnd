using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.AuthManager.Exceptions
{
    public class LoginUserException : Exception
    {
        public LoginUserException()
            : base()
        {

        }

        public LoginUserException(string message)
            : base(message)
        {

        }
    }
}
