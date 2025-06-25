using Microsoft.AspNetCore.Mvc;
using TiendaNetApi.Menu.DTOs;
namespace TiendaNetApi.Menu.Services
{
    public interface IMenuService
    {
        Task<List<MenuReadDTO>> GetAll();
        Task<List<MenuReadDTO>> GetAllActives();
        Task<MenuReadDTO?> GetById(int id);
        Task<MenuReadDetalleDTO?> GetByIdDetallado(int id);
        Task<MenuReadDTO> Create(MenuCreateDTO dto);
        Task<bool> Update(int id, MenuUpdateDTO dto);
        Task<bool> DeleteFisico(int id);
        Task<bool> DeleteLogico(int id);
    }
}