using CadoChat.DAL.Entity.BaseEntity;

namespace CadoChat.Auth.EF.Entities
{
    public class Role : Entity
    {

        private string name = null!;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public ICollection<UserRole> UserRoles { get; set; }

        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
    }
}
