using Microsoft.EntityFrameworkCore;

namespace CadoChat.DAL.EF.FacadeRepository.Base
{
    public class Repository<TEntity>
        where TEntity : Entity.BaseEntity.Entity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
    }
}
