using CadoChat.Auth.EF.Entities;
using CadoChat.AuthService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Auth.EF.DI
{
    public static class ServiceCollectionExtention
    {

        public static void AddDBContext(this IServiceCollection services, WebApplicationBuilder webApplicationBuilder)
        {
            services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("CadoChat.Auth.EF")));
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
