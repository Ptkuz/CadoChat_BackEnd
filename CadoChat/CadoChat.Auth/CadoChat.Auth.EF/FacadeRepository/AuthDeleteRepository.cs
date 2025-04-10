using CadoChat.Auth.EF.Context;
using CadoChat.DAL.EF.FacadeRepository;
using CadoChat.DAL.Entity.BaseEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Auth.EF.FacadeRepository
{
    public class AuthDeleteRepository<TEntity> : DeleteRepository<TEntity> where TEntity : Entity
    {
        public AuthDeleteRepository(AuthDbContext context) 
            : base(context)
        {
        }
    }
}
