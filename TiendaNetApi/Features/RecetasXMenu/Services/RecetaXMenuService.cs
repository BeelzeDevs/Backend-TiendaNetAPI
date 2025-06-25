using Microsoft.EntityFrameworkCore;
using TiendaNetApi.Data;
using TiendaNetApi.RecetasXMenu.DTOs;
using TiendaNetApi.RecetasXMenu.Services;

namespace TiendaNetApi.RecetasXMenu.Services
{
    public class RecetasXMenuService : IRecetasXMenuService
    {
        private readonly TiendaDbContext _context;
        public RecetasXMenuService(TiendaDbContext context)
        {
            _context = context;
        }
        public async Task<List<MenuConRecetasReadDTO>> GetAll()
        {
            return await _context.Menus
            .Include(m => m.RecetasXMenus)
                .ThenInclude(rxm => rxm.Receta)
            .Select(menu => new MenuConRecetasReadDTO
            {
                MenuId = menu.Id,
                TituloMenu = menu.TituloMenu,
                EstadoMenu = menu.EstadoMenu,
                Recetas = menu.RecetasXMenus.Select(rxm => new RecetaReadDTO
                {
                    Id = rxm.Receta.Id,
                    Nombre = rxm.Receta.Nombre,
                    DescripcionReceta = rxm.Receta.DescripcionReceta,
                    ImgUrl = rxm.Receta.ImgUrl,
                    PrecioReceta = rxm.Receta.PrecioReceta,
                    EstadoReceta = rxm.Receta.EstadoReceta
                }).ToList()
            }).ToListAsync();

        }
        public async Task<RecetasXMenuReadDTO?> GetById(int idReceta, int idMenu)
        {
            var rxm = await _context.RecetasXMenu
            .Where(rxm => rxm.MenuId == idMenu && rxm.RecetaId == idReceta)
            .Include(rxm => rxm.Receta)
            .Include(rxm => rxm.Menu)
            .FirstOrDefaultAsync();

            if (rxm is null) return null;

            return new RecetasXMenuReadDTO
            {
                MenuId = rxm.MenuId,
                RecetaId = rxm.RecetaId,
                Menu = new MenuReadDTO
                {
                    Id = rxm.MenuId,
                    TituloMenu = rxm.Menu.TituloMenu,
                    EstadoMenu = rxm.Menu.EstadoMenu
                },
                Receta = new RecetaReadDTO
                {
                    Id = rxm.RecetaId,
                    Nombre = rxm.Receta.Nombre,
                    PrecioReceta = rxm.Receta.PrecioReceta,
                    ImgUrl = rxm.Receta.ImgUrl,
                    DescripcionReceta = rxm.Receta.DescripcionReceta,
                    EstadoReceta = rxm.Receta.EstadoReceta,
                }
            };
        }
        public async Task<bool> Create(int idReceta, int idMenu)
        {
            var receta = await _context.Recetas.FindAsync(idReceta);
            if (receta is null) return false;
            var menu = await _context.Menus.FindAsync(idMenu);
            if (menu is null) return false;

            var yaExiste = await _context.RecetasXMenu
                .AnyAsync(r => r.RecetaId == idReceta && r.MenuId == idMenu);
            if (yaExiste) return false;

            var rxm = new Model.RecetaXMenu
            {
                MenuId = idMenu,
                RecetaId = idReceta
            };

            _context.RecetasXMenu.Add(rxm);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteFisico(int idReceta, int idMenu)
        {
            var rxm = await _context.RecetasXMenu
                .FirstOrDefaultAsync(r => r.RecetaId == idReceta && r.MenuId == idMenu);
            if (rxm is null) return false;

            _context.RecetasXMenu.Remove(rxm);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}