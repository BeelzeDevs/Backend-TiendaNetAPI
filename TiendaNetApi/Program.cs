using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using TiendaNetApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Agregar el DbContext con la cadena de conexión
builder.Services.AddDbContext<TiendaDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Habilitar sistema de controladores y la inyección de dependencias (DI)
builder.Services.AddControllers();

var app = builder.Build();

// Se esta utilizando swagger en development para testear los endpoints
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Activar controladores como UsuariosController o IngredientesController
app.MapControllers();

app.Run();
