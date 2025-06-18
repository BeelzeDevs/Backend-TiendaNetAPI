using Microsoft.EntityFrameworkCore;
using TiendaNetApi.Data;
using TiendaNetApi.Ingredientes.DTOs;
using TiendaNetApi.Model;
using TiendaNetApi.Ingredientes.Services;

namespace TiendaNetApi.Services
{
    public class IngredienteService : IIngredienteService
    {
        private readonly TiendaDbContext _context;
        public IngredienteService(TiendaDbContext context)
        {
            _context = context;
        }
        public async Task<List<IngredienteReadDTO>> GetAllAsync()
        {
            return await _context.Ingredientes
            .Include(i => i.UnidadMedida)
            .Select(i => new IngredienteReadDTO
            {
                Id = i.Id,
                Nombre = i.Nombre,
                Costo = i.Costo,
                Stock = i.Stock,
                DescripcionIngrediente = i.DescripcionIngrediente,
                UnidadMedidaNombre = i.UnidadMedida.Nombre
            })
            .ToListAsync();
        }
        public async Task<List<IngredienteReadDTO>> GetAllActivesAsync()
        {
            return await _context.Ingredientes
            .Where(i => i.EstadoIngrediente == true)
            .Include(i => i.UnidadMedida)
            .Select(i => new IngredienteReadDTO
            {
                Id = i.Id,
                Nombre = i.Nombre,
                Costo = i.Costo,
                Stock = i.Stock,
                DescripcionIngrediente = i.DescripcionIngrediente,
                UnidadMedidaNombre = i.UnidadMedida.Nombre
            })
            .ToListAsync();
        }
        public async Task<IngredienteReadDTO?> GetByIdAsync(int id)
        {
            var ingrediente = await _context.Ingredientes
            .Include(i => i.UnidadMedida)
            .FirstOrDefaultAsync(i => i.Id == id);

            if (ingrediente is null) return null;

            return new IngredienteReadDTO
            {
                Id = ingrediente.Id,
                Nombre = ingrediente.Nombre,
                Costo = ingrediente.Costo,
                Stock = ingrediente.Stock,
                DescripcionIngrediente = ingrediente.DescripcionIngrediente,
                UnidadMedidaNombre = ingrediente.UnidadMedida.Nombre

            };
        }
        public async Task<IngredienteReadDTO> CreateAsync(IngredienteCreateDTO dto)
        {
            var ingrediente = new TiendaNetApi.Model.Ingrediente
            {
                Nombre = dto.Nombre,
                Costo = dto.Costo,
                Stock = dto.Stock,
                DescripcionIngrediente = dto.DescripcionIngrediente,
                EstadoIngrediente = true,
                UnidadMedidaId = dto.UnidadMedidaId
            };
            _context.Ingredientes.Add(ingrediente);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(ingrediente.Id) ?? throw new Exception("Error al crear ingrediente");
        }
        public async Task<bool> UpdateAsync(int id, IngredienteUpdateDTO dto)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente is null) return false;

            ingrediente.Nombre = dto.Nombre;
            ingrediente.Costo = dto.Costo;
            ingrediente.Stock = dto.Stock;
            ingrediente.DescripcionIngrediente = dto.DescripcionIngrediente;
            ingrediente.UnidadMedidaId = dto.UnidadMedidaId;
            ingrediente.EstadoIngrediente = dto.EstadoIngrediente;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteLogicoAsync(int id)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente is null) return false;

            ingrediente.EstadoIngrediente = false;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteFisicoAsync(int id)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente is null) return false;

            _context.Ingredientes.Remove(ingrediente);
            await _context.SaveChangesAsync();
            return true;
        }
        
    }
}