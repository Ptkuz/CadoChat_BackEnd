using CadoChat.DAL.Entity.BaseEntity;
using CadoChat.DAL.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.DAL.Entity.FacadeRepository
{
    public interface IDeleteRepository<TEntity>
        where TEntity : IEntity
    {

        EntityResult DeleteEntity(TEntity entity);

    }
}
