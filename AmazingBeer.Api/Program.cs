using AmazingBeer.Api.Infraestructure.Data.Context;
using AmazingBeer.Api.Infraestructure.IoC;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
// Add services to the container.
builder.Services.AddControllers();
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

// Registrar dependências IoC
builder.Services.AdicionarDependencias();

// Adiciona a string de conexão ao contêiner de serviços
builder.Services.AddScoped<SqlDbContext>(sp =>
    new SqlDbContext(builder.Configuration.GetConnectionString("DefaultConnection")!));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
}

app.MapDefaultEndpoints();
app.UseMiddleware<GlobalExceptionMiddleware>();

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
