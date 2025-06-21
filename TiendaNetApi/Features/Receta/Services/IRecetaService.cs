using TiendaNetApi.Receta.DTOs;
namespace TiendaNetApi.Receta.Services
{
    public interface IRecetaService
    {
        Task<List<RecetaReadDTO>> GetAll();
        Task<List<RecetaReadDTO>> GetAllActives();
        Task<RecetaReadDTO?> GetById(int id);
        Task<RecetaConDetallesDTO?> GetByIdConDetalles(int id);

        Task<RecetaReadDTO> Create(RecetaCreateDTO dto);
        Task<bool> Update(int id, RecetaUpdateDTO dto);
        Task<bool> DeleteFisico(int id);
        Task<bool> DeleteLogico(int id);
    }
}