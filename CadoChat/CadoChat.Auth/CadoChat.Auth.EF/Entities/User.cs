using CadoChat.DAL.Entity.BaseEntity;

namespace CadoChat.Auth.EF.Entities
{
    public class User : Entity
    {

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public bool IsActive { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }

        public User()
        {
            UserRoles = new HashSet<UserRole>();
            RefreshTokens = new HashSet<RefreshToken>();
        }


    }
}
