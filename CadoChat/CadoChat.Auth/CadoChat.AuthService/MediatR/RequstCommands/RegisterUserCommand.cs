using CadoChat.AuthService.MediatR.Responses;
using CadoChat.Web.AspNetCore.MediatR;

namespace CadoChat.AuthService.MediatR.RequstCommands
{
    public class RegisterUserCommand : IRequestCommand<RegisterUserResponse>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public RegisterUserCommand(string username, string email, string password, string phoneNumber)
        {
            Username = username;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
        }
    }
}
