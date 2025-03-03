using CadoChat.AuthService.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CadoChat.AuthService.AuthService
{
    public class AuthDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            RenameTables(builder);
            InitData(builder);

        }

        private void RenameTables(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<UserClaim>().ToTable("UserClaims");
            builder.Entity<UserLogin>().ToTable("UserLogins");
            builder.Entity<RoleClaim>().ToTable("RoleClaims");
            builder.Entity<UserToken>().ToTable("UserTokens");
        }

        private void InitData(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                new Role { Id = new Guid("e7b8bc1c-9474-4202-b565-f75f1d734d01"), Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Id = new Guid("945e5c4f-9d07-4594-abe6-a7529057e3f0"), Name = "User", NormalizedName = "USER" }
);
        }
    }
}
