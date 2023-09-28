using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Versionamento.Application.Interfaces;
using Versionamento.Application.Mappings;
using Versionamento.Application.Services;
using Versionamento.Domain.Interfaces;
using Versionamento.Infra.Data.Context;
using Versionamento.Infra.Data.Repositories;

namespace Versionamento.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("VersionamentoAPI")));

            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IUsuarioServices, UsuarioServices>();

            services.AddAutoMapper(typeof(DomainToDtoMapping));

            return services;
        }
    }
}
