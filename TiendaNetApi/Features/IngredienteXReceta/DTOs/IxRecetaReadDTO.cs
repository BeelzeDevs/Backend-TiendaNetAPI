
namespace TiendaNetApi.IngredienteXReceta.DTOs
{
    public class IngredienteXRecetaReadDTO
    {
        public int IngredienteId { get; set; }
        public IngredienteReadDTO Ingrediente { get; set; } = null!;

        public int RecetaId { get; set; }
        public RecetaReadDTO Receta { get; set; } = null!;

        public decimal Cantidad { get; set; }
        
    }
}