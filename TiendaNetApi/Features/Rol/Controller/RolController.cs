using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaNetApi.Model;
using TiendaNetApi.Data;

namespace TiendaNetApi.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class RolesController : ControllerBase
    {
        private readonly TiendaDbContext _context;

        public RolesController(TiendaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IResult> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles is not null ? Results.Ok(roles) : Results.NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetRol(int id)
        {
            var rolEncontrado = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

            return rolEncontrado is not null ? Results.Ok(rolEncontrado) : Results.NotFound();
        }

        [HttpPost]
        public async Task<IResult> CrearRol([FromBody] Rol rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return Results.Created($"api/rol/{rol.Id}", rol);
        }

        [HttpPut("{id}")]
        public async Task<IResult> ModificarRol(int id, [FromBody] Rol rol)
        {
            var rolAModificar = await _context.Roles.FindAsync(id);
            if (rolAModificar == null) return Results.NotFound();

            rolAModificar.Nombre = rol.Nombre;
            rolAModificar.Id = rol.Id;

            await _context.SaveChangesAsync();
            return Results.Ok(rolAModificar);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> EliminarRol(int id)
        {
            var rolAEliminar = await _context.Roles.FindAsync(id);
            if (rolAEliminar is null) return Results.NotFound();

            rolAEliminar.Estado = false;
            await _context.SaveChangesAsync();
            return Results.NoContent();
        }
    }
}