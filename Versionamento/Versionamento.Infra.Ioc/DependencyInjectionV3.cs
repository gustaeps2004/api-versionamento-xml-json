using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
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

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                    AppDomain.CurrentDomain.Load("Versionamento.Application")
                ));

            return services;
        }
    }
}
