using CadoChat.DAL.EF.FacadeRepository.Base;
using CadoChat.DAL.Entity.FacadeRepository;
using CadoChat.DAL.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace CadoChat.DAL.EF.FacadeRepository
{
    public class DeleteRepository<TEntity> : Repository<TEntity>, IDeleteRepository<TEntity>
        where TEntity : Entity.BaseEntity.Entity
    {
        protected DeleteRepository(DbContext context)
            : base(context)
        {
        }

        public EntityResult DeleteEntity(TEntity entity)
        {
            entity.Validate();
            _dbSet.Remove(entity);
            return new EntityResult();
        }
    }
}
