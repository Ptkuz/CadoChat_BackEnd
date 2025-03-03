using Microsoft.AspNetCore.Identity;

namespace CadoChat.AuthManager.Services.Interfaces
{
    public interface ITokenManagerService<TUser> where TUser : IdentityUser<Guid>
    {

        string CreateAccessTokenAsync(TUser user);

    }
}
