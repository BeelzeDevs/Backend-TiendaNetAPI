using TiendaNetApi.Model;
namespace TiendaNetApi.Model
{

    public class Ingrediente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Costo { get; set; }
        public decimal Stock { get; set; }
        public string? DescripcionIngrediente { get; set; } = null!;
        public bool EstadoIngrediente { get; set; } = true;

        public int UnidadMedidaId { get; set; } // Foranea
        public UnidadMedida UnidadMedida { get; set; } = null!; // navegacion
        public ICollection<IngredienteXReceta> IngredientesXRecetas { get; set; } = new List<IngredienteXReceta>();
    }
}