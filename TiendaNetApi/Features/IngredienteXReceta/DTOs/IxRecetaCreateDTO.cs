namespace TiendaNetApi.IngredienteXReceta.DTOs
{
    public class IngredientesXRecetaCreateDTO
    {
        
        public int IngredienteId { get; set; }

        public int RecetaId { get; set; }

        public decimal Cantidad { get; set; }
        
    }
}