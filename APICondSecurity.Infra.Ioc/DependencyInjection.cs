using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace APICondSecurity.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddDbContext<condSecurityContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(condSecurityContext).Assembly.FullName));
            });
            Services.AddAutoMapper(typeof(EntitiesToDTOMappingProfile));
            Services.AddScoped<IUsuario, UsuarioRepository>();
            Services.AddScoped<IUsuarioService, UsuarioService>();
            return Services;
        }
    }
}
