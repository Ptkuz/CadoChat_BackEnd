using CadoChat.DAL.Entity.BaseEntity;

namespace CadoChat.Auth.EF.Entities
{
    public class User : Entity
    {

        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }

        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        // Навигационные свойства
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserClaim> Claims { get; set; }
        public ICollection<UserLogin> Logins { get; set; }
        public ICollection<UserToken> Tokens { get; set; }

        public User(string userName, string email, string passwordHash, string phoneNumber)
            : this()
        {
            UserName = userName;
            NormalizedUserName = userName.ToUpper();
            Email = email;
            NormalizedEmail = email.ToLower();
            EmailConfirmed = false;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = false;
            TwoFactorEnabled = false;
            LockoutEnabled = false;
            AccessFailedCount = 0;
        }

        public User()
            : base()
        {
            UserRoles = new HashSet<UserRole>();
            Claims = new HashSet<UserClaim>();
            Logins = new HashSet<UserLogin>();
            Tokens = new HashSet<UserToken>();
        }


    }
}
