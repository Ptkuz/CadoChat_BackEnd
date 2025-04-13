using CadoChat.DAL.Entity.BaseEntity;

namespace CadoChat.AuthManager.Services.Interfaces
{
    public interface ITokenManagerService<TUser> where TUser : IEntity
    {

        string GenerateToken(string userId, string username, List<string> roles, Dictionary<string, string> customClaims, List<string> scopes);

    }
}
