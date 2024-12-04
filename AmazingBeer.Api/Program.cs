using AmazingBeer.Api.Application.Behaviors.Validations;
using AmazingBeer.Api.Application.Interfaces;
using AmazingBeer.Api.Application.Services;
using AmazingBeer.Api.Domain.Interfaces;
using AmazingBeer.Api.Infraestructure.Data.Context;
using AmazingBeer.Api.Infraestructure.Data.Repositories;
using AmazingBeer.Api.Infraestructure.Mappers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
// Registra seus repositórios e serviços
builder.Services.AddScoped<ICervejaRepository, CervejaRepository>();
builder.Services.AddScoped<ICervejaService, CervejaService>();

// Fluent Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<CriarCervejaDtoValidation>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Amazing Beer",
        Version = "v1",
        Description = "Amazing Beer - Aspire 8",
    });

    options.EnableAnnotations();
});

// Registrando o AutoMapper
builder.Services.AddAutoMapper(typeof(CervejaProfile));

// Adiciona a string de conexão ao contêiner de serviços
builder.Services.AddScoped<SqlDbContext>(sp =>
    new SqlDbContext(builder.Configuration.GetConnectionString("DefaultConnection")!));

// Configurar o Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Usar Serilog
builder.Host.UseSerilog(); 

// Adicionar serviços
builder.Services.AddRazorPages();

var app = builder.Build();

app.MapDefaultEndpoints();
app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
}

// Configure Redoc
app.UseReDoc(options =>
{
    options.DocumentTitle = "Amazing Beer - Redoc";
    options.SpecUrl = "/swagger/v1/swagger.json"; 
    options.RoutePrefix = "redoc";               
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
