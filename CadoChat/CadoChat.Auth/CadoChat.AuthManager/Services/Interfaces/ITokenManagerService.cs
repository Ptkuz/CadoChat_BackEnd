using CadoChat.DAL.Entity.BaseEntity;

namespace CadoChat.AuthManager.Services.Interfaces
{
    public interface ITokenManagerService<TUser> where TUser : IEntity
    {

        string CreateAccessTokenAsync(TUser user);

    }
}
