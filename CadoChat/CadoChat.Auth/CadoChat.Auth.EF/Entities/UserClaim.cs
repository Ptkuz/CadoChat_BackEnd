using CadoChat.DAL.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Auth.EF.Entities
{
    public class UserClaim : Entity
    {
        public Guid UserId { get; set; } 

        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public User User { get; set; }

        public UserClaim()
        {

        }
    }
}
