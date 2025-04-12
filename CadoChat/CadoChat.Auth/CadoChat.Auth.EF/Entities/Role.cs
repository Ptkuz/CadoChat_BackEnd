using CadoChat.DAL.Entity.BaseEntity;

namespace CadoChat.Auth.EF.Entities
{
    public class Role : Entity
    {

        public string Name { get; set; }

        public string NormilizedName { get; set; }

        public string ConcurrencyStamp { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
    }
}
