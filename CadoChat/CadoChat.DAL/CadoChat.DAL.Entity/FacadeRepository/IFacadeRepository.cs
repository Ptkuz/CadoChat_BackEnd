using CadoChat.DAL.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.DAL.Entity.FacadeRepository
{
    public interface IFacadeRepository<TEntity> 
        where TEntity : IEntity
    {
        ISelectRepository<TEntity> SelectRepository { get; }
        ICreateRepository<TEntity> CreateRepository { get; }
        IUpdateRepository<TEntity> UpdateRepository { get; }
        IDeleteRepository<TEntity> DeleteRepository { get; }
    }
}
