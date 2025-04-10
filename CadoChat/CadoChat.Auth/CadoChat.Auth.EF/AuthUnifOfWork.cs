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

        public IFacadeRepository<LoginAttempt> LoginAttemptRepository { get; }
        public IFacadeRepository<RefreshToken> RefreshTokenRepository { get; }
        public IFacadeRepository<Role> RoleRepository { get; }
        public IFacadeRepository<User> UserRepository { get; }
        public IFacadeRepository<UserRole> UserRoleRepository { get; }

        public AuthUnifOfWork(AuthDbContext context, 
        IFacadeRepository<LoginAttempt> loginAttemptRepository,
        IFacadeRepository<RefreshToken> refreshTokenRepository,
        IFacadeRepository<Role> roleRepository,
        IFacadeRepository<User> userRepository,
        IFacadeRepository<UserRole> userRoleRepository) 
            : base(context)
        {
            LoginAttemptRepository = loginAttemptRepository;
            RefreshTokenRepository = refreshTokenRepository;
            RoleRepository = roleRepository;
            UserRepository = userRepository;
            UserRoleRepository = userRoleRepository;
        }
    }
}
