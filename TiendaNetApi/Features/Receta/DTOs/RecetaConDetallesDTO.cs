using TiendaNetApi.Receta.DTOs;
namespace TiendaNetApi.Receta.DTOs
{
    public class RecetaConDetallesDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? DescripcionReceta { get; set; }
        public string ImgUrl { get; set; } = "img/";
        public decimal PrecioReceta { get; set; }
        public bool EstadoReceta { get; set; }

        public List<IngredienteDetalleDTO> Ingredientes { get; set; } = new();
        public List<MenuDetalleDTO> Menus { get; set; } = new();


    }
}