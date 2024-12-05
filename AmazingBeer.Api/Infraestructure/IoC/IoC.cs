using AmazingBeer.Api.Application.Behaviors.Validations;
using AmazingBeer.Api.Application.Interfaces;
using AmazingBeer.Api.Application.Services;
using AmazingBeer.Api.Domain.Interfaces;
using AmazingBeer.Api.Infraestructure.Data.Repositories;
using AmazingBeer.Api.Infraestructure.Mappers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Serilog;

namespace AmazingBeer.Api.Infraestructure.IoC
{
    public static class IoC
    {
        public static IServiceCollection AdicionarDependencias(this IServiceCollection services)
        {
            // Registrar Serviços
            services.AddScoped<ICervejaService, CervejaService>();

            // Registrar Repositórios
            services.AddScoped<ICervejaRepository, CervejaRepository>();

            #region Registar Outros Serviços

            // FluentValidation
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CriarCervejaDtoValidation>();

            // AutoMapper
            services.AddAutoMapper(typeof(CervejaProfile));

            // Razor
            services.AddRazorPages();

            // Serilog
            services.AddSerilog();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration: services.BuildServiceProvider()
                .GetService<IConfiguration>() ?? new ConfigurationBuilder().Build())
                .CreateLogger();

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Amazing Beer",
                    Version = "v1",
                    Description = "Amazing Beer - Aspire",
                });

                options.EnableAnnotations();
            });

            #endregion

            return services;
        }
    }
}
