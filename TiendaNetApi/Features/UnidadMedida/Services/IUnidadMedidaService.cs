using TiendaNetApi.UnidadMedida.DTOs;

namespace TiendaNetApi.UnidadMedida.Services
{
    public interface IUnidadMedidaService
    {
        Task<List<UnidadMedidaReadDTO>> GetAllAsync();
        Task<UnidadMedidaReadDTO?> GetByIdAsync(int id);
        Task<UnidadMedidaReadDTO> CreateAsync(UnidadMedidaCreateDTO dto);
        Task<bool> UpdateAsync(int id, UnidadMedidaUpdateDTO dto);
        Task<bool> DeleteFisicoAsync(int id);
        
    }
}