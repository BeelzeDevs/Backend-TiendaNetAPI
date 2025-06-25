using TiendaNetApi.RecetasXMenu.DTOs;
namespace TiendaNetApi.RecetasXMenu.DTOs
{
    public class MenuConRecetasReadDTO
    {
        public int MenuId { get; set; }
        public string TituloMenu { get; set; } = string.Empty;
        public bool EstadoMenu { get; set; }
        public List<RecetaReadDTO> Recetas { get; set; } = new();
    }

}