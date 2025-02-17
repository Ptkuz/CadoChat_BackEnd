using Microsoft.AspNetCore.Identity;

namespace CadoChat.AuthManager.Services.Interfaces
{
    public interface ITokenManagerService
    {

        string CreateAccessTokenAsync(IdentityUser user);

    }
}
