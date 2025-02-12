using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CadoChat.AuthService
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Создание ролей при миграции
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "e7b8bc1c-9474-4202-b565-f75f1d734d01", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "945e5c4f-9d07-4594-abe6-a7529057e3f0", Name = "User", NormalizedName = "USER" }
            );

            
        }
    }
}
