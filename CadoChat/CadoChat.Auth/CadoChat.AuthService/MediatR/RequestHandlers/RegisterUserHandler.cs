using AutoMapper;
using CadoChat.AuthManager.Exceptions;
using CadoChat.AuthManager.Models.Model;
using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.AuthService.MediatR.RequstCommands;
using CadoChat.AuthService.MediatR.Responses;
using CadoChat.Web.AspNetCore.MediatR;

namespace CadoChat.AuthService.MediatR.RequestHandlers
{
    public class RegisterUserHandler : ICadoRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {

        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public RegisterUserHandler(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userModel = _mapper.Map<RegisterModel>(request);

            if (userModel != null)
            {
                var registerResult = await _userManager.RegisterUserAsync(userModel);

                if (registerResult.Success)
                {
                    return new RegisterUserResponse(registerResult.RegisterUser.UserName);
                }
                else
                {
                    return new RegisterUserResponse(registerResult.RegisterUser.UserName, new RegisterUserException(registerResult.Message));
                }

            }

            return new RegisterUserResponse(request.Username, new RegisterUserException("Не удалось смаппить объект"));
        }
    }
}
