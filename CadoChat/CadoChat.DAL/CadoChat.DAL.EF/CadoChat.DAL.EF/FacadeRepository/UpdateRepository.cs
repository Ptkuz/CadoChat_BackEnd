using CadoChat.DAL.EF.FacadeRepository.Base;
using CadoChat.DAL.Entity.FacadeRepository;
using CadoChat.DAL.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.DAL.EF.FacadeRepository
{
    public class UpdateRepository<TEntity> : Repository<TEntity>, IUpdateRepository<TEntity>
        where TEntity : Entity.BaseEntity.Entity
    {
        protected UpdateRepository(DbContext context) 
            : base(context)
        {
        }

        public EntityResult UpdateEntity(TEntity entity)
        {
            entity.Validate();
            _dbSet.Update(entity);
            return new EntityResult();
        }
    }
}
