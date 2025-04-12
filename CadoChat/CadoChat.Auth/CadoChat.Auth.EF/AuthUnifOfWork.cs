using CadoChat.Auth.EF.Context;
using CadoChat.Auth.EF.Entities;
using CadoChat.DAL.EF;
using CadoChat.DAL.Entity.FacadeRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Auth.EF
{
    public class AuthUnifOfWork : UnitOfWork, IAuthUnifOfWork
    {

        public IFacadeRepository<UserClaim> UserClaimRepository { get; }
        public IFacadeRepository<UserLogin> UserLoginRepository { get; }
        public IFacadeRepository<Role> RoleRepository { get; }
        public IFacadeRepository<User> UserRepository { get; }
        public IFacadeRepository<UserRole> UserRoleRepository { get; }
        public IFacadeRepository<UserToken> UserTokenRepository { get; }

        public AuthUnifOfWork(AuthDbContext context, 
        IFacadeRepository<UserClaim> loginAttemptRepository,
        IFacadeRepository<UserLogin> refreshTokenRepository,
        IFacadeRepository<Role> roleRepository,
        IFacadeRepository<User> userRepository,
        IFacadeRepository<UserRole> userRoleRepository,
        IFacadeRepository<UserToken> userTokenRepository) 
            : base(context)
        {
            UserClaimRepository = loginAttemptRepository;
            UserLoginRepository = refreshTokenRepository;
            RoleRepository = roleRepository;
            UserRepository = userRepository;
            UserRoleRepository = userRoleRepository;
            UserTokenRepository = userTokenRepository;
        }
    }
}
