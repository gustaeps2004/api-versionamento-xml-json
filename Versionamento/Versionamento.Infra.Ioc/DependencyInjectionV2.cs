using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Versionamento.Application.Interfaces.V2;
using Versionamento.Application.Services.V2;
using Versionamento.Application.Validation.Usuarios;

namespace Versionamento.Infra.Ioc
{
    public static class DependencyInjectionV2
    {
        [Obsolete]
        public static IServiceCollection AddInfrastructureV2(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioServices, UsuarioServices>();

            services.AddMvc()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<CommandUsuariosDtoValidationCreate>();

                    fv.RegisterValidatorsFromAssemblyContaining<CommandUsuariosDtoValidationUpdate>();
                    fv.ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");
                });

            //services.AddApiVersioning(setup =>
            //{
            //    setup.DefaultApiVersion = new ApiVersion(1, 0);
            //    setup.AssumeDefaultVersionWhenUnspecified = true;
            //    setup.ReportApiVersions = true;
            //});

            
            services.AddVersionedApiExplorer(v =>
            {
                v.GroupNameFormat = "'v'VVV";
                v.SubstituteApiVersionInUrl = true;
            });


            return services;
        }
    }
}
