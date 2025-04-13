using AutoMapper;
using CadoChat.AuthManager.Exceptions;
using CadoChat.AuthManager.Models.Model;
using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.AuthService.MediatR.RequstCommands;
using CadoChat.AuthService.MediatR.Responses;
using CadoChat.Web.AspNetCore.MediatR;

namespace CadoChat.AuthService.MediatR.RequestHandlers
{
    public class LoginViaEmailUserHandler : ICadoRequestHandler<LoginViaEmailCommand, LoginUserResponse>
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public LoginViaEmailUserHandler(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<LoginUserResponse> Handle(LoginViaEmailCommand request, CancellationToken cancellationToken)
        {
            var userModel = _mapper.Map<LoginViaEmailModel>(request);

            if (userModel != null)
            {
                var loginResult = await _userManager.LoginAsync(userModel);

                if (loginResult.Success)
                {
                    return new LoginUserResponse(loginResult.Token, loginResult.Message);
                }
                else
                {
                    return new LoginUserResponse(new LoginUserException(loginResult.Message!));
                }

            }

            return new LoginUserResponse(new LoginUserException("Не удалось смаппить объект"));
        }
    }
}
