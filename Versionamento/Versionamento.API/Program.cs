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
    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "API V1", Version = "V1.0" });
    options.SwaggerDoc("v2", new OpenApiInfo() { Title = "API V2", Version = "V2.0" });
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.CustomSchemaIds(x => x.FullName);
});

var app = builder.Build();

app.UseApiVersioning();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
