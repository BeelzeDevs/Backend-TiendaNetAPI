using TiendaNetApi.Model;
namespace TiendaNetApi.Model
{
    public class RecetaXMenu
    {
        public int RecetaId { get; set; }
        public Receta Receta { get; set; } = null!;
        
        public int MenuId { get; set; }
        public Menu Menu { get; set; } = null!;

    }
}