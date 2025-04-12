using CadoChat.Auth.EF.Entities;
using CadoChat.DAL.Entity.BaseUnitOfWork;
using CadoChat.DAL.Entity.FacadeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Auth.EF
{
    public interface IAuthUnifOfWork : IUnifOfWork
    {
        IFacadeRepository<UserClaim> UserClaimRepository { get; }
        IFacadeRepository<UserLogin> UserLoginRepository { get; }
        IFacadeRepository<Role> RoleRepository { get; }
        IFacadeRepository<User> UserRepository { get; }
        IFacadeRepository<UserRole> UserRoleRepository { get; }
        IFacadeRepository<UserToken> UserTokenRepository { get; }
    }
}
