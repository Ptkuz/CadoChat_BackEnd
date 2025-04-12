using CadoChat.AuthService.MediatR.Responses;
using CadoChat.Web.AspNetCore.MediatR;

namespace CadoChat.AuthService.MediatR.RequstCommands
{
    public class LoginViaEmailCommand : IRequestCommand<LoginUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginViaEmailCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
