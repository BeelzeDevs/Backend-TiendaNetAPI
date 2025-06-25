using TiendaNetApi.Rol.DTOs;
namespace TiendaNetApi.Rol.Services
{
    public interface IRolService
    {
        Task<List<RolReadDTO>> GetAll();
        Task<RolReadDTO?> GetById(int id);
        Task<RolConDetallesDTO?> GetByIdDetallado(int id);
        Task<RolReadDTO> Create(RolCreateDTO dto);
        Task<bool> Update(int id, RolUpdateDTO dto);
        Task<bool> DeleteFisico(int id);

    }
}