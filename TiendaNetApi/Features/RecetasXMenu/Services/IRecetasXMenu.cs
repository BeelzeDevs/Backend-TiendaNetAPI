using TiendaNetApi.RecetasXMenu.DTOs;
namespace TiendaNetApi.RecetasXMenu.Services
{
    public interface IRecetasXMenuService
    {
        Task<List<MenuConRecetasReadDTO>> GetAll();
        Task<RecetasXMenuReadDTO?> GetById(int idReceta, int idMenu);
        Task<bool> Create(int idReceta, int idMenu);
        Task<bool> DeleteFisico(int idReceta, int IdMenu);
    }
}