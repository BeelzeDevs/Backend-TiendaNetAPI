using TiendaNetApi.Ingredientes.DTOs;
namespace TiendaNetApi.Ingredientes.Services
{
    public interface IIngredienteService
    {
        Task<List<IngredienteReadDTO>> GetAllAsync();
        Task<List<IngredienteReadDTO>> GetAllActivesAsync();
        Task<IngredienteReadDTO?> GetByIdAsync(int id);
        Task<IngredienteReadDTO> CreateAsync(IngredienteCreateDTO dto);
        Task<bool> UpdateAsync(int id, IngredienteUpdateDTO dto);
        Task<bool> DeleteLogicoAsync(int id);
        Task<bool> DeleteFisicoAsync(int id);
    }
}