using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.AuthManager.Models.Base
{
    public class UserManagerResult
    {
        public bool Success { get; private set; }

        public UserManagerResult(bool success)
        {
            Success = success;
        }
    }
}
