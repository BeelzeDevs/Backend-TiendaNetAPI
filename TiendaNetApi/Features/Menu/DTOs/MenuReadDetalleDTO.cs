using TiendaNetApi.Menu.DTOs;
namespace TiendaNetApi.Menu.DTOs
{
    public class MenuReadDetalleDTO
    {
        public int Id { get; set; }
        public string TituloMenu { get; set; } = string.Empty;
        public bool EstadoMenu { get; set; } = true;

        public ICollection<MenuXRecetaDetalleDTO> RecetasXMenus { get; set; } = new List<MenuXRecetaDetalleDTO>();
    }
    
}