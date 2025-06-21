using TiendaNetApi.Rol.DTOs;
namespace TiendaNetApi.Rol.Services
{
    public interface IRolService
    {
        Task<List<RolReadDTO>> GetAllAsync();
        Task<RolReadDTO?> GetById(int id);
        Task<RolConDetallesDTO?> GetByIdDetallado(int id);
        Task<RolReadDTO> CreateAsync(RolCreateDTO dto);
        Task<bool> UpdateAsync(int id, RolUpdateDTO dto);
        Task<bool> DeleteFisicoAsync(int id);

    }
}