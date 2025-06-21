using TiendaNetApi.Usuario.DTOs;
using TiendaNetApi.Data;
using TiendaNetApi.Usuario.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TiendaNetApi.Model;
namespace TiendaNetApi.Usuario.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly TiendaDbContext _context;
        public UsuarioService(TiendaDbContext context)
        {
            _context = context;
        }
        public async Task<List<UsuarioReadDTO>> GetAll()
        {
            return await _context.Usuarios
            .Select(u => new UsuarioReadDTO
            {
                Id = u.Id,
                NombreUsuario = u.NombreUsuario,
                Email = u.Email,
                Direccion = u.Direccion,
                Municipio = u.Municipio,
                CodPostal = u.CodPostal,
                EstadoUsuario = u.EstadoUsuario,
                FechaCreacion = u.FechaCreacion,
            })
            .ToListAsync();
        }
        public async Task<List<UsuarioReadDTO>> GetAllActives()
        {
            return await _context.Usuarios
            .Where(u => u.EstadoUsuario)
            .Select(u => new UsuarioReadDTO
            {
                Id = u.Id,
                NombreUsuario = u.NombreUsuario,
                Email = u.Email,
                Direccion = u.Direccion,
                Municipio = u.Municipio,
                CodPostal = u.CodPostal,
                EstadoUsuario = u.EstadoUsuario,
                FechaCreacion = u.FechaCreacion,
            })
            .ToListAsync();
        }
        public async Task<UsuarioReadDTO?> GetById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario is null) return null;

            return new UsuarioReadDTO
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Email = usuario.Email,
                Direccion = usuario.Direccion,
                Municipio = usuario.Municipio,
                CodPostal = usuario.CodPostal,
                EstadoUsuario = usuario.EstadoUsuario,
                FechaCreacion = usuario.FechaCreacion,
            };
        }
        public async Task<UsuarioReadConDetallesDTO?> GetByIdDetallado(int id)
        {
            var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Id == id);
            if (usuario is null) return null;

            return new UsuarioReadConDetallesDTO
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Email = usuario.Email,
                Direccion = usuario.Direccion,
                Municipio = usuario.Municipio,
                CodPostal = usuario.CodPostal,
                EstadoUsuario = usuario.EstadoUsuario,
                FechaCreacion = usuario.FechaCreacion,
                Rol = new RolReadDTO
                {
                    Id = usuario.Rol.Id,
                    Nombre = usuario.Rol.Nombre
                }
            };
        }
        public async Task<UsuarioReadConDetallesDTO> Create([FromBody] UsuarioCrearDTO dto)
        {
            var usuario = new TiendaNetApi.Model.Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                Email = dto.Email,
                Direccion = dto.Direccion,
                Municipio = dto.Municipio,
                CodPostal = dto.CodPostal,
                Contraseña = dto.Contraseña,
                RolId = dto.RolId
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return new UsuarioReadConDetallesDTO
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Email = usuario.Email,
                Direccion = usuario.Direccion,
                Municipio = usuario.Municipio,
                CodPostal = usuario.CodPostal,
                EstadoUsuario = usuario.EstadoUsuario,
                FechaCreacion = usuario.FechaCreacion,
                Rol = new RolReadDTO
                {
                    Id = usuario.Rol.Id,
                    Nombre = usuario.Rol.Nombre
                }
            };

        }
        public async Task<bool> Update(int id, [FromBody] UsuarioUpdateDTO dto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario is null) return false;

            usuario.NombreUsuario = dto.NombreUsuario;
            usuario.Email = dto.Email;
            usuario.Direccion = dto.Direccion;
            usuario.Municipio = dto.Municipio;
            usuario.CodPostal = dto.CodPostal;
            usuario.Contraseña = dto.Contraseña;
            usuario.EstadoUsuario = dto.EstadoUsuario;
            usuario.RolId = dto.RolId;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteLogico(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario is null) return false;

            usuario.EstadoUsuario = false;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteFisico(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario is null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}