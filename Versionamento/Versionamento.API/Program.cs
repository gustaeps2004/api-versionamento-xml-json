using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Versionamento.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler =
    ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers()
    .AddXmlSerializerFormatters();

builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(v =>
{
    v.GroupNameFormat = "'v'VVV";
    v.SubstituteApiVersionInUrl = true;
});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
