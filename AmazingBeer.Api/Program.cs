using AmazingBeer.Api.Application.Behaviors.Validations;
using AmazingBeer.Api.Application.Interfaces;
using AmazingBeer.Api.Application.Services;
using AmazingBeer.Api.Domain.Interfaces;
using AmazingBeer.Api.Infraestructure.Data.Context;
using AmazingBeer.Api.Infraestructure.Data.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
// Registra seus reposit�rios e servi�os
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
builder.Services.AddAutoMapper(typeof(Program));

// Adiciona a string de conex�o ao cont�iner de servi�os
builder.Services.AddScoped<SqlDbContext>(sp =>
    new SqlDbContext(builder.Configuration.GetConnectionString("DefaultConnection")!));

// Configurar o Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Usar Serilog
builder.Host.UseSerilog(); 

// Adicionar servi�os
builder.Services.AddRazorPages();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
    app.UseMiddleware<GlobalExceptionMiddleware>();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
