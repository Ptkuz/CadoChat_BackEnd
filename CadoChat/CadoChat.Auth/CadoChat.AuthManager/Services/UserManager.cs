using AutoMapper;
using CadoChat.Auth.EF;
using CadoChat.Auth.EF.Entities;
using CadoChat.AuthManager.Models.Model;
using CadoChat.AuthManager.Models.Result;
using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.DAL.EF;
using CadoChat.DAL.Entity.BaseUnitOfWork;
using CadoChat.DAL.Entity.FacadeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.AuthManager.Services
{
    public class UserManager : IUserManager
    {

        private readonly IMapper _mapper;
        private readonly IAuthUnifOfWork _unifOfWork;
        private readonly ITokenManagerService<User> _tokenManagerService;

        public UserManager(IAuthUnifOfWork unitOfWork, IMapper mapper, ITokenManagerService<User> tokenManagerService)
        {
            _unifOfWork = unitOfWork;
            _mapper = mapper;
            _tokenManagerService = tokenManagerService;
        }

        public async Task<LoginUserResult> LoginAsync(LoginViaEmailModel model)
        {
            User? existingUser = await 
                CheckExistUser(user => user.Email == model.Email)
                .ConfigureAwait(false);

            if (existingUser == null)
            {
                var message = $"Пользователя с Email {model.Email} не существует в системе";
                return new LoginUserResult(false, message);
            }

            var inputHashPassword = HashPassword(model.Password);

            if (existingUser.PasswordHash != inputHashPassword)
            {
                var message = $"Не правильный пароль";
                return new LoginUserResult(false , message);
            }

            var token = _tokenManagerService.CreateAccessTokenAsync(existingUser);

            if (token == null)
            {
                var message = $"Не удалось сгенерировать токен для пользователя {existingUser.UserName}";
                return new LoginUserResult(false, message);
            }

            return new LoginUserResult(true, token, "Данные для входа валидные");
        }

        public async Task<RegisterUserResult> RegisterUserAsync(RegisterModel model)
        {
            User? existingUser = await 
                CheckExistUser(user => 
            user.Email == model.Email || 
            user.UserName == model.Username || 
            user.PhoneNumber == model.PhoneNumber)
                .ConfigureAwait(false);

            if (existingUser != null)
            {
                var message = $"Пользователь с ником {model.Username} уже зарегестрирован";
                return new RegisterUserResult(false, message);
            }

            var user = _mapper.Map<User>(model);
            user.PasswordHash = HashPassword(model.Password);
            return await AddUser(model, user).ConfigureAwait(false);
        }

        private async Task<RegisterUserResult> AddUser(RegisterModel userModel, User user)
        {
            var userAddedResult = await
                            _unifOfWork.UserRepository
                            .CreateRepository
                            .AddEntityAsync(user)
                            .ConfigureAwait(false);

            if (userAddedResult.Success)
            {
                await _unifOfWork.SaveChangesAsync();
                var message = $"Пользователь {userModel.Username} успешно добавлен в систему!";
                return new RegisterUserResult(true, user, message);
            }
            else
            {
                var message = userAddedResult.Message;
                return new RegisterUserResult(false, message);
            }
        }

        private async Task<User?> CheckExistUser(Expression<Func<User, bool>> expression)
        {
            return await
                            _unifOfWork
                            .UserRepository
                            .SelectRepository
                            .FirstOfDefaultAsync(expression)
                            .ConfigureAwait(false);
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }
    }
}
