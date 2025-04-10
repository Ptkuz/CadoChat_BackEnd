using CadoChat.DAL.Entity.BaseEntity;
using CadoChat.DAL.Entity.FacadeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.DAL.EF.FacadeRepository
{
    public class FacadeRepository<TEntity> : IFacadeRepository<TEntity>
        where TEntity : IEntity
    {
        public ISelectRepository<TEntity> SelectRepository { get; }

        public ICreateRepository<TEntity> CreateRepository { get; }

        public IUpdateRepository<TEntity> UpdateRepository { get; }

        public IDeleteRepository<TEntity> DeleteRepository { get; }

        public FacadeRepository(ISelectRepository<TEntity> selectRepository,
            ICreateRepository<TEntity> createRepository,
            IUpdateRepository<TEntity> updateRepository,
            IDeleteRepository<TEntity> deleteRepository)
        {
            SelectRepository = selectRepository;
            CreateRepository = createRepository;
            UpdateRepository = updateRepository;
            DeleteRepository = deleteRepository;
        }
    }
}
