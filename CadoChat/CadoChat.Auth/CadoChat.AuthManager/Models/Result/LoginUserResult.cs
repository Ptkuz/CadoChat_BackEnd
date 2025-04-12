using CadoChat.Auth.EF.Entities;
using CadoChat.AuthManager.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.AuthManager.Models.Result
{
    public class LoginUserResult : UserManagerResult
    {

        public string Token { get; private set; }

        public string? Message { get; private set; }

        public LoginUserResult(bool success, string? message = null)
            : base(success)
        {
            Message = message;
        }

        public LoginUserResult(bool success, string token, string? message = null)
            : base(success)
        {
            Token = token;
            Message = message;
        }
    }
}
