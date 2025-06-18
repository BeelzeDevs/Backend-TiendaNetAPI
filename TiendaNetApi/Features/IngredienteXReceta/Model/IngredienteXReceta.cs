using TiendaNetApi.Model;
namespace TiendaNetApi.Model
{
    public class IngredienteXReceta
    {
        public int IngredienteId { get; set; }
        public Ingrediente Ingrediente { get; set; } = null!;

        public int RecetaId { get; set; }
        public Receta Receta { get; set; } = null!;

        public decimal Cantidad { get; set; }
        
    }
}