using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Versionamento.Application.Interfaces.V1;
using Versionamento.Application.Mappings;
using Versionamento.Application.Services.V1;
using Versionamento.Domain.Interfaces;
using Versionamento.Infra.Data.Context;
using Versionamento.Infra.Data.Repositories;

namespace Versionamento.Infra.Ioc
{
    public static class DependencyInjectionV1
    {
        public static IServiceCollection AddInfrastructureV1(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("VersionamentoAPI"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IUsuarioServices, UsuarioServices>();

            services.AddAutoMapper(typeof(DomainToDtoMapping));

            return services;
        }
    }
}
