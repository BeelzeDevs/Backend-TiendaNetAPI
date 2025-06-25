using TiendaNetApi.IngredienteXReceta.DTOs;

namespace TiendaNetApi.IngredienteXReceta.Services
{
    public interface IIngredientesXRecetaService
    {
        Task<List<IngredienteXRecetaReadDTO>> GetAll();
        Task<IngredienteXRecetaByIdRecetaDTO?> GetIngredientesByIdReceta(int recetaId);
        Task<IngredienteXRecetaReadDTO?> AddIngredienteAReceta(IngredientesXRecetaCreateDTO dto);
        Task<bool> AddIngredientesARecetaPorBatch(IngredientesXRecetaBatchCreateDTO dto);
        Task<bool> RemoveIngredientesARecetaPorBatch(IngredientesXRecetaBatchRemoveDTO dto);
        Task<bool> RemoveIngredienteDeReceta(IngredienteXRecetaDeleteDTO dto);
        Task<bool> UpdateCantidadIngrediente(IngredienteXRecetaUpdateDTO dto);
        
    }
}