using CadoChat.AuthManager.Exceptions;
using CadoChat.Web.AspNetCore.WebResponse;

namespace CadoChat.AuthService.MediatR.Responses
{
    public class LoginUserResponse : BaseResponse
    {
        public string? Token { get;set; }

        public LoginUserResponse(string token)
            : base()
        {
            Token = token;
        }

        public LoginUserResponse(LoginUserException ex)
            : base(ex)
        {
        }
    }
}
