using ThomasGreg.Application.Contracts;
using ThomasGreg.Application.Core;
using ThomasGreg.Application.Libraries;
using ThomasGreg.Application.Services;
using ThomasGreg.Domain.Interfaces;
using ThomasGreg.Infra.Repositories;

namespace ThomasGreg.API.DependencyMap
{
    public static class RepositoryDependencyMap
    {
        public static void RepositoryMap(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<UsuarioAutenticado>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddScoped<Cookie>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ILogradouroRepository, LogradouroRepository>();
        }
    }
}
