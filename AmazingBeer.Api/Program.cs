using AmazingBeer.Api.Infraestructure.Data.Context;
using AmazingBeer.Api.Infraestructure.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

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
