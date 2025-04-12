using AutoMapper;
using CadoChat.AuthManager.Models.Model;
using CadoChat.AuthService.MediatR.RequstCommands;

namespace CadoChat.AuthService.MapperConfig
{
    public class LoginUserCommandUserModelProfile : Profile
    {
        public LoginUserCommandUserModelProfile()
        {
            CreateMap<LoginViaEmailCommand, LoginViaEmailModel>();
        }
    }
}
