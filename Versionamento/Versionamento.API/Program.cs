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
builder.Services.AddInfrastructureV3();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddConfigSwagger();
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.Conventions.Controller<Versionamento.API.Controllers.V1.UsuariosController>().HasApiVersion(new ApiVersion(1, 0));
    options.Conventions.Controller<Versionamento.API.Controllers.V2.UsuariosController>().HasApiVersion(new ApiVersion(2, 0));
    options.Conventions.Controller<Versionamento.API.Controllers.V3.UsuariosController>().HasApiVersion(new ApiVersion(3, 0));
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
        options.SwaggerEndpoint($"/swagger/v3/swagger.json", "v3");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
