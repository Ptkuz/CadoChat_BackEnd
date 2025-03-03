using CadoChat.AuthService.AuthService;
using CadoChat.AuthService.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CadoChat.AuthService.DI
{
    public static class ServiceCollectionExtention
    {

        public static void AddDBContext(this IServiceCollection services, WebApplicationBuilder webApplicationBuilder)
        {
            services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddEFServices(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddUserStore<UserStore<User, Role, AuthDbContext, Guid>>()
                .AddDefaultTokenProviders();
        }

    }
}
