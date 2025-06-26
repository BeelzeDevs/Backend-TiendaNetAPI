using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using TiendaNetApi.Data;
using TiendaNetApi.Auth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TiendaNetApi.Usuario.Services;
using TiendaNetApi.UnidadMedida.Services;
using TiendaNetApi.Rol.Services;
using TiendaNetApi.RecetasXMenu.Services;
using TiendaNetApi.Receta.Services;
using TiendaNetApi.Menu.Services;
using TiendaNetApi.IngredienteXReceta.Services;
using TiendaNetApi.Ingredientes.Services;
using TiendaNetApi.Model;
using TiendaNetApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Agregar el DbContext con la cadena de conexión
builder.Services.AddDbContext<TiendaDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Habilitar sistema de controladores y la inyección de dependencias (DI)
builder.Services.AddControllers();
// Services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUnidadMedidaService, UnidadMedidaService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IRecetasXMenuService, RecetasXMenuService>();
builder.Services.AddScoped<IRecetaService, RecetaService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IIngredientesXRecetaService, IngredientesXRecetaService>();
builder.Services.AddScoped<IIngredienteService,IngredienteService>();


// Auth JWT
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

// Política de autorización personalizada de solo Admin
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SoloAdmin", policy => policy.RequireRole("Admin"));
});


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "TiendaNetApi", Version = "v1.0" });


    // JWT en Swagger UI
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese el token JWT como: Bearer {su_token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});




var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
// Se esta utilizando swagger para testear los endpoints en development
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Activar controladores como UsuariosController o IngredientesController
app.MapControllers();

app.Run();
