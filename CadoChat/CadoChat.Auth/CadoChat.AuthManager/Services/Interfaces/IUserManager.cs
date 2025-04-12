using CadoChat.AuthManager.Models.Model;
using CadoChat.AuthManager.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.AuthManager.Services.Interfaces
{
    public interface IUserManager
    {
        Task<RegisterUserResult> RegisterUserAsync(RegisterModel model);

        Task<LoginUserResult> LoginAsync(LoginViaEmailModel model);

    }
}
