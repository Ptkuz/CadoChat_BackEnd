using CadoChat.DAL.Entity.BaseEntity;

namespace CadoChat.Auth.EF.Entities
{
    public class UserRole : Entity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }

        public UserRole()
        {
        }
    }
}
