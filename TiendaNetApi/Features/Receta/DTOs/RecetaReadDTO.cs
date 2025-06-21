namespace TiendaNetApi.Receta.DTOs
{
    public class RecetaReadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? DescripcionReceta { get; set; }
        public string ImgUrl { get; set; } = "img/";
        public decimal PrecioReceta { get; set; }
        public bool EstadoReceta { get; set; } 

    }
}