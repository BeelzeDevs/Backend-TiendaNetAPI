using Microsoft.EntityFrameworkCore;
using TiendaNetApi.Data;
using TiendaNetApi.UnidadMedida.DTOs;
using TiendaNetApi.UnidadMedida.Services;
namespace TiendaNetApi.UnidadMedida.Services
{
    public class UnidadMedidaService : IUnidadMedidaService
    {
        private readonly TiendaDbContext _context;
        public UnidadMedidaService(TiendaDbContext context)
        {
            _context = context;
        }
        public async Task<List<UnidadMedidaReadDTO>> GetAllAsync()
        {
            return await _context.UnidadesMedida
            .Select(u => new UnidadMedidaReadDTO
            {
                Id = u.Id,
                Nombre = u.Nombre
            })
            .ToListAsync();
        }
        public async Task<UnidadMedidaReadDTO?> GetByIdAsync(int id)
        {
            var unidadMedida = await _context.UnidadesMedida.FindAsync(id);
            if (unidadMedida is null) return null;
            return new UnidadMedidaReadDTO { Id = unidadMedida.Id, Nombre = unidadMedida.Nombre };
        }
        public async Task<UnidadMedidaReadDTO> CreateAsync(UnidadMedidaCreateDTO dto)
        {
            var unidadMedida = new TiendaNetApi.Model.UnidadMedida
            {
                Nombre = dto.Nombre
            };
            _context.UnidadesMedida.Add(unidadMedida);
            await _context.SaveChangesAsync();

            return new UnidadMedidaReadDTO { Id = unidadMedida.Id, Nombre = unidadMedida.Nombre };
        }
        public async Task<bool> UpdateAsync(int id, UnidadMedidaUpdateDTO dto)
        {
            var unidadMedida = await _context.UnidadesMedida.FindAsync(id);
            if (unidadMedida is null) return false;

            unidadMedida.Nombre = dto.Nombre;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteFisicoAsync(int id)
        {
            var unidadMedida = await _context.UnidadesMedida.FindAsync(id);
            if (unidadMedida is null) return false;

            _context.UnidadesMedida.Remove(unidadMedida);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}