using TiendaNetApi.Model;
namespace TiendaNetApi.Model
{
    public class Menu
    {
        public int Id { get; set; }
        public string TituloMenu { get; set; } = null!;
        public bool EstadoMenu { get; set; } = true;

        public ICollection<RecetaXMenu> RecetasXMenus { get; set; } = new List<RecetaXMenu>();
    }
}