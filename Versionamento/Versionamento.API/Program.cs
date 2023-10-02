using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Versionamento.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler =
    ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers()
    .AddXmlSerializerFormatters();

builder.Services.AddInfrastructureV1(builder.Configuration);
builder.Services.AddInfrastructureV2();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
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
        Description = "API Version 2, contém validações para a criação e edição de usuário!"
    });

    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.CustomSchemaIds(x => x.FullName);
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.Conventions.Controller<Versionamento.API.Controllers.V1.UsuariosController>().HasApiVersion(new ApiVersion(1, 0));
    options.Conventions.Controller<Versionamento.API.Controllers.V2.UsuariosController>().HasApiVersion(new ApiVersion(2, 0));
});


var app = builder.Build();

app.UseApiVersioning();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1");
        options.SwaggerEndpoint($"/swagger/v2/swagger.json", "v2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
