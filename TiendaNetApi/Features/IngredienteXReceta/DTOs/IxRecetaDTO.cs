namespace TiendaNetApi.IngredienteXReceta.DTOs
{
    public class IngredienteXRecetaByIdRecetaDTO
    {
        public RecetaReadDTO Receta { get; set; } = null!;
        public List<IngredienteReadDTO> Ingredientes { get; set; } = new();
    }
}