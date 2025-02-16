using Microsoft.AspNetCore.Identity;

namespace CadoChat.AuthService.Services.Interfaces
{
    public interface ITokenGenService
    {
        Task<string> CreateAccessTokenAsync(IdentityUser user);
    }
}
