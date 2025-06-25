namespace TiendaNetApi.Menu.DTOs
{
    public class MenuXRecetaDetalleDTO
    {
        
        public int RecetaId { get; set; }
        public RecetaConDetallesDTO Receta { get; set; } = null!;

    }
}