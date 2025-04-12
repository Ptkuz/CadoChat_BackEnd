using CadoChat.DAL.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.DAL.Entity.FacadeRepository
{
    public interface ISelectRepository<TEntity>
        where TEntity : IEntity
    {
        Task<TEntity?> FirstOfDefaultAsync(Expression<Func<TEntity, bool>>? condition = null, CancellationToken cancel = default);

        IEnumerable<TEntity>? Where(Func<TEntity, bool> where);

        Task<TEntity?> GetEntityByIdAsync(Guid id, CancellationToken cancel = default);

        Task<IEnumerable<TEntity>?> GetAllEntitiesAsync(CancellationToken cancel = default);
    }
}
