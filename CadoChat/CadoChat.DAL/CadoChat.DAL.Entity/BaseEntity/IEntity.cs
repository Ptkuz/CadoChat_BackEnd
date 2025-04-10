using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.DAL.Entity.BaseEntity
{
    public interface IEntity
    {
        Guid Id { get; }

        DateTime CreatedAt { get; }

        DateTime ModifiedAt { get; }

        void Validate();
    }
}
