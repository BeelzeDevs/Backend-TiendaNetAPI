namespace TiendaNetApi.RecetasXMenu.DTOs
{
    public class RecetasXMenuReadDTO
    {
        public int RecetaId { get; set; }
        public int MenuId { get; set; }
        public RecetaReadDTO Receta { get; set; } = null!;
        public MenuReadDTO Menu { get; set; } = null!;
    }
}