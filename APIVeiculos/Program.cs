using Microsoft.OpenApi.Models;
using APIVeiculos.Data;
using APIVeiculos.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "APIVeiculos",
            Description = "API de consulta a veículos implementada com .NET 6 + Bogus + Minimal APIs", 
            Version = "v1",
            Contact = new OpenApiContact()
            {
                Name = "Renato Groffe",
                Url = new Uri("https://github.com/renatogroffe"),
            },
            License = new OpenApiLicense()
            {
                Name = "MIT",
                Url = new Uri("http://opensource.org/licenses/MIT"),
            }
        });
});

builder.Services.AddSingleton<VeiculosRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIVeiculos v1");
});

app.MapGet("/veiculos",
    (VeiculosRepository repository) =>
    {
        var data = repository.GetAll();
        app.Logger.LogInformation($"[Get] No. de registros encontrados: {data.Count()}");
        return Results.Ok(data);
    }).Produces<IEnumerable<Veiculo>>();

app.Run();