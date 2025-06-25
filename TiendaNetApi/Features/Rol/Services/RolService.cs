using TiendaNetApi.Rol.DTOs;
using TiendaNetApi.Data;
using Microsoft.EntityFrameworkCore;
using TiendaNetApi.Controllers;
using TiendaNetApi.Model;
namespace TiendaNetApi.Rol.Services
{
    public class RolService : IRolService
    {
        private readonly TiendaDbContext _context;
        public RolService(TiendaDbContext context)
        {
            _context = context;
        }
        public async Task<List<RolReadDTO>> GetAll()
        {
            return await _context.Roles
            .Select(r => new RolReadDTO
            {
                Id = r.Id,
                Nombre = r.Nombre
            })
            .ToListAsync();
        }
        public async Task<RolReadDTO?> GetById(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol is null) return null;

            return new RolReadDTO { Id = rol.Id, Nombre = rol.Nombre };
        }
        public async Task<RolConDetallesDTO?> GetByIdDetallado(int id)
        {
            var rol = await _context.Roles
            .Include(r => r.Usuarios)
            .FirstOrDefaultAsync(r => r.Id == id);
            if (rol is null) return null;

            return new RolConDetallesDTO
            {
                Id = rol.Id,
                Nombre = rol.Nombre,
                Usuarios = rol.Usuarios
                .Select(u => new UsuarioRolDetalleDTO 
                {
                    Id = u.Id,
                    NombreUsuario = u.NombreUsuario,
                    Email = u.Email,
                    Direccion = u.Direccion,
                    Municipio = u.Municipio,
                    CodPostal = u.CodPostal,
                    EstadoUsuario = u.EstadoUsuario
                })
                .ToList()
                
            };

        }
        public async Task<RolReadDTO> Create(RolCreateDTO dto)
        {
            var rol = new TiendaNetApi.Model.Rol { Nombre = dto.Nombre };
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return new RolReadDTO { Id = rol.Id, Nombre = rol.Nombre };

        }
        public async Task<bool> Update(int id, RolUpdateDTO dto)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol is null) return false;

            rol.Nombre = dto.Nombre;
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> DeleteFisico(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol is null) return false;

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}