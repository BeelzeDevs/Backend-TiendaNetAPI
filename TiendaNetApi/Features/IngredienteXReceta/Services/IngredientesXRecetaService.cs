using Microsoft.EntityFrameworkCore;
using TiendaNetApi.Data;
using TiendaNetApi.IngredienteXReceta.DTOs;
using TiendaNetApi.IngredienteXReceta.Services;
namespace TiendaNetApi.IngredienteXReceta.Services
{
    public class IngredientesXRecetaService : IIngredientesXRecetaService
    {
        private readonly TiendaDbContext _context;
        public IngredientesXRecetaService(TiendaDbContext context)
        {
            _context = context;
        }
        public async Task<List<IngredienteXRecetaReadDTO>> GetAll()
        {
            return await _context.IngredientesXRecetas
            .Include(ixr => ixr.Receta)
            .Include(ixr => ixr.Ingrediente)
                .ThenInclude(i => i.UnidadMedida)
            .Select(ixr => new IngredienteXRecetaReadDTO
            {
                IngredienteId = ixr.IngredienteId,
                RecetaId = ixr.RecetaId,
                Cantidad = ixr.Cantidad,
                Ingrediente = new IngredienteReadDTO
                {
                    Id = ixr.Ingrediente.Id,
                    Nombre = ixr.Ingrediente.Nombre,
                    Costo = ixr.Ingrediente.Costo,
                    Stock = ixr.Ingrediente.Stock,
                    DescripcionIngrediente = ixr.Ingrediente.DescripcionIngrediente,
                    UnidadMedidaNombre = ixr.Ingrediente.UnidadMedida.Nombre,
                    CantidadReceta = ixr.Cantidad

                },
                Receta = new RecetaReadDTO
                {
                    Id = ixr.Receta.Id,
                    Nombre = ixr.Receta.Nombre,
                    DescripcionReceta = ixr.Receta.DescripcionReceta,
                    ImgUrl = ixr.Receta.ImgUrl,
                    PrecioReceta = ixr.Receta.PrecioReceta,
                    EstadoReceta = ixr.Receta.EstadoReceta
                }
            }).ToListAsync();
        }
        public async Task<IngredienteXRecetaByIdRecetaDTO?> GetIngredientesByIdReceta(int recetaId)
        {
            var receta = await _context.Recetas
            .Include(r => r.IngredientesXRecetas)
                .ThenInclude(ixr => ixr.Ingrediente)
                    .ThenInclude(i => i.UnidadMedida)
            .FirstOrDefaultAsync(r => r.Id == recetaId);

            if (receta is null) return null;

            return new IngredienteXRecetaByIdRecetaDTO
            {
                Receta = new RecetaReadDTO
                {
                    Id = receta.Id,
                    Nombre = receta.Nombre,
                    DescripcionReceta = receta.DescripcionReceta,
                    ImgUrl = receta.ImgUrl,
                    PrecioReceta = receta.PrecioReceta,
                    EstadoReceta = receta.EstadoReceta
                },
                Ingredientes = receta.IngredientesXRecetas
                .Select(ixr => new IngredienteReadDTO
                {
                    Id = ixr.IngredienteId,
                    Nombre = ixr.Ingrediente.Nombre,
                    Costo = ixr.Ingrediente.Costo,
                    Stock = ixr.Ingrediente.Stock,
                    DescripcionIngrediente = ixr.Ingrediente.DescripcionIngrediente,
                    CantidadReceta = ixr.Cantidad,
                    UnidadMedidaNombre = ixr.Ingrediente.UnidadMedida.Nombre

                }).ToList()
            };

        }
        public async Task<IngredienteXRecetaReadDTO?> AddIngredienteAReceta(IngredientesXRecetaCreateDTO dto)
        {
            var ingredienteExiste = await _context.Ingredientes.FindAsync(dto.IngredienteId);
            var recetaExiste = await _context.Recetas.FindAsync(dto.RecetaId);
            if (recetaExiste is null || ingredienteExiste is null) return null;
            var IxR = new Model.IngredienteXReceta
            {
                IngredienteId = dto.IngredienteId,
                Cantidad = dto.Cantidad,
                RecetaId = dto.RecetaId
            };

            _context.IngredientesXRecetas.Add(IxR);
            await _context.SaveChangesAsync();
            return new IngredienteXRecetaReadDTO
            {
                IngredienteId = IxR.IngredienteId,
                RecetaId = IxR.RecetaId,
                Cantidad = IxR.Cantidad,
                Ingrediente = new IngredienteReadDTO
                {
                    Id = ingredienteExiste.Id,
                    Nombre = ingredienteExiste.Nombre,
                    Costo = ingredienteExiste.Costo,
                    Stock = ingredienteExiste.Stock,
                    DescripcionIngrediente = ingredienteExiste.DescripcionIngrediente,
                    UnidadMedidaNombre = ingredienteExiste.UnidadMedida.Nombre
                },
                Receta = new RecetaReadDTO
                {
                    Id = recetaExiste.Id,
                    Nombre = recetaExiste.Nombre,
                    DescripcionReceta = recetaExiste.DescripcionReceta,
                    ImgUrl = recetaExiste.ImgUrl,
                    PrecioReceta = recetaExiste.PrecioReceta,
                    EstadoReceta = recetaExiste.EstadoReceta
                }
            };
        }

