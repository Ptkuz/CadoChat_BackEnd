using CadoChat.DAL.EF.FacadeRepository.Base;
using CadoChat.DAL.Entity.FacadeRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CadoChat.DAL.EF.FacadeRepository
{
    public class SelectRepository<TEntity> : Repository<TEntity>, ISelectRepository<TEntity>
        where TEntity : Entity.BaseEntity.Entity
    {
        protected SelectRepository(DbContext context)
            : base(context)
        {
        }

        public async Task<TEntity>? FirstOfDefaultAsync(Expression<Func<TEntity, bool>>? condition = null, CancellationToken cancel = default)
        {
            TEntity? entity = null;

            if (condition != null)
            {
                entity = await _dbSet.FirstOrDefaultAsync(condition, cancel);
            }
            else
            {
                entity = await _dbSet.FirstOrDefaultAsync(cancel);
            }

            return entity;
        }
        public IEnumerable<TEntity>? Where(Func<TEntity, bool> where)
        {
            if (where != null)
            {
                return _dbSet.Where(where);
            }
            return null;
        }
        public async Task<TEntity>? GetEntityByIdAsync(Guid id, CancellationToken cancel = default)
        {
            var entity = await _dbSet.FindAsync([id], cancel);
            return entity;
        }
        public async Task<IEnumerable<TEntity>>? GetAllEntitiesAsync(CancellationToken cancel = default)
        {
            var entities = await _dbSet.ToListAsync(cancel);
            return entities;
        }
    }
}
