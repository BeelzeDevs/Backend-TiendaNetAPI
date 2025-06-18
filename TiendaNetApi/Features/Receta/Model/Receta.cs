using TiendaNetApi.Model;
namespace TiendaNetApi.Model
{
    public class Receta
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? DescripcionReceta { get; set; }
        public string ImgUrl { get; set; } = "img/";
        public decimal PrecioReceta { get; set; }
        public bool EstadoReceta { get; set; } = true;

        public ICollection<IngredienteXReceta> IngredientesXRecetas { get; set; } = new List<IngredienteXReceta>();
        public ICollection<RecetaXMenu> RecetasXMenus { get; set; } = new List<RecetaXMenu>();

    }
}