        public async Task<bool> AddIngredientesARecetaPorBatch(IngredientesXRecetaBatchCreateDTO dto)
        {

            var recetaExiste = await _context.Recetas.FindAsync(dto.RecetaId);
            if (recetaExiste is null) return false;

            var ingredientesIds = dto.Ingredientes.Select(i => i.IngredienteId).ToList();
            var ingredientesExistentes = await _context.Ingredientes
                .Where(i => ingredientesIds.Contains(i.Id))
                .Select(i => i.Id)
                .ToListAsync();

            if (ingredientesExistentes.Count != ingredientesIds.Count)
                return false;


            var ingredientes = dto.Ingredientes.Select(i => new Model.IngredienteXReceta
            {
                RecetaId = dto.RecetaId,
                IngredienteId = i.IngredienteId,
                Cantidad = i.Cantidad
            });

            _context.IngredientesXRecetas.AddRange(ingredientes);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveIngredienteDeReceta(IngredienteXRecetaDeleteDTO dto)
        {
            var ixr = await _context.IngredientesXRecetas
            .FirstOrDefaultAsync(ixr => ixr.IngredienteId == dto.IngredienteId && ixr.RecetaId == dto.RecetaId);
            if (ixr is null) return false;

            _context.IngredientesXRecetas.Remove(ixr);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCantidadIngrediente(IngredienteXRecetaUpdateDTO dto)
        {
            var ixr = await _context.IngredientesXRecetas
            .FirstOrDefaultAsync(ixr => ixr.RecetaId == dto.RecetaId && ixr.IngredienteId == dto.IngredienteId);
            if (ixr is null) return false;

            ixr.Cantidad = dto.Cantidad;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveIngredientesARecetaPorBatch(IngredientesXRecetaBatchRemoveDTO dto)
        {
            var receta = await _context.Recetas.FindAsync(dto.RecetaId);
            if (receta is null) return false;

            var IngredienteIds = dto.Ingredientes.Select(i => i.IngredienteId).ToList();
            var IngredientesExistentes = await _context.Ingredientes
            .Where(i => IngredienteIds.Contains(i.Id))
            .Select(i => i.Id)
            .ToListAsync();

            if (IngredienteIds.Count != IngredientesExistentes.Count)
                return false;

            var ingredientesAEliminar =  await _context.IngredientesXRecetas
            .Where(ixr => ixr.RecetaId == dto.RecetaId && IngredienteIds.Contains(ixr.IngredienteId))
            .ToListAsync();

            _context.IngredientesXRecetas.RemoveRange(ingredientesAEliminar);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}