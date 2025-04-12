using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.AuthManager.Models.Model
{
    public class RegisterModel
    {
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }

        public RegisterModel(string username, string email, string password, string phoneNumber)
        {
            Username = username;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
        }
    }
}
