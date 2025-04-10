using CadoChat.Auth.EF.Context;
using CadoChat.DAL.EF.FacadeRepository;
using CadoChat.DAL.Entity.BaseEntity;
using CadoChat.DAL.Entity.FacadeRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Auth.EF.FacadeRepository
{
    public class AuthSelectRepository<TEntity> : 
        SelectRepository<TEntity> where TEntity : Entity
    {
        public AuthSelectRepository(AuthDbContext context) 
            : base(context)
        {

        }
    }
}
