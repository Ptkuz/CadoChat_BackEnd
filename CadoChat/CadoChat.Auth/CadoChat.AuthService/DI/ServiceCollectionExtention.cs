using CadoChat.Auth.EF;
using CadoChat.Auth.EF.Context;
using CadoChat.Auth.EF.FacadeRepository;
using CadoChat.DAL.EF;
using CadoChat.DAL.EF.FacadeRepository;
using CadoChat.DAL.Entity.BaseUnitOfWork;
using CadoChat.DAL.Entity.FacadeRepository;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore;

namespace CadoChat.AuthService.DI
{
    public static class ServiceCollectionExtention
    {

        public static IServiceCollection AddDBContext(this IServiceCollection services, WebApplicationBuilder webApplicationBuilder)
        {
            services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IServiceCollection AddAuthUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IAuthUnifOfWork, AuthUnifOfWork>();
            return services;
        }

        public static IServiceCollection AddAuthFacadeRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IFacadeRepository<>), typeof(FacadeRepository<>));
            return services;
        }

        public static IServiceCollection AddAuthRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(ISelectRepository<>), typeof(AuthSelectRepository<>));
            services.AddScoped(typeof(ICreateRepository<>), typeof(AuthCreateRepository<>));
            services.AddScoped(typeof(IUpdateRepository<>), typeof(AuthUpdateRepository<>));
            services.AddScoped(typeof(IDeleteRepository<>), typeof(AuthDeleteRepository<>));
            return services;
        }



    }
}
