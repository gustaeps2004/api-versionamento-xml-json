using Microsoft.Extensions.DependencyInjection;
using Versionamento.Application.Interfaces.V3;
using Versionamento.Application.Services.V3;

namespace Versionamento.Infra.Ioc
{
    public static class DependencyInjectionV3
    {
        public static IServiceCollection AddInfrastructureV3(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioServices, UsuarioServices>();

            return services;
        }
    }
}
