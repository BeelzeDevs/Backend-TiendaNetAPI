namespace TiendaNetApi.IngredienteXReceta.DTOs
{
    public class IngredienteReadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Costo { get; set; }
        public decimal Stock { get; set; }
        public string? DescripcionIngrediente { get; set; }
        public string UnidadMedidaNombre { get; set; } = string.Empty;
        public decimal CantidadReceta { get; set; }
    }
}