using TiendaNetApi.Menu.DTOs;
using TiendaNetApi.Data;
using TiendaNetApi.Menu.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
namespace TiendaNetApi.Menu.Services
{
    public class MenuService : IMenuService
    {
        private readonly TiendaDbContext _context;
        public MenuService(TiendaDbContext context)
        {
            _context = context;
        }
        public async Task<List<MenuReadDTO>> GetAll()
        {
            return await _context.Menus
            .Select(m => new MenuReadDTO
            {
                Id = m.Id,
                TituloMenu = m.TituloMenu,
                EstadoMenu = m.EstadoMenu
            })
            .ToListAsync();
        }
        public async Task<List<MenuReadDTO>> GetAllActives()
        {
            return await _context.Menus
            .Where(m => m.EstadoMenu == true)
            .Select(m => new MenuReadDTO
            {
                Id = m.Id,
                TituloMenu = m.TituloMenu,
                EstadoMenu = m.EstadoMenu
            })
            .ToListAsync();
        }
        public async Task<MenuReadDTO?> GetById(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu is null) return null;

            return new MenuReadDTO
            {
                Id = menu.Id,
                TituloMenu = menu.TituloMenu,
                EstadoMenu = menu.EstadoMenu
            };
        }
        public async Task<MenuReadDetalleDTO?> GetByIdDetallado(int id)
        {
            var menu = await _context.Menus
            .Include(m => m.RecetasXMenus)
                .ThenInclude(rxm => rxm.Receta)
                    .ThenInclude(r => r.IngredientesXRecetas)
                        .ThenInclude(ixr => ixr.Ingrediente)
                            .ThenInclude(i => i.UnidadMedida)
            .FirstOrDefaultAsync();

            if (menu is null) return null;

            return new MenuReadDetalleDTO
            {
                Id = menu.Id,
                TituloMenu = menu.TituloMenu,
                EstadoMenu = menu.EstadoMenu,
                RecetasXMenus = menu.RecetasXMenus
                .Select(rxm => new MenuXRecetaDetalleDTO
                    {
                        RecetaId = rxm.RecetaId,
                        Receta = new RecetaConDetallesDTO
                        {
                            Id = rxm.Receta.Id,
                            Nombre = rxm.Receta.Nombre,
                            DescripcionReceta = rxm.Receta.DescripcionReceta,
                            ImgUrl = rxm.Receta.ImgUrl,
                            PrecioReceta = rxm.Receta.PrecioReceta,
                            EstadoReceta = rxm.Receta.EstadoReceta,
                            Ingredientes = rxm.Receta.IngredientesXRecetas
                            .Select(ixr => new IngredienteDetalleDTO
                            {
                                IngredienteId = ixr.IngredienteId,
                                NombreIngrediente = ixr.Ingrediente.Nombre,
                                Cantidad = ixr.Cantidad,
                                UnidadMedidaNombre = ixr.Ingrediente.UnidadMedida.Nombre
                            }).ToList()
                        }
                    })
                .ToList()


            };
        }
        public async Task<MenuReadDTO> Create(MenuCreateDTO dto)
        {
            var menu = new TiendaNetApi.Model.Menu
            {
                TituloMenu = dto.TituloMenu
            };
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
            return new MenuReadDTO
            {
                Id = menu.Id,
                TituloMenu = menu.TituloMenu,
                EstadoMenu = menu.EstadoMenu
            };
        }
        public async Task<bool> Update(int id, MenuUpdateDTO dto)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu is null) return false;

            menu.TituloMenu = dto.TituloMenu;
            menu.EstadoMenu = dto.EstadoMenu;
            await _context.SaveChangesAsync();

            return true;

        }
        public async Task<bool> DeleteLogico(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu is null) return false;

            menu.EstadoMenu = false;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteFisico(int id)
        {
            var menu = await _context.Menus
                .Include(m => m.RecetasXMenus)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (menu is null) return false;

            _context.RecetasXMenu.RemoveRange(menu.RecetasXMenus);

            _context.Menus.Remove(menu);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}