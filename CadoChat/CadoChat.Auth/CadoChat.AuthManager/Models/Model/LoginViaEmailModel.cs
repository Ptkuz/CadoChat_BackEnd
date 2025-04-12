using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.AuthManager.Models.Model
{
    public class LoginViaEmailModel
    {

        public string Email { get; private set; }
        public string Password { get; private set; }

        public LoginViaEmailModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
