namespace TiendaNetApi.Receta.DTOs
{
    public class RecetaCreateDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string? DescripcionReceta { get; set; }
        public string ImgUrl { get; set; } = "img/";
        public decimal PrecioReceta { get; set; }
    }
}