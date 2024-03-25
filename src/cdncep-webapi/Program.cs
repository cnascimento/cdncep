using System.Reflection;
using cdncep_webapi;
using cdncep_webapi.Infrastructure.Repositories;
using cdncep_webapi.Infrastructure.Services;
using cdncep_webapi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.Configure<CdnCepSettings>(builder.Configuration.GetS);

builder.Services.AddTransient<ICepService, CepService>();
builder.Services.AddTransient<ICepRepository, CepRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Serviço de CEPs",
        Version = "v1",
        Description = "API REST responsável por manipular e consultar CEP's",
        Contact = new OpenApiContact
        {
            Name = "Cristiano do Nascimento",
            Email = "cristiano.n@gmail.com",
            Url = new Uri("https://www.cristianonascimento.com.br")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("api/cep/paginado", async (ICepService cepService,
    [FromQuery] int pageNumer = 1,
    [FromQuery] int pageLimit = 20,
    [FromQuery] string sort = "",
    [FromQuery] string cep = "",
    [FromQuery] string cidade = "",
    [FromQuery] string uf = "") =>
{
    var cepPagedReturn = await cepService.GetPagedCepAsync(pageNumer, pageLimit, sort, cep, cidade, uf);
    return Results.Ok(cepPagedReturn);
})
.WithDescription("Obtém uma lista paginada de CEP's")
.Produces<PagedResponse<CepPagedResponse>>(200, "application/json")
.ProducesProblem(404)
.WithName("GetPagedAsync")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
