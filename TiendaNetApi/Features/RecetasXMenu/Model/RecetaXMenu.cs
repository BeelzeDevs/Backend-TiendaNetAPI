using TiendaNetApi.Model;
namespace TiendaNetApi.Model
{
    public class RecetaXMenu
    {
        public int RecetaId { get; set; }
        public Model.Receta Receta { get; set; } = null!;
        
        public int MenuId { get; set; }
        public Model.Menu Menu { get; set; } = null!;

    }
}