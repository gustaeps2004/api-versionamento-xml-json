using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Versionamento.Infra.Ioc
{
    public static class ConfigurandoSwagger
    {
        public static IServiceCollection AddConfigSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "API Version 1",
                    Version = "v1",
                    Description = "API Version 1, não contém validaçãoes para criação e edição de usuário!"
                });

                options.SwaggerDoc("v2", new OpenApiInfo()
                {
                    Title = "API Version 2",
                    Version = "v2",
                    Description = "API Version 2, contém FluentValidation para a criação e edição de usuário!"
                });

                options.SwaggerDoc("v3", new OpenApiInfo()
                {
                    Title = "API Version 3",
                    Version = "v3",
                    Description = "API Version 3, aplicação mais robusta! Utilizando CQRS e FluentValidation!"
                });

                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                options.CustomSchemaIds(x => x.FullName);
            });

            return services;
        }
    }
}
