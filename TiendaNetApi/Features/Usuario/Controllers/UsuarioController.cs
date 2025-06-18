using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaNetApi.Data;
using TiendaNetApi.Model;
using TiendaNetApi.DTOs;

namespace TiendaNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly TiendaDbContext _context;

        public UsuariosController(TiendaDbContext context)
        {
            _context = context;
        }

        //Get : api/usuarios
        [HttpGet]
        public async Task<IResult> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
            .Include(u => u.Rol)
            .ToListAsync();

            return Results.Ok(usuarios);
        }

        // Get: api/usuarios/5
        [HttpGet("{id}")]
        public async Task<IResult> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Id == id);
            if (usuario is null) return Results.NotFound();

            var usuarioADevolver = new UsuarioDto
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Contraseña = usuario.Contraseña,
                Rol = usuario.Rol.Nombre
            };

            return Results.Ok(usuarioADevolver);

        }

        // Post: api/usuarios
        [HttpPost]
        public async Task<IResult> CrearUsuario([FromBody] Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Results.Created($"/api/usuarios/{usuario.Id}", usuario);
        }

        // Put : api/usuarios/5
        [HttpPut("{id}")]
        public async Task<IResult> ActualizarUsuario(int id, [FromBody] Usuario usuarioActualizado)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario is null) return Results.NotFound();

            usuario.NombreUsuario = usuarioActualizado.NombreUsuario;
            usuario.Contraseña = usuarioActualizado.Contraseña;
            usuario.RolId = usuarioActualizado.RolId;

            await _context.SaveChangesAsync();
            return Results.Ok(usuario);
        }

        // DELETE : api/usuarios/5
        [HttpDelete("{id}")]
        public async Task<IResult> EliminarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario is null) return Results.NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return Results.NoContent();
        }

    }
}