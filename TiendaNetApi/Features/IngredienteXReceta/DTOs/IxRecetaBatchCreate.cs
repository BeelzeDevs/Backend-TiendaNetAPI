namespace TiendaNetApi.IngredienteXReceta.DTOs
{
    public class IngredientesXRecetaBatchCreateDTO
    {
        public int RecetaId { get; set; }
        public List<IngredienteCantidadDTO> Ingredientes { get; set; } = new();
    }
    public class IngredientesXRecetaBatchRemoveDTO
    {
        public int RecetaId { get; set; }
        public List<IngredienteCantidadDTO> Ingredientes { get; set; } = new();
    }

    public class IngredienteCantidadDTO
    {
        public int IngredienteId { get; set; }
        public decimal Cantidad { get; set; }
    }
}