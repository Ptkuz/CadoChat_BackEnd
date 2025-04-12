using CadoChat.Auth.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Auth.EF.Context
{
    public class AuthDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<UserToken> UserTokens { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) 
            : base(options)
        {
            // Удаление базы данных
            //Database.EnsureDeleted();

            // Создание базы данных заново
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(u => u.NormalizedUserName).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(r => r.NormilizedName).IsUnique();

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserLogin>().HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });

            modelBuilder.Entity<UserToken>().HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}
