using TiendaNetApi.Receta.DTOs;
using TiendaNetApi.Data;
using Microsoft.EntityFrameworkCore;
namespace TiendaNetApi.Receta.Services
{
    public class RecetaService : IRecetaService
    {
        private readonly TiendaDbContext _context;
        public RecetaService(TiendaDbContext context)
        {
            _context = context;
        }

        public async Task<List<RecetaReadDTO>> GetAll()
        {
            return await _context.Recetas
            .Select(r => new RecetaReadDTO
            {
                Id = r.Id,
                Nombre = r.Nombre,
                DescripcionReceta = r.DescripcionReceta,
                ImgUrl = r.ImgUrl,
                PrecioReceta = r.PrecioReceta,
                EstadoReceta = r.EstadoReceta
            })
            .ToListAsync();
        }
        public async Task<List<RecetaReadDTO>> GetAllActives()
        {
            return await _context.Recetas
            .Where(r => r.EstadoReceta)
            .Select(r => new RecetaReadDTO
            {
                Id = r.Id,
                Nombre = r.Nombre,
                DescripcionReceta = r.DescripcionReceta,
                ImgUrl = r.ImgUrl,
                PrecioReceta = r.PrecioReceta,
                EstadoReceta = r.EstadoReceta
            })
            .ToListAsync();
        }
        public async Task<RecetaReadDTO?> GetById(int id)
        {
            var recetaEcontrada = await _context.Recetas.FindAsync(id);
            if (recetaEcontrada is null) return null;

            return new RecetaReadDTO
            {
                Id = recetaEcontrada.Id,
                Nombre = recetaEcontrada.Nombre,
                DescripcionReceta = recetaEcontrada.DescripcionReceta,
                ImgUrl = recetaEcontrada.ImgUrl,
                PrecioReceta = recetaEcontrada.PrecioReceta,
                EstadoReceta = recetaEcontrada.EstadoReceta
            };
        }
        public async Task<RecetaConDetallesDTO?> GetByIdConDetalles(int id)
        {
            var recetaEcontrada = await _context.Recetas
            .Include(r => r.IngredientesXRecetas)
                .ThenInclude(ixr => ixr.Ingrediente)
                    .ThenInclude(i=> i.UnidadMedida)
            .Include(r => r.RecetasXMenus)
                .ThenInclude(rxm => rxm.Menu)
            .FirstOrDefaultAsync(r => r.Id == id);
            if (recetaEcontrada is null) return null;

            return new RecetaConDetallesDTO
            {
                Id = recetaEcontrada.Id,
                Nombre = recetaEcontrada.Nombre,
                DescripcionReceta = recetaEcontrada.DescripcionReceta,
                ImgUrl = recetaEcontrada.ImgUrl,
                PrecioReceta = recetaEcontrada.PrecioReceta,
                EstadoReceta = recetaEcontrada.EstadoReceta,
                Ingredientes = recetaEcontrada.IngredientesXRecetas
                    .Select(ixr => new IngredienteDetalleDTO
                    {
                        IngredienteId = ixr.IngredienteId,
                        NombreIngrediente = ixr.Ingrediente.Nombre,
                        Cantidad = ixr.Cantidad,
                        UnidadMedidaNombre = ixr.Ingrediente.UnidadMedida.Nombre
                    }).ToList(),
                Menus = recetaEcontrada.RecetasXMenus
                    .Select(rxm => new MenuDetalleDTO
                    {
                        MenuId = rxm.MenuId,
                        NombreMenu = rxm.Menu.TituloMenu
                    }).ToList()
            };
        }
        public async Task<RecetaReadDTO> Create(RecetaCreateDTO dto)
        {
            var receta = new TiendaNetApi.Model.Receta
            {
                Nombre = dto.Nombre,
                DescripcionReceta = dto.DescripcionReceta,
                ImgUrl = dto.ImgUrl,
                PrecioReceta = dto.PrecioReceta,
            };
            _context.Recetas.Add(receta);
            await _context.SaveChangesAsync();

            return new RecetaReadDTO
            {
                Id = receta.Id,
                Nombre = receta.Nombre,
                DescripcionReceta = receta.DescripcionReceta,
                ImgUrl = receta.ImgUrl,
                PrecioReceta = receta.PrecioReceta,
                EstadoReceta = receta.EstadoReceta
            };
        }
        public async Task<bool> Update(int id, RecetaUpdateDTO dto)
        {
            var receta = await _context.Recetas.FindAsync(id);
            if (receta is null) return false;

            receta.Nombre = dto.Nombre;
            receta.PrecioReceta = dto.PrecioReceta;
            receta.EstadoReceta = dto.EstadoReceta;
            receta.ImgUrl = dto.ImgUrl;
            receta.DescripcionReceta = dto.DescripcionReceta;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteLogico(int id)
        {
            var receta = await _context.Recetas.FindAsync(id);
            if (receta is null) return false;

            receta.EstadoReceta = false;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteFisico(int id)
        {
            var receta = await _context.Recetas
            .Include(r => r.RecetasXMenus)
            .Include(r => r.IngredientesXRecetas)
            .FirstOrDefaultAsync(r=> r.Id == id);

            if (receta is null) return false;

            _context.RecetasXMenu.RemoveRange(receta.RecetasXMenus);
            _context.IngredientesXRecetas.RemoveRange(receta.IngredientesXRecetas);
            _context.Recetas.Remove(receta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}