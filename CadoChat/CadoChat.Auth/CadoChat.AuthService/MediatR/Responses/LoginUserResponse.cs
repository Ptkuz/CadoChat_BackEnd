using CadoChat.AuthManager.Exceptions;
using CadoChat.Web.AspNetCore.WebResponse;

namespace CadoChat.AuthService.MediatR.Responses
{
    public class LoginUserResponse : BaseResponse
    {
        public string? Token { get;set; }

        public LoginUserResponse(string token, string message)
            : base(message)
        {
            Token = token;
        }

        public LoginUserResponse(LoginUserException ex)
            : base(ex)
        {
        }
    }
}
