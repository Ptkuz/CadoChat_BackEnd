using CadoChat.DAL.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Auth.EF.Entities
{
    public class UserLogin : Entity
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public UserLogin()
        {

        }
    }
}
