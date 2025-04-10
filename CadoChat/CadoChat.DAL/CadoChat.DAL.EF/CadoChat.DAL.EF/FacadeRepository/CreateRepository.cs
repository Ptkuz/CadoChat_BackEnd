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
    public class CreateRepository<TEntity> : Repository<TEntity>, ICreateRepository<TEntity>
        where TEntity : Entity.BaseEntity.Entity
    {
        protected CreateRepository(DbContext context) 
            : base(context)
        {
        }

        public async Task<EntityResult> AddEntityAsync(TEntity entity, CancellationToken cancel = default)
        {
            entity.Validate();
            await _dbSet.AddAsync(entity, cancel);
            return new EntityResult();
        }
    }
}
