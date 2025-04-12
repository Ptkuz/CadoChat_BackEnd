using CadoChat.Auth.EF.Entities;
using CadoChat.AuthManager.Models.Base;

namespace CadoChat.AuthManager.Models.Result
{
    public class RegisterUserResult : UserManagerResult
    {

        public User? RegisterUser { get; private set; }

        public string? Message { get; private set; }

        public RegisterUserResult(bool success, string? message = null)
            : base(success)
        {
            Message = message;
        }

        public RegisterUserResult(bool success, User registerUser, string? message = null)
            : base(success)
        {
            RegisterUser = registerUser;
            Message = message;
        }
    }
}
