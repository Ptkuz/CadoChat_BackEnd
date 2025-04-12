using CadoChat.AuthManager.Exceptions;
using CadoChat.Web.AspNetCore.WebResponse;

namespace CadoChat.AuthService.MediatR.Responses
{
    public class RegisterUserResponse : BaseResponse
    {
        public string Username { get; private set; }

        public RegisterUserResponse(string username, RegisterUserException ex)
            : base(ex)
        {
            Username = username;
        }

        public RegisterUserResponse(string username)
            : base()
        {
            Username = username;
        }
    }
}
